using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net.Sockets;
using System.IO;

namespace WinClient_CS
{
    public partial class Form1 : Form
    {
        private string partialStr;
        private System.Net.IPHostEntry ips = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        const int PORTNO = 500;
        const int FTPPORTNO = 501;
        const string SERVERIP = "10.0.1.2";
        private TcpClient client;
        private byte[] data;
        private System.IO.FileStream fs;
        private string filename;
        private string fullfilename;

        public Form1()
        {
            InitializeComponent();
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
                    string messageReceived;
                    int i = 0;
                    int start = 0;
                    while (data[i] != 0)
                    {
                        if (i + 1 > bytesRead)
                        {
                            break;
                        }
                        if (data[i] == 10)
                        {
                            messageReceived = partialStr + System.Text.Encoding.ASCII.GetString(data, start, i - start) + Environment.NewLine;
                            object[] para = { messageReceived };
                            this.Invoke(new delUpdateHistory((this.UpdateHistory)), para);
                            start = i + 1;
                        }
                        i += 1;
                    }

                    if (start != i)
                    {
                        partialStr = System.Text.Encoding.ASCII.GetString(data, start, i - start);
                    }
                }
                client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(client.ReceiveBufferSize), ReceiveMessage, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        public void SendMessage(string message)
        {
            message += "\n";
            try
            {
                System.Net.Sockets.NetworkStream ns;
                lock (client.GetStream())
                {
                    ns = client.GetStream();
                    byte[] bytesToSend = System.Text.Encoding.ASCII.GetBytes(message);
                    ns.Write(bytesToSend, 0, bytesToSend.Length);                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public delegate void delUpdateHistory(string str);
        public void UpdateHistory(string str)
        {
            if (str.StartsWith("[Join]"))
            {
                int nameLength = str.IndexOf("]", 6);
                lstUsers.Items.Add(str.Substring(7, nameLength - 7));
                return;
            }
            else if (str.StartsWith("[Left]"))
            {
                int nameLength = str.IndexOf("]", 6);
                try
                {
                    lstUsers.Items.RemoveAt(lstUsers.Items.IndexOf(str.Substring(7, nameLength - 7)));
                }
                catch (Exception ex)
                {
                }
                return;
            }
            else if (str.StartsWith("[Usrs]"))
            {
                string[] users = str.Substring(7, str.Length - 8).Split(',');
                lstUsers.Items.Clear();
                foreach (string user in users)
                {
                    lstUsers.Items.Add(user);
                }
                lstUsers.Items.RemoveAt(lstUsers.Items.Count - 1);
                return;
            }
            else if (str.StartsWith("[File]"))
            {
                string[] users = str.Substring(7, str.IndexOf("]", 7) - 7).Split(',');
                int index = str.IndexOf("]", 7) + 2;
                string filename = str.Substring(index, str.Length - index - 3);
                DialogResult response;
                response = MessageBox.Show("Do you want to download the file " + filename, "Download", MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes)
                {
                    SendMessage("[Send_File][" + users[0] + "," + txtNick.Text + "]");
                    FTP_Receive(filename);
                }
                return;
            }
            else if (str.StartsWith("[Send_File]"))
            {
                string userIP = str.Substring(12, str.Length - 15);
                FTP_Send(fullfilename, userIP);
                return;
            }
            else if (str.StartsWith("[Talk]"))
            {
                str = str.Substring(str.IndexOf("]", 7) + 1);
                txtMessageHistory.AppendText(str);
            }
        }
        
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (btnSignIn.Text == "Sign In")
            {
                try
                {
                    client = new TcpClient();
                    client.Connect(SERVERIP, PORTNO);

                    data = new byte[client.ReceiveBufferSize];
                    SendMessage("[Join][" + txtNick.Text + "]");
                    client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(client.ReceiveBufferSize), ReceiveMessage, null);
                    btnSignIn.Text = "Sign Out";
                    btnSend.Enabled = true;
                    txtNick.Enabled = false;
                    System.Threading.Thread.Sleep(500);
                    SendMessage("[Usrs]");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                Disconnect();
                lstUsers.Items.Clear();
                btnSignIn.Text = "Sign In";
                btnSend.Enabled = false;
                txtNick.Enabled = true;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItems.Count < 1)
            {
                MessageBox.Show("You must select who to chat with.");
                return;
            }
            string Message = "[Talk][";

            foreach (object user in lstUsers.SelectedItems)
            {
                Message += user + ",";
            }
            Message += "]" + txtNick.Text + ">" + txtMessage.Text;
            txtMessageHistory.Text += txtNick.Text + ">" + txtMessage.Text + Environment.NewLine;
            SendMessage(Message);
            txtMessage.Clear();
        }

        private void btnFTP_Click(object sender, EventArgs e)
        {
            string Message = "[File][" + txtNick.Text + ",";

            if (lstUsers.SelectedItems.Count < 1)
            {
                MessageBox.Show("You must select who to send to.");
                return;
            }
            foreach (object user in lstUsers.SelectedItems)
            {
                Message += user + ",";
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fullfilename = openFileDialog1.FileName;
                filename = fullfilename.Substring(fullfilename.LastIndexOf("\\") + 1);
                Message += "][" + filename + "]";
                SendMessage(Message);
            }

        }

        public void FTP_Send(string filename, string recipientIP)
        {
            System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient();
            tcpClient.Connect(recipientIP, FTPPORTNO);
            int BufferSize = tcpClient.ReceiveBufferSize;
            NetworkStream nws = tcpClient.GetStream();
            FileStream fs;
            fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] bytesToSend = new byte[fs.Length];
            int numBytesRead = fs.Read(bytesToSend, 0, bytesToSend.Length);
            int totalBytes = 0;
            for (int i = 0; i <= fs.Length / BufferSize; i++)
            {
                if (fs.Length - (i * BufferSize) > BufferSize)
                {
                    nws.Write(bytesToSend, i * BufferSize, BufferSize);
                    totalBytes += BufferSize;
                }
                else
                {
                    nws.Write(bytesToSend, i * BufferSize, (int)fs.Length - (i * BufferSize));
                    totalBytes += (int)fs.Length - (i * BufferSize);
                }
                ToolStripStatusLabel1.Text = "Sending " + totalBytes + " bytes....";
                Application.DoEvents();
            }
            ToolStripStatusLabel1.Text = "Sending " + totalBytes + " bytes....Done.";
            fs.Close();
            tcpClient.Close();
        }

        public void FTP_Receive(string filename)
        {
            try
            {
                System.Net.IPAddress localAdd = System.Net.IPAddress.Parse(ips.AddressList[0].ToString());
                System.Net.Sockets.TcpListener listener = new System.Net.Sockets.TcpListener(localAdd, FTPPORTNO);
                listener.Start();
                TcpClient tcpClient = listener.AcceptTcpClient();
                NetworkStream nws = tcpClient.GetStream();
                if (File.Exists("c:\\temp\\" + filename))
                {
                    File.Delete("c:\\temp\\" + filename);
                }
                fs = new System.IO.FileStream("c:\\temp\\" + filename, FileMode.Append, FileAccess.Write);
                int counter = 0;
                int totalBytes = 0;
                do
                {
                    int bytesRead = nws.Read(data, 0, tcpClient.ReceiveBufferSize);
                    totalBytes += bytesRead;
                    fs.Write(data, 0, bytesRead);
                    ToolStripStatusLabel1.Text = "Receiving " + totalBytes + " bytes....";
                    Application.DoEvents();                    
                    counter += 1;
                } while (!(!(nws.DataAvailable)));
                ToolStripStatusLabel1.Text = "Receiving " + totalBytes + " bytes....Done.";
                fs.Close();
                tcpClient.Close();
                listener.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }
    }
}