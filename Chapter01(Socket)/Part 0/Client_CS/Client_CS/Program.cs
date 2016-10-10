using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Client_CS
{
    class Program
    {
        const int portNo = 500;
        static void Main(string[] args)
        {
            TcpClient tcpclient = new TcpClient();
            tcpclient.Connect("127.0.0.1", portNo);
            NetworkStream ns = tcpclient.GetStream();
            byte[] data = Encoding.ASCII.GetBytes("Hello");
            ns.Write(data, 0, data.Length);
        }
    }
}
