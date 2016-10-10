using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SerialCommChatPocketPC_CS
{
    public partial class Form1 : Form
    {
        private System.IO.Ports.SerialPort serialPort =
            new System.IO.Ports.SerialPort();

        public Form1()
        {
            InitializeComponent();
        }

        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            txtReceivedMessage.BeginInvoke(new myDelegate(updateTextBox));
        }

        public delegate void myDelegate();
        public void updateTextBox()
        {
            //---for receiving plan ASCII text---
            txtReceivedMessage.Text = (serialPort.ReadExisting()) + txtReceivedMessage.Text;
            txtReceivedMessage.ScrollToCaret();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort.DataReceived +=
               new System.IO.Ports.SerialDataReceivedEventHandler(DataReceived);
        }

        private void MenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort.Write(txtMessageToSend.Text + "\r");
                txtReceivedMessage.Text = ">" + txtMessageToSend.Text + "\r" + txtReceivedMessage.Text;
                txtMessageToSend.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                serialPort.Open();
                MessageBox.Show("Port opened successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}