namespace WinClient_CS
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
            this.Label1 = new System.Windows.Forms.Label();
            this.txtNick = new System.Windows.Forms.TextBox();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtMessageHistory = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 11);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(29, 13);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "Nick";
            // 
            // txtNick
            // 
            this.txtNick.Location = new System.Drawing.Point(44, 8);
            this.txtNick.Name = "txtNick";
            this.txtNick.Size = new System.Drawing.Size(144, 20);
            this.txtNick.TabIndex = 10;
            // 
            // btnSignIn
            // 
            this.btnSignIn.Location = new System.Drawing.Point(194, 6);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(75, 23);
            this.btnSignIn.TabIndex = 9;
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(194, 325);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(2, 327);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(186, 20);
            this.txtMessage.TabIndex = 7;
            // 
            // txtMessageHistory
            // 
            this.txtMessageHistory.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtMessageHistory.Location = new System.Drawing.Point(2, 35);
            this.txtMessageHistory.Multiline = true;
            this.txtMessageHistory.Name = "txtMessageHistory";
            this.txtMessageHistory.ReadOnly = true;
            this.txtMessageHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessageHistory.Size = new System.Drawing.Size(268, 285);
            this.txtMessageHistory.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 355);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtNick);
            this.Controls.Add(this.btnSignIn);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtMessageHistory);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtNick;
        internal System.Windows.Forms.Button btnSignIn;
        internal System.Windows.Forms.Button btnSend;
        internal System.Windows.Forms.TextBox txtMessage;
        internal System.Windows.Forms.TextBox txtMessageHistory;
    }
}

