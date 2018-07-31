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
            this.button1 = new System.Windows.Forms.Button();
            this.psgViewControl1 = new ShutEye.PsgViewControl();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // psgViewControl1
            // 
            this.psgViewControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.psgViewControl1.ChannelScale = 1D;
            this.psgViewControl1.ChannelSeparation = 100;
            this.psgViewControl1.Location = new System.Drawing.Point(178, 25);
            this.psgViewControl1.Name = "psgViewControl1";
            this.psgViewControl1.Size = new System.Drawing.Size(575, 324);
            this.psgViewControl1.TabIndex = 0;
            this.psgViewControl1.Zoom = 10D;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 409);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.psgViewControl1);
            this.Name = "MainForm";
            this.Text = "ShutEye";
            this.ResumeLayout(false);

        }

        #endregion

        private PsgViewControl psgViewControl1;
        private System.Windows.Forms.Button button1;
    }
}

