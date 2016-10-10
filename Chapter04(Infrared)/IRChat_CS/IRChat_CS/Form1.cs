using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.IO;
using System.Net.Sockets;

namespace IRChat_CS
{
    public partial class Form1 : Form
    {
        const int MAX_MESSAGE_SIZE = 1024;
        const int MAX_TRIES = 3;
        private string ServiceName = "default";
        
        public Form1()
        {
            InitializeComponent();
        }
         
        private void SendMessage(int NumRetries, string str)
        {
            IrDAClient client = null;
            int CurrentTries = 0;
            do
            {
                try
                {
                    client = new IrDAClient(ServiceName);
                }
                catch (Exception se)
                {
                    if ((CurrentTries >= NumRetries))
                    {
                        throw se;
                    }
                }
                CurrentTries = CurrentTries + 1;
            } while (client == null & CurrentTries < NumRetries);

            if ((client == null))
            {
                StatusBar1.BeginInvoke(new myDelegate(UpdateStatus), new object[] { "Error establishing contact" });

                return;
            }

            System.IO.Stream stream = null;
            try
            {
                stream = client.GetStream();
                stream.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(str), 0, str.Length);
                StatusBar1.BeginInvoke(new myDelegate(UpdateStatus), new object[] { "Message sent!" });
                txtMessagesArchive.Text = str + "\r\n" + txtMessagesArchive.Text;
            }
            catch (Exception e)
            {
                StatusBar1.BeginInvoke(new myDelegate(UpdateStatus), new object[] { "Error sending message." });
            }
            finally
            {
                if ((!(stream == null)))
                {
                    stream.Close();
                }
                if ((!(client == null)))
                {
                    client.Close();
                }
            }
        }
        
        private string ReceiveMessage()
        {
            int bytesRead = 0;
            IrDAListener listener = new IrDAListener(ServiceName);
            IrDAClient client = null;
            System.IO.Stream stream = null;
            byte[] Buffer = new byte[MAX_MESSAGE_SIZE - 1];
            string str = string.Empty;
            try
            {
                listener.Start();
                client = listener.AcceptIrDAClient();
                stream = client.GetStream();
                bytesRead = stream.Read(Buffer, 0, Buffer.Length);
                str = ">" + System.Text.ASCIIEncoding.ASCII.GetString(Buffer, 0, bytesRead);
            }
            catch (SocketException ex)
            {
            }
            catch (Exception e)
            {
                StatusBar1.BeginInvoke(new myDelegate(UpdateStatus), new object[] { e.ToString() });
            }
            finally
            {
                if ((!(stream == null)))
                {
                    stream.Close();
                }
                if ((!(client == null)))
                {
                    client.Close();
                }
                listener.Stop();
            }
            return str;
        }
        
        public void ReceiveLoop()
        {
            string strReceived;
            strReceived = ReceiveMessage();
            while (true)
            {
                if (strReceived != string.Empty)
                {
                    txtMessagesArchive.BeginInvoke(new myDelegate(UpdateTextBox), new object[] { strReceived });
                }
                strReceived = ReceiveMessage();
            }
        }

        private delegate void myDelegate(string str);

        private void UpdateTextBox(string str)
        {
            txtMessagesArchive.Text = str + "\r\n" + txtMessagesArchive.Text;
        }

        private void UpdateStatus(string str)
        {
            StatusBar1.Text = str;
        }
        
        private void mnuSend_Click(object sender, EventArgs e)
        {
            mnuSend.Enabled = false;
            SendMessage(MAX_TRIES, txtMessage.Text);
            mnuSend.Enabled = true;
            txtMessage.Text = string.Empty;
            txtMessage.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtMessage.Focus();
            System.Threading.Thread t1;
            t1 = new System.Threading.Thread(ReceiveLoop);
            t1.Start();
        }
    }
}