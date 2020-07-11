<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class About
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
        Me.lAbout = New System.Windows.Forms.Label()
        Me.lDeveloper = New System.Windows.Forms.Label()
        Me.lVersion = New System.Windows.Forms.Label()
        Me.pBottom = New System.Windows.Forms.Panel()
        Me.pBorder = New System.Windows.Forms.Panel()
        Me.lld2w = New System.Windows.Forms.LinkLabel()
        Me.bOK = New System.Windows.Forms.Button()
        Me.lHowToUse = New System.Windows.Forms.Label()
        Me.llHowToUse = New System.Windows.Forms.LinkLabel()
        Me.lVersionNo = New System.Windows.Forms.Label()
        Me.tlp = New System.Windows.Forms.TableLayoutPanel()
        Me.lKishanBagaria = New System.Windows.Forms.Label()
        Me.pBottom.SuspendLayout()
        Me.tlp.SuspendLayout()
        Me.SuspendLayout()
        '
        'lAbout
        '
        Me.lAbout.AutoSize = True
        Me.lAbout.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lAbout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.lAbout.Location = New System.Drawing.Point(14, 14)
        Me.lAbout.Margin = New System.Windows.Forms.Padding(5, 5, 3, 3)
        Me.lAbout.Name = "lAbout"
        Me.lAbout.Size = New System.Drawing.Size(118, 21)
        Me.lAbout.TabIndex = 0
        Me.lAbout.Text = "CustomizerGod"
        '
        'lDeveloper
        '
        Me.lDeveloper.AutoSize = True
        Me.lDeveloper.Location = New System.Drawing.Point(3, 50)
        Me.lDeveloper.Name = "lDeveloper"
        Me.lDeveloper.Size = New System.Drawing.Size(64, 15)
        Me.lDeveloper.TabIndex = 0
        Me.lDeveloper.Text = "Developer:"
        '
        'lVersion
        '
        Me.lVersion.AutoSize = True
        Me.lVersion.Location = New System.Drawing.Point(3, 0)
        Me.lVersion.Name = "lVersion"
        Me.lVersion.Size = New System.Drawing.Size(49, 15)
        Me.lVersion.TabIndex = 0
        Me.lVersion.Text = "Version:"
        '
        'pBottom
        '
        Me.pBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pBottom.Controls.Add(Me.pBorder)
        Me.pBottom.Controls.Add(Me.lld2w)
        Me.pBottom.Controls.Add(Me.bOK)
        Me.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pBottom.Location = New System.Drawing.Point(0, 130)
        Me.pBottom.Name = "pBottom"
        Me.pBottom.Size = New System.Drawing.Size(359, 41)
        Me.pBottom.TabIndex = 2
        '
        'pBorder
        '
        Me.pBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pBorder.Dock = System.Windows.Forms.DockStyle.Top
        Me.pBorder.Location = New System.Drawing.Point(0, 0)
        Me.pBorder.Name = "pBorder"
        Me.pBorder.Size = New System.Drawing.Size(359, 1)
        Me.pBorder.TabIndex = 2
        '
        'lld2w
        '
        Me.lld2w.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lld2w.AutoSize = True
        Me.lld2w.Location = New System.Drawing.Point(12, 11)
        Me.lld2w.Name = "lld2w"
        Me.lld2w.Size = New System.Drawing.Size(113, 15)
        Me.lld2w.TabIndex = 1
        Me.lld2w.TabStop = True
        Me.lld2w.Text = "door2windows.com"
        '
        'bOK
        '
        Me.bOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bOK.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.bOK.Location = New System.Drawing.Point(279, 9)
        Me.bOK.Name = "bOK"
        Me.bOK.Size = New System.Drawing.Size(68, 23)
        Me.bOK.TabIndex = 0
        Me.bOK.Text = "OK"
        Me.bOK.UseVisualStyleBackColor = True
        '
        'lHowToUse
        '
        Me.lHowToUse.AutoSize = True
        Me.lHowToUse.Location = New System.Drawing.Point(3, 25)
        Me.lHowToUse.Name = "lHowToUse"
        Me.lHowToUse.Size = New System.Drawing.Size(73, 15)
        Me.lHowToUse.TabIndex = 0
        Me.lHowToUse.Text = "How To Use:"
        '
        'llHowToUse
        '
        Me.llHowToUse.AutoSize = True
        Me.llHowToUse.Location = New System.Drawing.Point(82, 25)
        Me.llHowToUse.Name = "llHowToUse"
        Me.llHowToUse.Size = New System.Drawing.Size(198, 15)
        Me.llHowToUse.TabIndex = 0
        Me.llHowToUse.TabStop = True
        Me.llHowToUse.Text = "door2windows.com/customizergod"
        '
        'lVersionNo
        '
        Me.lVersionNo.AutoSize = True
        Me.lVersionNo.Location = New System.Drawing.Point(82, 0)
        Me.lVersionNo.Name = "lVersionNo"
        Me.lVersionNo.Size = New System.Drawing.Size(38, 15)
        Me.lVersionNo.TabIndex = 0
        Me.lVersionNo.Text = "<1.0>"
        '
        'tlp
        '
        Me.tlp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlp.ColumnCount = 2
        Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlp.Controls.Add(Me.lKishanBagaria, 1, 2)
        Me.tlp.Controls.Add(Me.lVersion, 0, 0)
        Me.tlp.Controls.Add(Me.lHowToUse, 0, 1)
        Me.tlp.Controls.Add(Me.lVersionNo, 1, 0)
        Me.tlp.Controls.Add(Me.llHowToUse, 1, 1)
        Me.tlp.Controls.Add(Me.lDeveloper, 0, 2)
        Me.tlp.Location = New System.Drawing.Point(13, 42)
        Me.tlp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 10)
        Me.tlp.Name = "tlp"
        Me.tlp.RowCount = 3
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlp.Size = New System.Drawing.Size(333, 75)
        Me.tlp.TabIndex = 1
        '
        'lKishanBagaria
        '
        Me.lKishanBagaria.AutoSize = True
        Me.lKishanBagaria.Location = New System.Drawing.Point(82, 50)
        Me.lKishanBagaria.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lKishanBagaria.Name = "lKishanBagaria"
        Me.lKishanBagaria.Size = New System.Drawing.Size(172, 15)
        Me.lKishanBagaria.TabIndex = 0
        Me.lKishanBagaria.Text = "Kishan Bagaria · hi@kishan.info"
        '
        'About
        '
        Me.AcceptButton = Me.bOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(359, 171)
        Me.Controls.Add(Me.tlp)
        Me.Controls.Add(Me.lAbout)
        Me.Controls.Add(Me.pBottom)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "About"
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "About"
        Me.pBottom.ResumeLayout(False)
        Me.pBottom.PerformLayout()
        Me.tlp.ResumeLayout(False)
        Me.tlp.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lAbout As System.Windows.Forms.Label
    Friend WithEvents lDeveloper As System.Windows.Forms.Label
    Friend WithEvents lVersion As System.Windows.Forms.Label
    Friend WithEvents pBottom As System.Windows.Forms.Panel
    Friend WithEvents pBorder As System.Windows.Forms.Panel
    Friend WithEvents lld2w As System.Windows.Forms.LinkLabel
    Friend WithEvents bOK As System.Windows.Forms.Button
    Friend WithEvents lHowToUse As System.Windows.Forms.Label
    Friend WithEvents llHowToUse As System.Windows.Forms.LinkLabel
    Friend WithEvents lVersionNo As System.Windows.Forms.Label
    Friend WithEvents tlp As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lKishanBagaria As System.Windows.Forms.Label
End Class
