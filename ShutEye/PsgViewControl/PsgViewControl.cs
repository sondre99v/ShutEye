using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDF;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ShutEye
{
	public partial class PsgViewControl: UserControl
	{
		public int ChannelSeparation { get; set; } = 10;
		public double ChannelScale { get; set; } = 0.25;
		public double Zoom { get; set; } = 10;

		private Polysomnogram PsgData;

		private double _timeOffset = 0;

		public PsgViewControl()
		{
			PsgData = new Polysomnogram();

			InitializeComponent();

			DoubleBuffered = true;

			MouseWheel += PsgViewControl_MouseWheel;
		}

		private void PsgViewControl_MouseWheel(object sender, MouseEventArgs e)
		{
			if(e.Delta < 0 && TimelineScrollBar.Value - TimelineScrollBar.SmallChange >= TimelineScrollBar.Minimum)
			{
				TimelineScrollBar.Value -= TimelineScrollBar.SmallChange;
			}
			else if(e.Delta > 0 && TimelineScrollBar.Value + TimelineScrollBar.SmallChange <= TimelineScrollBar.Maximum)
			{
				TimelineScrollBar.Value += TimelineScrollBar.SmallChange;
			}


			graphViewControl.TimeOffset = (float) TimelineScrollBar.Value / (TimelineScrollBar.Maximum - TimelineScrollBar.LargeChange - 1);
			graphViewControl.Invalidate();
		}

		public void SetEdfFile(EDFFile edfFile)
		{
			PsgData.LoadFromEdfFile(edfFile);
			TimelineScrollBar.Minimum = 0;
			TimelineScrollBar.Maximum = (int) PsgData.Duration;

			graphViewControl.DataChannels.AddRange(PsgData.Channels);
		}

		private void TimelineScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			graphViewControl.TimeOffset = (float) TimelineScrollBar.Value / (TimelineScrollBar.Maximum - TimelineScrollBar.LargeChange - 1);
			graphViewControl.Invalidate();
		}
	}
}
