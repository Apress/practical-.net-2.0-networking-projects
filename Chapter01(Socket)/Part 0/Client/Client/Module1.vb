Imports System.Net.Sockets
Imports System.Text

Module Module1
    Const portNo As Integer = 500
    Sub Main()
        Dim tcpclient As New TcpClient
        '---connect to the server---
        tcpclient.Connect("127.0.0.1", portNo)

        '---use a NetworkStream object to send and receive data---
        Dim ns As NetworkStream = tcpclient.GetStream
        Dim data As Byte() = Encoding.ASCII.GetBytes("Hello")

        '---send the text---
        ns.Write(data, 0, data.Length)
    End Sub
End Module
