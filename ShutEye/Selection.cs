using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPLib;

namespace ShutEye
{
	class Selection
	{
		Polysomnogram Polysomnogram;

		private float _startTime;
		public float StartTime
		{
			get => _startTime;
			set
			{
				_startTime = value;
				_calculateFrequency();
			}
		}

		private float _endTime;
		public float EndTime
		{
			get => _endTime;
			set
			{
				_endTime = value;
				_calculateFrequency();
			}
		}

		public float Length => EndTime - StartTime;

		public int SelectedChannelIndex { get; set; }

		public double[] ChannelFrequency { get; set; } = new double[0];

		public double AverageFrequency { get => ChannelFrequency.Sum() / ChannelFrequency.Length; }

		public double MedianFrequency
		{
			get
			{
				double[] freqs = new double[ChannelFrequency.Length];
				ChannelFrequency.CopyTo(freqs, 0);
				Array.Sort(freqs);
				if(freqs.Length % 2 == 0)
				{
					return (freqs[freqs.Length / 2] + freqs[freqs.Length / 2 + 1]) / 2.0;
				}
				else
				{
					return freqs[(freqs.Length - 1) / 2];
				}
			}
		}

		public double SelectedChannelFrequency
		{
			get
			{
				if(SelectedChannelIndex == -1) return double.NaN;
				else return ChannelFrequency[SelectedChannelIndex];
			}
		}

		public bool Active { get; set; }

		public Selection(float startTime, float endTime, Polysomnogram psg)
		{
			_startTime = startTime;
			_endTime = endTime;
			Polysomnogram = psg;
			_calculateFrequency();

			SelectedChannelIndex = -1;

			Active = false;
		}

		private void _calculateFrequency()
		{
			double minimumFrequency = 5.0;

			if(Polysomnogram == null) return;

			ChannelFrequency = new double[Polysomnogram.Channels.Length];

			for(int i = 0; i < ChannelFrequency.Length; i++)
			{
				double sampleRate = Polysomnogram.Channels[i].SampleRate;
				int startIndex = (int) (StartTime * sampleRate);
				int endIndex = (int) (EndTime * sampleRate);

				DFT dft = new DFT();

				double[] data = new double[endIndex - startIndex];

				for(int k = 0; k < data.Length; k++)
				{
					data[k] = Polysomnogram.Channels[i].Data[startIndex + k];
				}

				dft.Initialize((uint) (endIndex - startIndex));

				double[] magnitudes = dft.Execute(data).Select(c => c.Magnitude).ToArray();

				int minimumIndex = Array.FindIndex(dft.FrequencySpan(sampleRate), d => d > minimumFrequency);
				int peakIndex = Array.IndexOf(magnitudes, magnitudes.Skip(minimumIndex).Max());

				ChannelFrequency[i] = dft.FrequencySpan(sampleRate)[peakIndex];
			}
		}
	}
}
