using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutEye
{
    class PsgChannel
    {
        public double SampleRate { get; set; }
        double[] Data;

        public PsgChannel(double[] data, double sampleRate)
        {
            SampleRate = sampleRate;
            Data = data;
        }
    }
}
