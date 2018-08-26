using System;
using System.IO;
using System.Globalization;

namespace ShutEye
{
	class SleepSpindleInformation
	{
		public float StartTime { get; set; }
		public float Duration { get; set; }
		public float AverageFrequency { get; set; }
		public float MedianFrequency { get; set; }
		public string BestChannel { get; set; }
		public float FrequencyInBestChannel { get; set; }

		public void WriteToStream(StreamWriter sw)
		{
			sw.Write(StartTime.ToString(CultureInfo.InvariantCulture) + ";");
			sw.Write(Duration.ToString(CultureInfo.InvariantCulture) + ";");
			sw.Write(AverageFrequency.ToString(CultureInfo.InvariantCulture) + ";");
			sw.Write(MedianFrequency.ToString(CultureInfo.InvariantCulture) + ";");
			sw.Write(BestChannel + ";");
			sw.Write(FrequencyInBestChannel.ToString(CultureInfo.InvariantCulture) + ";");
		}
	}
}
