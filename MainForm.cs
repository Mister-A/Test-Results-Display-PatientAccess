using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace TestResultsDisplay
{
    public partial class MainForm : Form
    {
        static string? XmlFile { get; set; }
        public MainForm()
        {
            InitializeComponent();

            XmlFile = "c:\\temp\\RecordData.xml";
            XmlSerializer serializer = new(typeof(MedicalRecord));
            using (StreamReader reader = new(XmlFile))
            {
                var ImportedRecords = (MedicalRecord)serializer.Deserialize(reader);
                if (ImportedRecords != null)
                {

                    //Flatten all testlines together
                    List<MedicalRecordTestResultTestResultLine> allTests = new();

                    foreach (var test in ImportedRecords.TestResults)
                    {
                        foreach (var testLine in test.TestResultLines)
                        {
                            MedicalRecordTestResultTestResultLine thisLine = testLine;

                            //Import the date and description from it's parent
                            thisLine.Date = test.Created;
                            thisLine.ParentDescription = test.Title;

                            allTests.Add(thisLine);
                        }
                    }

                    //Then group by parent and test
                    var TestGroups = allTests.GroupBy(x => new { Parent = x.ParentDescription, Test = x.Description });

                    //Reshuffle the collection so one test group contains all tests results over time
                    var Tests = TestGroups.GroupBy(x => new { TestGroup = x.Key.Parent });

                    string resultsOutput = "";

                    Color resultColor = Color.Black;
                    Font headingFont = new Font("Arial", 12);
                    Font subheadingFont = new Font("Arial", 11);
                    Font bodyFont = new Font("Arial", 10);
                    char pageBreak = (char)12; //Might be useful but only if I can work out where the page breaks should go!

                    //Write the header
                    resultsOutput += "Summary test results, parsed from Patient Access XML results, by Test Results Display by Andrew Aldridge (c) 2023 (andrew@consultme.org)\r\n";
                    resultsOutput += "Processed: " + DateTime.Now.ToString();
                    resultsOutput += "Patient: " + ImportedRecords.Source.User.Name + " (d.o.b.: " + ImportedRecords.Source.User.DateOfBirth + ") - NHS ref: " + ImportedRecords.Source.User.Identifiers.PatientIdentifier.Value + "\r\n";
                    resultsWindow.AppendText("Summary test results, parsed from Patient Access XML results, by Test Results Display by Andrew Aldridge (c) 2023 (andrew@consultme.org)\r\n");
                    resultsWindow.AppendText("Processed: " + DateTime.Now.ToString() + "\r\n\r\n");
                    resultsWindow.AppendText("Patient: " + ImportedRecords.Source.User.Name + " (d.o.b.: " + ImportedRecords.Source.User.DateOfBirth + ") - NHS ref: " + ImportedRecords.Source.User.Identifiers.PatientIdentifier.Value + "\r\n", resultColor, headingFont, true);
                    //Step through and display results / build results string
                    foreach (var Test in Tests)
                    {
                        

                        resultsOutput += Test.Key.TestGroup + "\r\n";
                        resultsWindow.AppendText(Test.Key.TestGroup, resultColor, headingFont, true);
                        foreach (var Lines in Test)
                        {
                            //Parse NormalRange
                            double[] NormalRange = GetMinMax(Lines.First().NormalRange);

                            resultsOutput += "\t" + Lines.Key.Test + "(Normal: " + Lines.First().NormalRange + ")" + "\r\n";
                            resultsWindow.AppendText("\t" + Lines.Key.Test + "(Normal: " + Lines.First().NormalRange + ")", resultColor, subheadingFont, true);
                            foreach (var Line in Lines)
                            {
                                
                                if (NormalRange != null)
                                {
                                    if (NormalRange[0] == -1)
                                    {
                                        resultColor = Color.Black;
                                    }
                                    else
                                    {
                                        //Do range check ...
                                        double NumericResult = double.Parse(Regex.Match(Line.Result, @"\d+\.*\d*").Value);
                                        if (NumericResult > NormalRange[0] && NumericResult < NormalRange[1])
                                        {
                                            resultColor = Color.Green;
                                        }
                                        else if (NumericResult == NormalRange[0] || NumericResult == NormalRange[1])
                                        {
                                            resultColor = Color.Orange;
                                        }
                                        else
                                        {
                                            resultColor = Color.Red;
                                        }
                                    }
                                }
                                resultsOutput += "\t\t" + Line.Date + " - " + Line.Result + "\r\n";
                                resultsWindow.AppendText("\t\t" + Line.Date + " - " + Line.Result, resultColor, bodyFont, true);

                                resultColor = Color.Black;
                            }
                            resultsOutput += "\r\n";
                            resultsWindow.AppendText("\r\n");
                        }

                    }

                    string outputFileName = AppDomain.CurrentDomain.BaseDirectory + "/results.txt";
                    using StreamWriter outputFile = new(outputFileName);
                    {
                        outputFile.WriteLine(resultsOutput);
                    }

                    //Save to RTF
                    outputFileName = AppDomain.CurrentDomain.BaseDirectory + "/results.rtf";
                    resultsWindow.SaveFile(outputFileName);
                }
            }
        }

        /// <summary>
        /// Parses expected normal range from string
        /// </summary>
        /// <param name="normalRange"></param>
        /// <returns>int array -1 if there's no range, {min, max} if there's a normal range</returns>
        private static double[] GetMinMax(string normalRange)
        {
            if (normalRange == null || normalRange == "N/A")
            {
                double[] temp = { -1 };
                return temp;

            }
            else
            {
                double[] temp = new double[2];
                if (normalRange.Contains('-'))
                {
                    //it's a min-max pair
                    string split = " - ";
                    string[] result = normalRange.Split(split);
                    temp[0] = double.Parse(result[0]);
                    temp[1] = double.Parse(result[1]);
                }
                else if (normalRange.Contains('<'))
                {
                    //it's less than
                    string split = "< ";
                    string[] result = normalRange.Split(split);
                    temp[0] = 0;
                    temp[1] = double.Parse(result[1]);
                }
                else if (normalRange.Contains('>'))
                {
                    //it's greater than
                    string split = "> ";
                    string[] result = normalRange.Split(split);
                    temp[0] = double.Parse(result[1]);
                    temp[1] = 0;
                }
                return temp;
            }
        }
    }

    /// <summary>
    /// RichTextBox Extensions to allow easier colour and font changes while appending
    /// </summary>
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color, bool addNewLine = true)
        {
            box.SuspendLayout();
            box.SelectionColor = color;
            box.AppendText(addNewLine
                ? $"{text}{Environment.NewLine}"
                : text);
            box.ScrollToCaret();
            box.ResumeLayout();
        }

        public static void AppendText(this RichTextBox box, string text, Color color, Font font, bool addNewLine = true)
        {
            box.SuspendLayout();
            box.SelectionColor = color;
            box.SelectionFont = font;   
            box.AppendText(addNewLine
                ? $"{text}{Environment.NewLine}"
                : text);
            box.ScrollToCaret();
            box.ResumeLayout();
        }

        public static void AppendText(this RichTextBox box, string text, bool addNewLine = true)
        {
            box.SuspendLayout();
            box.AppendText(addNewLine
                ? $"{text}{Environment.NewLine}"
                : text);
            box.ScrollToCaret();
            box.ResumeLayout();
        }
    }
}
