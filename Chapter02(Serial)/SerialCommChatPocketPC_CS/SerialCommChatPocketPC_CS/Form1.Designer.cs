namespace SerialCommChatPocketPC_CS
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.cbbCOMPorts = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.MenuItem1 = new System.Windows.Forms.MenuItem();
            this.txtMessageToSend = new System.Windows.Forms.TextBox();
            this.txtReceivedMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(157, 242);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 20);
            this.btnConnect.TabIndex = 16;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(3, 247);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(59, 20);
            this.Label3.Text = "COM Port";
            // 
            // cbbCOMPorts
            // 
            this.cbbCOMPorts.Items.Add("COM1");
            this.cbbCOMPorts.Items.Add("COM2");
            this.cbbCOMPorts.Items.Add("COM3");
            this.cbbCOMPorts.Items.Add("COM4");
            this.cbbCOMPorts.Items.Add("COM5");
            this.cbbCOMPorts.Items.Add("COM6");
            this.cbbCOMPorts.Items.Add("COM7");
            this.cbbCOMPorts.Items.Add("COM8");
            this.cbbCOMPorts.Items.Add("COM9");
            this.cbbCOMPorts.Location = new System.Drawing.Point(68, 242);
            this.cbbCOMPorts.Name = "cbbCOMPorts";
            this.cbbCOMPorts.Size = new System.Drawing.Size(83, 22);
            this.cbbCOMPorts.TabIndex = 15;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(6, 48);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(135, 20);
            this.Label2.Text = "Received Message";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(6, 1);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(100, 20);
            this.Label1.Text = "Message to send";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.MenuItem1);
            // 
            // MenuItem1
            // 
            this.MenuItem1.Text = "Send";
            this.MenuItem1.Click += new System.EventHandler(this.MenuItem1_Click);
            // 
            // txtMessageToSend
            // 
            this.txtMessageToSend.Location = new System.Drawing.Point(3, 24);
            this.txtMessageToSend.Name = "txtMessageToSend";
            this.txtMessageToSend.Size = new System.Drawing.Size(234, 21);
            this.txtMessageToSend.TabIndex = 14;
            // 
            // txtReceivedMessage
            // 
            this.txtReceivedMessage.Location = new System.Drawing.Point(3, 71);
            this.txtReceivedMessage.Multiline = true;
            this.txtReceivedMessage.Name = "txtReceivedMessage";
            this.txtReceivedMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReceivedMessage.Size = new System.Drawing.Size(234, 165);
            this.txtReceivedMessage.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.cbbCOMPorts);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtMessageToSend);
            this.Controls.Add(this.txtReceivedMessage);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnConnect;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.ComboBox cbbCOMPorts;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.MainMenu mainMenu1;
        internal System.Windows.Forms.MenuItem MenuItem1;
        internal System.Windows.Forms.TextBox txtMessageToSend;
        internal System.Windows.Forms.TextBox txtReceivedMessage;
    }
}

