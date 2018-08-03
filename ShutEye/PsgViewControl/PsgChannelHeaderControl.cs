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
	public partial class PsgChannelHeaderControl: UserControl
	{
		public int ChannelIndex { get; private set; }
		public event Action<int, float> ScaleButtonPressed;

		public string ChannelLabel
		{
			get => labelChannelName.Text;
			set => labelChannelName.Text = value;
		}

		public PsgChannelHeaderControl(int channelIndex)
		{
			ChannelIndex = channelIndex;
			InitializeComponent();
		}

		private void buttonIncreaseScale_Click(object sender, EventArgs e)
		{
			ScaleButtonPressed.Invoke(ChannelIndex, ModifierKeys == Keys.Shift ? 2.0F : 1.2F);
		}

		private void buttonDecreaseScale_Click(object sender, EventArgs e)
		{
			ScaleButtonPressed.Invoke(ChannelIndex, 1.0F / (ModifierKeys == Keys.Shift ? 2.0F : 1.2F));
		}
	}
}
