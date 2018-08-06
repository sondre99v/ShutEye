using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutEye
{
	class Selection
	{
		public float StartTime { get; set; }
		public float EndTime { get; set; }

		public float Length => EndTime - StartTime;

		public int SelectedChannelIndex { get; set; }

		public bool Active { get; set; }

		public Selection(float startTime, float endTime)
		{
			StartTime = startTime;
			EndTime = endTime;

			SelectedChannelIndex = -1;

			Active = false;
		}
	}
}
