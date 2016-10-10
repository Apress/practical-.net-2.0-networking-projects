namespace Attendance_CS
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
            this.chkEnableReader = new System.Windows.Forms.CheckBox();
            this.chkTurnOnLED = new System.Windows.Forms.CheckBox();
            this.txtTagID = new System.Windows.Forms.TextBox();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label1 = new System.Windows.Forms.Label();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkEnableReader
            // 
            this.chkEnableReader.AutoSize = true;
            this.chkEnableReader.Location = new System.Drawing.Point(106, 32);
            this.chkEnableReader.Name = "chkEnableReader";
            this.chkEnableReader.Size = new System.Drawing.Size(97, 17);
            this.chkEnableReader.TabIndex = 9;
            this.chkEnableReader.Text = "Enable Reader";
            this.chkEnableReader.UseVisualStyleBackColor = true;
            this.chkEnableReader.CheckedChanged += new System.EventHandler(this.chkEnableReader_CheckedChanged);
            // 
            // chkTurnOnLED
            // 
            this.chkTurnOnLED.AutoSize = true;
            this.chkTurnOnLED.Location = new System.Drawing.Point(15, 32);
            this.chkTurnOnLED.Name = "chkTurnOnLED";
            this.chkTurnOnLED.Size = new System.Drawing.Size(87, 17);
            this.chkTurnOnLED.TabIndex = 8;
            this.chkTurnOnLED.Text = "Turn on LED";
            this.chkTurnOnLED.UseVisualStyleBackColor = true;
            this.chkTurnOnLED.CheckedChanged += new System.EventHandler(this.chkTurnOnLED_CheckedChanged);
            // 
            // txtTagID
            // 
            this.txtTagID.Location = new System.Drawing.Point(57, 6);
            this.txtTagID.Name = "txtTagID";
            this.txtTagID.Size = new System.Drawing.Size(146, 20);
            this.txtTagID.TabIndex = 7;
            // 
            // ToolStripStatusLabel1
            // 
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Size = new System.Drawing.Size(111, 17);
            this.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(40, 13);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Tag ID";
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel1});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 72);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(219, 22);
            this.StatusStrip1.TabIndex = 5;
            this.StatusStrip1.Text = "StatusStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 94);
            this.Controls.Add(this.chkEnableReader);
            this.Controls.Add(this.chkTurnOnLED);
            this.Controls.Add(this.txtTagID);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.StatusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox chkEnableReader;
        internal System.Windows.Forms.CheckBox chkTurnOnLED;
        internal System.Windows.Forms.TextBox txtTagID;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.StatusStrip StatusStrip1;
    }
}

