<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbbCOMPorts = New System.Windows.Forms.ComboBox
        Me.txtDataToSend = New System.Windows.Forms.TextBox
        Me.btnSend = New System.Windows.Forms.Button
        Me.lblMessage = New System.Windows.Forms.Label
        Me.btnConnect = New System.Windows.Forms.Button
        Me.btnDisconnect = New System.Windows.Forms.Button
        Me.txtDataReceived = New System.Windows.Forms.RichTextBox
        Me.txtPhoneNumber = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnDialNumber = New System.Windows.Forms.Button
        Me.btnAnswerCall = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Available COM Ports"
        '
        'cbbCOMPorts
        '
        Me.cbbCOMPorts.FormattingEnabled = True
        Me.cbbCOMPorts.Location = New System.Drawing.Point(122, 6)
        Me.cbbCOMPorts.Name = "cbbCOMPorts"
        Me.cbbCOMPorts.Size = New System.Drawing.Size(80, 21)
        Me.cbbCOMPorts.TabIndex = 1
        '
        'txtDataToSend
        '
        Me.txtDataToSend.Location = New System.Drawing.Point(12, 236)
        Me.txtDataToSend.Multiline = True
        Me.txtDataToSend.Name = "txtDataToSend"
        Me.txtDataToSend.Size = New System.Drawing.Size(273, 47)
        Me.txtDataToSend.TabIndex = 2
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(289, 260)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(75, 23)
        Me.btnSend.TabIndex = 3
        Me.btnSend.Text = "Send"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMessage.Location = New System.Drawing.Point(12, 36)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(352, 23)
        Me.lblMessage.TabIndex = 5
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(210, 4)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 6
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'btnDisconnect
        '
        Me.btnDisconnect.Location = New System.Drawing.Point(291, 4)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(75, 23)
        Me.btnDisconnect.TabIndex = 7
        Me.btnDisconnect.Text = "Disconnect"
        Me.btnDisconnect.UseVisualStyleBackColor = True
        '
        'txtDataReceived
        '
        Me.txtDataReceived.Location = New System.Drawing.Point(12, 62)
        Me.txtDataReceived.Name = "txtDataReceived"
        Me.txtDataReceived.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.txtDataReceived.Size = New System.Drawing.Size(352, 168)
        Me.txtDataReceived.TabIndex = 8
        Me.txtDataReceived.Text = ""
        '
        'txtPhoneNumber
        '
        Me.txtPhoneNumber.Location = New System.Drawing.Point(85, 19)
        Me.txtPhoneNumber.Name = "txtPhoneNumber"
        Me.txtPhoneNumber.Size = New System.Drawing.Size(99, 20)
        Me.txtPhoneNumber.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Phone Number"
        '
        'btnDialNumber
        '
        Me.btnDialNumber.Location = New System.Drawing.Point(190, 17)
        Me.btnDialNumber.Name = "btnDialNumber"
        Me.btnDialNumber.Size = New System.Drawing.Size(75, 23)
        Me.btnDialNumber.TabIndex = 11
        Me.btnDialNumber.Text = "Dial Number"
        Me.btnDialNumber.UseVisualStyleBackColor = True
        '
        'btnAnswerCall
        '
        Me.btnAnswerCall.Location = New System.Drawing.Point(271, 17)
        Me.btnAnswerCall.Name = "btnAnswerCall"
        Me.btnAnswerCall.Size = New System.Drawing.Size(75, 23)
        Me.btnAnswerCall.TabIndex = 12
        Me.btnAnswerCall.Text = "Answer Call"
        Me.btnAnswerCall.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnAnswerCall)
        Me.GroupBox1.Controls.Add(Me.txtPhoneNumber)
        Me.GroupBox1.Controls.Add(Me.btnDialNumber)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 289)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(352, 50)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bluetooth Handset"
        '
        'Form1
        '
        Me.AcceptButton = Me.btnSend
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(379, 351)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtDataReceived)
        Me.Controls.Add(Me.btnDisconnect)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.txtDataToSend)
        Me.Controls.Add(Me.cbbCOMPorts)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Serial Chat"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbbCOMPorts As System.Windows.Forms.ComboBox
    Friend WithEvents txtDataToSend As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents btnDisconnect As System.Windows.Forms.Button
    Friend WithEvents txtDataReceived As System.Windows.Forms.RichTextBox
    Friend WithEvents txtPhoneNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDialNumber As System.Windows.Forms.Button
    Friend WithEvents btnAnswerCall As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

End Class
