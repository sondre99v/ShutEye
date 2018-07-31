using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDF;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ShutEye
{
    public partial class PsgViewControl: UserControl
    {
        public int ChannelSeparation { get; set; } = 10;
        public double ChannelScale { get; set; } = 0.25;
        public double Zoom { get; set; } = 10;

        private Polysomnogram PsgData;

        private double _timeOffset = 0;

        public PsgViewControl()
        {
            PsgData = new Polysomnogram();

            InitializeComponent();

            DoubleBuffered = true;
        }

        public void SetEdfFile(EDFFile edfFile)
        {
            PsgData.LoadFromEdfFile(edfFile);
            TimelineScrollBar.Minimum = 0;
            TimelineScrollBar.Maximum = (int) PsgData.Duration;
        }

        private void TimelineScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            _timeOffset = TimelineScrollBar.Value;
            graphViewGLControl.Invalidate();
        }

        private void TimelineScrollBar_ValueChanged(object sender, EventArgs e)
        {
            _timeOffset = TimelineScrollBar.Value;
        }

        private void graphViewGLControl_Paint(object sender, PaintEventArgs e)
        {
            if(DesignMode)
            {
                e.Graphics.Clear(Color.Black);
                return;
            }

            graphViewGLControl.MakeCurrent();

            float x = (float) TimelineScrollBar.Value / (TimelineScrollBar.Maximum - TimelineScrollBar.LargeChange + 1);

            x = (x - 0.5F) * 2.0F * 0.83F;

            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Begin(BeginMode.Quads);
            GL.Color3(Color.DarkGreen);
            GL.Vertex2(0.9, 0.9);
            GL.Vertex2(-0.9, 0.9);
            GL.Vertex2(-0.9, -0.9);
            GL.Vertex2(0.9, -0.9);

            GL.Color3(Color.Beige);
            GL.Vertex2(x + 0.1, 1.0);
            GL.Vertex2(x - 0.1, 1.0);
            GL.Vertex2(x - 0.1, -1.0);
            GL.Vertex2(x + 0.1, -1.0);
            GL.End();

            graphViewGLControl.SwapBuffers();
        }
    }
}
