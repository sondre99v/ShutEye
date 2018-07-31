﻿namespace ShutEye
{
    partial class PsgViewControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TimelineScrollBar = new System.Windows.Forms.HScrollBar();
            this.graphViewGLControl = new OpenTK.GLControl();
            this.SuspendLayout();
            // 
            // TimelineScrollBar
            // 
            this.TimelineScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TimelineScrollBar.Location = new System.Drawing.Point(0, 299);
            this.TimelineScrollBar.Name = "TimelineScrollBar";
            this.TimelineScrollBar.Size = new System.Drawing.Size(663, 17);
            this.TimelineScrollBar.TabIndex = 1;
            this.TimelineScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TimelineScrollBar_Scroll);
            this.TimelineScrollBar.ValueChanged += new System.EventHandler(this.TimelineScrollBar_ValueChanged);
            // 
            // graphViewGLControl
            // 
            this.graphViewGLControl.BackColor = System.Drawing.Color.Black;
            this.graphViewGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphViewGLControl.Location = new System.Drawing.Point(0, 0);
            this.graphViewGLControl.Name = "graphViewGLControl";
            this.graphViewGLControl.Size = new System.Drawing.Size(663, 299);
            this.graphViewGLControl.TabIndex = 3;
            this.graphViewGLControl.VSync = false;
            this.graphViewGLControl.Paint += new System.Windows.Forms.PaintEventHandler(this.graphViewGLControl_Paint);
            // 
            // PsgViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.graphViewGLControl);
            this.Controls.Add(this.TimelineScrollBar);
            this.Name = "PsgViewControl";
            this.Size = new System.Drawing.Size(663, 316);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.HScrollBar TimelineScrollBar;
        private OpenTK.GLControl graphViewGLControl;
    }
}
