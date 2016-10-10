using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Sockets;
using System.Collections;

namespace server_CS
{
    class ChatClient
    {
        const int LF = 10;
        public static Hashtable AllClients = new Hashtable();
        private TcpClient _client;
        private string _clientIP;
        private string _clientNick;
        private byte[] data;
        private string partialStr;

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
                    byte[] bytesToSend = System.Text.Encoding.ASCII.GetBytes(message);
                    ns.Write(bytesToSend, 0, bytesToSend.Length);
                    ns.Flush();
                }
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
                    Broadcast("[Left][" + _clientNick + "] has left the chat.", null);
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
                        if (data[i] == LF)
                        {
                            messageReceived = partialStr + System.Text.Encoding.ASCII.GetString(data, start, i - start);
                            Console.WriteLine("received <----- " + messageReceived);
                            if (messageReceived.StartsWith("[Join]"))
                            {
                                int nameLength = messageReceived.IndexOf("]", 6);
                                _clientNick = messageReceived.Substring(7, nameLength - 7);
                                Broadcast(messageReceived, null);
                            }
                            else if (messageReceived.StartsWith("[Usrs]"))
                            {
                                string allUsers = "[Usrs][";

                                foreach (DictionaryEntry c in AllClients)
                                {
                                    allUsers += ((ChatClient)(c.Value))._clientNick + ",";
                                }
                                allUsers += "]";
                                Broadcast(allUsers, null);
                            }
                            else if (messageReceived.StartsWith("[Talk]"))
                            {
                                string[] users = messageReceived.Substring(7, messageReceived.IndexOf("]", 7) - 8).Split(',');
                                Broadcast(messageReceived, users);
                            }
                            else if (messageReceived.StartsWith("[File]"))
                            {
                                string[] users = messageReceived.Substring(7, messageReceived.IndexOf("]", 7) - 8).Split(',');
                                int index = messageReceived.IndexOf("]", 7) + 2;
                                string filename = messageReceived.Substring(index, messageReceived.Length - index - 1);
                                string from = users[0];
                                for (int j = 1; j <= users.Length - 1; j++)
                                {
                                    users[j - 1] = users[j];
                                }
                                users[users.Length - 1] = string.Empty;
                                Broadcast("[File][" + from + "][" + filename + "]", users);
                            }
                            else if (messageReceived.StartsWith("[Send_File]"))
                            {
                                string[] users = messageReceived.Substring(12, messageReceived.IndexOf("]", 12) - 12).Split(',');
                                string RecipientIP = string.Empty;

                                foreach (DictionaryEntry c in AllClients)
                                {
                                    if (((ChatClient)(c.Value))._clientNick == users[1])
                                    {
                                        RecipientIP = ((ChatClient)(c.Value))._clientIP.Substring(0, _clientIP.IndexOf(":"));
                                        break;
                                    }
                                }                            
                                users[1] = string.Empty;
                                Broadcast("[Send_File][" + RecipientIP + "]", users);
                            }
                            start = i + 1;
                        }
                        i += 1;
                    }                
                    if (start != i)
                    {
                        partialStr = System.Text.Encoding.ASCII.GetString(data, start, i - start);
                    }
                }
                lock (_client.GetStream())
                {
                    _client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(_client.ReceiveBufferSize), ReceiveMessage, null);
                }
            }
            catch (Exception ex)
            {
                AllClients.Remove(_clientIP);
                Broadcast("[Left][" + _clientNick + "] has left the chat.", null);
            }
        }

        public void Broadcast(string message, string[] users)
        {
            if (users == null)
            {
                foreach (DictionaryEntry c in AllClients)
                {
                    ((ChatClient)(c.Value)).SendMessage(message + "\n");
                }
            }
            else
            {
                foreach (DictionaryEntry c in AllClients)
                {
                    foreach (string user in users)
                    {
                        if (((ChatClient)(c.Value))._clientNick == user)
                        {
                            ((ChatClient)(c.Value)).SendMessage(message + "\n");
                            Console.WriteLine("sending -----> " + message );
                            break;
                        }
                    }
                }
            }
        }
    }
}
