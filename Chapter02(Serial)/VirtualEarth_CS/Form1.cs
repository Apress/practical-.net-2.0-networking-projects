using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VirtualEarth_CS
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Form1 : Form
    {
        private int pushpin = 0;

        private int pointCounter;

        private int lineIndex = 0;
        string[] line;

        private System.IO.Ports.SerialPort serialPort = new System.IO.Ports.SerialPort();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGotoPoint_Click(object sender, EventArgs e)
        {
            double lat, lng;
            lat = Convert.ToDouble(txtLatitude.Text);
            lng = Convert.ToDouble(txtLongitude.Text);
            gotoPosition(lat, lng, false, "");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // set the event handler for the DataReceived event
            serialPort.DataReceived +=
                new System.IO.Ports.SerialDataReceivedEventHandler(DataReceived);

            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();
            for (int i = 0; i <= portNames.Length - 1; i++)
            {
                cbbCOMPorts.Items.Add(portNames[i]);
            }

            string fileContents;
            fileContents = System.IO.File.ReadAllText(Application.StartupPath + "\\Map.html");
            WebBrowser1.DocumentText = fileContents;
            WebBrowser1.ObjectForScripting = this;
        }

        private void btnClearPath_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= pushpin; i++)
            {
                removePushpin(i);
            }
        }

        private void btnShowPath_Click(object sender, EventArgs e)
        {
            string fileContents = string.Empty;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileContents = System.IO.File.ReadAllText(openFileDialog1.FileName);
            }

            line = fileContents.Split('$');
            lineIndex = 0;
            Timer1.Enabled = true;
        }

        //---go to a particular location on the map---
        private void gotoPosition(double lat, double lng, bool showPushpin, string pushPinText)
        {
            object[] param = new object[] { lat, lng };
            WebBrowser1.Document.InvokeScript("goto_map_position", param);
            if (showPushpin)
            {
                param = new object[] { pushpin, pushPinText, lat, lng };
                WebBrowser1.Document.InvokeScript("addPushpin", param);
                pushpin += 1;
            }
        }

        //---update the latitude and longitude on the TextBox controls---
        public void mapPositionChange(double lat, double lng)
        {
            txtLatitude.Text = Convert.ToString(lat);
            txtLongitude.Text = Convert.ToString(lng);
        }

        //---remove a pushpin---
        private void removePushpin(int id)
        {
            object[] param = new object[] { id };
            WebBrowser1.Document.InvokeScript("removePushpin", param);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (lineIndex == 0)
            {
                pointCounter = 1;
            }
            while ((lineIndex <= line.Length - 1))
            {
                if (line[lineIndex].StartsWith("GPGGA") && processGPSData(line[lineIndex]))
                {
                    lblMessage.Text = "Updating map...point " + pointCounter;
                    pointCounter += 1;
                    break;
                }
                lineIndex += 1;
            }
            lineIndex += 1;
            if (lineIndex > line.Length - 1)
            {
                Timer1.Enabled = false;
                lblMessage.Text = "Plotting completed.";
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            {
                btnConnect.Text = "Disconnect";
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
                    lblMessage.Text = cbbCOMPorts.Text + " connected.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                btnConnect.Text = "Connect";
                serialPort.Close();
            }
        }

        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            txtDataReceived.BeginInvoke(new myDelegate(updateTextBox));
        }

        public delegate void myDelegate();
        public void updateTextBox()
        {
            try
            {
                string Data = serialPort.ReadExisting();
                txtDataReceived.AppendText(Data);
                txtDataReceived.ScrollToCaret();
                string GPSData = txtDataReceived.Lines[txtDataReceived.Lines.Length - 2];
                if (GPSData.StartsWith("$GPGGA"))
                {
                    if (!(processGPSData(GPSData)))
                    {
                        lblMessage.Text = "No fix...";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private bool processGPSData(string str)
        {
            try
            {
                string[] field;
                field = str.Split(',');
                double lat;
                double lng;
                double rawLatLng;
                if (field.Length < 15)
                {
                    return false;
                }

                rawLatLng = Convert.ToDouble(field[2]);
                lat = ((int)(rawLatLng / 100)) + ((rawLatLng - (((int)(rawLatLng / 100)) * 100)) / 60);
                if (field[3] == "S")
                {
                    lat *= -1;
                }

                rawLatLng = Convert.ToDouble(field[4]);
                lng = ((int)(rawLatLng / 100)) + ((rawLatLng - (((int)(rawLatLng / 100)) * 100)) / 60);
                if (field[5] == "W")
                {
                    lng *= -1;
                }

                if (str.StartsWith("$")) 
                {
                   gotoPosition(lat, lng, false, "");
                
                } else 
                {
                   gotoPosition(lat, lng, true, "*");

                }
                lblMessage.Text = "Latitude: " + lat + " Longitude: " + lng;
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}