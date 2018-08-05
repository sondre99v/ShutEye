using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDF;

namespace ShutEye
{
	class Polysomnogram
	{
		public float Duration { get; set; }

		public Timeseries[] Channels;
		public bool[] VisibleChannels;


		public void LoadFromChannelConfigurations(EDFFile file, ChannelConfiguration[] configurations)
		{
			Duration = file.Header.DurationOfDataRecordInSeconds * file.Header.NumberOfDataRecords;

			Channels = new Timeseries[configurations.Length];
			VisibleChannels = new bool[configurations.Length];

			for(int i = 0; i < Channels.Length; i++)
			{
				Channels[i] = new Timeseries();
				Channels[i].LoadFromEdfFile(file, configurations[i].Signal, configurations[i].Reference);

				VisibleChannels[i] = configurations[i].IsShown;
			}
		}
	}
}
