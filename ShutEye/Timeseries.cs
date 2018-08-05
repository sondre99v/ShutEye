using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDF;

namespace ShutEye
{
	public class Timeseries
	{
		public string Label { get; /*private*/ set; }
		public string ReferenceLabel { get; set; }

		public float SampleRate { get; /*private*/ set; }

		public float ViewAmplitude { get; /*private*/ set; }

		public float[] Data;

		public void LoadFromEdfFile(EDFFile file, EDFSignal signal, EDFSignal reference)
		{
			Label = signal.Label;
			ReferenceLabel = reference?.Label ?? "mono";

			SampleRate = signal.NumberOfSamplesPerDataRecord / file.Header.DurationOfDataRecordInSeconds;

			int numberOfSamples = signal.NumberOfSamplesPerDataRecord * file.Header.NumberOfDataRecords;

			Data = new float[numberOfSamples];

			ViewAmplitude = 0.005F;

			for(int i = 0; i < Data.Length; i++)
			{
				EDFDataRecord record = file.DataRecords[i / signal.NumberOfSamplesPerDataRecord];
				float[] recordData = record[signal.IndexNumber];

				if(reference == null)
				{
					Data[i] = recordData[i % signal.NumberOfSamplesPerDataRecord];
				}
				else
				{
					EDFDataRecord refRecord = file.DataRecords[i / signal.NumberOfSamplesPerDataRecord];
					float[] refRecordData = refRecord[reference.IndexNumber];
					Data[i] = recordData[i % reference.NumberOfSamplesPerDataRecord] - refRecordData[i % reference.NumberOfSamplesPerDataRecord];
				}
			}
		}
	}
}
