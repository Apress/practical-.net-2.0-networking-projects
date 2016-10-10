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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnStartRecording = New System.Windows.Forms.Button
        Me.btnTakeSnapshot = New System.Windows.Forms.Button
        Me.btnStopRecording = New System.Windows.Forms.Button
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.lblProximity = New System.Windows.Forms.Label
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(12, 41)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(336, 253)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'btnStartRecording
        '
        Me.btnStartRecording.Location = New System.Drawing.Point(12, 300)
        Me.btnStartRecording.Name = "btnStartRecording"
        Me.btnStartRecording.Size = New System.Drawing.Size(108, 23)
        Me.btnStartRecording.TabIndex = 3
        Me.btnStartRecording.Text = "Start Recording"
        Me.btnStartRecording.UseVisualStyleBackColor = True
        '
        'btnTakeSnapshot
        '
        Me.btnTakeSnapshot.Location = New System.Drawing.Point(240, 300)
        Me.btnTakeSnapshot.Name = "btnTakeSnapshot"
        Me.btnTakeSnapshot.Size = New System.Drawing.Size(108, 23)
        Me.btnTakeSnapshot.TabIndex = 4
        Me.btnTakeSnapshot.Text = "Take Snapshot"
        Me.btnTakeSnapshot.UseVisualStyleBackColor = True
        '
        'btnStopRecording
        '
        Me.btnStopRecording.Location = New System.Drawing.Point(126, 300)
        Me.btnStopRecording.Name = "btnStopRecording"
        Me.btnStopRecording.Size = New System.Drawing.Size(108, 23)
        Me.btnStopRecording.TabIndex = 7
        Me.btnStopRecording.Text = "Stop Recording"
        Me.btnStopRecording.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 12)
        Me.ProgressBar1.Maximum = 160
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(291, 23)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.TabIndex = 9
        '
        'lblProximity
        '
        Me.lblProximity.AutoSize = True
        Me.lblProximity.Location = New System.Drawing.Point(309, 22)
        Me.lblProximity.Name = "lblProximity"
        Me.lblProximity.Size = New System.Drawing.Size(39, 13)
        Me.lblProximity.TabIndex = 10
        Me.lblProximity.Text = "Label1"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 333)
        Me.Controls.Add(Me.lblProximity)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnStopRecording)
        Me.Controls.Add(Me.btnTakeSnapshot)
        Me.Controls.Add(Me.btnStartRecording)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Form1"
        Me.Text = "Security System"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnStartRecording As System.Windows.Forms.Button
    Friend WithEvents btnTakeSnapshot As System.Windows.Forms.Button
    Friend WithEvents btnStopRecording As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProximity As System.Windows.Forms.Label

End Class
