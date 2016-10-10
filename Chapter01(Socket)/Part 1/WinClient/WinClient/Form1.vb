Imports System.Net.Sockets

Public Class Form1
    Const portNo As Integer = 500
    Dim client As TcpClient
    Dim data() As Byte

    Private Sub btnSend_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnSend.Click
        SendMessage(txtMessage.Text)
        txtMessage.Clear()
    End Sub

    Public Sub SendMessage(ByVal message As String)
        Try
            '---send a message to the server
            Dim ns As NetworkStream = client.GetStream
            Dim data As Byte() = _
            System.Text.Encoding.ASCII.GetBytes(message)
            '---send the text---
            ns.Write(data, 0, data.Length)
            ns.Flush()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub ReceiveMessage(ByVal ar As IAsyncResult)
        Try
            Dim bytesRead As Integer
            bytesRead = client.GetStream.EndRead(ar)
            If bytesRead < 1 Then
                Exit Sub
            Else
                Dim para() As Object = _
                   {System.Text.Encoding.ASCII.GetString( _
                   data, 0, bytesRead)}
                Me.Invoke(New delUpdateHistory( _
                   AddressOf Me.UpdateHistory), para)
            End If
            client.GetStream.BeginRead( _
               data, 0, CInt(client.ReceiveBufferSize), _
               AddressOf ReceiveMessage, Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSignIn_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnSignIn.Click
        If btnSignIn.Text = "Sign In" Then
            Try
                '---connect to server
                client = New TcpClient
                client.Connect("127.0.0.1", portNo)
                ReDim data(client.ReceiveBufferSize)
                SendMessage(txtNick.Text)
                '---read from server
                client.GetStream.BeginRead( _
                   data, 0, CInt(client.ReceiveBufferSize), _
                   AddressOf ReceiveMessage, Nothing)
                btnSignIn.Text = "Sign Out"
                btnSend.Enabled = True
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            '---disconnect from server
            Disconnect()
            btnSignIn.Text = "Sign In"
            btnSend.Enabled = False
        End If
    End Sub

    Public Sub Disconnect()
        '---Disconnect from server
        Try
            client.GetStream.Close()
            client.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    '---delegate and subroutine to update the TextBox control
    Public Delegate Sub delUpdateHistory(ByVal str As String)
    Public Sub UpdateHistory(ByVal str As String)
        txtMessageHistory.AppendText(str)
    End Sub

    Private Sub Form1_FormClosing( _
       ByVal sender As Object, _
       ByVal e As System.Windows.Forms.FormClosingEventArgs) _
       Handles Me.FormClosing
        Disconnect()
    End Sub
End Class

