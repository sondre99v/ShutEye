namespace ShutEye
{
	partial class ChannelSelectionForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelSelectionForm));
			this.dgvChannels = new System.Windows.Forms.DataGridView();
			this.IndexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LabelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ShowColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ReferenceColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgvChannels)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvChannels
			// 
			this.dgvChannels.AllowUserToAddRows = false;
			this.dgvChannels.AllowUserToDeleteRows = false;
			this.dgvChannels.AllowUserToResizeRows = false;
			this.dgvChannels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgvChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvChannels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndexColumn,
            this.LabelColumn,
            this.ShowColumn,
            this.ReferenceColumn});
			this.dgvChannels.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dgvChannels.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dgvChannels.Location = new System.Drawing.Point(12, 12);
			this.dgvChannels.Name = "dgvChannels";
			this.dgvChannels.RowHeadersVisible = false;
			this.dgvChannels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvChannels.Size = new System.Drawing.Size(359, 270);
			this.dgvChannels.TabIndex = 2;
			// 
			// IndexColumn
			// 
			this.IndexColumn.HeaderText = "Index";
			this.IndexColumn.Name = "IndexColumn";
			this.IndexColumn.ReadOnly = true;
			this.IndexColumn.Width = 50;
			// 
			// LabelColumn
			// 
			this.LabelColumn.HeaderText = "Label";
			this.LabelColumn.Name = "LabelColumn";
			// 
			// ShowColumn
			// 
			this.ShowColumn.HeaderText = "Show";
			this.ShowColumn.Name = "ShowColumn";
			this.ShowColumn.Width = 50;
			// 
			// ReferenceColumn
			// 
			this.ReferenceColumn.HeaderText = "Reference";
			this.ReferenceColumn.Name = "ReferenceColumn";
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOk.Location = new System.Drawing.Point(215, 288);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 3;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(296, 288);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// ChannelSelectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(383, 323);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.dgvChannels);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ChannelSelectionForm";
			this.Text = "Channels";
			this.Load += new System.EventHandler(this.ChannelSelectionForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvChannels)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvChannels;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.DataGridViewTextBoxColumn IndexColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn LabelColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ShowColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn ReferenceColumn;
	}
}