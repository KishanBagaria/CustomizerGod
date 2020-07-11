<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Feedback
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.bSend = New System.Windows.Forms.Button()
        Me.tbEmail = New System.Windows.Forms.TextBox()
        Me.tbFeedback = New System.Windows.Forms.TextBox()
        Me.lld2w = New System.Windows.Forms.LinkLabel()
        Me.lFeedback = New System.Windows.Forms.Label()
        Me.pBottom = New System.Windows.Forms.Panel()
        Me.pBorder = New System.Windows.Forms.Panel()
        Me.llEmail = New System.Windows.Forms.LinkLabel()
        Me.lEmail = New System.Windows.Forms.Label()
        Me.pBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'bSend
        '
        Me.bSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bSend.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.bSend.Location = New System.Drawing.Point(304, 9)
        Me.bSend.Name = "bSend"
        Me.bSend.Size = New System.Drawing.Size(68, 23)
        Me.bSend.TabIndex = 0
        Me.bSend.Text = "Send"
        Me.bSend.UseVisualStyleBackColor = True
        '
        'tbEmail
        '
        Me.tbEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbEmail.Location = New System.Drawing.Point(12, 268)
        Me.tbEmail.Name = "tbEmail"
        Me.tbEmail.Size = New System.Drawing.Size(360, 23)
        Me.tbEmail.TabIndex = 3
        '
        'tbFeedback
        '
        Me.tbFeedback.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFeedback.Location = New System.Drawing.Point(12, 43)
        Me.tbFeedback.Multiline = True
        Me.tbFeedback.Name = "tbFeedback"
        Me.tbFeedback.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbFeedback.Size = New System.Drawing.Size(360, 219)
        Me.tbFeedback.TabIndex = 2
        '
        'lld2w
        '
        Me.lld2w.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lld2w.AutoSize = True
        Me.lld2w.Location = New System.Drawing.Point(12, 11)
        Me.lld2w.Name = "lld2w"
        Me.lld2w.Size = New System.Drawing.Size(113, 15)
        Me.lld2w.TabIndex = 6
        Me.lld2w.TabStop = True
        Me.lld2w.Text = "door2windows.com"
        '
        'lFeedback
        '
        Me.lFeedback.AutoSize = True
        Me.lFeedback.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lFeedback.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.lFeedback.Location = New System.Drawing.Point(8, 14)
        Me.lFeedback.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.lFeedback.Name = "lFeedback"
        Me.lFeedback.Size = New System.Drawing.Size(75, 21)
        Me.lFeedback.TabIndex = 1
        Me.lFeedback.Text = "Feedback"
        '
        'pBottom
        '
        Me.pBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pBottom.Controls.Add(Me.pBorder)
        Me.pBottom.Controls.Add(Me.lld2w)
        Me.pBottom.Controls.Add(Me.bSend)
        Me.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pBottom.Location = New System.Drawing.Point(0, 320)
        Me.pBottom.Name = "pBottom"
        Me.pBottom.Size = New System.Drawing.Size(384, 41)
        Me.pBottom.TabIndex = 3
        '
        'pBorder
        '
        Me.pBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pBorder.Dock = System.Windows.Forms.DockStyle.Top
        Me.pBorder.Location = New System.Drawing.Point(0, 0)
        Me.pBorder.Name = "pBorder"
        Me.pBorder.Size = New System.Drawing.Size(384, 1)
        Me.pBorder.TabIndex = 2
        '
        'llEmail
        '
        Me.llEmail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llEmail.AutoSize = True
        Me.llEmail.Location = New System.Drawing.Point(201, 297)
        Me.llEmail.Margin = New System.Windows.Forms.Padding(0, 3, 3, 5)
        Me.llEmail.Name = "llEmail"
        Me.llEmail.Size = New System.Drawing.Size(115, 15)
        Me.llEmail.TabIndex = 5
        Me.llEmail.TabStop = True
        Me.llEmail.Text = "<developer@email>"
        '
        'lEmail
        '
        Me.lEmail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lEmail.AutoSize = True
        Me.lEmail.Location = New System.Drawing.Point(12, 297)
        Me.lEmail.Margin = New System.Windows.Forms.Padding(3, 3, 0, 5)
        Me.lEmail.Name = "lEmail"
        Me.lEmail.Size = New System.Drawing.Size(190, 15)
        Me.lEmail.TabIndex = 4
        Me.lEmail.Text = "You can also email your feeback to"
        '
        'Feedback
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(384, 361)
        Me.Controls.Add(Me.llEmail)
        Me.Controls.Add(Me.lEmail)
        Me.Controls.Add(Me.lFeedback)
        Me.Controls.Add(Me.tbFeedback)
        Me.Controls.Add(Me.tbEmail)
        Me.Controls.Add(Me.pBottom)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(400, 300)
        Me.Name = "Feedback"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Feedback"
        Me.pBottom.ResumeLayout(False)
        Me.pBottom.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bSend As System.Windows.Forms.Button
    Friend WithEvents tbEmail As System.Windows.Forms.TextBox
    Friend WithEvents tbFeedback As System.Windows.Forms.TextBox
    Friend WithEvents lld2w As System.Windows.Forms.LinkLabel
    Friend WithEvents lFeedback As System.Windows.Forms.Label
    Friend WithEvents pBottom As System.Windows.Forms.Panel
    Friend WithEvents pBorder As System.Windows.Forms.Panel
    Friend WithEvents llEmail As System.Windows.Forms.LinkLabel
    Friend WithEvents lEmail As System.Windows.Forms.Label
End Class