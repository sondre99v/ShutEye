using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDF;

namespace ShutEye
{
    class PsgChannel
    {
        public string Label { get; private set; }
        public float SampleRate { get; private set; }

        public short[] Data;

        public PsgChannel(EDFFile edfFile, int channelIndex)
        {
            Label = edfFile.Header.Signals[channelIndex].Label;
            SampleRate = edfFile.Header.Signals[channelIndex].NumberOfSamplesPerDataRecord / edfFile.Header.DurationOfDataRecordInSeconds;

            int numberOfSamples = edfFile.Header.Signals[channelIndex].NumberOfSamplesPerDataRecord * edfFile.Header.NumberOfDataRecords;

            Data = new short[numberOfSamples];

            for(int i = 0; i < Data.Length; i++)
            {
                Data[i] = (short)edfFile.DataRecords
                    [i / edfFile.Header.Signals[channelIndex].NumberOfSamplesPerDataRecord]
                    [channelIndex + 1]
                    [i % edfFile.Header.Signals[channelIndex].NumberOfSamplesPerDataRecord];
            }

        }
    }
}
