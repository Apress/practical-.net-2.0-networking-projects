Imports System.Net.Sockets

Module Module1
    Const portNo As Integer = 500

    Sub Main()
        Dim localAdd As System.Net.IPAddress = _
               System.Net.IPAddress.Parse("127.0.0.1")
        Dim listener As New System.Net.Sockets.TcpListener(localAdd, portNo)
        listener.Start()
        While True
            Dim user As New ChatClient(listener.AcceptTcpClient)
        End While
    End Sub
End Module
