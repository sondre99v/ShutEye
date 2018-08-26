using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace ShutEye
{
	class ResultsFile
	{
		/// <summary>
		/// The sample rate used by the EEG.
		/// </summary>
		public int SampleRate { get; private set; }

		/// <summary>
		/// The start time for the EEG.
		/// </summary>
		public DateTime StartTime { get; private set; }

		/// <summary>
		/// The total number of sleep spindles in this file.
		/// </summary>
		public int NumberOfSpindles { get; private set; }

		/// <summary>
		/// The average duration of the sleep spindles in this file.
		/// </summary>
		public float AverageSpindleDuration { get; private set; }

		/// <summary>
		/// The average frequency of the sleep spindles in this file.
		/// </summary>
		public float AverageSpindleFrequency { get; private set; }

		/// <summary>
		/// The median frequency of the sleep spindles in this file.
		/// </summary>
		public float MedianSpindleFrequency { get; private set; }

		/// <summary>
		/// The name (label) of the channel with the most clearly visible spindles.
		/// </summary>
		public string MostSignificantChannel { get; private set; }

		/// <summary>
		/// The average occurance rate (occurances/second) of sleep spindles during sleep.
		/// </summary>
		public float SpindleDensity { get; private set; }

		/// <summary>
		/// The ratio between total length of sleep spindles and total duration of sleep.
		/// </summary>
		public float SpindleRatio { get; private set; }

		/// <summary>
		/// List of sleep spindles that are in the file.
		/// </summary>
		public List<SleepSpindleInformation> SleepSpindles { get; private set; }


		public ResultsFile()
		{
			SleepSpindles = new List<SleepSpindleInformation>();
		}

		public void LoadFromSelections(EDF.EDFFile edfFile, Timeseries[] channels, Selection[] selections)
		{
			SampleRate = (int) channels[0].SampleRate;
			StartTime = edfFile.Header.StartDateTime;
			NumberOfSpindles = selections.Length;
			AverageSpindleDuration = selections.Sum(s => s.Length) / selections.Length;
			AverageSpindleFrequency = 0.0F;
			MedianSpindleFrequency = 0.0F;
			SpindleDensity = selections.Length / (edfFile.Header.DurationOfDataRecordInSeconds * edfFile.Header.NumberOfDataRecords);
			SpindleRatio = selections.Sum(s => s.Length) / (edfFile.Header.DurationOfDataRecordInSeconds * edfFile.Header.NumberOfDataRecords);

			int[] mostSignificantChannelBuckets = new int[channels.Length];
			List<float> frequencies = new List<float>();

			foreach(Selection s in selections)
			{
				var info = new SleepSpindleInformation();
				info.BestChannel = s.SelectedChannelIndex == -1 ? "none" : channels[s.SelectedChannelIndex].Label;
				if(s.SelectedChannelIndex != -1)
				{
					mostSignificantChannelBuckets[s.SelectedChannelIndex]++;
				}

				info.StartTime = s.StartTime;
				info.Duration = s.Length;
				info.AverageFrequency = (float) s.AverageFrequency;
				info.MedianFrequency = (float) s.MedianFrequency;
				info.FrequencyInBestChannel = (float) s.SelectedChannelFrequency;

				AverageSpindleFrequency += (float) s.AverageFrequency;
				frequencies.Add((float) s.MedianFrequency);

				SleepSpindles.Add(info);
			}

			AverageSpindleFrequency /= selections.Length;

			int fc = frequencies.Count;
			frequencies.Sort();
			if(fc % 2 == 0)
			{
				MedianSpindleFrequency = (frequencies[fc / 2] + frequencies[fc / 2 - 1]) / 2.0F;
			}
			else
			{
				MedianSpindleFrequency = frequencies[fc / 2];
			}

			MostSignificantChannel = channels[mostSignificantChannelBuckets.Max()].Label;
		}

		public void SaveToFile(string filename)
		{
			FileStream fs = new FileStream(filename, FileMode.Create);

			StreamWriter sw = new StreamWriter(fs);

			sw.WriteLine("SPINDELANALYSE");
			sw.WriteLine("Sample Rate for EEG:;" + SampleRate.ToString());
			sw.WriteLine("Startklokkeslett for EEG:;" + StartTime.ToString("HH:mm:ss"));
			sw.WriteLine("Gjennomsnittlig spindelvarighet:;" + AverageSpindleDuration.ToString(CultureInfo.InvariantCulture));
			sw.WriteLine("Gjennomsnittlig spindelfrekvens:;" + AverageSpindleFrequency.ToString(CultureInfo.InvariantCulture));
			sw.WriteLine("Median av medianspindelfrekvens:;" + MedianSpindleFrequency.ToString(CultureInfo.InvariantCulture));
			sw.WriteLine("Avledning med mest tydelige spindler:;" + MostSignificantChannel);
			sw.WriteLine("Antall spindler per 30 sekunder sovn:;" + SpindleDensity.ToString(CultureInfo.InvariantCulture));
			sw.WriteLine("Andel av sovn som er sovnspindel:;" + SpindleRatio.ToString(CultureInfo.InvariantCulture));
			sw.WriteLine();
			sw.WriteLine("Spindelnummer;Starttidspunkt;Varighet;Snittfrekvens;Medianfrekvens;Sees best i avledning;Frekvens i beste avledning");

			for(int i = 0; i < SleepSpindles.Count; i++)
			{
				sw.Write((i + 1).ToString() + ";");
				SleepSpindles[i].WriteToStream(sw);
				sw.WriteLine();
			}

			sw.Close();
		}

		public void LoadFromFile(string filename)
		{
			throw new NotImplementedException();
		}
	}
}
