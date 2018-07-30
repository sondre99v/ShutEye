using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutEye
{
    class Polysomnogram
    {
        public double ScrollPosition { get; set; } = 0;
        public double ZoomScale { get; set; } = 100;
        public double SampleRate { get; set; }
        public double DataLength { get; set; }
        public int SamplesPrChannel { get => (int) (SampleRate * DataLength); }

        PsgChannel[] Channels;

        public Polysomnogram()
        {
            // Load file
            Channels = new PsgChannel[6];
            SampleRate = 10000;
            DataLength = 10;
            Random rng = new Random("hei".GetHashCode());

            for(int i = 0; i < 6; i++)
            {
                double[] data = new double[SamplesPrChannel];

                for(int s = 0; s < data.Length; s++)
                {
                    data[s] = rng.NextDouble();
                }

                Channels[i] = new PsgChannel(data, SampleRate);
            }
        }
    }
}
