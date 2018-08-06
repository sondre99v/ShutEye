namespace ShutEye
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadSamplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.psgViewControl1 = new ShutEye.PsgViewControl();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageDefault = new System.Windows.Forms.TabPage();
			this.loadSelectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageDefault.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(866, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSamplesToolStripMenuItem,
            this.loadFileToolStripMenuItem,
            this.loadSelectionsToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// loadSamplesToolStripMenuItem
			// 
			this.loadSamplesToolStripMenuItem.Name = "loadSamplesToolStripMenuItem";
			this.loadSamplesToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.loadSamplesToolStripMenuItem.Text = "Load Samples";
			this.loadSamplesToolStripMenuItem.Click += new System.EventHandler(this.loadSamplesToolStripMenuItem_Click);
			// 
			// loadFileToolStripMenuItem
			// 
			this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
			this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.loadFileToolStripMenuItem.Text = "Load File";
			this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 421);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(866, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
			this.toolStripStatusLabel1.Text = "Ready";
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Right;
			this.propertyGrid1.Location = new System.Drawing.Point(699, 24);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(167, 397);
			this.propertyGrid1.TabIndex = 5;
			// 
			// psgViewControl1
			// 
			this.psgViewControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.psgViewControl1.ChannelScale = 1D;
			this.psgViewControl1.ChannelSeparation = 100;
			this.psgViewControl1.Cursor = System.Windows.Forms.Cursors.Default;
			this.psgViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.psgViewControl1.Location = new System.Drawing.Point(3, 3);
			this.psgViewControl1.Name = "psgViewControl1";
			this.psgViewControl1.Size = new System.Drawing.Size(685, 365);
			this.psgViewControl1.TabIndex = 0;
			this.psgViewControl1.Zoom = 10D;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageDefault);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 24);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(699, 397);
			this.tabControl1.TabIndex = 6;
			// 
			// tabPageDefault
			// 
			this.tabPageDefault.Controls.Add(this.psgViewControl1);
			this.tabPageDefault.Location = new System.Drawing.Point(4, 22);
			this.tabPageDefault.Name = "tabPageDefault";
			this.tabPageDefault.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDefault.Size = new System.Drawing.Size(691, 371);
			this.tabPageDefault.TabIndex = 0;
			this.tabPageDefault.Text = "Default";
			this.tabPageDefault.UseVisualStyleBackColor = true;
			// 
			// loadSelectionsToolStripMenuItem
			// 
			this.loadSelectionsToolStripMenuItem.Name = "loadSelectionsToolStripMenuItem";
			this.loadSelectionsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.loadSelectionsToolStripMenuItem.Text = "Load Selections";
			this.loadSelectionsToolStripMenuItem.Click += new System.EventHandler(this.loadSelectionsToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(866, 443);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.propertyGrid1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "ShutEye";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPageDefault.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private PsgViewControl psgViewControl1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadSamplesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageDefault;
		private System.Windows.Forms.ToolStripMenuItem loadSelectionsToolStripMenuItem;
	}
}

