using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Sockets;

namespace server_CS
{
    class Program
    {
        const int portNo = 500;
        static void Main(string[] args)
        {
//            System.Net.IPAddress localAdd = System.Net.IPAddress.Parse("10.0.1.2");
            System.Net.IPAddress localAdd = System.Net.IPAddress.Parse("127.0.0.1");
            System.Net.Sockets.TcpListener listener = new System.Net.Sockets.TcpListener(localAdd, portNo);
            listener.Start();
            while (true)
            {
                ChatClient user = new ChatClient(listener.AcceptTcpClient());
            }
        }
    }
}
