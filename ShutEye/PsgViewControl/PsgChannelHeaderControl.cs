﻿using System;
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
		public PsgChannelHeaderControl(string channelName)
		{
			InitializeComponent();
			labelChannelName.Text = channelName;
		}
	}
}
