Imports System.Net.Sockets

'---class to contain information of each client
Public Class ChatClient

    Private Const LF As Integer = 10

    '---contains a list of all the clients
    Public Shared AllClients As New Hashtable

    '---information about the client
    Private _client As TcpClient
    Private _clientIP As String
    Private _clientNick As String

    '---used for sending/receiving data
    Private data() As Byte

    '***************************************************************************************************
    Private partialStr As String
    '***************************************************************************************************

    '---when a client is connected
    Public Sub New(ByVal client As TcpClient)
        _client = client

        '---get the client IP address
        _clientIP = client.Client.RemoteEndPoint.ToString

        '---add the current client to the hash table
        AllClients.Add(_clientIP, Me)

        '---start reading data from the client in a separate thread
        ReDim data(_client.ReceiveBufferSize - 1)
        _client.GetStream.BeginRead(data, 0, _
           CInt(_client.ReceiveBufferSize), _
           AddressOf ReceiveMessage, Nothing)
    End Sub

    '---send the message to the client
    Public Sub SendMessage(ByVal message As String)
        Try
            '---send the text
            Dim ns As System.Net.Sockets.NetworkStream

            SyncLock _client.GetStream
                ns = _client.GetStream
                Dim bytesToSend As Byte() = _
                    System.Text.Encoding.ASCII.GetBytes(message)
                ns.Write(bytesToSend, 0, bytesToSend.Length)
                ns.Flush()
            End SyncLock

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    '---receiving a message from the client
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
                Broadcast("[Left][" & _clientNick & _
                   "] has left the chat.", Nothing)
                Exit Sub
            Else
                '***************************************************************************************************
                Dim messageReceived As String
                Dim i As Integer = 0
                Dim start As Integer = 0
                '---loop until no more chars---
                While data(i) <> 0

                    '---do not scan more than what is read---
                    If i + 1 > bytesRead Then Exit While

                    '---if LF is detected---
                    If data(i) = LF Then
                        messageReceived = _
                           partialStr & _
                           System.Text.Encoding.ASCII.GetString(data, start, i - start)
                        '------------------------------------------------------------------------
                        Console.WriteLine("received <----- " & messageReceived)
                        If messageReceived.StartsWith("[Join]") Then
                            '====client is sending its nickname====
                            'e.g. [Join][User1]
                            '---extract user's name---
                            Dim nameLength As Integer = messageReceived.IndexOf("]", 6)
                            _clientNick = messageReceived.Substring(7, nameLength - 7)
                            '---tell everyone client has entered the chat---
                            Broadcast(messageReceived, Nothing)

                        ElseIf messageReceived.StartsWith("[Usrs]") Then
                            '===client is requesting for all users names===
                            ' e.g. [Usrs]
                            '---get all the users---
                            Dim allUsers As String = "[Usrs]["
                            Dim c As DictionaryEntry
                            For Each c In AllClients
                                '---get all the users' name---
                                allUsers += _
                                   CType(c.Value, ChatClient)._clientNick & ","
                            Next
                            allUsers += "]"
                            'e.g. [Usrs][User1,User2,etc]
                            Broadcast(allUsers, Nothing)

                        ElseIf messageReceived.StartsWith("[Talk]") Then
                            '===Chatting with someone===
                            'e.g. [Talk][User2,User3]User1>Hello everyone!
                            '---get all users---
                            Dim users() As String = _
                               messageReceived.Substring(7, _
                               messageReceived.IndexOf("]", 7) - 8).Split(",")
                            '---send to specified users---
                            Broadcast(messageReceived, users)

                        ElseIf messageReceived.StartsWith("[File]") Then
                            '===FTP request===
                            'e.g. [File][User1,User2][Filename.txt]
                            '---get all users---
                            Dim users() As String = _
                               messageReceived.Substring(7, _
                               messageReceived.IndexOf("]", 7) - 8).Split(",")
                            Dim index As Integer = _
                               messageReceived.IndexOf("]", 7) + 2
                            Dim filename As String = _
                               messageReceived.Substring(index, _
                               messageReceived.Length - index - 1)
                            '---see who initiated the request---
                            Dim from As String = users(0)
                            '---remove the first user (initiator)---
                            For j As Integer = 1 To users.Length - 1
                                users(j - 1) = users(j)
                            Next
                            users(users.Length - 1) = String.Empty
                            '---send to user---
                            'e.g. [File][User1][Filename.txt]
                            Broadcast("[File][" & from & "][" & filename & "]", users)

                        ElseIf messageReceived.StartsWith("[Send_File]") Then
                            '===send file via FTP===
                            ' e.g. [Send_File][User1,User2]
                            '---send file from User1 to User2---
                            '---check send to who---
                            Dim users() As String = _
                               messageReceived.Substring(12, _
                               messageReceived.IndexOf("]", 12) - 12).Split(",")

                            Dim RecipientIP As String = String.Empty
                            '---find out the recipient's IP address---
                            Dim c As DictionaryEntry
                            For Each c In AllClients
                                If CType(c.Value, ChatClient)._clientNick = users(1) Then
                                    '---send message to user---
                                    RecipientIP = CType(c.Value, ChatClient). _
                                       _clientIP.Substring(0, _clientIP.IndexOf(":"))
                                    Exit For
                                End If
                            Next
                            users(1) = String.Empty
                            'e.g. [Send_File][1.2.3.4]
                            Broadcast("[Send_File][" & RecipientIP & "]", users)
                        End If
                        '------------------------------------------------------------------------
                        start = i + 1
                    End If
                    i += 1
                End While
                '---partial string---
                If start <> i Then
                    partialStr = System.Text.Encoding.ASCII.GetString(data, start, i - start)
                End If
                '***************************************************************************************************
            End If
            '---continue reading from client
            SyncLock _client.GetStream
                _client.GetStream.BeginRead(data, 0, _
                CInt(_client.ReceiveBufferSize), _
                AddressOf ReceiveMessage, Nothing)
            End SyncLock
        Catch ex As Exception
            AllClients.Remove(_clientIP)
            Broadcast("[Left][" & _clientNick & _
                   "] has left the chat.", Nothing)
        End Try
    End Sub

    '---broadcast message to selected users
    Public Sub Broadcast(ByVal message As String, ByVal users() As String)

        If users Is Nothing Then
            '---broadcasting to everyone
            Dim c As DictionaryEntry
            For Each c In AllClients
                '---broadcast message to all users
                CType(c.Value, ChatClient).SendMessage(message & vbLf)
            Next
        Else
            '---broadcasting to selected ones
            Dim c As DictionaryEntry
            For Each c In AllClients
                Dim user As String
                For Each user In users
                    If CType(c.Value, ChatClient)._clientNick = user Then
                        '---send message to user
                        CType(c.Value, ChatClient).SendMessage(message & vbLf)
                        '---log it locally
                        Console.WriteLine("sending -----> " & message)
                        Exit For
                    End If
                Next
            Next
        End If
    End Sub
End Class
