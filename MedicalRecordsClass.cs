using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestResultsDisplay
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class MedicalRecord
    {

        private MedicalRecordSource sourceField;

        private MedicalRecordTestResult[] testResultsField;

        /// <remarks/>
        public MedicalRecordSource Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TestResult", IsNullable = false)]
        public MedicalRecordTestResult[] TestResults
        {
            get
            {
                return this.testResultsField;
            }
            set
            {
                this.testResultsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MedicalRecordSource
    {

        private MedicalRecordSourceSystem systemField;

        private MedicalRecordSourceUser userField;

        /// <remarks/>
        public MedicalRecordSourceSystem System
        {
            get
            {
                return this.systemField;
            }
            set
            {
                this.systemField = value;
            }
        }

        /// <remarks/>
        public MedicalRecordSourceUser User
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MedicalRecordSourceSystem
    {

        private string nameField;

        private string providerField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Provider
        {
            get
            {
                return this.providerField;
            }
            set
            {
                this.providerField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MedicalRecordSourceUser
    {

        private string nameField;

        private string dateOfBirthField;

        private MedicalRecordSourceUserIdentifiers identifiersField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string DateOfBirth
        {
            get
            {
                return this.dateOfBirthField;
            }
            set
            {
                this.dateOfBirthField = value;
            }
        }

        /// <remarks/>
        public MedicalRecordSourceUserIdentifiers Identifiers
        {
            get
            {
                return this.identifiersField;
            }
            set
            {
                this.identifiersField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MedicalRecordSourceUserIdentifiers
    {

        private MedicalRecordSourceUserIdentifiersPatientIdentifier patientIdentifierField;

        /// <remarks/>
        public MedicalRecordSourceUserIdentifiersPatientIdentifier PatientIdentifier
        {
            get
            {
                return this.patientIdentifierField;
            }
            set
            {
                this.patientIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MedicalRecordSourceUserIdentifiersPatientIdentifier
    {

        private string typeField;

        private ulong valueField;

        /// <remarks/>
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public ulong Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MedicalRecordTestResult
    {

        private string createdField;

        private string titleField;

        private string typeField;

        private MedicalRecordTestResultTestResultLine[] testResultLinesField;

        /// <remarks/>
        public string Created
        {
            get
            {
                return this.createdField;
            }
            set
            {
                this.createdField = value;
            }
        }

        /// <remarks/>
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TestResultLine", IsNullable = false)]
        public MedicalRecordTestResultTestResultLine[] TestResultLines
        {
            get
            {
                return this.testResultLinesField;
            }
            set
            {
                this.testResultLinesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MedicalRecordTestResultTestResultLine
    {

        private string descriptionField;

        private string resultField;

        private string normalRangeField;

        private string dateField;

        private string parentDescriptionField;

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }

        /// <remarks/>
        public string NormalRange
        {
            get
            {
                return this.normalRangeField;
            }
            set
            {
                this.normalRangeField = value;
            }
        }

        public string Date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value; 
            }
        }

        public string ParentDescription
        {
            get
            {
                return this.parentDescriptionField;
            }
            set 
            { 
                this.parentDescriptionField = value; 
            }    
        }
    }

}
