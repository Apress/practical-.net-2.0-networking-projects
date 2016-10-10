Imports System.Net
Imports System.IO
Imports System.Net.Sockets

Public Class Form1
    Inherits System.Windows.Forms.Form

    '---define the constants---
    Const MAX_MESSAGE_SIZE As Integer = 1024
    Const MAX_TRIES As Integer = 3

    Private ServiceName As String = "default"

    '---SEND menu item---
    Private Sub mnuSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSend.Click
        mnuSend.Enabled = False
        sendMessage(MAX_TRIES, txtMessage.Text)
        mnuSend.Enabled = True
        txtMessage.Text = String.Empty
        txtMessage.Focus()
    End Sub

    Private Sub SendMessage(ByVal NumRetries As Integer, ByVal str As String)

        Dim client As IrDAClient = Nothing
        Dim CurrentTries As Integer = 0

        '---try to establish a connection---
        Do
            Try
                client = New IrDAClient(ServiceName)
            Catch se As Exception
                If (CurrentTries >= NumRetries) Then
                    Throw se
                End If
            End Try
            CurrentTries = CurrentTries + 1
        Loop While client Is Nothing And CurrentTries < NumRetries

        '---timeout occurred---
        If (client Is Nothing) Then
            StatusBar1.BeginInvoke( _
               New myDelegate(AddressOf UpdateStatus), New Object() _
               {"Error establishing contact"})
            Return
        End If

        '---send the message over a stream object---
        Dim stream As System.IO.Stream = Nothing
        Try
            stream = client.GetStream()
            stream.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(str), 0, str.Length)
            '---update the status bar---
            StatusBar1.BeginInvoke( _
               New myDelegate(AddressOf UpdateStatus), New Object() _
               {"Message sent!"})
            '---display the message that was sent---
            txtMessagesArchive.Text = str & vbCrLf & txtMessagesArchive.Text
        Catch e As Exception
            StatusBar1.BeginInvoke( _
               New myDelegate(AddressOf UpdateStatus), New Object() _
               {"Error sending message."})
        Finally
            If (Not stream Is Nothing) Then stream.Close()
            If (Not client Is Nothing) Then client.Close()
        End Try
    End Sub

    Private Function ReceiveMessage() As String
        Dim bytesRead As Integer = 0
        Dim listener As IrDAListener = New IrDAListener(ServiceName)
        Dim client As IrDAClient = Nothing
        Dim stream As System.IO.Stream = Nothing
        Dim Buffer(MAX_MESSAGE_SIZE - 1) As Byte
        Dim str As String = String.Empty
        Try
            listener.Start()
            client = listener.AcceptIrDAClient()  '---blocking call---
            stream = client.GetStream()
            bytesRead = stream.Read(Buffer, 0, Buffer.Length)
            '---display the received message---
            str = ">" & System.Text.ASCIIEncoding.ASCII.GetString(Buffer, 0, bytesRead)
        Catch ex As SocketException
            '---ignore error---
        Catch e As Exception
            StatusBar1.BeginInvoke( _
               New myDelegate(AddressOf UpdateStatus), New Object() _
               {e.ToString})
        Finally
            If (Not stream Is Nothing) Then stream.Close()
            If (Not client Is Nothing) Then client.Close()
            listener.Stop()
        End Try
        Return str
    End Function

    Public Sub ReceiveLoop()
        Dim strReceived As String
        strReceived = ReceiveMessage()
        '---keep on listening for new message
        While True
            If strReceived <> String.Empty Then
                txtMessagesArchive.BeginInvoke( _
                New myDelegate(AddressOf UpdateTextBox), New Object() {strReceived})
            End If
            strReceived = ReceiveMessage()
        End While
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtMessage.Focus()
        '---receive incoming messages as a separate thread---
        Dim t1 As System.Threading.Thread
        t1 = New Threading.Thread(AddressOf receiveLoop)
        t1.Start()
    End Sub

    Private Delegate Sub myDelegate(ByVal str As String)
    Private Sub UpdateTextBox(ByVal str As String)
        '---delegate to update the textbox control
        txtMessagesArchive.Text = str & vbCrLf & txtMessagesArchive.Text
    End Sub

    Private Sub UpdateStatus(ByVal str As String)
        '---delegate to update the statusbar control
        StatusBar1.Text = str
    End Sub

End Class
