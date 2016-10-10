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
        Me.lstUsers = New System.Windows.Forms.ListBox
        Me.btnFTP = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtMessageHistory
        '
        Me.txtMessageHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessageHistory.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtMessageHistory.Location = New System.Drawing.Point(128, 43)
        Me.txtMessageHistory.Multiline = True
        Me.txtMessageHistory.Name = "txtMessageHistory"
        Me.txtMessageHistory.ReadOnly = True
        Me.txtMessageHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMessageHistory.Size = New System.Drawing.Size(304, 186)
        Me.txtMessageHistory.TabIndex = 0
        '
        'txtMessage
        '
        Me.txtMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessage.Location = New System.Drawing.Point(2, 240)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(271, 20)
        Me.txtMessage.TabIndex = 1
        '
        'btnSend
        '
        Me.btnSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSend.Enabled = False
        Me.btnSend.Location = New System.Drawing.Point(276, 238)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(75, 23)
        Me.btnSend.TabIndex = 2
        Me.btnSend.Text = "Send"
        '
        'btnSignIn
        '
        Me.btnSignIn.Location = New System.Drawing.Point(357, 2)
        Me.btnSignIn.Name = "btnSignIn"
        Me.btnSignIn.Size = New System.Drawing.Size(75, 23)
        Me.btnSignIn.TabIndex = 3
        Me.btnSignIn.Text = "Sign In"
        '
        'txtNick
        '
        Me.txtNick.Location = New System.Drawing.Point(34, 4)
        Me.txtNick.Name = "txtNick"
        Me.txtNick.Size = New System.Drawing.Size(317, 20)
        Me.txtNick.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-1, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Nick"
        '
        'lstUsers
        '
        Me.lstUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstUsers.FormattingEnabled = True
        Me.lstUsers.Location = New System.Drawing.Point(2, 43)
        Me.lstUsers.Name = "lstUsers"
        Me.lstUsers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstUsers.Size = New System.Drawing.Size(120, 186)
        Me.lstUsers.TabIndex = 6
        '
        'btnFTP
        '
        Me.btnFTP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFTP.Location = New System.Drawing.Point(357, 238)
        Me.btnFTP.Name = "btnFTP"
        Me.btnFTP.Size = New System.Drawing.Size(75, 23)
        Me.btnFTP.TabIndex = 7
        Me.btnFTP.Text = "Send File"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(-1, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Online users"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 263)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(436, 22)
        Me.StatusStrip1.TabIndex = 9
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(111, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'Form1
        '
        Me.AcceptButton = Me.btnSend
        Me.ClientSize = New System.Drawing.Size(436, 285)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnFTP)
        Me.Controls.Add(Me.lstUsers)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtNick)
        Me.Controls.Add(Me.btnSignIn)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.txtMessageHistory)
        Me.Name = "Form1"
        Me.Text = "Chat Client"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMessageHistory As System.Windows.Forms.TextBox
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnSignIn As System.Windows.Forms.Button
    Friend WithEvents txtNick As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstUsers As System.Windows.Forms.ListBox
    Friend WithEvents btnFTP As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel

End Class
