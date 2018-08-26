using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using DSPLib;

namespace ShutEye
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			FFT fft = new FFT();
			fft.Initialize(4096);

			double Fs = 1000;
			double[] d = new double[4096];
			for (int i = 0; i < 4096; i++)
			{
				double t = i / Fs;

				d[i] = Math.Sin(Math.PI * 2.0 * t * 200.0) + 0.2 * Math.Sin(Math.PI * 2.0 * t * 90.0);
			}

			var c = fft.Execute(d);
			


			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
