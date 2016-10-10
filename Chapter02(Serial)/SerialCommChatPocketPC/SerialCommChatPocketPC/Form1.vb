Public Class Form1
    Private WithEvents serialPort As New IO.Ports.SerialPort

    Private Sub DataReceived( _
           ByVal sender As Object, _
           ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) _
           Handles serialPort.DataReceived

        txtReceivedMessage.BeginInvoke(New _
           myDelegate(AddressOf updateTextBox), _
           New Object() {})
    End Sub

    Public Delegate Sub myDelegate()
    Public Sub updateTextBox()
        txtReceivedMessage.Text = _
           serialPort.ReadExisting & _
           txtReceivedMessage.Text
    End Sub

    Private Sub MenuItem1_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles MenuItem1.Click
        Try
            serialPort.WriteLine(txtMessageToSend.Text)
            txtReceivedMessage.Text = ">" & _
               txtMessageToSend.Text & vbCrLf & _
               txtReceivedMessage.Text
            txtMessageToSend.Text = String.Empty
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnConnect_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnConnect.Click
        Try
            If serialPort.IsOpen Then
                serialPort.Close()
            End If
            With serialPort
                .PortName = cbbCOMPorts.Text
                .BaudRate = 9600
                .Parity = IO.Ports.Parity.None
                .DataBits = 8
                .StopBits = IO.Ports.StopBits.One
            End With
            serialPort.Open()
            MsgBox("Port opened successfully!")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
