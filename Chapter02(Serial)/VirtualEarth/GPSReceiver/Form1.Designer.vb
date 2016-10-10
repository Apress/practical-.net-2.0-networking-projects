<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Form1
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
    private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.btnConnect = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.cbbCOMPorts = New System.Windows.Forms.ComboBox
        Me.txtGPSData = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnDisconnect = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(157, 3)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(80, 20)
        Me.btnConnect.TabIndex = 12
        Me.btnConnect.Text = "Connect"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(3, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 20)
        Me.Label3.Text = "COM Port"
        '
        'cbbCOMPorts
        '
        Me.cbbCOMPorts.Items.Add("COM1")
        Me.cbbCOMPorts.Items.Add("COM2")
        Me.cbbCOMPorts.Items.Add("COM3")
        Me.cbbCOMPorts.Items.Add("COM4")
        Me.cbbCOMPorts.Items.Add("COM5")
        Me.cbbCOMPorts.Items.Add("COM6")
        Me.cbbCOMPorts.Items.Add("COM7")
        Me.cbbCOMPorts.Items.Add("COM8")
        Me.cbbCOMPorts.Items.Add("COM9")
        Me.cbbCOMPorts.Location = New System.Drawing.Point(68, 3)
        Me.cbbCOMPorts.Name = "cbbCOMPorts"
        Me.cbbCOMPorts.Size = New System.Drawing.Size(83, 22)
        Me.cbbCOMPorts.TabIndex = 11
        '
        'txtGPSData
        '
        Me.txtGPSData.Location = New System.Drawing.Point(3, 51)
        Me.txtGPSData.Multiline = True
        Me.txtGPSData.Name = "txtGPSData"
        Me.txtGPSData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGPSData.Size = New System.Drawing.Size(234, 214)
        Me.txtGPSData.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(3, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 20)
        Me.Label2.Text = "Data from GPS"
        '
        'btnDisconnect
        '
        Me.btnDisconnect.Location = New System.Drawing.Point(157, 25)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(80, 20)
        Me.btnDisconnect.TabIndex = 16
        Me.btnDisconnect.Text = "Disconnect"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.Controls.Add(Me.btnDisconnect)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtGPSData)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbbCOMPorts)
        Me.Menu = Me.mainMenu1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbbCOMPorts As System.Windows.Forms.ComboBox
    Friend WithEvents txtGPSData As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDisconnect As System.Windows.Forms.Button

End Class
