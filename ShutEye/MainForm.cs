using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDF;

namespace ShutEye
{
	public partial class MainForm: Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void loadSamplesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			psgViewControl1.LoadRandomData();
			psgViewControl1.Invalidate();
		}

		private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EDFFile EdfFile = new EDFFile();

			EdfFile.readFile("../../../ExampleData/36.rec");

			var d = new ChannelSelectionForm(EdfFile.Header);
			DialogResult result = d.ShowDialog();

			if(result == DialogResult.OK)
			{
				psgViewControl1.SetEdfFile(EdfFile, d.ChannelConfigurations);
				psgViewControl1.Invalidate();
				psgViewControl1.Update();
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch(keyData)
			{
				case Keys.Right:
					psgViewControl1.SkipForward();
					return true;
				case Keys.Left:
					psgViewControl1.SkipBackward();
					return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
