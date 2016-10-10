Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class Form1
    '---serial port to listen to incoming data---
    Private WithEvents serialPort As New IO.Ports.SerialPort
    '---tag ID read from the reader---
    Private tagID As String = String.Empty
    '---the time that the tag ID was recorded---
    Private timeRecorded As DateTime = Now

    '---COM port to listen to---
    Const COM As String = "COM3"
    '---file name of the log file---
    Const FILE_NAME As String = "C:\Attendance.csv"
    '---the interval before the employee record is cleared 
    ' from the screen (in seconds)---
    Const INTERVAL As Integer = 3

    Private Sub Form1_Load( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) Handles MyBase.Load

        'TODO: This line of code loads data into the 
        ' 'NorthwindDataSet.Employees' table. You can move, 
        ' or remove it, as needed.
        Me.EmployeesTableAdapter.Fill(Me.NorthwindDataSet.Employees)

        '---Clear the employee when the app is loaded---
        EmployeesBindingSource.Filter = "TAGID='xxxxxxxxxx'"
        '---set the timer interval to clear the employee record---
        Timer1.Interval = INTERVAL * 1000    'convert to milliseconds

        '---open the serial port connecting to the reader---
        If serialPort.IsOpen Then
            serialPort.Close()
        End If
        Try
            With serialPort
                .PortName = COM
                .BaudRate = 2400
                .Parity = IO.Ports.Parity.None
                .DataBits = 8
                .StopBits = IO.Ports.StopBits.One
                .Handshake = IO.Ports.Handshake.None
            End With
            serialPort.Open()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub EmployeesBindingNavigatorSaveItem_Click( _
       ByVal sender As System.Object, ByVal e As System.EventArgs) _
       Handles EmployeesBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.EmployeesBindingSource.EndEdit()
        Me.EmployeesTableAdapter.Update(Me.NorthwindDataSet.Employees)
        '-------------------------------------------------------------
    End Sub

    Private Sub DataReceived( _
       ByVal sender As Object, _
       ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) _
       Handles serialPort.DataReceived

        '---when incoming data is received, update the TagID textbox---
        txtTagID.BeginInvoke(New _
                       myDelegate(AddressOf updateTextBox), _
                       New Object() {})
    End Sub

    '---update the Tag ID textbox---
    Public Delegate Sub myDelegate()
    Public Sub updateTextBox()
        '---for receiving plain ASCII text---
        With txtTagID
            .AppendText(serialPort.ReadExisting)
            .ScrollToCaret()
        End With
    End Sub
 
    Private Sub btnAssign_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnAssign.Click
        '---obtain the tag ID that was read---
        If txtTagID.Lines.Length > 1 Then
            Dim tagID As String = txtTagID.Lines(txtTagID.Lines.Length - 2)
        Else
            ToolStripStatusLabel1.Text = "No tag id scanned."
            Exit Sub
        End If

        If txtTagID.Text <> String.Empty Then
            '---assign the Tag ID to the current employee---
            TagIDLabel1.Text = tagID
            ToolStripStatusLabel1.Text = "Tag associated with employee."
            '---save the record---
            Me.Validate()
            Me.EmployeesBindingSource.EndEdit()
            Me.EmployeesTableAdapter.Update(Me.NorthwindDataSet.Employees)
        End If
    End Sub

    Private Sub txtTagID_TextChanged( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles txtTagID.TextChanged

        If txtTagID.Lines(txtTagID.Lines.Length - 1) = String.Empty Then
            '---get the tag ID that is read---
            Dim temptagID As String = txtTagID.Lines(txtTagID.Lines.Length - 2)

            '---get the time interval between the last read time
            ' and the current time---
            Dim tp As TimeSpan = Now.Subtract(timeRecorded)
            Dim timeInterval As Double = tp.Ticks / TimeSpan.TicksPerSecond

            If (temptagID = tagID) And timeInterval < INTERVAL Then
                '---if it is the same tag and the time interval 
                ' is less than 3 seconds, the tag won't be registered---
                Exit Sub
            End If

            '---the tag is saved---
            tagID = temptagID
            EmployeesBindingSource.RemoveFilter()

            '---find the employee associated with the tag---
            EmployeesBindingSource.Filter = "TAGID='" & tagID & "'"
            If EmployeesBindingSource.Count < 1 Then
                ToolStripStatusLabel1.Text = "Employee not found."
            Else
                ToolStripStatusLabel1.Text = "Employee found."
                '---write the employee information to log file---
                WriteToLog(EmployeeIDLabel1.Text, _
                   LastNameTextBox.Text & ", " & FirstNameTextBox.Text)
                '---reset the timer---
                Timer1.Enabled = False
                Timer1.Enabled = True
            End If
            '---save the time this tag was recorded---
            timeRecorded = Now
        End If
    End Sub

    Private Sub btnFind_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnFind.Click
        '---search for employee---
        If txtEmployeeID.Text = String.Empty Then
            EmployeesBindingSource.RemoveFilter()
        Else
            EmployeesBindingSource.Filter = _
               "EmployeeID='" & txtEmployeeID.Text & "'"
        End If
    End Sub

    Private Sub btnDeassign_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnDeassign.Click

        If Trim(TagIDLabel1.Text) = String.Empty Then
            ToolStripStatusLabel1.Text = "Current employee has no tag ID."
            Exit Sub
        End If
        '---deassociate tag ID from employee---
        TagIDLabel1.Text = String.Empty
        '---save the record---
        Me.Validate()
        Me.EmployeesBindingSource.EndEdit()
        Me.EmployeesTableAdapter.Update(Me.NorthwindDataSet.Employees)
        ToolStripStatusLabel1.Text = "Tag deassociated from employee."
    End Sub

    Private Sub WriteToLog( _
       ByVal employeeID As String, _
       ByVal employeeName As String)
        '---write to log file---
        Dim str As String = employeeID & "," & _
                            employeeName & "," & Now & Chr(13)
        My.Computer.FileSystem.WriteAllText(FILE_NAME, str, True)
    End Sub

    Private Sub Timer1_Tick( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles Timer1.Tick
        '---clear the employee---
        EmployeesBindingSource.Filter = "TAGID='xxxxxxxxxx'"
        Timer1.Enabled = False
    End Sub
End Class
