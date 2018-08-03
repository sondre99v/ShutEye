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
		public PsgViewChannelHeadersControl()
		{
			InitializeComponent();
		}

		public void LoadHeaders(EDF.EDFHeader edfHeader)
		{
			flowLayoutPanel1.Controls.Clear();

			foreach(var signal in edfHeader.Signals)
			{
				string label = signal.Label.Trim();
				flowLayoutPanel1.Controls.Add(new PsgChannelHeaderControl(label));
			}
		}
	}
}
