using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SerialCommChat_CS
{
    public partial class Form1 : Form
    {
        private System.IO.Ports.SerialPort serialPort =
            new System.IO.Ports.SerialPort();

        public Form1()
        {
            InitializeComponent();                      
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            try
            {
                serialPort.PortName = cbbCOMPorts.Text;
                serialPort.BaudRate = 9600;
                serialPort.Parity = System.IO.Ports.Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = System.IO.Ports.StopBits.One;
                // serialPort.Encoding = System.Text.Encoding.Unicode;
                serialPort.Open();
                lblMessage.Text = cbbCOMPorts.Text + " connected.";
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            txtDataReceived.BeginInvoke(new myDelegate(updateTextBox));
        }

        public delegate void myDelegate();
        public void updateTextBox()
        {
            //---for receiving plan ASCII text---
            //txtDataReceived.AppendText(serialPort.ReadExisting());
            //txtDataReceived.ScrollToCaret();

            //---UNICODE work-around---
            int bytesToRead = serialPort.BytesToRead;
            char[] ch = new char[bytesToRead];
            int bytesRead = 0;
            bytesRead = serialPort.Read(ch, 0, bytesToRead);
            string str = new string(ch, 0, bytesRead);
            txtDataReceived.AppendText(str);
            txtDataReceived.ScrollToCaret();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.Close();
                lblMessage.Text = serialPort.PortName + " disconnected.";
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.Write(txtDataToSend.Text + Environment.NewLine);
                txtDataReceived.AppendText(">" + txtDataToSend.Text + Environment.NewLine);
                txtDataReceived.ScrollToCaret();
                txtDataToSend.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // set the event handler for the DataReceived event
            serialPort.DataReceived +=
                new System.IO.Ports.SerialDataReceivedEventHandler(DataReceived);

            // display all the serial port names on the local computer
            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i <= portNames.Length - 1; i++)
            {
                cbbCOMPorts.Items.Add(portNames[i]);
            }
            btnDisconnect.Enabled = false;
        }

        private void btnDialNumber_Click(object sender, EventArgs e)
        {
            serialPort.Write("ATDT " + txtPhoneNumber.Text + Environment.NewLine);
        }

        private void btnAnswerCall_Click(object sender, EventArgs e)
        {
            serialPort.Write("AT*EVA" + Environment.NewLine);
        }
    }
}