<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DictionaryViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DictionaryViewer))
        Me.lv = New System.Windows.Forms.ListView()
        Me.ch1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ch2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'lv
        '
        Me.lv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ch1, Me.ch2})
        Me.lv.FullRowSelect = True
        Me.lv.Location = New System.Drawing.Point(12, 12)
        Me.lv.Name = "lv"
        Me.lv.Size = New System.Drawing.Size(560, 337)
        Me.lv.TabIndex = 0
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ch1
        '
        Me.ch1.Text = ""
        Me.ch1.Width = 175
        '
        'ch2
        '
        Me.ch2.Text = ""
        Me.ch2.Width = 360
        '
        'DictionaryViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(584, 361)
        Me.Controls.Add(Me.lv)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DictionaryViewer"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lv As System.Windows.Forms.ListView
    Friend WithEvents ch1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ch2 As System.Windows.Forms.ColumnHeader
End Class
