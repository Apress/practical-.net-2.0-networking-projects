Imports System.IO

Public Class Form1

    Dim WithEvents serialPort As New IO.Ports.SerialPort

    Const FILE_NAME = "\My Documents\Personal\GPS.dat"
    Private Sub DataReceived( _
           ByVal sender As Object, _
           ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) _
           Handles serialPort.DataReceived

        txtGPSData.BeginInvoke(New _
           myDelegate(AddressOf updateTextBox), _
           New Object() {})
    End Sub

    Public Delegate Sub myDelegate()
    Public Sub updateTextBox()
        Try
            Dim data As String = serialPort.ReadExisting
            txtGPSData.Text = _
               data & _
               txtGPSData.Text
            '---Using a streamWriter to write to a file
            Dim sw As StreamWriter
            sw = New StreamWriter(FILE_NAME, True, System.Text.Encoding.ASCII)
            sw.WriteLine(data)
            sw.Close()
        Catch ex As Exception
            MsgBox("Error")
        End Try

    End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
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

    Private Sub btnDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisconnect.Click
        serialPort.Close()
    End Sub
End Class
