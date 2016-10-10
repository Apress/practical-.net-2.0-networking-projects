namespace VirtualEarth_CS
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
            this.components = new System.ComponentModel.Container();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtDataReceived = new System.Windows.Forms.TextBox();
            this.cbbCOMPorts = new System.Windows.Forms.ComboBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.btnShowPath = new System.Windows.Forms.Button();
            this.btnClearPath = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnGotoPoint = new System.Windows.Forms.Button();
            this.WebBrowser1 = new System.Windows.Forms.WebBrowser();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Label3 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(472, 351);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(240, 21);
            this.lblMessage.TabIndex = 20;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(164, 14);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(16, 22);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(45, 13);
            this.Label2.TabIndex = 9;
            this.Label2.Text = "Latitude";
            // 
            // txtDataReceived
            // 
            this.txtDataReceived.Location = new System.Drawing.Point(6, 43);
            this.txtDataReceived.Multiline = true;
            this.txtDataReceived.Name = "txtDataReceived";
            this.txtDataReceived.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDataReceived.Size = new System.Drawing.Size(233, 107);
            this.txtDataReceived.TabIndex = 14;
            this.txtDataReceived.WordWrap = false;
            // 
            // cbbCOMPorts
            // 
            this.cbbCOMPorts.FormattingEnabled = true;
            this.cbbCOMPorts.Location = new System.Drawing.Point(78, 16);
            this.cbbCOMPorts.Name = "cbbCOMPorts";
            this.cbbCOMPorts.Size = new System.Drawing.Size(80, 21);
            this.cbbCOMPorts.TabIndex = 12;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.btnShowPath);
            this.GroupBox3.Controls.Add(this.btnClearPath);
            this.GroupBox3.Location = new System.Drawing.Point(467, 275);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(245, 73);
            this.GroupBox3.TabIndex = 19;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Recorded Path";
            // 
            // btnShowPath
            // 
            this.btnShowPath.Location = new System.Drawing.Point(6, 19);
            this.btnShowPath.Name = "btnShowPath";
            this.btnShowPath.Size = new System.Drawing.Size(233, 23);
            this.btnShowPath.TabIndex = 2;
            this.btnShowPath.Text = "Show Path";
            this.btnShowPath.UseVisualStyleBackColor = true;
            this.btnShowPath.Click += new System.EventHandler(this.btnShowPath_Click);
            // 
            // btnClearPath
            // 
            this.btnClearPath.Location = new System.Drawing.Point(6, 42);
            this.btnClearPath.Name = "btnClearPath";
            this.btnClearPath.Size = new System.Drawing.Size(233, 23);
            this.btnClearPath.TabIndex = 1;
            this.btnClearPath.Text = "Clear Path";
            this.btnClearPath.UseVisualStyleBackColor = true;
            this.btnClearPath.Click += new System.EventHandler(this.btnClearPath_Click);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.txtDataReceived);
            this.GroupBox2.Controls.Add(this.btnConnect);
            this.GroupBox2.Controls.Add(this.cbbCOMPorts);
            this.GroupBox2.Controls.Add(this.Label1);
            this.GroupBox2.Location = new System.Drawing.Point(467, 113);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(245, 156);
            this.GroupBox2.TabIndex = 18;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "GPS";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(21, 19);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(51, 13);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "GPS Port";
            // 
            // btnGotoPoint
            // 
            this.btnGotoPoint.Location = new System.Drawing.Point(141, 71);
            this.btnGotoPoint.Name = "btnGotoPoint";
            this.btnGotoPoint.Size = new System.Drawing.Size(98, 23);
            this.btnGotoPoint.TabIndex = 13;
            this.btnGotoPoint.Text = "Goto Point";
            this.btnGotoPoint.UseVisualStyleBackColor = true;
            this.btnGotoPoint.Click += new System.EventHandler(this.btnGotoPoint_Click);
            // 
            // WebBrowser1
            // 
            this.WebBrowser1.Location = new System.Drawing.Point(2, 3);
            this.WebBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowser1.Name = "WebBrowser1";
            this.WebBrowser1.ScrollBarsEnabled = false;
            this.WebBrowser1.Size = new System.Drawing.Size(460, 380);
            this.WebBrowser1.TabIndex = 16;
            // 
            // txtLongitude
            // 
            this.txtLongitude.Location = new System.Drawing.Point(67, 45);
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(172, 20);
            this.txtLongitude.TabIndex = 12;
            // 
            // txtLatitude
            // 
            this.txtLatitude.Location = new System.Drawing.Point(67, 19);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(172, 20);
            this.txtLatitude.TabIndex = 11;
            // 
            // Timer1
            // 
            this.Timer1.Interval = 500;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(7, 48);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(54, 13);
            this.Label3.TabIndex = 10;
            this.Label3.Text = "Longitude";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.btnGotoPoint);
            this.GroupBox1.Controls.Add(this.txtLongitude);
            this.GroupBox1.Controls.Add(this.txtLatitude);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Location = new System.Drawing.Point(467, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(245, 104);
            this.GroupBox1.TabIndex = 17;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Current Location:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(715, 386);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.WebBrowser1);
            this.Controls.Add(this.GroupBox1);
            this.Name = "Form1";
            this.Text = "Microsoft Virtual Earth";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblMessage;
        internal System.Windows.Forms.Button btnConnect;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtDataReceived;
        internal System.Windows.Forms.ComboBox cbbCOMPorts;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Button btnShowPath;
        internal System.Windows.Forms.Button btnClearPath;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnGotoPoint;
        internal System.Windows.Forms.WebBrowser WebBrowser1;
        internal System.Windows.Forms.TextBox txtLongitude;
        internal System.Windows.Forms.TextBox txtLatitude;
        internal System.Windows.Forms.Timer Timer1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.GroupBox GroupBox1;

    }
}

