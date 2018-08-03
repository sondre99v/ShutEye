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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.ChannelScrollBar = new System.Windows.Forms.VScrollBar();
			this.graphViewControl = new ShutEye.GLGraphView();
			this.psgViewChannelHeadersControl1 = new ShutEye.PsgViewChannelHeadersControl();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TimelineScrollBar
			// 
			this.TimelineScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TimelineScrollBar.Location = new System.Drawing.Point(0, 299);
			this.TimelineScrollBar.Name = "TimelineScrollBar";
			this.TimelineScrollBar.Size = new System.Drawing.Size(594, 17);
			this.TimelineScrollBar.TabIndex = 1;
			this.TimelineScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TimelineScrollBar_Scroll);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
			this.tableLayoutPanel1.Controls.Add(this.TimelineScrollBar, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.ChannelScrollBar, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.graphViewControl, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(52, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(611, 316);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// ChannelScrollBar
			// 
			this.ChannelScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChannelScrollBar.Location = new System.Drawing.Point(594, 0);
			this.ChannelScrollBar.Name = "ChannelScrollBar";
			this.ChannelScrollBar.Size = new System.Drawing.Size(17, 299);
			this.ChannelScrollBar.TabIndex = 4;
			this.ChannelScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ChannelScrollBar_Scroll);
			// 
			// graphViewControl
			// 
			this.graphViewControl.BackColor = System.Drawing.Color.Black;
			this.graphViewControl.ChannelHeight = 57;
			this.graphViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graphViewControl.Location = new System.Drawing.Point(3, 3);
			this.graphViewControl.Name = "graphViewControl";
			this.graphViewControl.OffsetY = 0;
			this.graphViewControl.ScaleX = 100F;
			this.graphViewControl.Size = new System.Drawing.Size(588, 293);
			this.graphViewControl.TabIndex = 3;
			this.graphViewControl.TimeOffset = 0F;
			this.graphViewControl.VSync = false;
			// 
			// psgViewChannelHeadersControl1
			// 
			this.psgViewChannelHeadersControl1.Dock = System.Windows.Forms.DockStyle.Left;
			this.psgViewChannelHeadersControl1.Location = new System.Drawing.Point(0, 0);
			this.psgViewChannelHeadersControl1.Name = "psgViewChannelHeadersControl1";
			this.psgViewChannelHeadersControl1.Size = new System.Drawing.Size(52, 316);
			this.psgViewChannelHeadersControl1.TabIndex = 4;
			// 
			// PsgViewControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.psgViewChannelHeadersControl1);
			this.Name = "PsgViewControl";
			this.Size = new System.Drawing.Size(663, 316);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.HScrollBar TimelineScrollBar;
		private GLGraphView graphViewControl;
		private PsgViewChannelHeadersControl psgViewChannelHeadersControl1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.VScrollBar ChannelScrollBar;
	}
}
