Imports System.Runtime.InteropServices

Public Class Form1
    Private WithEvents serialPort As New IO.Ports.SerialPort
    Private proximity As Integer

    Const WM_CAP_START = &H400S
    Const WS_CHILD = &H40000000
    Const WS_VISIBLE = &H10000000

    Const WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10
    Const WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11
    Const WM_CAP_EDIT_COPY = WM_CAP_START + 30
    Const WM_CAP_SEQUENCE = WM_CAP_START + 62
    Const WM_CAP_FILE_SAVEAS = WM_CAP_START + 23

    Const WM_CAP_SET_SCALE = WM_CAP_START + 53
    Const WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52
    Const WM_CAP_SET_PREVIEW = WM_CAP_START + 50

    Const SWP_NOMOVE = &H2S
    Const SWP_NOSIZE = 1
    Const SWP_NOZORDER = &H4S
    Const HWND_BOTTOM = 1

    '--The capGetDriverDescription function retrieves the version 
    ' description of the capture driver--
    Declare Function capGetDriverDescriptionA Lib "avicap32.dll" _
       (ByVal wDriverIndex As Short, _
        ByVal lpszName As String, ByVal cbName As Integer, _
        ByVal lpszVer As String, _
        ByVal cbVer As Integer) As Boolean

    '--The capCreateCaptureWindow function creates a capture window--
    Declare Function capCreateCaptureWindowA Lib "avicap32.dll" _
       (ByVal lpszWindowName As String, ByVal dwStyle As Integer, _
        ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, _
        ByVal nHeight As Short, ByVal hWnd As Integer, _
        ByVal nID As Integer) As Integer

    '--This function sends the specified message to a window or windows--
    Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
       (ByVal hwnd As Integer, ByVal Msg As Integer, _
        ByVal wParam As Integer, _
       <MarshalAs(UnmanagedType.AsAny)> ByVal lParam As Object) As Integer

    '--Sets the position of the window relative to the screen buffer--
    Declare Function SetWindowPos Lib "user32" Alias "SetWindowPos" _
       (ByVal hwnd As Integer, _
        ByVal hWndInsertAfter As Integer, ByVal x As Integer, _
        ByVal y As Integer, _
        ByVal cx As Integer, ByVal cy As Integer, _
        ByVal wFlags As Integer) As Integer

    '--This function destroys the specified window--
    Declare Function DestroyWindow Lib "user32" _
       (ByVal hndw As Integer) As Boolean

    '---used as a window handle---
    Private hWnd As Integer

    Private Sub Form1_Load( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles MyBase.Load

        If serialPort.IsOpen Then
            serialPort.Close()
        End If
        Try
            With serialPort
                .PortName = "COM3"
                .BaudRate = 9600
                .Parity = IO.Ports.Parity.None
                .DataBits = 8
                .StopBits = IO.Ports.StopBits.One
                .Handshake = IO.Ports.Handshake.None
            End With
            serialPort.Open()
            serialPort.DiscardInBuffer()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        '---preview the selected video source
        PreviewVideo(PictureBox1)
    End Sub

    '---preview the selected video source---
    Private Sub PreviewVideo(ByVal pbCtrl As PictureBox)
        hWnd = capCreateCaptureWindowA(0, _
            WS_VISIBLE Or WS_CHILD, 0, 0, 0, _
            0, pbCtrl.Handle.ToInt32, 0)
        If SendMessage( _
           hWnd, WM_CAP_DRIVER_CONNECT, _
           0, 0) Then
            '---set the preview scale---
            SendMessage(hWnd, WM_CAP_SET_SCALE, True, 0)
            '---set the preview rate (ms)---
            SendMessage(hWnd, WM_CAP_SET_PREVIEWRATE, 30, 0)
            '---start previewing the image---
            SendMessage(hWnd, WM_CAP_SET_PREVIEW, True, 0)
            '---resize window to fit in PictureBox control---
            SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, _
               pbCtrl.Width, pbCtrl.Height, _
               SWP_NOMOVE Or SWP_NOZORDER)
        Else
            '--error connecting to video source---
            DestroyWindow(hWnd)
        End If
    End Sub

    Private Sub DataReceived( _
       ByVal sender As Object, _
       ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) _
       Handles serialPort.DataReceived

        Dim str As String = serialPort.ReadLine
        If str <> String.Empty Then
            proximity = CInt(str)
            ProgressBar1.BeginInvoke(New _
               myDelegate(AddressOf updateControl), _
               New Object() {})
            Console.WriteLine(proximity)
        End If
    End Sub

    Public Delegate Sub myDelegate()
    Public Sub updateControl()
        Try
            If proximity <= 160 Then
                ProgressBar1.Value = proximity
                lblProximity.Text = proximity & " cm"
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnStartRecording_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnStartRecording.Click
        btnStartRecording.Enabled = False
        btnStopRecording.Enabled = True
        Application.DoEvents()
        '---start recording---
        SendMessage(hWnd, WM_CAP_SEQUENCE, 0, 0)

    End Sub

    Private Sub btnStopRecording_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnStopRecording.Click
        btnStartRecording.Enabled = True
        btnStopRecording.Enabled = False
        Application.DoEvents()
        '---save the recording to file---
        SendMessage(hWnd, WM_CAP_FILE_SAVEAS, 0, _
           "C:\" & Now.ToFileTime & ".avi")
    End Sub

    Private Sub btnTakeSnapshot_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnTakeSnapshot.Click

        Dim data As IDataObject
        Dim bmap As Image

        '---copy the image to the clipboard---
        SendMessage(hWnd, WM_CAP_EDIT_COPY, 0, 0)

        '---retrieve the image from clipboard and convert it 
        ' to the bitmap format
        data = Clipboard.GetDataObject()
        If data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then
            bmap = _
               CType(data.GetData(GetType(System.Drawing.Bitmap)), _
               Image)
            bmap.Save("C:\" & Now.ToFileTime & ".bmp")
        End If
    End Sub
End Class
