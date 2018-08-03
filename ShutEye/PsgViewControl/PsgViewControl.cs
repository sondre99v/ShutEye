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

			ChannelHeadersPanel.ScaleButtonPressed += ChannelHeaders_ScaleButtonPressed;
		}

		public void SetEdfFile(EDFFile edfFile)
		{
			PsgData.LoadFromEdfFile(edfFile);
			TimelineScrollBar.Minimum = 0;
			TimelineScrollBar.Maximum = (int) PsgData.Duration;// + TimelineScrollBar.LargeChange - 1;

			ChannelHeadersPanel.LoadHeaders(PsgData.Channels);
			ChannelScrollBar.Maximum = PsgData.Channels.Length * 57;
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
				data[i].ViewAmplitude = 30.0F;
				data[i].Data = new float[40000];

				float filter = 0;

				for(int j = 0; j < data[i].Data.Length; j++)
				{
					float s = (float)(rng.NextDouble() - 0.5) * 0.1F;
					filter = filter * 0.97F + 0.03F * s;
					data[i].Data[j] = filter * 4.0F;
				}
			}
			

			TimelineScrollBar.Maximum = (int)(data[0].Data.Length / data[0].SampleRate) + TimelineScrollBar.LargeChange - 1;

			ChannelHeadersPanel.LoadHeaders(data);
			PsgData.Channels = data;
			ChannelScrollBar.Maximum = PsgData.Channels.Length * 57;

			graphViewControl.LoadChannelData(data);
		}
		
		public void SkipForward()
		{
			graphViewControl.TimeOffset += Width / graphViewControl.ScaleX;
			if (graphViewControl.TimeOffset > TimelineScrollBar.Maximum) graphViewControl.TimeOffset = TimelineScrollBar.Maximum;

			TimelineScrollBar.Value = (int)graphViewControl.TimeOffset;
			Invalidate();
		}

		public void SkipBackward()
		{
			graphViewControl.TimeOffset -= Width / graphViewControl.ScaleX;
			if (graphViewControl.TimeOffset < 0) graphViewControl.TimeOffset = 0;

			TimelineScrollBar.Value = (int)graphViewControl.TimeOffset;
			Invalidate();
		}

		private void TimelineScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			graphViewControl.TimeOffset = TimelineScrollBar.Value;
			graphViewControl.Invalidate();
		}

		private void ChannelScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			ChannelHeadersPanel.ScrollHeaders(ChannelScrollBar.Value);
			ChannelHeadersPanel.ScrollControlIntoView(ChannelHeadersPanel);

			graphViewControl.OffsetY = ChannelScrollBar.Value;
			graphViewControl.Invalidate();
		}

		private void ChannelHeaders_ScaleButtonPressed(int channelIndex, float scaleFactor)
		{
			PsgData.Channels[channelIndex].ViewAmplitude *= scaleFactor;
			graphViewControl.Invalidate();
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			graphViewControl.Invalidate();
			base.OnInvalidated(e);
		}
	}
}
