Imports System.Net.Sockets
Imports System.Text

Module Module1
    '---port number to use for listening---
    Const portNo As Integer = 500

    Sub Main()
        Dim localAdd As System.Net.IPAddress = _
            System.Net.IPAddress.Parse("127.0.0.1")

        '---listen at the local address---
        Dim listener As New TcpListener(localAdd, portNo)
        listener.Start()

        '---Accepts a pending connection request---
        Dim tcpClient As TcpClient = listener.AcceptTcpClient()

        '---use a NetworkStream object to send and receive data---
        Dim ns As NetworkStream = tcpClient.GetStream
        Dim data(tcpClient.ReceiveBufferSize) As Byte

        '---read incoming stream; Read() is a blocking call---
        Dim numBytesRead As Integer = ns.Read(data, 0, _
            CInt(tcpClient.ReceiveBufferSize))

        '---display data received---
        Console.WriteLine("Received :" & _
            Encoding.ASCII.GetString(data, 0, numBytesRead))

        '---prevent the console window from closing immediately---
        Console.ReadLine()
    End Sub

End Module
