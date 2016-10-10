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
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtMessageToSend = New System.Windows.Forms.TextBox
        Me.txtReceivedMessage = New System.Windows.Forms.TextBox
        Me.cbbCOMPorts = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnConnect = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'mainMenu1
        '
        Me.mainMenu1.MenuItems.Add(Me.MenuItem1)
        '
        'MenuItem1
        '
        Me.MenuItem1.Text = "Send"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 20)
        Me.Label2.Text = "Received Message"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(6, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 20)
        Me.Label1.Text = "Message to send"
        '
        'txtMessageToSend
        '
        Me.txtMessageToSend.Location = New System.Drawing.Point(3, 25)
        Me.txtMessageToSend.Name = "txtMessageToSend"
        Me.txtMessageToSend.Size = New System.Drawing.Size(234, 21)
        Me.txtMessageToSend.TabIndex = 5
        '
        'txtReceivedMessage
        '
        Me.txtReceivedMessage.Location = New System.Drawing.Point(3, 72)
        Me.txtReceivedMessage.Multiline = True
        Me.txtReceivedMessage.Name = "txtReceivedMessage"
        Me.txtReceivedMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReceivedMessage.Size = New System.Drawing.Size(234, 165)
        Me.txtReceivedMessage.TabIndex = 2
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
        Me.cbbCOMPorts.Location = New System.Drawing.Point(68, 243)
        Me.cbbCOMPorts.Name = "cbbCOMPorts"
        Me.cbbCOMPorts.Size = New System.Drawing.Size(83, 22)
        Me.cbbCOMPorts.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(3, 248)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 20)
        Me.Label3.Text = "COM Port"
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(157, 243)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(80, 20)
        Me.btnConnect.TabIndex = 9
        Me.btnConnect.Text = "Connect"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbbCOMPorts)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtMessageToSend)
        Me.Controls.Add(Me.txtReceivedMessage)
        Me.Menu = Me.mainMenu1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMessageToSend As System.Windows.Forms.TextBox
    Friend WithEvents txtReceivedMessage As System.Windows.Forms.TextBox
    Friend WithEvents cbbCOMPorts As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnConnect As System.Windows.Forms.Button

End Class
