Imports GrFingerXLib

Public Class Form1
    '---name of the database---
    Const DBFile = "GrFingerSample.mdb"
    Const Logfile = "C:\Log.csv"
    Const ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="

    '---for an instance of the Util.vb class---
    Private myUtil As Util
    '---for storing user’s ID---
    Private _UserID As Integer
    '---database connection string---
    Private connection As System.Data.OleDb.OleDbConnection

    Private Sub Form1_Load( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles MyBase.Load
        Dim err As Integer
        ' initialize util class
        myUtil = New Util(ListBox1, PictureBox1, AxGrFingerXCtrl1)
        ' Initialize GrFingerX Library
        err = myUtil.InitializeGrFinger()
        ' Print result in log
        If err < 0 Then
            myUtil.WriteError(err)
            Exit Sub
        Else
            myUtil.WriteLog( _
               "**GrFingerX Initialized Successfull**")
        End If

        '---create a log file---
        If Not System.IO.File.Exists(Logfile) Then
            System.IO.File.Create(Logfile)
        End If
    End Sub

    ' -----------------------------------------------------------------------------------
    ' GrFingerX events
    ' -----------------------------------------------------------------------------------
    ' A fingerprint reader was plugged on system
    Private Sub AxGrFingerXCtrl1_SensorPlug( _
       ByVal sender As System.Object, _
       ByVal e As AxGrFingerXLib._IGrFingerXCtrlEvents_SensorPlugEvent) _
       Handles AxGrFingerXCtrl1.SensorPlug
        myUtil.WriteLog("Sensor: " & e.idSensor & ". Event: Plugged.")
        AxGrFingerXCtrl1.CapStartCapture(e.idSensor)
    End Sub

    ' A fingerprint reader was unplugged from system
    Private Sub AxGrFingerXCtrl1_SensorUnplug( _
       ByVal sender As System.Object, _
       ByVal e As AxGrFingerXLib._IGrFingerXCtrlEvents_SensorUnplugEvent) _
       Handles AxGrFingerXCtrl1.SensorUnplug
        myUtil.WriteLog("Sensor: " & e.idSensor & ". Event: Unplugged.")
        AxGrFingerXCtrl1.CapStopCapture(e.idSensor)
    End Sub

    ' A finger was placed on reader
    Private Sub AxGrFingerXCtrl1_FingerDown( _
       ByVal sender As System.Object, _
       ByVal e As AxGrFingerXLib._IGrFingerXCtrlEvents_FingerDownEvent) _
       Handles AxGrFingerXCtrl1.FingerDown
        myUtil.WriteLog("Sensor: " & e.idSensor & ". Event: Finger Placed.")
    End Sub

    ' A finger was removed from reader
    Private Sub AxGrFingerXCtrl1_FingerUp( _
       ByVal sender As System.Object, _
       ByVal e As AxGrFingerXLib._IGrFingerXCtrlEvents_FingerUpEvent) _
       Handles AxGrFingerXCtrl1.FingerUp
        myUtil.WriteLog("Sensor: " & e.idSensor & ". Event: Finger removed.")
    End Sub

    ' An image was acquired from reader
    Private Sub AxGrFingerXCtrl1_ImageAcquired( _
       ByVal sender As System.Object, _
       ByVal e As AxGrFingerXLib._IGrFingerXCtrlEvents_ImageAcquiredEvent) _
       Handles AxGrFingerXCtrl1.ImageAcquired

        ' Copying aquired image
        myUtil.raw.height = e.height
        myUtil.raw.width = e.width
        myUtil.raw.res = e.res
        myUtil.raw.img = e.rawImage

        ' Signaling that an Image Event occurred.
        myUtil.WriteLog("Sensor: " & e.idSensor & ". Event: Image captured.")

        ' display fingerprint image
        myUtil.PrintBiometricDisplay(False, GRConstants.GR_DEFAULT_CONTEXT)

        '---extract the template from the fingerprint scanned---
        ExtractTemplate()

        '---identify who the user is---
        _UserID = IdentifyFingerprint()
        If _UserID > 0 Then
            '---user found---
            Beep()
            btnRegister.Enabled = False
            '---display user's information---
            GetUserInfo()
            '---writes to log file---
            WriteToLog(_UserID)
        Else
            '---user not found---
            ClearDisplay()
            btnRegister.Enabled = True
            Beep()
            lblMessage.Text = "User not found! Please register your information below"
        End If
    End Sub

    ' Extract a template from a fingerprint image
    Private Function ExtractTemplate() As Integer
        Dim ret As Integer
        ' extract template
        ret = myUtil.ExtractTemplate()
        ' write template quality to log
        If ret = GRConstants.GR_BAD_QUALITY Then
            myUtil.WriteLog("Template extracted successfully. Bad quality.")
        ElseIf ret = GRConstants.GR_MEDIUM_QUALITY Then
            myUtil.WriteLog("Template extracted successfully. Medium quality.")
        ElseIf ret = GRConstants.GR_HIGH_QUALITY Then
            myUtil.WriteLog("Template extracted successfully. High quality.")
        End If
        If ret >= 0 Then
            ' if no error, display minutiae/segments/directions into the image
            myUtil.PrintBiometricDisplay(True, GRConstants.GR_NO_CONTEXT)
        Else
            ' write error to log
            myUtil.WriteError(ret)
        End If
        Return ret
    End Function


    '---Identify a fingerprint; returns the ID of the user---
    Private Function IdentifyFingerprint() As Integer
        Dim ret As Integer, score As Integer
        score = 0
        ' identify it
        ret = myUtil.Identify(score)
        ' write result to log
        If ret > 0 Then
            myUtil.WriteLog("Fingerprint identified. ID = " & ret & ". Score = " & score & ".")
            myUtil.PrintBiometricDisplay(True, GRConstants.GR_DEFAULT_CONTEXT)
        ElseIf ret = 0 Then
            myUtil.WriteLog("Fingerprint not Found.")
        Else
            myUtil.WriteError(ret)
        End If
        Return ret
    End Function

    '---get user's information---
    Public Sub GetUserInfo()
        Dim filePath As String
        Try
            filePath = Application.StartupPath() & "\" & DBFile
            connection = New OleDb.OleDbConnection(ConnectionString & filePath)
            connection.Open()
            Dim reader As OleDb.OleDbDataReader
            Dim command As OleDb.OleDbCommand = New OleDb.OleDbCommand
            command.Connection = connection
            '---retrieve user's particulars---
            command.CommandText = "SELECT * FROM Enroll WHERE ID=" & _UserID
            reader = command.ExecuteReader(CommandBehavior.CloseConnection)
            reader.Read()

            '---display user's particulars---
            lblMessage.Text = "Welcome, " & reader("name")
            txtSSN.Text = reader("SSN")
            txtName.Text = reader("Name")
            txtCompany.Text = reader("Company")
            txtContactNumber.Text = reader("ContactNumber")
            txtEmail.Text = reader("Email")

            '---reset the timer to another 5 seconds---
            Timer1.Enabled = False
            Timer1.Enabled = True
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            connection.Close()
        End Try
    End Sub


    '---Register button---
    Private Sub btnRegister_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnRegister.Click
        '---first add the fingerprint---
        _UserID = EnrollFingerprint()
        '---then add the particulars---
        AddNewUser()
        '---clears the display---
        ClearDisplay()
        '---writes to log file---
        WriteToLog(_UserID)
    End Sub

    '---adds a fingerprint to the database; returns the ID of the user---
    Private Function EnrollFingerprint() As Integer
        Dim id As Integer
        ' add fingerprint
        id = myUtil.Enroll()
        ' write result to log
        If id >= 0 Then
            myUtil.WriteLog("Fingerprint enrolled with id = " & id)
        Else
            myUtil.WriteLog("Error: Fingerprint not enrolled")
        End If
        Return id
    End Function

    '---Add a new user's information to the database---
    Public Sub AddNewUser()
        Dim filePath As String
        Try
            filePath = Application.StartupPath() & "\" & DBFile
            connection = New OleDb.OleDbConnection(ConnectionString & filePath)
            connection.Open()
            Dim command As OleDb.OleDbCommand = New OleDb.OleDbCommand
            command.Connection = connection

            '---set the user's particulars in the table---
            Dim sql As String = "UPDATE enroll SET SSN='" & txtSSN.Text & "', " & _
               "Name='" & txtName.Text & "', " & _
               "Company='" & txtCompany.Text & "', " & _
               "ContactNumber='" & txtContactNumber.Text & "', " & _
               "Email='" & txtEmail.Text & "' " & _
               " WHERE ID=" & _UserID
            command.CommandText = sql
            command.ExecuteNonQuery()
            MsgBox("User added successfully!")
            connection.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    '---Clears the user's particulars---
    Public Sub ClearDisplay()
        lblMessage.Text = _
           "Please place your index finger on the fingerprint reader"
        PictureBox1.Image = My.Resources.fingerprintreader

        txtSSN.Text = String.Empty
        txtName.Text = String.Empty
        txtCompany.Text = String.Empty
        txtContactNumber.Text = String.Empty
        txtEmail.Text = String.Empty
    End Sub

    '---the Timer control---
    Private Sub Timer1_Tick( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles Timer1.Tick
        ClearDisplay()
        Timer1.Enabled = False
    End Sub

    Public Sub WriteToLog(ByVal ID As String)
        '---write to a log file---
        Dim sw As New System.IO.StreamWriter( _
           Logfile, True, System.Text.Encoding.ASCII)
        sw.WriteLine(id & "," & Now.ToString)
        sw.Close()
    End Sub

End Class
