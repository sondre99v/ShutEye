using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ShutEye
{
	public class Hypnogram
	{
		public double SampleRate { get; private set; }

		public SleepStage[] SleepStages { get; private set; }

		public SleepStage GetSleepStageAtTime(double timeOffset)
		{
			int index = (int) Math.Floor(timeOffset * SampleRate);

			if(index < 0 || index >= SleepStages.Length)
			{
				return SleepStage.None;
			}
			else
			{
				return SleepStages[index];
			}
		}

		public void LoadFromFile(string path)
		{
			string[] lines = File.ReadAllLines(path);

			DateTime t0 = DateTime.Parse(lines[0].Split(',')[2]);
			DateTime t1 = DateTime.Parse(lines[1].Split(',')[2]);

			SampleRate = 1.0 / (t1 - t0).TotalSeconds;

			SleepStages = lines.Select(l => SleepStage.Parse(l.Split(',')[3])).ToArray();
		}
	}
}
