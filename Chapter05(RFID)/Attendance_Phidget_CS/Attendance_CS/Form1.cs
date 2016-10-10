using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Attendance_CS
{
    public partial class Form1 : Form
    {
        PhidgetsNET.PhidgetRFID RFIDReader;

        public Form1()
        {
            InitializeComponent();
        }
             
        private void RFIDReader_Attach(object sender, PhidgetsNET.AttachEventArgs e)
        {
            ToolStripStatusLabel1.Text = "Phidget RFID Reader Connected";
            chkTurnOnLED.Checked = true;
            RFIDReader.SetOutputState(2, true);
            chkEnableReader.Checked = true;
            RFIDReader.SetOutputState(3, true);
        }

        private void RFIDReader_Detach(object sender, PhidgetsNET.DetachEventArgs e)
        {
            ToolStripStatusLabel1.Text = "Phidget RFID Reader Not Connected";
        }
        
        private void RFIDReader_Error(object sender, PhidgetsNET.ErrorEventArgs e) 
        {
            //---display the error---
            ToolStripStatusLabel1.Text = e.getError();
        }

        private void RFIDReader_Tag(object sender, PhidgetsNET.TagEventArgs e)
        {
            Console.WriteLine("tag detected - " + e.getTag());
            txtTagID.BeginInvoke(new myDelegate(updateTextBox), new object[] { e.getTag() });
        }

        public delegate void myDelegate(string str);
        public void updateTextBox(string str)
        {
            txtTagID.Text = str;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            RFIDReader = new PhidgetsNET.PhidgetRFID();

            RFIDReader.Attach += new PhidgetsNET.AttachEventHandler(this.RFIDReader_Attach);
            RFIDReader.Detach += new PhidgetsNET.DetachEventHandler(this.RFIDReader_Detach);
            RFIDReader.Tag += new PhidgetsNET.TagEventHandler(this.RFIDReader_Tag);
            RFIDReader.Error += new PhidgetsNET.ErrorEventHandler(this.RFIDReader_Error);
            
            RFIDReader.OpenRemoteIP("localhost", 5001, -1, "pass");
            ToolStripStatusLabel1.Text = "Not Connected";
        }

        private void chkTurnOnLED_CheckedChanged(object sender, EventArgs e)
        {
            RFIDReader.SetOutputState(2, chkTurnOnLED.Checked);
        }

        private void chkEnableReader_CheckedChanged(object sender, EventArgs e)
        {
            RFIDReader.SetOutputState(3, chkEnableReader.Checked);
        }
    }
}