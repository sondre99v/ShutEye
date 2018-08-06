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
			DialogResult result;
			OpenFileDialog ofd = new OpenFileDialog();

			result = ofd.ShowDialog();
			if(result != DialogResult.OK)
			{
				return;
			}


			EDFFile EdfFile = new EDFFile();

			EdfFile.readFile(ofd.FileName);

			var d = new ChannelSelectionForm(EdfFile.Header);
			result = d.ShowDialog();

			if(result != DialogResult.OK)
			{
				return;
			}

			psgViewControl1.SetEdfFile(EdfFile, d.ChannelConfigurations);
			psgViewControl1.Invalidate();
			psgViewControl1.Update();
		}

		private void loadSelectionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult result;
			OpenFileDialog ofd = new OpenFileDialog();

			result = ofd.ShowDialog();
			if(result != DialogResult.OK)
			{
				return;
			}

			psgViewControl1.OpenSelectionFile(ofd.FileName);
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
