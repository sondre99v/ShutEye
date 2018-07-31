using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDF;

namespace ShutEye
{
    class Timeseries
    {
        public string Label { get; private set; }

        public float SampleRate { get; private set; }

        public float[] Data;

        public void LoadFromEdfFile(EDFFile file, int channelIndex)
        {
            Label = file.Header.Signals[channelIndex].Label;
            SampleRate = file.Header.Signals[channelIndex].NumberOfSamplesPerDataRecord / file.Header.DurationOfDataRecordInSeconds;

            int numberOfSamples = file.Header.Signals[channelIndex].NumberOfSamplesPerDataRecord * file.Header.NumberOfDataRecords;

            Data = new float[numberOfSamples];

            for(int i = 0; i < Data.Length; i++)
            {
                EDFDataRecord record = file.DataRecords[i / file.Header.Signals[channelIndex].NumberOfSamplesPerDataRecord];
                List<float> recordData = record[channelIndex + 1];

                Data[i] = recordData[i % file.Header.Signals[channelIndex].NumberOfSamplesPerDataRecord];
            }
        }
    }
}
