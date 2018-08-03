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

		public PsgViewControl()
		{
			PsgData = new Polysomnogram();

			InitializeComponent();

			DoubleBuffered = true;
		}

		public void SetEdfFile(EDFFile edfFile)
		{
			PsgData.LoadFromEdfFile(edfFile);
			TimelineScrollBar.Minimum = 0;
			TimelineScrollBar.Maximum = (int) PsgData.Duration + TimelineScrollBar.LargeChange - 1;
			
			ChannelHeadersPanel.LoadHeaders(edfFile.Header);
			graphViewControl.LoadChannelData(PsgData.Channels);
		}

		public void LoadRandomData()
		{
			Timeseries[] data = new Timeseries[7];
			Random rng = new Random("hei".GetHashCode());

			for(int i = 0; i < data.Length; i++)
			{
				data[i] = new Timeseries();
				data[i].Label = $"Example {i + 1}";
				data[i].SampleRate = 200;
				data[i].Data = new float[40000];

				float filter = 0;

				for(int j = 0; j < data[i].Data.Length; j++)
				{
					float s = (float)(rng.NextDouble() - 0.5) * 60.0F;
					filter = filter * 0.97F + 0.03F * s;
					data[i].Data[j] = filter * 4.0F;
				}
			}
			

			TimelineScrollBar.Maximum = (int)(data[0].Data.Length / data[0].SampleRate) + TimelineScrollBar.LargeChange - 1;

			graphViewControl.LoadChannelData(data);
		}
		
		public void SkipForward()
		{
			graphViewControl.TimeOffset += Width / graphViewControl.ScaleX;
			Invalidate();
		}

		public void SkipBackward()
		{
			graphViewControl.TimeOffset -= Width / graphViewControl.ScaleX;
			Invalidate();
		}

		private void TimelineScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			graphViewControl.TimeOffset = TimelineScrollBar.Value;
			graphViewControl.Invalidate();
		}

		private void ChannelScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			graphViewControl.OffsetY = ChannelScrollBar.Value;
			graphViewControl.Invalidate();
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			graphViewControl.Invalidate();
			base.OnInvalidated(e);
		}
	}
}
