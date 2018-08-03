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

			psgViewControl1.SetEdfFile(EdfFile);
			psgViewControl1.Invalidate();
			psgViewControl1.Update();
		}
	}
}
