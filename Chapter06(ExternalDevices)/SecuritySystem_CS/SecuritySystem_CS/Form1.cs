using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace SecuritySystem_CS
{
    public partial class Form1 : Form
    {
        System.IO.Ports.SerialPort serialPort = new System.IO.Ports.SerialPort();
        int proximity;

        const int WM_CAP_START = 1024;
        const int WS_CHILD = 1073741824;
        const int WS_VISIBLE = 268435456;
        const int WM_CAP_DRIVER_CONNECT = (WM_CAP_START + 10);
        const int WM_CAP_DRIVER_DISCONNECT = (WM_CAP_START + 11);
        const int WM_CAP_EDIT_COPY = (WM_CAP_START + 30);
        const int WM_CAP_SEQUENCE = (WM_CAP_START + 62);
        const int WM_CAP_FILE_SAVEAS = (WM_CAP_START + 23);
        const int WM_CAP_SET_SCALE = (WM_CAP_START + 53);
        const int WM_CAP_SET_PREVIEWRATE = (WM_CAP_START + 52);
        const int WM_CAP_SET_PREVIEW = (WM_CAP_START + 50);
        const int SWP_NOMOVE = 2;
        const int SWP_NOSIZE = 1;
        const int SWP_NOZORDER = 4;
        const int HWND_BOTTOM = 1;

        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern bool capGetDriverDescriptionA(short wDriverIndex, string lpszName, int cbName, string lpszVer, int cbVer);
        [System.Runtime.InteropServices.DllImport("avicap32.dll")]

        static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWnd, int nID);
        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SendMessageA")]

        static extern int SendMessage(int hwnd, int Msg, int wParam, [MarshalAs(UnmanagedType.AsAny)] 
object lParam);
        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SetWindowPos")]
        static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        [System.Runtime.InteropServices.DllImport("user32")]
        static extern bool DestroyWindow(int hndw);
        private int hWnd;

        public Form1()
        {
            InitializeComponent();
        }

        private void PreviewVideo(PictureBox pbCtrl)
        {
            hWnd = capCreateCaptureWindowA("0", WS_VISIBLE | WS_CHILD, 0, 0, 0, 0, pbCtrl.Handle.ToInt32(), 0);
            if (SendMessage(hWnd, WM_CAP_DRIVER_CONNECT, 0, 0) != 0)
            {
                SendMessage(hWnd, WM_CAP_SET_SCALE, 1, 0);
                SendMessage(hWnd, WM_CAP_SET_PREVIEWRATE, 30, 0);
                SendMessage(hWnd, WM_CAP_SET_PREVIEW, 1, 0);
                SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, pbCtrl.Width, pbCtrl.Height, SWP_NOMOVE | SWP_NOZORDER);
            }
            else
            {
                DestroyWindow(hWnd);
            }
        }

        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string str = serialPort.ReadLine();
            if (str != string.Empty)
            {
                proximity = System.Convert.ToInt32(str);
                ProgressBar1.BeginInvoke(new myDelegate(updateControl));
                Console.WriteLine(proximity);
            }
        }

        public delegate void myDelegate();
        public void updateControl()
        {
            try
            {
                if (proximity <= 160)
                {
                    ProgressBar1.Value = proximity;
                    lblProximity.Text = proximity + " cm";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            btnStartRecording.Enabled = false;
            btnStopRecording.Enabled = true;
            Application.DoEvents();
            SendMessage(hWnd, WM_CAP_SEQUENCE, 0, 0);
        }

        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            btnStartRecording.Enabled = true;
            btnStopRecording.Enabled = false;
            Application.DoEvents();
            SendMessage(hWnd, WM_CAP_FILE_SAVEAS, 0, "C:\\" + System.DateTime.Now.ToFileTime() + ".avi");            
        }

        private void btnTakeSnapshot_Click(object sender, EventArgs e)
        {
            IDataObject data;
            Image bmap;
            SendMessage(hWnd, WM_CAP_EDIT_COPY, 0, 0);
            data = Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
            {
                bmap = ((Image)(data.GetData(typeof(System.Drawing.Bitmap))));
                bmap.Save("C:\\" + System.DateTime.Now.ToFileTime() + ".bmp");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            try
            {
                serialPort.PortName = "COM3";
                serialPort.BaudRate = 9600;
                serialPort.Parity = System.IO.Ports.Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = System.IO.Ports.StopBits.One;
                serialPort.Handshake = System.IO.Ports.Handshake.None;
                serialPort.DataReceived +=
                   new System.IO.Ports.SerialDataReceivedEventHandler(DataReceived);
                serialPort.Open();
                serialPort.DiscardInBuffer();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            PreviewVideo(PictureBox1);
        }
    }
}