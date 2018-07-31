using System;
using System.Collections.Generic;
using System.Text;

namespace EdfFile
{
    public class EDFLocalRecordingIdentification
    {
        public DateTime RecordingStartDate { get; set; }
        public string RecordingCode { get; set; }
        public string RecordingTechnician { get; set; }
        public string RecordingEquipment { get; set; }
        public List<string> AdditionalRecordingIdentification { get; set; }

        public EDFLocalRecordingIdentification(char[] recordingIdentification)
        {
            parseRecordingIdentificationSubFields(recordingIdentification);
        }

        private void parseRecordingIdentificationSubFields(char[] recordingIdentification)
        {
            StringBuilder strBuilder = new StringBuilder(new string(recordingIdentification));
            AdditionalRecordingIdentification = new List<string>();

            string[] arrayRecordingInformation = strBuilder.ToString().Trim().Split(' ');
            if (arrayRecordingInformation.Length >= 5)
            {
                if (!arrayRecordingInformation[0].ToLower().Equals("startdate"))
                {
                    intializeEDF();
                    throw new ArgumentException("Recording Identification must start with the text 'Startdate'");
                }
                try
                {
                    RecordingStartDate = DateTime.Parse(arrayRecordingInformation[1]);
                }
                catch (FormatException ex)
                {
                    System.Diagnostics.Debug.WriteLine("A FormatException occurred on the Recording Start Date, this is not in EDF+ format\n\n" + ex.StackTrace);
                    intializeEDF();
                    return;
                }

                RecordingCode = arrayRecordingInformation[2];
                RecordingTechnician = arrayRecordingInformation[3];
                RecordingEquipment = arrayRecordingInformation[4];

                for (int i = 5; i < arrayRecordingInformation.Length; i++)
                {
                    AdditionalRecordingIdentification.Add(arrayRecordingInformation[i]);
                }
            }
            else
            {
                intializeEDF();
            }
        }

        private void intializeEDF()
        {
            RecordingStartDate = DateTime.MinValue;
            RecordingCode = string.Empty;
            RecordingTechnician = string.Empty;
            RecordingEquipment = string.Empty;
        }


        public override String ToString()
        {
            StringBuilder strBuilder = new StringBuilder(string.Empty);

            strBuilder.Append("Startdate");
            strBuilder.Append(" ");
            strBuilder.Append(RecordingStartDate.ToString("dd-MMM-yyyy"));
            strBuilder.Append(" ");
            strBuilder.Append(RecordingCode);
            strBuilder.Append(RecordingTechnician);
            strBuilder.Append(RecordingEquipment);

            foreach (string info in AdditionalRecordingIdentification)
            {
                strBuilder.Append(" ");
                strBuilder.Append(info);
            }
            strBuilder = new StringBuilder(
                strBuilder.Length > EDFHeader.FixedLength_LocalRecordingIdentifiaction ? 
                    strBuilder.ToString().Substring(0, EDFHeader.FixedLength_LocalRecordingIdentifiaction) : 
                    strBuilder.ToString().PadRight(EDFHeader.FixedLength_LocalRecordingIdentifiaction));

            return strBuilder.ToString();
        }
    }
}
