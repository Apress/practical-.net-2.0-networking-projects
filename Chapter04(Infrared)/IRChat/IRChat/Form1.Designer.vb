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
        Me.mnuSend = New System.Windows.Forms.MenuItem
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.txtMessagesArchive = New System.Windows.Forms.TextBox
        Me.StatusBar1 = New System.Windows.Forms.StatusBar
        Me.SuspendLayout()
        '
        'mainMenu1
        '
        Me.mainMenu1.MenuItems.Add(Me.mnuSend)
        '
        'mnuSend
        '
        Me.mnuSend.Text = "Send"
        '
        'txtMessage
        '
        Me.txtMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessage.Location = New System.Drawing.Point(3, 3)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(234, 21)
        Me.txtMessage.TabIndex = 3
        '
        'txtMessagesArchive
        '
        Me.txtMessagesArchive.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessagesArchive.Location = New System.Drawing.Point(3, 30)
        Me.txtMessagesArchive.Multiline = True
        Me.txtMessagesArchive.Name = "txtMessagesArchive"
        Me.txtMessagesArchive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMessagesArchive.Size = New System.Drawing.Size(234, 210)
        Me.txtMessagesArchive.TabIndex = 4
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 246)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(240, 22)
        Me.StatusBar1.Text = "StatusBar1"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.txtMessagesArchive)
        Me.Menu = Me.mainMenu1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtMessagesArchive As System.Windows.Forms.TextBox
    Friend WithEvents mnuSend As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar

End Class
