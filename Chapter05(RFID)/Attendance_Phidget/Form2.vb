Public Class Form2
    '***************************************************
    Dim WithEvents RFIDReader As PhidgetsNET.PhidgetRFID
    '***************************************************
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '************************************************
        RFIDReader = New PhidgetsNET.PhidgetRFID
        RFIDReader.OpenRemoteIP("localhost", 5001, -1, "pass")
        ToolStripStatusLabel1.Text = "Not Connected"
        '************************************************
    End Sub

    Private Sub RFIDReader_Attach( _
       ByVal sender As Object, _
       ByVal e As PhidgetsNET.AttachEventArgs) _
       Handles RFIDReader.Attach
        '---display the status---
        ToolStripStatusLabel1.Text = "Phidget RFID Reader Connected"
        '---Enable onboard LED---
        chkTurnOnLED.Checked = True
        RFIDReader.SetOutputState(2, True)
        '---Enable RFID Reader---
        chkEnableReader.Checked = True
        RFIDReader.SetOutputState(3, True)
    End Sub

    Private Sub RFIDReader_Detach( _
       ByVal sender As Object, _
       ByVal e As PhidgetsNET.DetachEventArgs) _
       Handles RFIDReader.Detach
        '---display the status---
        ToolStripStatusLabel1.Text = "Phidget RFID Reader Not Connected"
    End Sub

    Private Sub RFIDReader_Error( _
       ByVal sender As Object, _
       ByVal e As PhidgetsNET.ErrorEventArgs) _
       Handles RFIDReader.Error
        '---display the error---
        ToolStripStatusLabel1.Text = e.getError
    End Sub

    Private Sub RFIDReader_Tag( _
       ByVal sender As Object, _
       ByVal e As PhidgetsNET.TagEventArgs) _
       Handles RFIDReader.Tag
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
            .Text = str
        End With
    End Sub

    Private Sub chkTurnOnLED_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTurnOnLED.CheckedChanged
        '---Enable/Disable onboard LED---
        RFIDReader.SetOutputState(2, chkTurnOnLED.Checked)
    End Sub

    Private Sub chkEnableReader_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableReader.CheckedChanged
        '---Enable RFID Reader---
        RFIDReader.SetOutputState(3, chkEnableReader.Checked)
    End Sub
End Class