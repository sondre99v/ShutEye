using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShutEye
{
	public partial class HypnogramControl: Control
	{
		private Hypnogram _hypnogram;
		private double _markerPosition;

		public HypnogramControl()
		{
			Margin = new Padding(0);
			DoubleBuffered = true;
			InitializeComponent();
		}

		public void SetHypnogram(Hypnogram hypnogram)
		{
			_hypnogram = hypnogram;
			Invalidate();
		}

		public void SetMarkerPosition(double position)
		{
			_markerPosition = position;
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			pe.Graphics.Clear(BackColor);

			if(_hypnogram == null)
			{
				return;
			}

			int px = 0;
			int py = 0;

			for(int i = 0; i < _hypnogram.SleepStages.Length; i++)
			{
				int x = Width * i / _hypnogram.SleepStages.Length;
				int y = _hypnogram.SleepStages[i].GetValue() * 8 + 2;

				if(i != 0)
				{
					pe.Graphics.DrawLine(Pens.Black, px, py, px, y);
					pe.Graphics.DrawLine(Pens.Black, px, y, x, y);
				}

				px = x;
				py = y;
			}

			int markerX = (int) ((Width - 1) * _markerPosition / _hypnogram.Duration);

			pe.Graphics.DrawLine(Pens.Red, markerX, 0, markerX, Height);

			base.OnPaint(pe);
		}
	}
}
