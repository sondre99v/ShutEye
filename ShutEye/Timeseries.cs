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
		public string Label { get; /*private*/ set; }

		public float SampleRate { get; /*private*/ set; }

		public float[] Data;

		public void LoadFromEdfFile(EDFFile file, int channelIndex)
		{
			EDFSignal signal = file.Header.Signals[channelIndex];

			Label = signal.Label;
			SampleRate = signal.NumberOfSamplesPerDataRecord / file.Header.DurationOfDataRecordInSeconds;

			int numberOfSamples = signal.NumberOfSamplesPerDataRecord * file.Header.NumberOfDataRecords;

			Data = new float[numberOfSamples];

			for(int i = 0; i < Data.Length; i++)
			{
				EDFDataRecord record = file.DataRecords[i / signal.NumberOfSamplesPerDataRecord];
				List<float> recordData = record[signal.IndexNumber];

				Data[i] = recordData[i % signal.NumberOfSamplesPerDataRecord];
			}
		}
	}
}
