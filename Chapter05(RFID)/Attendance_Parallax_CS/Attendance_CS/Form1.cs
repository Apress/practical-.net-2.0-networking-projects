using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Attendance_CS
{
    public partial class Form1 : Form
    {
        private System.IO.Ports.SerialPort serialPort = new System.IO.Ports.SerialPort();
        private string tagID = string.Empty;
        private DateTime timeRecorded = System.DateTime.Today;
        const string COM = "COM6";
        const string FILE_NAME = "C:\\Attendance.csv";
        const int INTERVAL = 3;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void employeesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.employeesBindingSource.EndEdit();
            this.employeesTableAdapter.Update(this.northwindDataSet.Employees);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.northwindDataSet.Employees);

            serialPort.DataReceived +=
               new System.IO.Ports.SerialDataReceivedEventHandler(DataReceived);

            employeesBindingSource.Filter = "TAGID='xxxxxxxxxx'";
            timer1.Interval = INTERVAL * 1000;
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            try
            {
                serialPort.PortName = COM;
                serialPort.BaudRate = 9600;
                serialPort.Parity = System.IO.Ports.Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = System.IO.Ports.StopBits.One;
                serialPort.Handshake = System.IO.Ports.Handshake.None;
                serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            txtTagID.BeginInvoke(new myDelegate(updateTextBox), new object[] { });
        }

        public delegate void myDelegate();
        public void updateTextBox()
        {
            txtTagID.AppendText(serialPort.ReadExisting());
            txtTagID.ScrollToCaret();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtEmployeeID.Text == string.Empty)
            {
                employeesBindingSource.RemoveFilter();
            }
            else
            {
                employeesBindingSource.Filter = "EmployeeID='" + txtEmployeeID.Text + "'";
            }
        }

        private void WriteToLog(string employeeID, string employeeName)
        {
            string str = employeeID + "," + employeeName + "," + System.DateTime.Today.ToString() + Environment.NewLine;
            File.AppendAllText(FILE_NAME, str);
        }

        private void txtTagID_TextChanged(object sender, EventArgs e)
        {
            if (txtTagID.Lines[txtTagID.Lines.Length - 1] == string.Empty)
            {
                string temptagID = txtTagID.Lines[txtTagID.Lines.Length - 2];
                TimeSpan tp = System.DateTime.Today.Subtract(timeRecorded);
                double timeInterval = tp.Ticks / TimeSpan.TicksPerSecond;
                if ((temptagID == tagID) & timeInterval < INTERVAL)
                {
                    return;
                }
                tagID = temptagID;
                employeesBindingSource.RemoveFilter();
                employeesBindingSource.Filter = "TAGID='" + tagID + "'";
                if (employeesBindingSource.Count < 1)
                {
                    ToolStripStatusLabel1.Text = "Employee not found.";
                }
                else
                {
                    ToolStripStatusLabel1.Text = "Employee found.";
                    WriteToLog(employeeIDLabel1.Text, lastNameTextBox.Text + ", " + firstNameTextBox.Text);
                    timer1.Enabled = false;
                    timer1.Enabled = true;
                }
                timeRecorded = System.DateTime.Today;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            employeesBindingSource.Filter = "TAGID='xxxxxxxxxx'";
            timer1.Enabled = false;
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (txtTagID.Lines.Length > 1)
            {
                string tagID = txtTagID.Lines[txtTagID.Lines.Length - 2];
            }
            else
            {
                ToolStripStatusLabel1.Text = "No tag id scanned.";
                return;
            }
            if (txtTagID.Text != string.Empty)
            {
                tagIDLabel1.Text = tagID;
                ToolStripStatusLabel1.Text = "Tag associated with employee.";
                this.Validate();
                this.employeesBindingSource.EndEdit();
                this.employeesTableAdapter.Update(this.northwindDataSet.Employees);
            }
        }

        private void btnDeassign_Click(object sender, EventArgs e)
        {
            if (tagIDLabel1.Text.Trim() == string.Empty)
            {
                ToolStripStatusLabel1.Text = "Current employee has no tag ID.";
                return;
            }
            tagIDLabel1.Text = string.Empty;
            this.Validate();
            this.employeesBindingSource.EndEdit();
            this.employeesTableAdapter.Update(this.northwindDataSet.Employees);
            ToolStripStatusLabel1.Text = "Tag deassociated from employee.";
        }
    }
}