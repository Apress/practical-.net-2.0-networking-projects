<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTagID = New System.Windows.Forms.TextBox
        Me.chkTurnOnLED = New System.Windows.Forms.CheckBox
        Me.chkEnableReader = New System.Windows.Forms.CheckBox
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 72)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(219, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(111, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Tag ID"
        '
        'txtTagID
        '
        Me.txtTagID.Location = New System.Drawing.Point(57, 12)
        Me.txtTagID.Name = "txtTagID"
        Me.txtTagID.Size = New System.Drawing.Size(146, 20)
        Me.txtTagID.TabIndex = 2
        '
        'chkTurnOnLED
        '
        Me.chkTurnOnLED.AutoSize = True
        Me.chkTurnOnLED.Location = New System.Drawing.Point(15, 38)
        Me.chkTurnOnLED.Name = "chkTurnOnLED"
        Me.chkTurnOnLED.Size = New System.Drawing.Size(87, 17)
        Me.chkTurnOnLED.TabIndex = 3
        Me.chkTurnOnLED.Text = "Turn on LED"
        Me.chkTurnOnLED.UseVisualStyleBackColor = True
        '
        'chkEnableReader
        '
        Me.chkEnableReader.AutoSize = True
        Me.chkEnableReader.Location = New System.Drawing.Point(106, 38)
        Me.chkEnableReader.Name = "chkEnableReader"
        Me.chkEnableReader.Size = New System.Drawing.Size(97, 17)
        Me.chkEnableReader.TabIndex = 4
        Me.chkEnableReader.Text = "Enable Reader"
        Me.chkEnableReader.UseVisualStyleBackColor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(219, 94)
        Me.Controls.Add(Me.chkEnableReader)
        Me.Controls.Add(Me.chkTurnOnLED)
        Me.Controls.Add(Me.txtTagID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "Form2"
        Me.Text = "Form1"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTagID As System.Windows.Forms.TextBox
    Friend WithEvents chkTurnOnLED As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableReader As System.Windows.Forms.CheckBox
End Class
