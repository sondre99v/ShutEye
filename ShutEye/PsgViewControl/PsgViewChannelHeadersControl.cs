using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShutEye
{
	public partial class PsgViewChannelHeadersControl: UserControl
	{
		public event Action<int, float> ScaleButtonPressed;

		private List<PsgChannelHeaderControl> ChannelHeaders = new List<PsgChannelHeaderControl>();

		public PsgViewChannelHeadersControl()
		{
			InitializeComponent();
		}

		public void LoadHeaders(Timeseries[] signals)
		{
			ChannelHeaders.Clear();

			for(int i = 0; i < signals.Length; i++)
			{
				Timeseries signal = signals[i];
				var header = new PsgChannelHeaderControl(i);
				header.ChannelLabel = signal.Label;
				header.ScaleButtonPressed += ChannelHeaders_ScaleButtonPressed;
				header.BackColor = (i % 2 == 0) ? SystemColors.Control : SystemColors.ControlLight;

				header.Location = new Point(0, header.Height * i);

				ChannelHeaders.Add(header);
			}

			Controls.AddRange(ChannelHeaders.ToArray());
		}

		public void ScrollHeaders(int scrollPosition)
		{
			for(int i = 0; i < ChannelHeaders.Count; i++)
			{
				ChannelHeaders[i].Location = new Point(0, ChannelHeaders[i].Height * i - scrollPosition);
			}
		}

		private void ChannelHeaders_ScaleButtonPressed(int channelIndex, float scaleFactor)
		{
			ScaleButtonPressed.Invoke(channelIndex, scaleFactor);
		}
	}
}
