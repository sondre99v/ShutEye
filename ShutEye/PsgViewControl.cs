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

        private GLControl graphView;

        public PsgViewControl()
        {
            PsgData = new Polysomnogram();

            graphView = new GLControl();
            graphView.Location = new Point(0, 0);
            graphView.Dock = DockStyle.Fill;
            graphView.Paint += GraphView_Paint;

            Controls.Add(graphView);

            InitializeComponent();

            DoubleBuffered = true;
        }

        private void GraphView_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode) return;

            graphView.MakeCurrent();
            
            float x = (float)TimelineScrollBar.Value / TimelineScrollBar.Maximum;

            x = (x - 0.5F) * 2.0F * 0.9F;

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

            graphView.SwapBuffers();
        }

        public void SetEdfFile(EDFFile edfFile)
        {
            PsgData.LoadFromEdfFile(edfFile);
            TimelineScrollBar.Minimum = 0;
            TimelineScrollBar.Maximum = (int) PsgData.Duration;
        }

        private void PsgGraphPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackColor);

            Random rng = new Random();

            if(PsgData == null) return;

            for(int signalIndex = 0; signalIndex < 12; signalIndex++)
            {
                double sampleRate = PsgData.SampleRate;

                int initialIndex = (int) Math.Floor(_timeOffset * sampleRate);
                int windowLength = (int) Math.Ceiling(graphView.Width / Zoom);

                int xStart = 0;
                int yStart = 10 + signalIndex * ChannelSeparation + ChannelSeparation / 2;

                float prevSample = 0.0F;

                for(int i = 0; i < windowLength; i++)
                {
                    float sample = PsgData.Channels[signalIndex].Data[initialIndex + i];

                    if(i != initialIndex)
                    {
                        g.DrawLine(Pens.Black,
                            (float) (xStart + (i - 1) * Zoom),
                            (float) (yStart + prevSample * ChannelScale),
                            (float) (xStart + i * Zoom),
                            (float) (yStart + sample * ChannelScale));
                    }

                    prevSample = sample;
                }
            }

        }

        private void TimelineScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            _timeOffset = TimelineScrollBar.Value;
            graphView.Invalidate();
        }

        private void TimelineScrollBar_ValueChanged(object sender, EventArgs e)
        {
            _timeOffset = TimelineScrollBar.Value;
        }
    }
}
