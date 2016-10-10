namespace SecuritySystem_CS
{
    partial class Form1
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
            if (disposing && (components != null))
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
            this.lblProximity = new System.Windows.Forms.Label();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnStopRecording = new System.Windows.Forms.Button();
            this.btnTakeSnapshot = new System.Windows.Forms.Button();
            this.btnStartRecording = new System.Windows.Forms.Button();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProximity
            // 
            this.lblProximity.AutoSize = true;
            this.lblProximity.Location = new System.Drawing.Point(309, 23);
            this.lblProximity.Name = "lblProximity";
            this.lblProximity.Size = new System.Drawing.Size(39, 13);
            this.lblProximity.TabIndex = 16;
            this.lblProximity.Text = "Label1";
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Location = new System.Drawing.Point(12, 13);
            this.ProgressBar1.Maximum = 160;
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(291, 23);
            this.ProgressBar1.Step = 1;
            this.ProgressBar1.TabIndex = 15;
            // 
            // btnStopRecording
            // 
            this.btnStopRecording.Location = new System.Drawing.Point(126, 301);
            this.btnStopRecording.Name = "btnStopRecording";
            this.btnStopRecording.Size = new System.Drawing.Size(108, 23);
            this.btnStopRecording.TabIndex = 14;
            this.btnStopRecording.Text = "Stop Recording";
            this.btnStopRecording.UseVisualStyleBackColor = true;
            this.btnStopRecording.Click += new System.EventHandler(this.btnStopRecording_Click);
            // 
            // btnTakeSnapshot
            // 
            this.btnTakeSnapshot.Location = new System.Drawing.Point(240, 301);
            this.btnTakeSnapshot.Name = "btnTakeSnapshot";
            this.btnTakeSnapshot.Size = new System.Drawing.Size(108, 23);
            this.btnTakeSnapshot.TabIndex = 13;
            this.btnTakeSnapshot.Text = "Take Snapshot";
            this.btnTakeSnapshot.UseVisualStyleBackColor = true;
            this.btnTakeSnapshot.Click += new System.EventHandler(this.btnTakeSnapshot_Click);
            // 
            // btnStartRecording
            // 
            this.btnStartRecording.Location = new System.Drawing.Point(12, 301);
            this.btnStartRecording.Name = "btnStartRecording";
            this.btnStartRecording.Size = new System.Drawing.Size(108, 23);
            this.btnStartRecording.TabIndex = 12;
            this.btnStartRecording.Text = "Start Recording";
            this.btnStartRecording.UseVisualStyleBackColor = true;
            this.btnStartRecording.Click += new System.EventHandler(this.btnStartRecording_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Location = new System.Drawing.Point(12, 42);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(336, 253);
            this.PictureBox1.TabIndex = 11;
            this.PictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 336);
            this.Controls.Add(this.lblProximity);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.btnStopRecording);
            this.Controls.Add(this.btnTakeSnapshot);
            this.Controls.Add(this.btnStartRecording);
            this.Controls.Add(this.PictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblProximity;
        internal System.Windows.Forms.ProgressBar ProgressBar1;
        internal System.Windows.Forms.Button btnStopRecording;
        internal System.Windows.Forms.Button btnTakeSnapshot;
        internal System.Windows.Forms.Button btnStartRecording;
        internal System.Windows.Forms.PictureBox PictureBox1;
    }
}

