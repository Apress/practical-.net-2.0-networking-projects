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
        Me.components = New System.ComponentModel.Container
        Me.btnClearPath = New System.Windows.Forms.Button
        Me.btnShowPath = New System.Windows.Forms.Button
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnGotoPoint = New System.Windows.Forms.Button
        Me.txtLongitude = New System.Windows.Forms.TextBox
        Me.txtLatitude = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtDataReceived = New System.Windows.Forms.TextBox
        Me.btnConnect = New System.Windows.Forms.Button
        Me.cbbCOMPorts = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblMessage = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClearPath
        '
        Me.btnClearPath.Location = New System.Drawing.Point(6, 42)
        Me.btnClearPath.Name = "btnClearPath"
        Me.btnClearPath.Size = New System.Drawing.Size(233, 23)
        Me.btnClearPath.TabIndex = 1
        Me.btnClearPath.Text = "Clear Path"
        Me.btnClearPath.UseVisualStyleBackColor = True
        '
        'btnShowPath
        '
        Me.btnShowPath.Location = New System.Drawing.Point(6, 19)
        Me.btnShowPath.Name = "btnShowPath"
        Me.btnShowPath.Size = New System.Drawing.Size(233, 23)
        Me.btnShowPath.TabIndex = 2
        Me.btnShowPath.Text = "Show Path"
        Me.btnShowPath.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(2, 3)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.ScrollBarsEnabled = False
        Me.WebBrowser1.Size = New System.Drawing.Size(460, 380)
        Me.WebBrowser1.TabIndex = 9
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnGotoPoint)
        Me.GroupBox1.Controls.Add(Me.txtLongitude)
        Me.GroupBox1.Controls.Add(Me.txtLatitude)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(468, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(245, 104)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Current Location:"
        '
        'btnGotoPoint
        '
        Me.btnGotoPoint.Location = New System.Drawing.Point(141, 71)
        Me.btnGotoPoint.Name = "btnGotoPoint"
        Me.btnGotoPoint.Size = New System.Drawing.Size(98, 23)
        Me.btnGotoPoint.TabIndex = 13
        Me.btnGotoPoint.Text = "Goto Point"
        Me.btnGotoPoint.UseVisualStyleBackColor = True
        '
        'txtLongitude
        '
        Me.txtLongitude.Location = New System.Drawing.Point(67, 45)
        Me.txtLongitude.Name = "txtLongitude"
        Me.txtLongitude.Size = New System.Drawing.Size(172, 20)
        Me.txtLongitude.TabIndex = 12
        '
        'txtLatitude
        '
        Me.txtLatitude.Location = New System.Drawing.Point(67, 19)
        Me.txtLatitude.Name = "txtLatitude"
        Me.txtLatitude.Size = New System.Drawing.Size(172, 20)
        Me.txtLatitude.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Longitude"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Latitude"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtDataReceived)
        Me.GroupBox2.Controls.Add(Me.btnConnect)
        Me.GroupBox2.Controls.Add(Me.cbbCOMPorts)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(468, 113)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(245, 159)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GPS"
        '
        'txtDataReceived
        '
        Me.txtDataReceived.Location = New System.Drawing.Point(6, 43)
        Me.txtDataReceived.Multiline = True
        Me.txtDataReceived.Name = "txtDataReceived"
        Me.txtDataReceived.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDataReceived.Size = New System.Drawing.Size(233, 107)
        Me.txtDataReceived.TabIndex = 14
        Me.txtDataReceived.WordWrap = False
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(164, 14)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 13
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'cbbCOMPorts
        '
        Me.cbbCOMPorts.FormattingEnabled = True
        Me.cbbCOMPorts.Location = New System.Drawing.Point(78, 16)
        Me.cbbCOMPorts.Name = "cbbCOMPorts"
        Me.cbbCOMPorts.Size = New System.Drawing.Size(80, 21)
        Me.cbbCOMPorts.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "GPS Port"
        '
        'lblMessage
        '
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMessage.Location = New System.Drawing.Point(471, 354)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(240, 21)
        Me.lblMessage.TabIndex = 15
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnShowPath)
        Me.GroupBox3.Controls.Add(Me.btnClearPath)
        Me.GroupBox3.Location = New System.Drawing.Point(468, 278)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(245, 73)
        Me.GroupBox3.TabIndex = 13
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Recorded Path"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Khaki
        Me.ClientSize = New System.Drawing.Size(718, 385)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Name = "Form1"
        Me.Text = "Microsoft Virtual Earth"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClearPath As System.Windows.Forms.Button
    Friend WithEvents btnShowPath As System.Windows.Forms.Button
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGotoPoint As System.Windows.Forms.Button
    Friend WithEvents txtLongitude As System.Windows.Forms.TextBox
    Friend WithEvents txtLatitude As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents cbbCOMPorts As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDataReceived As System.Windows.Forms.TextBox
    Friend WithEvents lblMessage As System.Windows.Forms.Label

End Class
