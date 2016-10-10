Imports System.Net.Sockets

Public Class ChatClient
    '---contains a list of all the clients
    Public Shared AllClients As New Hashtable

    '---information about the client
    Private _client As TcpClient
    Private _clientIP As String
    Private _ClientNick As String

    '---used for sending/receiving data
    Private data() As Byte

    '---is the nick name being sent?
    Private ReceiveNick As Boolean = True

    Public Sub New(ByVal client As TcpClient)
        _client = client

        '---get the client IP address
        _clientIP = client.Client.RemoteEndPoint.ToString

        '---add the current client to the hash table
        AllClients.Add(_clientIP, Me)

        '---start reading data from the client in a separate thread
        ReDim data(_client.ReceiveBufferSize)

        _client.GetStream.BeginRead(data, 0, _
           CInt(_client.ReceiveBufferSize), _
           AddressOf ReceiveMessage, Nothing)
    End Sub

    Public Sub SendMessage(ByVal message As String)
        Try
            '---send the text
            Dim ns As System.Net.Sockets.NetworkStream
            SyncLock _client.GetStream
                ns = _client.GetStream
            End SyncLock
            Dim bytesToSend As Byte() = _
                System.Text.Encoding.ASCII.GetBytes(message)
            ns.Write(bytesToSend, 0, bytesToSend.Length)
            ns.Flush()
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Public Sub ReceiveMessage(ByVal ar As IAsyncResult)
        '---read from client---
        Dim bytesRead As Integer
        Try
            SyncLock _client.GetStream
                bytesRead = _client.GetStream.EndRead(ar)
            End SyncLock
            '---client has disconnected
            If bytesRead < 1 Then
                AllClients.Remove(_clientIP)
                Broadcast(_ClientNick & " has left the chat.")
                Exit Sub
            Else
                '---get the message sent
                Dim messageReceived As String = _
                    System.Text.Encoding.ASCII. _
                    GetString(data, 0, bytesRead)
                '---client is sending its nickname
                If ReceiveNick Then
                    _ClientNick = messageReceived

                    '---tell everyone client has entered the chat
                    Broadcast(_ClientNick & " has joined the chat.")
                    ReceiveNick = False
                Else
                    '---broadcast the message to everyone
                    Broadcast(_ClientNick & ">" & messageReceived)
                End If
            End If
            '---continue reading from client
            SyncLock _client.GetStream
                _client.GetStream.BeginRead(data, 0, _
                   CInt(_client.ReceiveBufferSize), _
                   AddressOf ReceiveMessage, Nothing)
            End SyncLock
        Catch ex As Exception
            AllClients.Remove(_clientIP)
            Broadcast(_ClientNick & " has left the chat.")
        End Try
    End Sub

    Public Sub Broadcast(ByVal message As String)
        '---log it locally
        Console.WriteLine(message)
        Dim c As DictionaryEntry
        For Each c In AllClients
            '---broadcast message to all users
            CType(c.Value, ChatClient).SendMessage(message & vbLf)
        Next
    End Sub
End Class
