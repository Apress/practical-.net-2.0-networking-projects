<System.Runtime.InteropServices.ComVisibleAttribute(True)> _
Public Class Form1

    '---index of the push pin---
    Private pushpin As Integer = 0

    '---keeping track of the points---
    Private pointCounter As Integer

    '--used for remembering the lines read 
    ' from a file containing coordinates---
    Private lineIndex As Integer = 0
    Dim line() As String

    '---serial port for communicating with GPS receiver---
    Private WithEvents serialPort As New IO.Ports.SerialPort

    Private Sub Form1_Load( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles MyBase.Load

        '---display the available COM port on the computer---
        For i As Integer = 0 To _
           My.Computer.Ports.SerialPortNames.Count - 1
            cbbCOMPorts.Items.Add( _
               My.Computer.Ports.SerialPortNames(i))
        Next

        '--Load the Webbrowser control with the Virtual Earth Map---
        Dim fileContents As String

        '---remember to set the Copy to Output Directory 
        ' property of Map.html to "Copy if newer"
        fileContents = My.Computer.FileSystem.ReadAllText( _
           Application.StartupPath & "\Map.html")

        WebBrowser1.DocumentText = fileContents
        WebBrowser1.ObjectForScripting = Me
    End Sub

    '---clear the plotted path by removing all pushpins---
    Private Sub btnClearPath_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnClearPath.Click
        For i As Integer = 0 To pushpin
            removePushpin(i)
        Next
    End Sub

    '---plot a path from a GPS data file---
    Private Sub btnShowPath_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnShowPath.Click

        Dim fileContents As String = String.Empty
        '---let user choose a file---
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        '---Load the content of the selected file---
        If openFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            fileContents = My.Computer.FileSystem.ReadAllText(openFileDialog1.FileName)
        End If

        '---split the content various lines using the $ as the delimiter---
        line = fileContents.Split("$")
        lineIndex = 0
        Timer1.Enabled = True
    End Sub

    '---go to a particular location on the map---
    Private Sub gotoPosition( _
       ByVal lat As Double, _
       ByVal lng As Double, _
       ByVal showPushpin As Boolean, _
       ByVal pushPinText As String)

        '---display map at specific location---
        Dim param() As Object = New Object() {lat, lng}
        WebBrowser1.Document.InvokeScript("goto_map_position", param)

        '---if need to insert pushpin---
        If showPushpin Then
            '---set the push pin---
            param = New Object() {pushpin, pushPinText, lat, lng}
            WebBrowser1.Document.InvokeScript("addPushpin", param)
            pushpin += 1
        End If
    End Sub

    '---update the latitude and longitude on the TextBox controls---
    Public Sub mapPositionChange(ByVal lat As Double, ByVal lng As Double)
        txtLatitude.Text = lat
        txtLongitude.Text = lng
    End Sub

    '---remove a pushpin---
    Private Sub removePushpin(ByVal id As Integer)
        Dim param() As Object = New Object() {id}
        WebBrowser1.Document.InvokeScript("removePushpin", param)
    End Sub

    '---set the map to a particular location---
    Private Sub btnGotoPoint_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnGotoPoint.Click

        Dim lat, lng As Double
        '---get the latitude and longitude---
        lat = txtLatitude.Text
        lng = txtLongitude.Text
        '        gotoPosition(lat, lng, False, "")
        gotoPosition(lat, lng, True, "X")
    End Sub

    '---for ploting a path---
    Private Sub Timer1_Tick( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles Timer1.Tick

        If lineIndex = 0 Then pointCounter = 1
        '---plot a point in the path---
        While (lineIndex <= line.Length - 1)
            If line(lineIndex).StartsWith("GPGGA") AndAlso _
               processGPSData(line(lineIndex)) Then
                lblMessage.Text = "Updating map...point " & pointCounter
                pointCounter += 1
                Exit While
            End If
            lineIndex += 1
        End While
        lineIndex += 1
        '---stop the Timer control when the end of the path is reached---
        If lineIndex > line.Length - 1 Then
            Timer1.Enabled = False
            lblMessage.Text = "Plotting completed."
        End If
    End Sub

    '---connect to a serial port to communicate with the GPS receiver---
    Private Sub btnConnect_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnConnect.Click
        If btnConnect.Text = "Connect" Then
            btnConnect.Text = "Disconnect"
            If serialPort.IsOpen Then
                serialPort.Close()
            End If
            Try
                With serialPort
                    .PortName = cbbCOMPorts.Text
                    .BaudRate = 9600
                    .Parity = IO.Ports.Parity.None
                    .DataBits = 8
                    .StopBits = IO.Ports.StopBits.One
                End With
                serialPort.Open()
                lblMessage.Text = cbbCOMPorts.Text & " connected."
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            btnConnect.Text = "Connect"
            serialPort.Close()
        End If
    End Sub

    Private Sub DataReceived( _
       ByVal sender As Object, _
       ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) _
       Handles serialPort.DataReceived
        txtDataReceived.BeginInvoke(New _
           myDelegate(AddressOf updateTextBox), _
           New Object() {})
    End Sub

    Public Delegate Sub myDelegate()
    Public Sub updateTextBox()
        Try
            '---for receiving plain ASCII text---
            With txtDataReceived
                Dim Data As String = serialPort.ReadExisting
                .AppendText(Data)
                .ScrollToCaret()
                '---process only lines starting with $GPGGA---
                Dim GPSData As String = txtDataReceived.Lines( _
                   txtDataReceived.Lines.Length - 2)
                If GPSData.StartsWith("$GPGGA") Then
                    If Not processGPSData(GPSData) Then
                        lblMessage.Text = "No fix..."
                    End If
                End If
            End With
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Function processGPSData(ByVal str As String) As Boolean
        'SAMPLE - $GPGGA,092204.999,4250.5589,S,14718.5084,E,1,04,24.4,19.7,M,,,,0000*1F
        '            0       1           2    3       4    5 6  7  8    
        ' str = "$GPGGA,092204.999,4250.5589,S,14718.5084,E,1,04,24.4,19.7,M,,,,0000*1F"
        Try
            '---separate the GPS data into various fields---
            Dim field() As String
            field = str.Split(",")
            Dim lat, lng As Double
            Dim rawLatLng As Double
            If field.Length < 15 Then Return False

            '---latitude---
            rawLatLng = Convert.ToDouble(field(2))
            lat = (rawLatLng \ 100) + _
                 ((rawLatLng - ((rawLatLng \ 100) * 100)) / 60)

            '---latitude is negative if South---
            If field(3) = "S" Then
                lat *= -1.0
            End If

            '---longitude---
            rawLatLng = Convert.ToDouble(field(4))
            lng = (rawLatLng \ 100) + _
                 ((rawLatLng - ((rawLatLng \ 100) * 100)) / 60)

            '---longitude is negative if West---
            If field(5) = "W" Then
                lng *= -1.0
            End If

            '---update map---
            If str.StartsWith("$") Then
                '---live data from GPS---
                gotoPosition(lat, lng, False, "")
            Else
                '---recorded path---
                gotoPosition(lat, lng, True, "*")
            End If

            lblMessage.Text = "Latitude: " & lat & " Longitude: " & lng
            Return True
        Catch
            Return False
        End Try
    End Function

End Class
