Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class Form1

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

    '***************************************************
    Dim WithEvents RFIDReader As PhidgetsNET.PhidgetRFID
    '***************************************************

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


        '************************************************
        RFIDReader = New PhidgetsNET.PhidgetRFID
        RFIDReader.OpenRemoteIP("localhost", 5001, -1, "pass")
        ToolStripStatusLabel1.Text = "Not Connected"
        '************************************************

    End Sub

    '************************************************
    Private Sub RFIDReader_Attach(ByVal sender As Object, ByVal e As PhidgetsNET.AttachEventArgs) Handles RFIDReader.Attach
        '---display the status---
        ToolStripStatusLabel1.Text = "Phidget RFID Reader Connected"
        '---Enable onboard LED---
        RFIDReader.SetOutputState(2, True)
        '---Enable RFID Reader---
        RFIDReader.SetOutputState(3, False)
    End Sub

    Private Sub RFIDReader_Detach(ByVal sender As Object, ByVal e As PhidgetsNET.DetachEventArgs) Handles RFIDReader.Detach
        '---display the status---
        ToolStripStatusLabel1.Text = "Phidget RFID Reader Not Connected"
    End Sub

    Private Sub RFIDReader_Error(ByVal sender As Object, ByVal e As PhidgetsNET.ErrorEventArgs) Handles RFIDReader.Error
        '---display the error---
        ToolStripStatusLabel1.Text = e.getError
    End Sub
    Private Sub RFIDReader_Tag(ByVal sender As Object, ByVal e As PhidgetsNET.TagEventArgs) Handles RFIDReader.Tag
        '---save the tag ID---
        Console.WriteLine("tag detected - " & e.getTag)
        '---when incoming data is received, update the TagID textbox---
        txtTagID.BeginInvoke(New _
                       myDelegate(AddressOf updateTextBox), _
                       New Object() {e.getTag})
    End Sub

    '---update the Tag ID textbox---
    Public Delegate Sub myDelegate(ByVal str As String)
    Public Sub updateTextBox(ByVal str As String)
        '---for receiving plain ASCII text---
        With txtTagID
            .Text = "" ' ensure that the TextChanged event is fired
            .Text = str
        End With
    End Sub

    Private Sub txtTagID_TextChanged( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles txtTagID.TextChanged

        '---get the tag ID that is read---
        Dim temptagID As String = txtTagID.Text
        '---if no tag ID, exit---
        If temptagID = String.Empty Then
            Exit Sub
        End If

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
            WriteToLog(EmployeeIDLabel2.Text, _
               LastNameTextBox.Text & ", " & FirstNameTextBox.Text)
            '---reset the timer---
            Timer1.Enabled = False
            Timer1.Enabled = True
        End If
        '---save the time this tag was recorded---
        timeRecorded = Now
    End Sub

    Private Sub btnAssign_Click( _
       ByVal sender As System.Object, _
       ByVal e As System.EventArgs) _
       Handles btnAssign.Click
        '---obtain the tag ID that was read---
        If txtTagID.Text <> String.Empty Then
            '---assign the Tag ID to the current employee---
            TagIDLabel1.Text = txtTagID.Text
            ToolStripStatusLabel1.Text = "Tag associated with employee."
            '---save the record---
            Me.Validate()
            Me.EmployeesBindingSource.EndEdit()
            Me.EmployeesTableAdapter.Update(Me.NorthwindDataSet.Employees)
        Else
            ToolStripStatusLabel1.Text = "No tag id scanned."
            Exit Sub
        End If
    End Sub


    '************************************************

    Private Sub EmployeesBindingNavigatorSaveItem_Click( _
       ByVal sender As System.Object, ByVal e As System.EventArgs) _
       Handles EmployeesBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.EmployeesBindingSource.EndEdit()
        Me.EmployeesTableAdapter.Update(Me.NorthwindDataSet.Employees)
        '-------------------------------------------------------------
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
