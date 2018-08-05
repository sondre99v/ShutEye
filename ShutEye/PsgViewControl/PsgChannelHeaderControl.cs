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
		public int ChannelViewIndex { get; set; }

		public event Action<int, float> ScaleButtonPressed;
		public event Action<int> RemoveChannelButtonPressed;

		public string ChannelLabel
		{
			get => labelChannelName.Text;
			set => labelChannelName.Text = value;
		}

		public string ReferenceLabel
		{
			get => labelReference.Text;
			set => labelReference.Text = value ?? "mono";
		}

		public PsgChannelHeaderControl(int channelViewIndex)
		{
			ChannelViewIndex = channelViewIndex;
			InitializeComponent();
		}

		private void buttonIncreaseScale_Click(object sender, EventArgs e)
		{
			ScaleButtonPressed.Invoke(ChannelViewIndex, ModifierKeys == Keys.Shift ? 2.0F : 1.2F);
		}

		private void buttonDecreaseScale_Click(object sender, EventArgs e)
		{
			ScaleButtonPressed.Invoke(ChannelViewIndex, 1.0F / (ModifierKeys == Keys.Shift ? 2.0F : 1.2F));
		}

		private void buttonRemoveChannel_Click(object sender, EventArgs e)
		{
			RemoveChannelButtonPressed.Invoke(ChannelViewIndex);
		}
	}
}
