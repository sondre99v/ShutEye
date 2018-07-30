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

namespace ShutEye
{
    public partial class PsgViewControl: UserControl
    {
        public int ChannelSeparation { get; set; } = 10;
        public double ChannelScale { get; set; } = 0.25;
        public double Zoom { get; set; } = 10;
        
        private EDFFile EdfFile;

        private double _timeOffset = 0;

        public PsgViewControl()
        {
            InitializeComponent();

            PsgGraphPanel.Paint += PsgGraphPanel_Paint;
        }

        public void SetEdfFile(EDFFile edfFile)
        {
            EdfFile = edfFile;
            
            TimelineScrollBar.Minimum = 0;
            TimelineScrollBar.Maximum = EdfFile.Header.NumberOfDataRecords * EdfFile.Header.DurationOfDataRecordInSeconds;

            PsgGraphPanel.Invalidate();
        }

        private void PsgGraphPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackColor);

            if(EdfFile == null) return;

            for(int signalIndex = 0; signalIndex < 12; signalIndex++)
            {
                string signalKey = EdfFile.Header.Signals[signalIndex].IndexNumberWithLabel;

                double sampleRate = EdfFile.Header.Signals[0].NumberOfSamplesPerDataRecord * EdfFile.Header.DurationOfDataRecordInSeconds;
                int initialIndex = (int) Math.Floor(_timeOffset * sampleRate);
                int windowLength = (int) Math.Ceiling(PsgGraphPanel.Width / Zoom);

                int xStart = 0;
                int yStart = 10 + signalIndex * ChannelSeparation + ChannelSeparation / 2;
                
                float prevSample = 0.0F;

                for(int i = 0; i < windowLength; i++)
                {
                    int recordIndex = (initialIndex + i) / EdfFile.Header.Signals[signalIndex].NumberOfSamplesPerDataRecord;
                    int indexInRecord = (initialIndex + i) % EdfFile.Header.Signals[signalIndex].NumberOfSamplesPerDataRecord;
                    
                    float sample = EdfFile.DataRecords[recordIndex][signalIndex + 1][indexInRecord];

                    if (i != initialIndex)
                    {
                        g.DrawLine(Pens.Black, 
                            (float)(xStart + (i - 1) * Zoom), 
                            (float)(yStart + prevSample * ChannelScale),
                            (float)(xStart + i * Zoom), 
                            (float)(yStart + sample * ChannelScale));
                    }

                    prevSample = sample;
                }
            }

        }

        private void TimelineScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            _timeOffset = TimelineScrollBar.Value;
            PsgGraphPanel.Invalidate();
        }
    }
}
