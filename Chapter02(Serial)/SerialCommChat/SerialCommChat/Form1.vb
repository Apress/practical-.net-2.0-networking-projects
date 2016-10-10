Public Class Form1
    Private WithEvents serialPort As New IO.Ports.SerialPort

    Private Sub Form1_Load( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles MyBase.Load

        For i As Integer = 0 To _
           My.Computer.Ports.SerialPortNames.Count - 1
            cbbCOMPorts.Items.Add( _
               My.Computer.Ports.SerialPortNames(i))
        Next
        btnDisconnect.Enabled = False
    End Sub

    Private Sub DataReceived( _
       ByVal sender As Object, _
       ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) _
       Handles serialPort.DataReceived

        txtDataReceived.BeginInvoke(New _
                       myDelegate(AddressOf updateTextBox), _
                       New Object() {})
    End Sub

    Private Sub btnSend_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnSend.Click
        Try
            serialPort.Write(txtDataToSend.Text & vbCrLf)
            'serialPort.Write(txtDataToSend.Text)


            With txtDataReceived
                .AppendText(">" & txtDataToSend.Text & vbCrLf)
                .ScrollToCaret()
            End With
            txtDataToSend.Text = String.Empty
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Delegate Sub myDelegate()
    Public Sub updateTextBox()
        '---for receiving plan ASCII text---
        'With txtDataReceived
        '    .AppendText(serialPort.ReadExisting)
        '    .ScrollToCaret()
        'End With

        '---UNICODE work-around---
        With txtDataReceived
            '---find out the number of bytes to read---
            Dim bytesToRead As Integer = serialPort.BytesToRead
            '---declare a char array---
            Dim ch(bytesToRead) As Char
            '---read the bytes into the ch array---
            Dim bytesRead As Integer = 0
            bytesRead = serialPort.Read(ch, 0, bytesToRead)
            '---convert the ch array into a string---
            Dim str As String = New String(ch, 0, bytesRead)
            .AppendText(str)
            .ScrollToCaret()
        End With
    End Sub

    Private Sub btnConnect_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnConnect.Click
        If serialPort.IsOpen Then
            serialPort.Close()
        End If
        Try
            With serialPort
                .PortName = cbbCOMPorts.Text
                .BaudRate = 2400 ' 9600
                .Parity = IO.Ports.Parity.None
                .DataBits = 8
                .StopBits = IO.Ports.StopBits.One
                .Handshake = IO.Ports.Handshake.None
                '  .Encoding = System.Text.Encoding.Unicode
            End With
            serialPort.Open()

            lblMessage.Text = cbbCOMPorts.Text & " connected."
            btnConnect.Enabled = False
            btnDisconnect.Enabled = True
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnDisconnect_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnDisconnect.Click
        Try
            serialPort.Close()
            lblMessage.Text = serialPort.PortName & " disconnected."
            btnConnect.Enabled = True
            btnDisconnect.Enabled = False
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnDialNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDialNumber.Click
        serialPort.Write("ATDT " & txtPhoneNumber.Text & vbCrLf)
    End Sub

    Private Sub btnAnswerCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnswerCall.Click
        serialPort.Write("AT*EVA" & vbCrLf)
    End Sub
End Class
