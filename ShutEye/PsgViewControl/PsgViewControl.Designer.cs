namespace ShutEye
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
			this.graphViewControl = new ShutEye.GLGraphView();
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
			// 
			// graphViewControl
			// 
			this.graphViewControl.BackColor = System.Drawing.Color.Black;
			this.graphViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graphViewControl.Location = new System.Drawing.Point(0, 0);
			this.graphViewControl.Name = "graphViewControl";
			this.graphViewControl.Size = new System.Drawing.Size(663, 299);
			this.graphViewControl.TabIndex = 3;
			this.graphViewControl.TimeOffset = 0F;
			this.graphViewControl.VSync = false;
			// 
			// PsgViewControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.graphViewControl);
			this.Controls.Add(this.TimelineScrollBar);
			this.Name = "PsgViewControl";
			this.Size = new System.Drawing.Size(663, 316);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.HScrollBar TimelineScrollBar;
		private GLGraphView graphViewControl;
	}
}
