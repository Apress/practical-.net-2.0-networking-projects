Imports System.Net.Sockets
Imports System.IO

Public Class Form1

    '***************************************************************************************************
    Private partialStr As String
    '***************************************************************************************************

    '---get own IP address
    Private ips As Net.IPHostEntry = _
       Net.Dns.GetHostEntry(Net.Dns.GetHostName())

    '---port nos and server IP address
    Const PORTNO As Integer = 500
    Const FTPPORTNO As Integer = 501
    Const SERVERIP As String = "127.0.0.1"
    'Const SERVERIP As String = "10.0.1.2"

    Private client As TcpClient
    '--used for sending and receiving data
    Private data() As Byte

    '---for FTP use 
    Private fs As System.IO.FileStream
    Private filename As String
    Private fullfilename As String

    '---Send Button
    Private Sub btnSend_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnSend.Click
        ' e.g. [Talk][User2,User3,etc]User1>Hello world!

        '---select users to chat
        If lstUsers.SelectedItems.Count < 1 Then
            MsgBox("You must select who to chat with.")
            Exit Sub
        End If

        '---formulate the message
        Dim Message As String = "[Talk]["

        '---check who to chat with
        Dim user As Object
        For Each user In lstUsers.SelectedItems
            Message += user & ","
        Next
        Message += "]" & txtNick.Text & ">" & txtMessage.Text

        '---update the message history
        txtMessageHistory.Text += txtNick.Text & _
           ">" & txtMessage.Text & vbCrLf

        '---send message
        SendMessage(Message)
        txtMessage.Clear()
    End Sub

    '---Sends the message to the server
    Public Sub SendMessage(ByVal message As String)
        '---adds a carriage return char---
        message += vbLf
        Try
            '---send the text
            Dim ns As System.Net.Sockets.NetworkStream
            SyncLock client.GetStream
                ns = client.GetStream
                Dim bytesToSend As Byte() = _
                   System.Text.Encoding.ASCII.GetBytes(message)

                '---sends the text---
                ns.Write(bytesToSend, 0, bytesToSend.Length)
            End SyncLock
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    '---Receives a message from the server
    Public Sub ReceiveMessage(ByVal ar As IAsyncResult)
        Try
            Dim bytesRead As Integer
            bytesRead = client.GetStream.EndRead(ar)
            If bytesRead < 1 Then
                Exit Sub
            Else
                '******************************************************************************
                'Dim messageReceived As String = _
                '   System.Text.Encoding.ASCII.GetString( _
                '   data, 0, bytesRead)
                ''---update the message history
                'Dim para() As Object = {messageReceived}
                'Me.Invoke(New delUpdateHistory(AddressOf Me.UpdateHistory), para)
                '------------------------------------------------------------------------------

                'e.g. [Talk][user1,]user2>Hello*LF*[Talk][user2,]user1>Hello back*LF**0*
                'e.g. [Talk][user1,]user2>Hello*LF*[Talk]*0*
                'e.g. [user2,]user1>Hello back*LF**0*

                '***************************************************************************************************
                Dim messageReceived As String
                Dim i As Integer = 0
                Dim start As Integer = 0
                '---loop until no more chars---
                While data(i) <> 0

                    '---do not scan more than what is read---
                    If i + 1 > bytesRead Then Exit While

                    '---if LF is detected---
                    If data(i) = 10 Then
                        messageReceived = _
                           partialStr & _
                           System.Text.Encoding.ASCII.GetString(data, start, i - start) & _
                           vbCrLf
                        '---update the message history
                        Dim para() As Object = {messageReceived}
                        Me.Invoke(New delUpdateHistory(AddressOf Me.UpdateHistory), para)
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

            '---continue reading for more data
            client.GetStream.BeginRead(data, 0, _
               CInt(client.ReceiveBufferSize), _
               AddressOf ReceiveMessage, Nothing)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    '--Sign in to server---
    Private Sub btnSignIn_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnSignIn.Click
        If btnSignIn.Text = "Sign In" Then

            '---Sign in to the server
            Try
                client = New TcpClient
                ' client.NoDelay = True

                '---connect to the server
                client.Connect(SERVERIP, PORTNO)
                ReDim data(client.ReceiveBufferSize - 1)
                '---inform the server of your nick name---
                ' e.g. [Join][User1]
                SendMessage("[Join][" & txtNick.Text & "]")

                '---begin reading data asynchronously from the server
                client.GetStream.BeginRead( _
                   data, 0, CInt(client.ReceiveBufferSize), _
                   AddressOf ReceiveMessage, Nothing)

                '---change the button and textbox
                btnSignIn.Text = "Sign Out"
                btnSend.Enabled = True
                txtNick.Enabled = False

                '---get all users connected
                ' e.g. [Usrs]
                System.Threading.Thread.Sleep(500)
                SendMessage("[Usrs]")

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            '---Sign off from the server
            Disconnect()
            lstUsers.Items.Clear()

            '---change the button and textbox
            btnSignIn.Text = "Sign In"
            btnSend.Enabled = False
            txtNick.Enabled = True
        End If
    End Sub

    '---disconnect from the server
    Public Sub Disconnect()
        Try
            client.GetStream.Close()
            client.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    '---delegate to update the textboxes in the main thread
    Public Delegate Sub delUpdateHistory(ByVal str As String)
    Public Sub UpdateHistory(ByVal str As String)

        If str.StartsWith("[Join]") Then
            'e.g. [Join][User1]

            '---extract user's name
            Dim nameLength As Integer = str.IndexOf("]", 6)

            '---display in the ListBox
            lstUsers.Items.Add(str.Substring(7, nameLength - 7))
            Exit Sub

        ElseIf str.StartsWith("[Left]") Then
            'e.g. [Left][User1]

            '---extract user's name
            Dim nameLength As Integer = str.IndexOf("]", 6)

            '---remove the user from the listbox
            Try
                lstUsers.Items.RemoveAt( _
                   lstUsers.Items.IndexOf( _
                   str.Substring(7, nameLength - 7)))
            Catch ex As Exception
            End Try
            Exit Sub

        ElseIf str.StartsWith("[Usrs]") Then
            'e.g. [Usrs][User1,User2,User3,etc]

            '---extract the user names
            Dim users() As String = _
               str.Substring(7, str.Length - 8).Split(",")

            Dim user As String
            lstUsers.Items.Clear()
            '---add the user to ListBox
            For Each user In users
                lstUsers.Items.Add(user)
            Next
            '---remove the last empty user
            lstUsers.Items.RemoveAt(lstUsers.Items.Count - 1)
            Exit Sub

        ElseIf str.StartsWith("[File]") Then
            'e.g. [File][User1][Filename.ext]

            '---get user name
            Dim users() As String = _
               str.Substring(7, str.IndexOf("]", 7) - 7).Split(",")

            '---extract file name
            Dim index As Integer = str.IndexOf("]", 7) + 2
            Dim filename As String = str.Substring(index, str.Length - index - 3)

            '---prompt the user
            Dim response As MsgBoxResult
            response = MsgBox("Do you want to download the file " & filename, MsgBoxStyle.YesNo)

            '---proceed with download
            If response = MsgBoxResult.Yes Then
                '---tell the client that he can proceed to send the file
                ' e.g. [Send_File][User1,User2]
                SendMessage("[Send_File][" & users(0) & "," & txtNick.Text & "]")

                '---start the FTP process
                FTP_Receive(filename)
            End If
            Exit Sub

        ElseIf str.StartsWith("[Send_File]") Then
            'e.g. [Send_File][1.2.3.4]

            '---extract the IP address of file recipient
            Dim userIP As String = str.Substring(12, str.Length - 15)
            '---start the FTP process
            FTP_Send(fullfilename, userIP)
            Exit Sub

        ElseIf str.StartsWith("[Talk]") Then

            ' Dim users() As String = str.Substring(7, str.IndexOf("]", 7) - 7).Split(",")
            ' lstUsers.SelectedItem = lstUsers.Items.IndexOf(users(0))

            '---display the message in the textbox
            str = str.Substring(str.IndexOf("]", 7) + 1)
            txtMessageHistory.AppendText(str)
        End If

    End Sub

    Private Sub Form1_FormClosing( _
       ByVal sender As Object, _
       ByVal e As System.Windows.Forms.FormClosingEventArgs) _
       Handles Me.FormClosing
        Disconnect()
    End Sub

    '---Send File button
    Private Sub btnFTP_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) Handles btnFTP.Click

        '---formulate the message
        '---e.g. [FILE][User1,User2,User3,][Filename.ext]
        Dim Message As String = "[File][" & txtNick.Text & ","
        Dim user As Object
        If lstUsers.SelectedItems.Count < 1 Then
            MsgBox("You must select who to send to.")
            Exit Sub
        End If

        '---check who to send to
        For Each user In lstUsers.SelectedItems
            Message += user & ","
        Next

        '---select the file to send
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            fullfilename = openFileDialog1.FileName
            filename = fullfilename.Substring(fullfilename.LastIndexOf("\") + 1)

            Message += "][" & filename & "]"
            SendMessage(Message)
        End If
        '----------------------------
    End Sub

    '---FTP process - Send file
    Public Sub FTP_Send( _
       ByVal filename As String, _
       ByVal recipientIP As String)

        '---connect to the recipient
        Dim tcpClient As New System.Net.Sockets.TcpClient
        tcpClient.Connect(recipientIP, FTPPORTNO)
        Dim BufferSize As Integer = tcpClient.ReceiveBufferSize
        Dim nws As NetworkStream = tcpClient.GetStream

        '---open the file
        Dim fs As FileStream
        fs = New FileStream(filename, FileMode.Open, _
                            FileAccess.Read)

        Dim bytesToSend(fs.Length - 1) As Byte
        Dim numBytesRead As Integer = fs.Read(bytesToSend, _
                                      0, bytesToSend.Length)

        Dim totalBytes As Integer = 0
        For i As Integer = 0 To fs.Length \ BufferSize
            '---send the file
            If fs.Length - (i * BufferSize) > BufferSize Then
                nws.Write(bytesToSend, i * BufferSize, BufferSize)
                totalBytes += BufferSize
            Else
                nws.Write(bytesToSend, i * _
                   BufferSize, fs.Length - (i * BufferSize))
                totalBytes += fs.Length - (i * BufferSize)
            End If
            '--update the status label
            ToolStripStatusLabel1.Text = _
               "Sending " & totalBytes & " bytes...."
            Application.DoEvents()
        Next
        ToolStripStatusLabel1.Text = _
           "Sending " & totalBytes & " bytes....Done."
        fs.Close()
        tcpClient.Close()
    End Sub

    '---FTP Process = Receive Files
    Public Sub FTP_Receive(ByVal filename As String)

        Try
            '---get the local IP address
            Dim localAdd As System.Net.IPAddress = _
               System.Net.IPAddress.Parse(ips.AddressList(0).ToString)

            '---start listening for incoming connection
            Dim listener As New _
               System.Net.Sockets.TcpListener(localAdd, FTPPORTNO)
            listener.Start()

            '---read incoming stream
            Dim tcpClient As TcpClient = listener.AcceptTcpClient()
            Dim nws As NetworkStream = tcpClient.GetStream

            '---delete the file if it exists
            If File.Exists("c:\temp\" & filename) Then
                File.Delete("c:\temp\" & filename)
            End If

            '---create the file
            fs = New System.IO.FileStream("c:\temp\" & filename, _
               FileMode.Append, FileAccess.Write)

            Dim counter As Integer = 0
            Dim totalBytes As Integer = 0
            Do
                '---read the incoming data
                Dim bytesRead As Integer = _
                    nws.Read(data, 0, tcpClient.ReceiveBufferSize)
                totalBytes += bytesRead
                fs.Write(data, 0, bytesRead)

                '--update the status label
                ToolStripStatusLabel1.Text = "Receiving " & totalBytes & " bytes...."
                Application.DoEvents()
                counter += 1
            Loop Until Not nws.DataAvailable
            ToolStripStatusLabel1.Text = "Receiving " & totalBytes & " bytes....Done."
            fs.Close()
            tcpClient.Close()
            listener.Stop()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

End Class

