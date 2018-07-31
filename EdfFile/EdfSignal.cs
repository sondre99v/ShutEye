using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdfFile
{
    public class EDFSignal
    {
        // Channel Identification

        public int IndexNumber { get; set; }

        public string Label { get; set; }

        public string IndexNumberWithLabel => IndexNumber + "." + Label;
        
        public string LabelType { get; set; }

        public string LabelSpecification { get; set; }
        
        public override string ToString() => IndexNumberWithLabel;


        // Measurement Metadata

        public string TransducerType { get; set; }

        public string PhysicalDimension { get; set; }

        public string PhysicalDimensionPrefix { get; set; }

        public string PhysicalDimensionBasic { get; set; }

        public float PhysicalMinimum { get; set; }

        public float PhysicalMaximum { get; set; }

        public float DigitalMinimum { get; set; }

        public float DigitalMaximum { get; set; }

        public string Prefiltering { get; set; }

        public int NumberOfSamplesPerDataRecord { get; set; }
        
        public float AmplifierGain => (PhysicalMaximum - PhysicalMinimum) / (DigitalMaximum - DigitalMinimum);

        public float Offset => (PhysicalMaximum / AmplifierGain) - DigitalMaximum;

        public float SamplePeriodWithinDataRecord { get; set; }


        // Measurement Data

        public float[] Data;
    }
}
