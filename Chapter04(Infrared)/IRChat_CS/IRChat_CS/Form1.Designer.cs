namespace IRChat_CS
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
            this.mnuSend = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.StatusBar1 = new System.Windows.Forms.StatusBar();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtMessagesArchive = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mnuSend
            // 
            this.mnuSend.Text = "Send";
            this.mnuSend.Click += new System.EventHandler(this.mnuSend_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuSend);
            // 
            // StatusBar1
            // 
            this.StatusBar1.Location = new System.Drawing.Point(0, 246);
            this.StatusBar1.Name = "StatusBar1";
            this.StatusBar1.Size = new System.Drawing.Size(240, 22);
            this.StatusBar1.Text = "StatusBar1";
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(3, 2);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(234, 21);
            this.txtMessage.TabIndex = 6;
            // 
            // txtMessagesArchive
            // 
            this.txtMessagesArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessagesArchive.Location = new System.Drawing.Point(3, 29);
            this.txtMessagesArchive.Multiline = true;
            this.txtMessagesArchive.Name = "txtMessagesArchive";
            this.txtMessagesArchive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessagesArchive.Size = new System.Drawing.Size(234, 210);
            this.txtMessagesArchive.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.StatusBar1);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtMessagesArchive);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.MenuItem mnuSend;
        private System.Windows.Forms.MainMenu mainMenu1;
        internal System.Windows.Forms.StatusBar StatusBar1;
        internal System.Windows.Forms.TextBox txtMessage;
        internal System.Windows.Forms.TextBox txtMessagesArchive;
    }
}

