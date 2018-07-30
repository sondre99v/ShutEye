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
        EDFFile EdfFile;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EdfFile = new EDFFile();

            EdfFile.readFile("../../../ExampleData/36.rec");

            MessageBox.Show("Done!");

            psgViewControl1.SetEdfFile(EdfFile);
            psgViewControl1.Invalidate();
            psgViewControl1.Update();
        }
    }
}
