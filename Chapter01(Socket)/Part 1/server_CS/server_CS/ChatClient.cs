using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Sockets;
using System.Collections;

namespace server_CS
{
    class ChatClient
    {
        public static Hashtable AllClients = 
            new Hashtable();
        private TcpClient _client;
        private string _clientIP;
        private string _ClientNick;
        private byte[] data;
        private bool ReceiveNick = true;

        public ChatClient(TcpClient client)
        {
            _client = client;
            _clientIP = client.Client.RemoteEndPoint.ToString();
            AllClients.Add(_clientIP, this);
            
            data = new byte[_client.ReceiveBufferSize];
            _client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(_client.ReceiveBufferSize), ReceiveMessage, null);           
        }

        public void SendMessage(string message)
        {
            try
            {
                System.Net.Sockets.NetworkStream ns;
                lock (_client.GetStream())
                {
                    ns = _client.GetStream();
                }
                byte[] bytesToSend = System.Text.Encoding.ASCII.GetBytes(message);
                ns.Write(bytesToSend, 0, bytesToSend.Length);
                ns.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ReceiveMessage(IAsyncResult ar)
        {
            int bytesRead;
            try
            {
                lock (_client.GetStream())
                {
                    bytesRead = _client.GetStream().EndRead(ar);
                }
                if (bytesRead < 1)
                {
                    AllClients.Remove(_clientIP);
                    Broadcast(_ClientNick + " has left the chat.");
                    return;
                }
                else
                {
                    string messageReceived = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);
                    if (ReceiveNick)
                    {
                        _ClientNick = messageReceived;
                        Broadcast(_ClientNick + " has joined the chat.");
                        ReceiveNick = false;
                    }
                    else
                    {
                        Broadcast(_ClientNick + ">" + messageReceived);
                    }
                }
                lock (_client.GetStream())
                {
                    _client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(_client.ReceiveBufferSize),ReceiveMessage, null);
                }
            }
            catch (Exception ex)
            {
                AllClients.Remove(_clientIP);
                Broadcast(_ClientNick + " has left the chat.");
            }
        }
        public void Broadcast(string message)
        {
            Console.WriteLine(message);
            foreach (DictionaryEntry c in AllClients)
            {
                ((ChatClient)(c.Value)).SendMessage(message + Environment.NewLine);
            }
        }
    }
}
