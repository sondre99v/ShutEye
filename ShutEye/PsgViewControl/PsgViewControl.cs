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
		private EDFFile file;

		public PsgViewControl()
		{
			PsgData = new Polysomnogram();

			InitializeComponent();

			DoubleBuffered = true;

			ChannelHeadersPanel.ScaleButtonPressed += ChannelHeaders_ScaleButtonPressed;
			ChannelHeadersPanel.RemoveChannelButtonPressed += ChannelHeadersPanel_RemoveChannelButtonPressed;
		}

		public void SetHypnogram(Hypnogram hypnogram)
		{
			hypnogramControl.SetHypnogram(hypnogram);
		}

		public void SetEdfFile(EDFFile file, ChannelConfiguration[] configurations)
		{
			this.file = file;

			PsgData.LoadFromChannelConfigurations(file, configurations);
			TimelineScrollBar.Minimum = 0;
			TimelineScrollBar.Maximum = (int) PsgData.Duration;
			TimelineScrollBar.LargeChange = (int) graphViewControl.ViewDuration;
			TimelineScrollBar.SmallChange = 1;

			ChannelHeadersPanel.LoadHeaders(PsgData.Channels);
			ChannelScrollBar.Maximum = PsgData.Channels.Length * 57;
			ChannelScrollBar.SmallChange = 57;
			ChannelScrollBar.LargeChange = graphViewControl.Height;

			graphViewControl.Polysomnogram = PsgData;
			graphViewControl.AddChannelRange(PsgData.Channels);
		}

		public void OpenSelectionFile(string filename)
		{
			string[] lines = System.IO.File.ReadAllLines(filename);

			graphViewControl.ClearSelections();

			int sampleRate = int.Parse(lines[1]);

			foreach(string line in lines.Skip(7))
			{
				string[] fields = line.Split(' ');

				int startSample = int.Parse(fields[0]);
				int endSample = int.Parse(fields[1]);

				graphViewControl.AddSelection((float) startSample / sampleRate, (float) endSample / sampleRate);
			}

			Console.WriteLine($"Loaded {graphViewControl.Selections.Count} selections.");
		}

		public void SaveSelectionFile(string filename)
		{
			ResultsFile rs = new ResultsFile();

			rs.LoadFromSelections(file, graphViewControl.ChannelsData.ToArray(), graphViewControl.Selections.ToArray());

			rs.SaveToFile(filename);

			Console.WriteLine($"Saved {graphViewControl.Selections.Count} selections.");
		}

		public void LoadRandomData()
		{
			Timeseries[] data = new Timeseries[7];
			Random rng = new Random("hei".GetHashCode());

			for(int i = 0; i < data.Length; i++)
			{
				data[i] = new Timeseries();
				data[i].Label = $"Ex {i + 1}";
				data[i].SampleRate = 200;
				data[i].ViewAmplitude = 30.0F;
				data[i].Data = new float[40000];

				float filter = 0;

				for(int j = 0; j < data[i].Data.Length; j++)
				{
					float s = (float) (rng.NextDouble() - 0.5) * 0.1F;
					filter = filter * 0.97F + 0.03F * s;
					data[i].Data[j] = filter * 4.0F;
				}
			}


			TimelineScrollBar.Maximum = (int) (data[0].Data.Length / data[0].SampleRate) + TimelineScrollBar.LargeChange - 1;

			ChannelHeadersPanel.LoadHeaders(data);
			PsgData.Channels = data;
			ChannelScrollBar.Maximum = PsgData.Channels.Length * 57;

			graphViewControl.AddChannelRange(data);
		}

		public void SkipForward()
		{
			graphViewControl.TimeOffset += TimelineScrollBar.SmallChange;
			if(graphViewControl.TimeOffset > TimelineScrollBar.Maximum) graphViewControl.TimeOffset = TimelineScrollBar.Maximum;

			TimelineScrollBar.Value = (int) graphViewControl.TimeOffset;
			hypnogramControl.SetMarkerPosition(TimelineScrollBar.Value);
			Invalidate();
		}

		public void SkipBackward()
		{
			graphViewControl.TimeOffset -= TimelineScrollBar.SmallChange;
			if(graphViewControl.TimeOffset < 0) graphViewControl.TimeOffset = 0;

			TimelineScrollBar.Value = (int) graphViewControl.TimeOffset;
			hypnogramControl.SetMarkerPosition(TimelineScrollBar.Value);
			Invalidate();
		}

		private void TimelineScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			graphViewControl.TimeOffset = TimelineScrollBar.Value;
			hypnogramControl.SetMarkerPosition(TimelineScrollBar.Value);
			graphViewControl.Invalidate();
		}

		private void ChannelScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			ChannelHeadersPanel.ScrollPosition = ChannelScrollBar.Value;
			ChannelHeadersPanel.ScrollControlIntoView(ChannelHeadersPanel);

			graphViewControl.OffsetY = ChannelScrollBar.Value;
			graphViewControl.Invalidate();
			graphViewControl.Refresh();
			ChannelHeadersPanel.Refresh();
		}

		private void ChannelHeaders_ScaleButtonPressed(int channelIndex, float scaleFactor)
		{
			graphViewControl.ChannelsData[channelIndex].ViewAmplitude *= scaleFactor;
			graphViewControl.Invalidate();
		}

		private void ChannelHeadersPanel_RemoveChannelButtonPressed(int channelIndex)
		{
			graphViewControl.RemoveChannel(channelIndex);
			ChannelHeadersPanel.RemoveChannel(channelIndex);
			graphViewControl.Invalidate();
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			graphViewControl.Invalidate();
			base.OnInvalidated(e);
		}
	}
}
