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
using InTheHand.Net.Sockets;

namespace IRChat_Desktop_CS
{
    public partial class Form1 : Form
    {
        //---define the constants---
        const int MAX_MESSAGE_SIZE = 1024;
        const int MAX_TRIES = 3;

        //---define the member variables---
        private string ServiceName = "default";
                        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            SendMessage(MAX_TRIES, txtMessage.Text);
            btnSend.Enabled = true;
            txtMessage.Text = string.Empty;
            txtMessage.Focus();
        }

        private void SendMessage(int NumRetries, string str)
        {
           IrDAClient client = null;
            int CurrentTries = 0;
            //---try to establish a connection---
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

            //---timeout occurred---
            if ((client == null))
            {
                txtMessagesArchive.BeginInvoke(new myDelegate(UpdateStatus),
                   new object[] { "Error establishing contact" });
                return;
            }

            //---send the message over a stream object---
            System.IO.Stream stream = null;
            try
            {
                stream = client.GetStream();
                stream.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(str), 0, str.Length);

                //---update the status bar---
                txtMessagesArchive.BeginInvoke(new myDelegate(UpdateStatus),
                   new object[] { "Message sent!" });

                //---display the message that was sent---
                txtMessagesArchive.Text = str + Environment.NewLine + txtMessagesArchive.Text;
            }
            catch (Exception e)
            {
                txtMessagesArchive.BeginInvoke(new myDelegate(UpdateStatus),
                   new object[] { "Error sending message." });
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

        private void Form1_Load(object sender, EventArgs e)
        {
            txtMessage.Focus();

            //---receive incoming messages as a separate thread---
            System.Threading.Thread t1;
            t1 = new System.Threading.Thread(ReceiveLoop);
            t1.Start();
        }

        public void ReceiveLoop()
        {
            string strReceived;
            strReceived = ReceiveMessage();

            //---keep on listening for new message---
            while (true)
            {
                if (strReceived != string.Empty)
                {
                    txtMessagesArchive.BeginInvoke(
                       new myDelegate(UpdateTextBox),
                       new object[] { strReceived });
                }
                strReceived = ReceiveMessage();
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

                //---blocking call---
                client = listener.AcceptIrDAClient();
                stream = client.GetStream();
                bytesRead = stream.Read(Buffer, 0, Buffer.Length);

                //---format the received message---
                str = ">" + System.Text.ASCIIEncoding.ASCII.GetString(Buffer, 0, bytesRead);
            }
            catch (SocketException ex)
            {
                //---ignore error---
            }
            catch (Exception e)
            {
                txtMessagesArchive.BeginInvoke(new myDelegate(UpdateStatus),
                   new object[] { e.ToString() });
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
        
        private delegate void myDelegate(string str);
        private void UpdateTextBox(string str)
        {
            //---delegate to update the textbox control---
            txtMessagesArchive.Text = str + Environment.NewLine + txtMessagesArchive.Text;
        }
        private void UpdateStatus(string str)
        {
            //---delegate to update the statusbar control---
            ToolStripStatusLabel1.Text = str;
        }
    }
}