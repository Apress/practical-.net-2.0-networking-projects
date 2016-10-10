namespace SerialCommChat_CS
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
            this.Label2 = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.btnDialNumber = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAnswerCall = new System.Windows.Forms.Button();
            this.txtDataReceived = new System.Windows.Forms.RichTextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtDataToSend = new System.Windows.Forms.TextBox();
            this.cbbCOMPorts = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(6, 22);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(78, 13);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Phone Number";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(85, 19);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(99, 20);
            this.txtPhoneNumber.TabIndex = 9;
            // 
            // btnDialNumber
            // 
            this.btnDialNumber.Location = new System.Drawing.Point(190, 17);
            this.btnDialNumber.Name = "btnDialNumber";
            this.btnDialNumber.Size = new System.Drawing.Size(75, 23);
            this.btnDialNumber.TabIndex = 11;
            this.btnDialNumber.Text = "Dial Number";
            this.btnDialNumber.UseVisualStyleBackColor = true;
            this.btnDialNumber.Click += new System.EventHandler(this.btnDialNumber_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.btnAnswerCall);
            this.GroupBox1.Controls.Add(this.txtPhoneNumber);
            this.GroupBox1.Controls.Add(this.btnDialNumber);
            this.GroupBox1.Location = new System.Drawing.Point(12, 290);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(352, 50);
            this.GroupBox1.TabIndex = 22;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Bluetooth Handset";
            // 
            // btnAnswerCall
            // 
            this.btnAnswerCall.Location = new System.Drawing.Point(271, 17);
            this.btnAnswerCall.Name = "btnAnswerCall";
            this.btnAnswerCall.Size = new System.Drawing.Size(75, 23);
            this.btnAnswerCall.TabIndex = 12;
            this.btnAnswerCall.Text = "Answer Call";
            this.btnAnswerCall.UseVisualStyleBackColor = true;
            this.btnAnswerCall.Click += new System.EventHandler(this.btnAnswerCall_Click);
            // 
            // txtDataReceived
            // 
            this.txtDataReceived.Location = new System.Drawing.Point(12, 63);
            this.txtDataReceived.Name = "txtDataReceived";
            this.txtDataReceived.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtDataReceived.Size = new System.Drawing.Size(352, 168);
            this.txtDataReceived.TabIndex = 21;
            this.txtDataReceived.Text = "";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(291, 5);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 20;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(210, 5);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 19;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessage.Location = new System.Drawing.Point(12, 37);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(352, 23);
            this.lblMessage.TabIndex = 18;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(289, 261);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 17;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtDataToSend
            // 
            this.txtDataToSend.Location = new System.Drawing.Point(12, 237);
            this.txtDataToSend.Multiline = true;
            this.txtDataToSend.Name = "txtDataToSend";
            this.txtDataToSend.Size = new System.Drawing.Size(273, 47);
            this.txtDataToSend.TabIndex = 16;
            // 
            // cbbCOMPorts
            // 
            this.cbbCOMPorts.FormattingEnabled = true;
            this.cbbCOMPorts.Location = new System.Drawing.Point(122, 7);
            this.cbbCOMPorts.Name = "cbbCOMPorts";
            this.cbbCOMPorts.Size = new System.Drawing.Size(80, 21);
            this.cbbCOMPorts.TabIndex = 15;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(104, 13);
            this.Label1.TabIndex = 14;
            this.Label1.Text = "Available COM Ports";
            // 
            // Form1
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(378, 349);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.txtDataReceived);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtDataToSend);
            this.Controls.Add(this.cbbCOMPorts);
            this.Controls.Add(this.Label1);
            this.Name = "Form1";
            this.Text = "Serial Chat";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtPhoneNumber;
        internal System.Windows.Forms.Button btnDialNumber;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button btnAnswerCall;
        internal System.Windows.Forms.RichTextBox txtDataReceived;
        internal System.Windows.Forms.Button btnDisconnect;
        internal System.Windows.Forms.Button btnConnect;
        internal System.Windows.Forms.Label lblMessage;
        internal System.Windows.Forms.Button btnSend;
        internal System.Windows.Forms.TextBox txtDataToSend;
        internal System.Windows.Forms.ComboBox cbbCOMPorts;
        internal System.Windows.Forms.Label Label1;
    }
}

