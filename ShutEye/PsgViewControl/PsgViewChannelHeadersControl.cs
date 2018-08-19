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
		public event Action<int> RemoveChannelButtonPressed;

		private List<PsgChannelHeaderControl> ChannelHeaders = new List<PsgChannelHeaderControl>();

		private int _scrollPosition;
		public int ScrollPosition
		{
			get => _scrollPosition;
			set
			{
				_scrollPosition = value;
				_repositionHeaders();
			}
		}

		public PsgViewChannelHeadersControl()
		{
			InitializeComponent();
		}

		public void LoadHeaders(Timeseries[] signals)
		{
			for(int i = 0; i < signals.Length; i++)
			{
				Timeseries signal = signals[i];
				var header = new PsgChannelHeaderControl(i);
				header.ChannelLabel = signal.Label;
				header.ReferenceLabel = signal.ReferenceLabel;
				header.ScaleButtonPressed += ChannelHeaders_ScaleButtonPressed;
				header.RemoveChannelButtonPressed += ChannelHeaders_RemoveChannelButtonPressed;
				header.BackColor = (i % 2 == 0) ? SystemColors.Control : SystemColors.ControlLight;

				ChannelHeaders.Add(header);
			}

			Controls.AddRange(ChannelHeaders.ToArray());

			_repositionHeaders();
		}

		public void RemoveChannel(int channelIndex)
		{
			ChannelHeaders[channelIndex].Dispose();
			ChannelHeaders.RemoveAt(channelIndex);

			for(int i = channelIndex; i < ChannelHeaders.Count; i++)
			{
				ChannelHeaders[i].ChannelViewIndex = i;
			}

			_repositionHeaders();
		}

		private void _repositionHeaders()
		{
			for(int i = 0; i < ChannelHeaders.Count; i++)
			{
				ChannelHeaders[i].Location = new Point(0, ChannelHeaders[i].Height * i - _scrollPosition);
			}
		}

		private void ChannelHeaders_ScaleButtonPressed(int channelIndex, float scaleFactor)
		{
			ScaleButtonPressed.Invoke(channelIndex, scaleFactor);
		}

		private void ChannelHeaders_RemoveChannelButtonPressed(int channelIndex)
		{
			RemoveChannelButtonPressed.Invoke(channelIndex);
		}
	}
}
