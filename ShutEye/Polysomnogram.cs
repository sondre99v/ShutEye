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

        public Timeseries[] Channels;

        
        public void LoadFromEdfFile(EDFFile file)
        {
            Channels = new Timeseries[file.Header.NumberOfSignalsInDataRecord];
            SampleRate = file.Header.Signals[0].NumberOfSamplesPerDataRecord / file.Header.DurationOfDataRecordInSeconds;
            Duration = file.Header.DurationOfDataRecordInSeconds * file.Header.NumberOfDataRecords;

            for(int i = 0; i < Channels.Length; i++)
            {
                Channels[i] = new Timeseries();
                Channels[i].LoadFromEdfFile(file, i);
            }
        }
    }
}
