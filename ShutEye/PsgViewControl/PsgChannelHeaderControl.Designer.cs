namespace ShutEye
{
	partial class PsgChannelHeaderControl
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
			this.labelChannelName = new System.Windows.Forms.Label();
			this.buttonIncreaseScale = new System.Windows.Forms.Button();
			this.buttonDecreaseScale = new System.Windows.Forms.Button();
			this.buttonRemoveChannel = new System.Windows.Forms.Button();
			this.labelReference = new System.Windows.Forms.Label();
			this.buttonColor = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelChannelName
			// 
			this.labelChannelName.AutoSize = true;
			this.labelChannelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelChannelName.Location = new System.Drawing.Point(3, 0);
			this.labelChannelName.Name = "labelChannelName";
			this.labelChannelName.Size = new System.Drawing.Size(21, 15);
			this.labelChannelName.TabIndex = 0;
			this.labelChannelName.Text = "A1";
			// 
			// buttonIncreaseScale
			// 
			this.buttonIncreaseScale.Font = new System.Drawing.Font("Lucida Console", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonIncreaseScale.Location = new System.Drawing.Point(35, 21);
			this.buttonIncreaseScale.Margin = new System.Windows.Forms.Padding(1);
			this.buttonIncreaseScale.Name = "buttonIncreaseScale";
			this.buttonIncreaseScale.Size = new System.Drawing.Size(17, 17);
			this.buttonIncreaseScale.TabIndex = 1;
			this.buttonIncreaseScale.Text = "+";
			this.buttonIncreaseScale.UseVisualStyleBackColor = true;
			this.buttonIncreaseScale.Click += new System.EventHandler(this.buttonIncreaseScale_Click);
			// 
			// buttonDecreaseScale
			// 
			this.buttonDecreaseScale.Font = new System.Drawing.Font("Lucida Console", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonDecreaseScale.Location = new System.Drawing.Point(35, 40);
			this.buttonDecreaseScale.Margin = new System.Windows.Forms.Padding(1);
			this.buttonDecreaseScale.Name = "buttonDecreaseScale";
			this.buttonDecreaseScale.Size = new System.Drawing.Size(17, 17);
			this.buttonDecreaseScale.TabIndex = 2;
			this.buttonDecreaseScale.Text = "−";
			this.buttonDecreaseScale.UseVisualStyleBackColor = true;
			this.buttonDecreaseScale.Click += new System.EventHandler(this.buttonDecreaseScale_Click);
			// 
			// buttonRemoveChannel
			// 
			this.buttonRemoveChannel.Font = new System.Drawing.Font("Lucida Console", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonRemoveChannel.Location = new System.Drawing.Point(1, 40);
			this.buttonRemoveChannel.Margin = new System.Windows.Forms.Padding(1);
			this.buttonRemoveChannel.Name = "buttonRemoveChannel";
			this.buttonRemoveChannel.Size = new System.Drawing.Size(17, 17);
			this.buttonRemoveChannel.TabIndex = 3;
			this.buttonRemoveChannel.Text = "×";
			this.buttonRemoveChannel.UseVisualStyleBackColor = true;
			this.buttonRemoveChannel.Click += new System.EventHandler(this.buttonRemoveChannel_Click);
			// 
			// labelReference
			// 
			this.labelReference.AutoSize = true;
			this.labelReference.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelReference.Location = new System.Drawing.Point(4, 15);
			this.labelReference.Name = "labelReference";
			this.labelReference.Size = new System.Drawing.Size(17, 12);
			this.labelReference.TabIndex = 4;
			this.labelReference.Text = "A1";
			// 
			// buttonColor
			// 
			this.buttonColor.BackColor = System.Drawing.Color.Red;
			this.buttonColor.Font = new System.Drawing.Font("Lucida Console", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonColor.Location = new System.Drawing.Point(18, 40);
			this.buttonColor.Margin = new System.Windows.Forms.Padding(1);
			this.buttonColor.Name = "buttonColor";
			this.buttonColor.Size = new System.Drawing.Size(17, 17);
			this.buttonColor.TabIndex = 5;
			this.buttonColor.UseVisualStyleBackColor = false;
			this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
			// 
			// PsgChannelHeaderControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.buttonColor);
			this.Controls.Add(this.labelReference);
			this.Controls.Add(this.buttonRemoveChannel);
			this.Controls.Add(this.buttonDecreaseScale);
			this.Controls.Add(this.buttonIncreaseScale);
			this.Controls.Add(this.labelChannelName);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "PsgChannelHeaderControl";
			this.Size = new System.Drawing.Size(52, 57);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelChannelName;
		private System.Windows.Forms.Button buttonIncreaseScale;
		private System.Windows.Forms.Button buttonDecreaseScale;
		private System.Windows.Forms.Button buttonRemoveChannel;
		private System.Windows.Forms.Label labelReference;
		private System.Windows.Forms.Button buttonColor;
	}
}
