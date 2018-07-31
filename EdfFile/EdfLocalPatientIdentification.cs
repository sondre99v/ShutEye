using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdfFile
{
    public class EDFLocalPatientIdentification
    {
        public string PatientCode { get; set; }
        public string PatientSex { get; set; }
        public DateTime PatientBirthDate { get; set; }
        public string PatientName { get; set; }
        public List<string> AdditionalPatientIdentification { get; set; }
        private StringBuilder _strPatientIdentification = new StringBuilder(String.Empty);


        public EDFLocalPatientIdentification(char[] patientIdentification)
        {
            parsePatientIdentificationSubFields(patientIdentification);
        }

        public EDFLocalPatientIdentification(string patientCode, string patientSex, DateTime patientBirthDate, string patientName, List<string> patientAdditional)
        {
            setupPatient(patientCode, patientSex, patientBirthDate, patientName, patientAdditional);
        }

        public EDFLocalPatientIdentification(string patientCode, string patientSex, DateTime patientBirthDate, string patientName)
        {
            setupPatient(patientCode, patientSex, patientBirthDate, patientName, new List<string>());
        }


        private void setupPatient(string patientCode, string patientSex, DateTime patientBirthDate, string patientName, List<string> patientAdditional)
        {
            PatientCode = patientCode;
            PatientSex = patientSex;
            PatientBirthDate = patientBirthDate;
            PatientName = patientName;
            AdditionalPatientIdentification = patientAdditional;
        }

        private void parsePatientIdentificationSubFields(char[] patientIdentification)
        {
            _strPatientIdentification = new StringBuilder(new string(patientIdentification));
            string[] arrayPatientInformation = _strPatientIdentification.ToString().Trim().Split(' ');
            AdditionalPatientIdentification = new List<string>();

            if (arrayPatientInformation.Length >= 4)
            {
                PatientCode = arrayPatientInformation[0];
                PatientSex = arrayPatientInformation[1];
                try
                {
                    PatientBirthDate = DateTime.Parse(arrayPatientInformation[2]);
                }
                catch (FormatException ex)
                {
                    System.Diagnostics.Debug.WriteLine("A FormatException occurred on the Patient BirthDate, this is not in EDF+ format\n\n" + ex.StackTrace);
                    intializeEDF();
                    return;
                }
                PatientName = arrayPatientInformation[3];

                
                for (int i = 4; i < arrayPatientInformation.Length; i++)
                {
                    AdditionalPatientIdentification.Add(arrayPatientInformation[i]);
                }
            }
            else
            {
                intializeEDF();
            }
        }

        private void intializeEDF()
        {
            PatientCode = string.Empty;
            PatientSex = string.Empty;
            PatientBirthDate = DateTime.MinValue;
            PatientName = string.Empty;
        }


        public override string ToString()
        {
            _strPatientIdentification = new StringBuilder(string.Empty);
            _strPatientIdentification.Append(PatientCode);
            _strPatientIdentification.Append(" ");
            _strPatientIdentification.Append(PatientSex);
            _strPatientIdentification.Append(" ");

            if(!PatientBirthDate.Equals(DateTime.MinValue))
            {
                _strPatientIdentification.Append(PatientBirthDate.ToString("dd-MMM-yyyy"));
            }

            _strPatientIdentification.Append(" ");
            _strPatientIdentification.Append(PatientName);

            foreach(string info in AdditionalPatientIdentification)
            {
                _strPatientIdentification.Append(" ");
                _strPatientIdentification.Append(info);
            }

            _strPatientIdentification =  new StringBuilder(_strPatientIdentification.Length > EDFHeader.FixedLength_LocalPatientIdentification ? _strPatientIdentification.ToString().Substring(0, EDFHeader.FixedLength_LocalPatientIdentification) : _strPatientIdentification.ToString().PadRight(EDFHeader.FixedLength_LocalPatientIdentification));

            return _strPatientIdentification.ToString();
        }
    }
}
