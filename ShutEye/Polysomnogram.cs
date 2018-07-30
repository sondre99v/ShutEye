using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDF;

namespace ShutEye
{
    class Polysomnogram
    {
        public float SampleRate { get; set; }
        public float Duration { get; set; }

        public int SamplesPrChannel { get => (int) (SampleRate * Duration); }

        public PsgChannel[] Channels;

        public Polysomnogram(EDFFile edfFile)
        {
            // Load file
            Channels = new PsgChannel[edfFile.Header.NumberOfSignalsInDataRecord];
            SampleRate = edfFile.Header.Signals[0].NumberOfSamplesPerDataRecord / edfFile.Header.DurationOfDataRecordInSeconds;
            Duration = edfFile.Header.DurationOfDataRecordInSeconds * edfFile.Header.NumberOfDataRecords;

            for(int i = 0; i < Channels.Length; i++)
            {
                Channels[i] = new PsgChannel(edfFile, i);
            }
        }
    }
}
