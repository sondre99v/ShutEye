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
			this.hypnogramControl = new ShutEye.HypnogramControl();
			this.ChannelHeadersPanel = new ShutEye.PsgViewChannelHeadersControl();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TimelineScrollBar
			// 
			this.TimelineScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TimelineScrollBar.Location = new System.Drawing.Point(52, 569);
			this.TimelineScrollBar.Name = "TimelineScrollBar";
			this.TimelineScrollBar.Size = new System.Drawing.Size(979, 17);
			this.TimelineScrollBar.TabIndex = 1;
			this.TimelineScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TimelineScrollBar_Scroll);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
			this.tableLayoutPanel1.Controls.Add(this.ChannelScrollBar, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.ChannelHeadersPanel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.graphViewControl, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.TimelineScrollBar, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.hypnogramControl, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1048, 586);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// ChannelScrollBar
			// 
			this.ChannelScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChannelScrollBar.Location = new System.Drawing.Point(1031, 0);
			this.ChannelScrollBar.Name = "ChannelScrollBar";
			this.ChannelScrollBar.Size = new System.Drawing.Size(17, 519);
			this.ChannelScrollBar.TabIndex = 4;
			this.ChannelScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ChannelScrollBar_Scroll);
			// 
			// graphViewControl
			// 
			this.graphViewControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.graphViewControl.ChannelHeight = 57;
			this.graphViewControl.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.graphViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.graphViewControl.ForeColor = System.Drawing.Color.White;
			this.graphViewControl.Location = new System.Drawing.Point(52, 0);
			this.graphViewControl.Margin = new System.Windows.Forms.Padding(0);
			this.graphViewControl.Name = "graphViewControl";
			this.graphViewControl.OffsetY = 0;
			this.graphViewControl.ScaleX = 100F;
			this.graphViewControl.Size = new System.Drawing.Size(979, 519);
			this.graphViewControl.TabIndex = 3;
			this.graphViewControl.TimeOffset = 0F;
			this.graphViewControl.VSync = false;
			// 
			// hypnogramControl
			// 
			this.hypnogramControl.BackColor = System.Drawing.Color.Gray;
			this.hypnogramControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hypnogramControl.Location = new System.Drawing.Point(52, 519);
			this.hypnogramControl.Margin = new System.Windows.Forms.Padding(0);
			this.hypnogramControl.Name = "hypnogramControl";
			this.hypnogramControl.Size = new System.Drawing.Size(979, 50);
			this.hypnogramControl.TabIndex = 5;
			this.hypnogramControl.Text = "hypnogramControl1";
			// 
			// ChannelHeadersPanel
			// 
			this.ChannelHeadersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChannelHeadersPanel.Location = new System.Drawing.Point(0, 0);
			this.ChannelHeadersPanel.Margin = new System.Windows.Forms.Padding(0);
			this.ChannelHeadersPanel.Name = "ChannelHeadersPanel";
			this.ChannelHeadersPanel.ScrollPosition = 0;
			this.ChannelHeadersPanel.Size = new System.Drawing.Size(52, 519);
			this.ChannelHeadersPanel.TabIndex = 4;
			// 
			// PsgViewControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PsgViewControl";
			this.Size = new System.Drawing.Size(1048, 586);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.HScrollBar TimelineScrollBar;
		private GLGraphView graphViewControl;
		private PsgViewChannelHeadersControl ChannelHeadersPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.VScrollBar ChannelScrollBar;
		private HypnogramControl hypnogramControl;
	}
}
