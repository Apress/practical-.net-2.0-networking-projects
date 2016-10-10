using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using System.Net.Sockets;

namespace WinClient_CS
{
    public partial class Form1 : Form
    {
        const int portNo = 500;
        TcpClient client;
        byte[] data;

        public Form1()
        {
            InitializeComponent();
        }

        public void SendMessage(string message)
        {
            try
            {
                NetworkStream ns = client.GetStream();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                ns.Write(data, 0, data.Length);
                ns.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                int bytesRead;
                bytesRead = client.GetStream().EndRead(ar);
                if (bytesRead < 1)
                {
                    return;
                }
                else
                {
                    object[] para = { System.Text.Encoding.ASCII.GetString(data, 0, bytesRead) };
                    this.Invoke(new delUpdateHistory(UpdateHistory), para);
                }
                client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(client.ReceiveBufferSize),ReceiveMessage, null);
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (btnSignIn.Text == "Sign In")
            {
                try
                {
                    client = new TcpClient();
                    client.Connect("127.0.0.1", portNo);

                    data = new byte[client.ReceiveBufferSize];

                    SendMessage(txtNick.Text);
                    client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(client.ReceiveBufferSize), ReceiveMessage, null);
                    btnSignIn.Text = "Sign Out";
                    btnSend.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                Disconnect();
                btnSignIn.Text = "Sign In";
                btnSend.Enabled = false;
            }
        }
        public void Disconnect()
        {
            try
            {
                client.GetStream().Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public delegate void delUpdateHistory(string str);
        public void UpdateHistory(string str)
        {
            txtMessageHistory.AppendText(str);
        }
        
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage(txtMessage.Text);
            txtMessage.Clear();
        }

    }
}