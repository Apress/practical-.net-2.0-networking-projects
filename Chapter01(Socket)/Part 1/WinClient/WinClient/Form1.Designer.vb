Partial Public Class Form1
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.txtMessageHistory = New System.Windows.Forms.TextBox
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.btnSend = New System.Windows.Forms.Button
        Me.btnSignIn = New System.Windows.Forms.Button
        Me.txtNick = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtMessageHistory
        '
        Me.txtMessageHistory.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtMessageHistory.Location = New System.Drawing.Point(2, 39)
        Me.txtMessageHistory.Multiline = True
        Me.txtMessageHistory.Name = "txtMessageHistory"
        Me.txtMessageHistory.ReadOnly = True
        Me.txtMessageHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMessageHistory.Size = New System.Drawing.Size(268, 285)
        Me.txtMessageHistory.TabIndex = 0
        '
        'txtMessage
        '
        Me.txtMessage.Location = New System.Drawing.Point(2, 331)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(186, 20)
        Me.txtMessage.TabIndex = 1
        '
        'btnSend
        '
        Me.btnSend.Enabled = False
        Me.btnSend.Location = New System.Drawing.Point(194, 329)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(75, 23)
        Me.btnSend.TabIndex = 2
        Me.btnSend.Text = "Send"
        '
        'btnSignIn
        '
        Me.btnSignIn.Location = New System.Drawing.Point(194, 10)
        Me.btnSignIn.Name = "btnSignIn"
        Me.btnSignIn.Size = New System.Drawing.Size(75, 23)
        Me.btnSignIn.TabIndex = 3
        Me.btnSignIn.Text = "Sign In"
        '
        'txtNick
        '
        Me.txtNick.Location = New System.Drawing.Point(44, 12)
        Me.txtNick.Name = "txtNick"
        Me.txtNick.Size = New System.Drawing.Size(144, 20)
        Me.txtNick.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Nick"
        '
        'Form1
        '
        Me.AcceptButton = Me.btnSend
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(272, 355)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtNick)
        Me.Controls.Add(Me.btnSignIn)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.txtMessageHistory)
        Me.Name = "Form1"
        Me.Text = "Chat Client"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMessageHistory As System.Windows.Forms.TextBox
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnSignIn As System.Windows.Forms.Button
    Friend WithEvents txtNick As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
