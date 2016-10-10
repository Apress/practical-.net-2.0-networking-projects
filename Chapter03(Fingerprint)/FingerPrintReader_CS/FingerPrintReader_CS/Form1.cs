using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using GrFingerXLib;

namespace FingerPrintReader_CS
{
    public partial class Form1 : Form
    {

        // ---name of the database---
        const string DBFile = "GrFingerSample.mdb";
        const string Logfile = "C:\\Log.csv";
        const string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
        Util myUtil;
        int _UserID;
        System.Data.OleDb.OleDbConnection connection;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int err;
            //  initialize util class
            myUtil = new Util(ListBox1, PictureBox1, null, null, null, null, null, null);

            axGrFingerXCtrl1.SensorPlug += new AxGrFingerXLib.
                _IGrFingerXCtrlEvents_SensorPlugEventHandler(axGrFingerXCtrl1_SensorPlug);
            axGrFingerXCtrl1.SensorUnplug += new AxGrFingerXLib.
                _IGrFingerXCtrlEvents_SensorUnplugEventHandler(axGrFingerXCtrl1_SensorUnplug);
            axGrFingerXCtrl1.FingerDown += new AxGrFingerXLib.
                _IGrFingerXCtrlEvents_FingerDownEventHandler(axGrFingerXCtrl1_FingerDown);
            axGrFingerXCtrl1.FingerUp += new AxGrFingerXLib.
                _IGrFingerXCtrlEvents_FingerUpEventHandler(axGrFingerXCtrl1_FingerUp);
            axGrFingerXCtrl1.ImageAcquired += new AxGrFingerXLib.
                _IGrFingerXCtrlEvents_ImageAcquiredEventHandler(axGrFingerXCtrl1_ImageAcquired);

            //  Initialize GrFingerX Library
            err = myUtil.InitializeGrFinger(axGrFingerXCtrl1);
            //  Print result in log
            if ((err < 0))
            {
                myUtil.WriteError((GRConstants)err);
                return;
            }
            else
            {
                myUtil.WriteLog("**GrFingerX Initialized Successfull**");
            }
            // ---create a log file---
            if (!System.IO.File.Exists(Logfile))
            {
                System.IO.File.Create(Logfile);
            }
        }
                
        //  -----------------------------------------------------------------------------------
        //  GrFingerX events
        //  -----------------------------------------------------------------------------------
        //  A fingerprint reader was plugged on system
        private void axGrFingerXCtrl1_SensorPlug(object sender, AxGrFingerXLib._IGrFingerXCtrlEvents_SensorPlugEvent e)
        {
            myUtil.WriteLog(("Sensor: "
                            + (e.idSensor + ". Event: Plugged.")));
            axGrFingerXCtrl1.CapStartCapture(e.idSensor);
        }

        //  A fingerprint reader was unplugged from system
        private void axGrFingerXCtrl1_SensorUnplug(object sender, AxGrFingerXLib._IGrFingerXCtrlEvents_SensorUnplugEvent e)
        {
            myUtil.WriteLog(("Sensor: "
                            + (e.idSensor + ". Event: Unplugged.")));
            axGrFingerXCtrl1.CapStopCapture(e.idSensor);
        }

        //  A finger was placed on reader
        private void axGrFingerXCtrl1_FingerDown(object sender, AxGrFingerXLib._IGrFingerXCtrlEvents_FingerDownEvent e)
        {
            myUtil.WriteLog(("Sensor: "
                            + (e.idSensor + ". Event: Finger Placed.")));
        }

        //  A finger was removed from reader
        private void axGrFingerXCtrl1_FingerUp(object sender, AxGrFingerXLib._IGrFingerXCtrlEvents_FingerUpEvent e)
        {
            myUtil.WriteLog(("Sensor: "
                            + (e.idSensor + ". Event: Finger removed.")));
        }

        //  An image was acquired from reader
        private void axGrFingerXCtrl1_ImageAcquired(object sender, AxGrFingerXLib._IGrFingerXCtrlEvents_ImageAcquiredEvent e)
        {
            //  Copying aquired image
            myUtil._raw.height = e.height;
            myUtil._raw.width = e.width;
            myUtil._raw.Res = e.res;
            myUtil._raw.img = e.rawImage;
            //  Signaling that an Image Event occurred.
            myUtil.WriteLog(("Sensor: "
                            + (e.idSensor + ". Event: Image captured.")));
            //  display fingerprint image
            myUtil.PrintBiometricDisplay(false, GRConstants.GR_DEFAULT_CONTEXT);
            // ---extract the template from the fingerprint scanned---
            ExtractTemplate();
            // ---identify who the user is---
            _UserID = IdentifyFingerprint();
            if ((_UserID > 0))
            {
                // ---user found---                
                btnRegister.Enabled = false;
                GetUserInfo();
                // ---writes to log file---
                WriteToLog(_UserID.ToString());
            }
            else
            {
                // ---user not found---
                ClearDisplay();
                btnRegister.Enabled = true;
                lblMessage.Text = "User not found! Please register your information below";
            }
        }
        
        //  Extract a template from a fingerprint image
        private int ExtractTemplate()
        {
            int ret;
            //  extract template
            ret = myUtil.ExtractTemplate();
            //  write template quality to log
            if ((GRConstants)ret == GRConstants.GR_BAD_QUALITY)
            {
                myUtil.WriteLog("Template extracted successfully. Bad quality.");
            }
            else if ((GRConstants)ret == GRConstants.GR_MEDIUM_QUALITY)
            {
                myUtil.WriteLog("Template extracted successfully. Medium quality.");
            }
            else if ((GRConstants)ret == GRConstants.GR_HIGH_QUALITY)
            {
                myUtil.WriteLog("Template extracted successfully. High quality.");
            }
            if ((ret >= 0))
            {
                //  if no error, display minutiae/segments/directions into the image
                myUtil.PrintBiometricDisplay(true, GRConstants.GR_NO_CONTEXT);
            }
            else
            {
                //  write error to log
                myUtil.WriteError((GRConstants)ret);
            }
            return ret;
        }

        // ---Identify a fingerprint; returns the ID of the user---
        private int IdentifyFingerprint()
        {
            int ret;
            int score;
            score = 0;
            //  identify it
            ret = myUtil.Identify(ref score);
            //  write result to log
            if ((ret > 0))
            {
                myUtil.WriteLog(("Fingerprint identified. ID = "
                                + (ret + (". Score = "
                                + (score + ".")))));
                myUtil.PrintBiometricDisplay(true, GRConstants.GR_DEFAULT_CONTEXT);
            }
            else if ((ret == 0))
            {
                myUtil.WriteLog("Fingerprint not Found.");
            }
            else
            {
                myUtil.WriteError((GRConstants)ret);
            }
            return ret;
        }

        // ---get user's information---
        public void GetUserInfo()
        {
            string filePath;
            try
            {
                filePath = (Application.StartupPath + ("\\" + DBFile));
                connection = new System.Data.OleDb.OleDbConnection((ConnectionString + filePath));
                connection.Open();
                System.Data.OleDb.OleDbDataReader reader;
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                command.Connection = connection;
                // ---retrieve user's particulars---
                command.CommandText = ("SELECT * FROM Enroll WHERE ID=" + _UserID);
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                reader.Read();
                // ---display user's particulars---
                lblMessage.Text = ("Welcome, " + reader["name"]);
                txtSSN.Text = reader["SSN"].ToString();
                txtName.Text = reader["Name"].ToString();
                txtCompany.Text = reader["Company"].ToString();
                txtContactNumber.Text = reader["ContactNumber"].ToString();
                txtEmail.Text = reader["Email"].ToString();
                // ---reset the timer to another 5 seconds---
                Timer1.Enabled = false;
                Timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
            finally
            {
                connection.Close();
            }
        }

        // ---Register button---
        private void btnRegister_Click(object sender, System.EventArgs e)
        {
            // ---first add the fingerprint---
            _UserID = EnrollFingerprint();
            // ---then add the particulars---
            AddNewUser();
            // ---clears the display---
            ClearDisplay();
            // ---writes to log file---
            WriteToLog(_UserID.ToString());
        }

        // ---adds a fingerprint to the database; returns the ID of the user---
        private int EnrollFingerprint()
        {
            int id;
            //  add fingerprint
            id = myUtil.Enroll();
            //  write result to log
            if ((id >= 0))
            {
                myUtil.WriteLog(("Fingerprint enrolled with id = " + id));
            }
            else
            {
                myUtil.WriteLog("Error: Fingerprint not enrolled");
            }
            return id;
        }

        // ---Add a new user's information to the database---
        public void AddNewUser()
        {
            string filePath;
            try
            {
                filePath = (Application.StartupPath + ("\\" + DBFile));
                connection = new System.Data.OleDb.OleDbConnection((ConnectionString + filePath));
                connection.Open();
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                command.Connection = connection;
                // ---set the user's particulars in the table---
                string sql = ("UPDATE enroll SET SSN=\'"
                            + (txtSSN.Text + ("\', " + ("Name=\'"
                            + (txtName.Text + ("\', " + ("Company=\'"
                            + (txtCompany.Text + ("\', " + ("ContactNumber=\'"
                            + (txtContactNumber.Text + ("\', " + ("Email=\'"
                            + (txtEmail.Text + ("\' " + (" WHERE ID=" + _UserID))))))))))))))));
                command.CommandText = sql;
                command.ExecuteNonQuery();
                MessageBox.Show("User added successfully!", "Error");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
            finally
            {
                connection.Close();
            }
        }

        // ---Clears the user's particulars---
        public void ClearDisplay()
        {
            lblMessage.Text = "Please place your index finger " + "on the fingerprint reader";
            PictureBox1.Image = FingerPrintReader_CS.
                Properties.Resources.fingerprintreader;
            txtSSN.Text = String.Empty;
            txtName.Text = String.Empty;
            txtCompany.Text = String.Empty;
            txtContactNumber.Text = String.Empty;
            txtEmail.Text = String.Empty;
        }
        
        public void WriteToLog(string ID)
        {
            // ---write to a log file---
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Logfile, true, System.Text.Encoding.ASCII);
            sw.WriteLine((ID + ("," + System.DateTime.Now.ToString())));
            sw.Close();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            ClearDisplay();
            Timer1.Enabled = false;
        }
    }
}