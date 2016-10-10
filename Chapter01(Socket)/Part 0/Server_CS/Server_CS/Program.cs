using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Server_CS
{
    class Program
    {
        const int portNo = 500;
        static void Main(string[] args)
        {
            System.Net.IPAddress localAdd = System.Net.IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(localAdd, portNo);
            listener.Start();
            TcpClient tcpClient = listener.AcceptTcpClient();
            NetworkStream ns = tcpClient.GetStream();
            byte[] data = new byte[tcpClient.ReceiveBufferSize];
            int numBytesRead = ns.Read(data, 0, System.Convert.ToInt32(tcpClient.ReceiveBufferSize));
            Console.WriteLine("Received :" + Encoding.ASCII.GetString(data, 0, numBytesRead));
            Console.ReadLine();
        }
    }
}
