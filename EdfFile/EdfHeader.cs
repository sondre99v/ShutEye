﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdfFile
{
    public class EDFHeader
    {
        public static string EDFContinuous = "EDF+C";
        public static string EDFDiscontinuous = "EDF+D";
        private static string VERSION_DEFAULT = "0       ";
        private static int FixedLength_Version = 8;

        private bool _isEDFPlus = false;
        public bool IsEDFPlus => _isEDFPlus;
        private string _Version = VERSION_DEFAULT;
        public string Version
        {
            get => _Version;
            set => _Version = getFixedLengthString(value, FixedLength_Version);
        }

        public static int FixedLength_LocalPatientIdentification = 80;
        private EDFLocalPatientIdentification _PatientInformation;
        public EDFLocalPatientIdentification PatientIdentification
        {
            get => _PatientInformation;
            set
            {
                if(value.ToString().Length != FixedLength_LocalPatientIdentification)
                {
                    throw new FormatException("Patient Information must be " + FixedLength_LocalPatientIdentification + " characters fixed length");
                }
                _PatientInformation = value;
            }
        }

        public static int FixedLength_LocalRecordingIdentifiaction = 80;
        private EDFLocalRecordingIdentification _RecordingInformation;
        public EDFLocalRecordingIdentification RecordingIdentification
        {
            get => _RecordingInformation;
            set
            {
                if(value.ToString().Length != EDFHeader.FixedLength_LocalRecordingIdentifiaction)
                {
                    throw new FormatException("Recording Information must be " + EDFHeader.FixedLength_LocalRecordingIdentifiaction + " characters fixed length");
                }
                _RecordingInformation = value;
            }
        }

        public static int FixedLength_StartDateEDF = 8;
        public string StartDateEDF { get; private set; }

        public static int FixedLength_StartTimeEDF = 8;
        public string StartTimeEDF { get; private set; }

        private DateTime _StartDateTime;
        public DateTime StartDateTime
        {
            get => _StartDateTime;
            set
            {
                StartDateEDF = value.ToString("dd.MM.yy");
                StartTimeEDF = value.ToString("H.mm.ss");
                _StartDateTime = value;
            }
        }

        public static int FixedLength_NumberOfBytes = 8;
        private string _NumberOfBytesFixedLengthString = "0";
        private int _NumberOfBytes = 0;
        public int NumberOfBytes
        {
            get => _NumberOfBytes;
            set
            {
                _NumberOfBytes = value;
                _NumberOfBytesFixedLengthString = getFixedLengthString(Convert.ToString(value), FixedLength_NumberOfBytes);
            }
        }

        public static int FixedLength_NumberOfDataRecords = 8;
        private string _NumberOfDataRecordsFixedLengthString = "0";
        private int _NumberOfDataRecords = 0;
        public int NumberOfDataRecords
        {
            get => _NumberOfDataRecords;
            set
            {
                _NumberOfDataRecords = value;
                _NumberOfDataRecordsFixedLengthString = getFixedLengthString(Convert.ToString(value), FixedLength_NumberOfDataRecords);
            }
        }

        public static int FixedLength_DuraitonOfDataRecordInSeconds = 8;
        private string _DurationOfDataRecordInSecondsFixedLengthString = "0";
        private int _DurationOfDataRecordInSeconds = 0;
        public int DurationOfDataRecordInSeconds
        {
            get => _DurationOfDataRecordInSeconds;
            set
            {
                _DurationOfDataRecordInSeconds = value;
                _DurationOfDataRecordInSecondsFixedLengthString = getFixedLengthString(Convert.ToString(value), FixedLength_DuraitonOfDataRecordInSeconds);
            }
        }

        public static int FixedLength_NumberOfSignalsInDataRecord = 4;
        private string _NumberOfSignalsInDataRecordFixedLengthString = "0";
        private int _NumberOfSignalsInDataRecord = 0;
        public int NumberOfSignalsInDataRecord
        {
            get => _NumberOfSignalsInDataRecord;
            set
            {
                _NumberOfSignalsInDataRecord = value;
                _NumberOfSignalsInDataRecordFixedLengthString = getFixedLengthString(Convert.ToString(value), FixedLength_NumberOfSignalsInDataRecord);
            }
        }

        public static int FixedLength_Reserved = 44;

        private string _Reserved;
        public string Reserved
        {
            get => _Reserved;
            set => _Reserved = getFixedLengthString(value, FixedLength_Reserved);
        }
        
        public List<EDFSignal> Signals { get; set; }
        private StringBuilder _strHeader = new StringBuilder(string.Empty);


        public EDFHeader()
        {
            initializeEDFHeader();
        }

        public EDFHeader(bool isEDFPlus)
        {
            _isEDFPlus = isEDFPlus;
            initializeEDFHeader();
        }

        public EDFHeader(char[] header)
        {
            if(header.Length != 256)
            {
                throw new ArgumentException("Header must be 256 characters");
            }
            parseHeader(header);
        }


        private void initializeEDFHeader()
        {
            Signals = new List<EDFSignal>();
            Version = string.Empty;
            _PatientInformation = new EDFLocalPatientIdentification(getFixedLengthString(string.Empty, EDFHeader.FixedLength_LocalPatientIdentification).ToCharArray());
            _RecordingInformation = new EDFLocalRecordingIdentification(getFixedLengthString(string.Empty, EDFHeader.FixedLength_LocalRecordingIdentifiaction).ToCharArray());
            StartDateEDF = DateTime.MinValue.ToString("dd.MM.yy");
            StartTimeEDF = DateTime.MinValue.ToString("hh.mm.ss");
            NumberOfBytes = 0;
            NumberOfDataRecords = 0;
            DurationOfDataRecordInSeconds = 0;
            NumberOfSignalsInDataRecord = Signals.Count;
            Reserved = string.Empty;
        }


        private void parseHeader(char[] header)
        {
            int i = 0;
            foreach(char c in header)
            {
                if(header[i] == (char) 0)
                {
                    header[i] = (char) 32;
                }
                i++;
            }

            _strHeader.Append(header);

            int fileIndex = 0;

            char[] version = getFixedLengthCharArrayFromHeader(header, fileIndex, EDFHeader.FixedLength_Version);
            Version = new string(version);
            fileIndex += FixedLength_Version;

            char[] localPatientIdentification = getFixedLengthCharArrayFromHeader(header, fileIndex, EDFHeader.FixedLength_LocalPatientIdentification);
            fileIndex += EDFHeader.FixedLength_LocalPatientIdentification;

            char[] localRecordingIdentification = getFixedLengthCharArrayFromHeader(header, fileIndex, EDFHeader.FixedLength_LocalRecordingIdentifiaction);
            fileIndex += EDFHeader.FixedLength_LocalRecordingIdentifiaction;

            char[] startDate = getFixedLengthCharArrayFromHeader(header, fileIndex, EDFHeader.FixedLength_StartDateEDF);
            StartDateEDF = new string(startDate);
            fileIndex += EDFHeader.FixedLength_StartDateEDF;

            char[] startTime = getFixedLengthCharArrayFromHeader(header, fileIndex, EDFHeader.FixedLength_StartTimeEDF);
            StartTimeEDF = new string(startTime);
            fileIndex += EDFHeader.FixedLength_StartTimeEDF;

            char[] numberOfBytesInHeaderRow = getFixedLengthCharArrayFromHeader(header, fileIndex, EDFHeader.FixedLength_NumberOfBytes);
            NumberOfBytes = int.Parse(new string(numberOfBytesInHeaderRow).Trim());
            fileIndex += EDFHeader.FixedLength_NumberOfBytes;

            char[] reserved = getFixedLengthCharArrayFromHeader(header, fileIndex, EDFHeader.FixedLength_Reserved);
            if(new string(reserved).StartsWith(EDFHeader.EDFContinuous) || new string(reserved).StartsWith(EDFHeader.EDFDiscontinuous))
            {
                _isEDFPlus = true;
            }

            Reserved = new string(reserved);
            fileIndex += EDFHeader.FixedLength_Reserved;

            char[] numberOfDataRecords = getFixedLengthCharArrayFromHeader(header, fileIndex, EDFHeader.FixedLength_NumberOfDataRecords);
            NumberOfDataRecords = (int.Parse(new string(numberOfDataRecords).Trim()));
            fileIndex += EDFHeader.FixedLength_NumberOfDataRecords;

            char[] durationOfDataRecord = getFixedLengthCharArrayFromHeader(header, fileIndex, EDFHeader.FixedLength_DuraitonOfDataRecordInSeconds);
            DurationOfDataRecordInSeconds = int.Parse(new string(durationOfDataRecord).Trim());
            fileIndex += EDFHeader.FixedLength_DuraitonOfDataRecordInSeconds;

            char[] numberOfSignals = getFixedLengthCharArrayFromHeader(header, fileIndex, EDFHeader.FixedLength_NumberOfSignalsInDataRecord);
            NumberOfSignalsInDataRecord = int.Parse(new string(numberOfSignals).Trim());

            if(NumberOfSignalsInDataRecord < 1 || NumberOfSignalsInDataRecord > 256)
            {
                throw new ArgumentException("EDF File has " + NumberOfSignalsInDataRecord + " Signals; Number of Signals must be >1 and <=256");
            }
            fileIndex += EDFHeader.FixedLength_NumberOfSignalsInDataRecord;

            PatientIdentification = new EDFLocalPatientIdentification(localPatientIdentification);
            RecordingIdentification = new EDFLocalRecordingIdentification(localRecordingIdentification);

            StartDateTime = DateTime.ParseExact(StartDateEDF + " " + StartTimeEDF, "dd.MM.yy HH.mm.ss", System.Globalization.CultureInfo.InvariantCulture);
            if(IsEDFPlus)
            {
                if(!StartDateTime.Date.Equals(RecordingIdentification.RecordingStartDate))
                {
                    throw new ArgumentException("Header StartDateTime does not equal Header.RecordingIdentification StartDate!");
                }
                else
                {
                    RecordingIdentification.RecordingStartDate = StartDateTime;
                }
            }
        }

        public void parseSignals(char[] signals)
        {
            _strHeader.Append(signals);

            Signals = new List<EDFSignal>();

            int h = 0;
            foreach(char c in signals)
            {
                if(signals[h] == (char) 0)
                {
                    signals[h] = (char) 32;
                }
                h++;
            }

            for(int i = 0; i < NumberOfSignalsInDataRecord; i++)
            {
                EDFSignal edf_signal = new EDFSignal();

                int charIndex = 0;

                char[] label = getFixedLengthCharArrayFromHeader(signals, (i * 16) + (NumberOfSignalsInDataRecord * charIndex), 16);
                edf_signal.Label = new string(label);
                charIndex += 16;

                edf_signal.IndexNumber = (i + 1);

                char[] transducer_type = getFixedLengthCharArrayFromHeader(signals, (i * 80) + (NumberOfSignalsInDataRecord * charIndex), 80);
                edf_signal.TransducerType = new string(transducer_type);
                charIndex += 80;

                char[] physical_dimension = getFixedLengthCharArrayFromHeader(signals, (i * 8) + (NumberOfSignalsInDataRecord * charIndex), 8);
                edf_signal.PhysicalDimension = new string(physical_dimension);
                charIndex += 8;

                char[] physical_min = getFixedLengthCharArrayFromHeader(signals, (i * 8) + (NumberOfSignalsInDataRecord * charIndex), 8);
                edf_signal.PhysicalMinimum = float.Parse(new string(physical_min).Trim());
                charIndex += 8;

                char[] physical_max = getFixedLengthCharArrayFromHeader(signals, (i * 8) + (NumberOfSignalsInDataRecord * charIndex), 8);
                edf_signal.PhysicalMaximum = float.Parse(new string(physical_max).Trim());
                charIndex += 8;

                char[] digital_min = getFixedLengthCharArrayFromHeader(signals, (i * 8) + (NumberOfSignalsInDataRecord * charIndex), 8);
                edf_signal.DigitalMinimum = float.Parse(new string(digital_min).Trim());
                charIndex += 8;

                char[] digital_max = getFixedLengthCharArrayFromHeader(signals, (i * 8) + (NumberOfSignalsInDataRecord * charIndex), 8);
                edf_signal.DigitalMaximum = float.Parse(new string(digital_max).Trim());
                charIndex += 8;

                char[] prefiltering = getFixedLengthCharArrayFromHeader(signals, (i * 80) + (NumberOfSignalsInDataRecord * charIndex), 80);
                edf_signal.Prefiltering = new string(prefiltering);
                charIndex += 80;

                char[] samples_each_datarecord = getFixedLengthCharArrayFromHeader(signals, (i * 8) + (NumberOfSignalsInDataRecord * charIndex), 8);
                edf_signal.NumberOfSamplesPerDataRecord = int.Parse(new string(samples_each_datarecord).Trim());
                charIndex += 8;

                Signals.Add(edf_signal);
            }
        }

        private string getFixedLengthString(string input, int length)
        {
            return (input ?? "").Length > length ? (input ?? "").Substring(0, length) : (input ?? "").PadRight(length);
        }

        private char[] getFixedLengthCharArrayFromHeader(char[] header, int startPoint, int length)
        {
            char[] ch = new char[length];
            Array.Copy(header, startPoint, ch, 0, length);
            return ch;
        }


        public override string ToString()
        {
            StringBuilder _strHeaderBuilder = new StringBuilder(string.Empty);

            _strHeaderBuilder.Append(getFixedLengthString(Version, EDFHeader.FixedLength_Version));
            _strHeaderBuilder.Append(PatientIdentification.ToString());
            _strHeaderBuilder.Append(RecordingIdentification.ToString());
            _strHeaderBuilder.Append(StartDateEDF);
            _strHeaderBuilder.Append(StartTimeEDF);
            _strHeaderBuilder.Append(getFixedLengthString(Convert.ToString(NumberOfBytes), EDFHeader.FixedLength_NumberOfBytes));
            _strHeaderBuilder.Append(Reserved);
            _strHeaderBuilder.Append(getFixedLengthString(Convert.ToString(NumberOfDataRecords), EDFHeader.FixedLength_NumberOfDataRecords));
            _strHeaderBuilder.Append(getFixedLengthString(Convert.ToString(DurationOfDataRecordInSeconds), EDFHeader.FixedLength_DuraitonOfDataRecordInSeconds));
            _strHeaderBuilder.Append(getFixedLengthString(Convert.ToString(NumberOfSignalsInDataRecord), EDFHeader.FixedLength_NumberOfSignalsInDataRecord));

            foreach(EDFSignal s in Signals)
                _strHeaderBuilder.Append(getFixedLengthString(s.Label, 16));
            foreach(EDFSignal s in Signals)
                _strHeaderBuilder.Append(getFixedLengthString(s.TransducerType, 80));
            foreach(EDFSignal s in Signals)
                _strHeaderBuilder.Append(getFixedLengthString(s.PhysicalDimension, 8));
            foreach(EDFSignal s in Signals)
                _strHeaderBuilder.Append(getFixedLengthString(Convert.ToString(s.PhysicalMinimum), 8));
            foreach(EDFSignal s in Signals)
                _strHeaderBuilder.Append(getFixedLengthString(Convert.ToString(s.PhysicalMaximum), 8));
            foreach(EDFSignal s in Signals)
                _strHeaderBuilder.Append(getFixedLengthString(Convert.ToString(s.DigitalMinimum), 8));
            foreach(EDFSignal s in Signals)
                _strHeaderBuilder.Append(getFixedLengthString(Convert.ToString(s.DigitalMaximum), 8));
            foreach(EDFSignal s in Signals)
                _strHeaderBuilder.Append(getFixedLengthString(s.Prefiltering, 80));
            foreach(EDFSignal s in Signals)
                _strHeaderBuilder.Append(getFixedLengthString(Convert.ToString(s.NumberOfSamplesPerDataRecord), 8));
            foreach(EDFSignal s in Signals)
                _strHeaderBuilder.Append(getFixedLengthString("", 32));

            if(_strHeaderBuilder.ToString().ToCharArray().Length != (256 + (Signals.Count * 256)))
            {
                throw new InvalidOperationException("Header Length must be equal to (256 characters + (number of signals) * 256 ).  Header length=" + _strHeaderBuilder.ToString().ToCharArray().Length + "  Header=" + _strHeaderBuilder.ToString());
            }

            _strHeader = _strHeaderBuilder;
            return _strHeaderBuilder.ToString();
        }
    }
}
