<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSiswa
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
        Me.PanelUtama = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelUtama
        '
        Me.PanelUtama.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelUtama.Location = New System.Drawing.Point(212, 50)
        Me.PanelUtama.Name = "PanelUtama"
        Me.PanelUtama.Size = New System.Drawing.Size(821, 491)
        Me.PanelUtama.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.LightPink
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(212, 491)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.LightPink
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Button3.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Button3.Location = New System.Drawing.Point(0, 183)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(212, 39)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "JADWAL"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.LightPink
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Button2.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Button2.Location = New System.Drawing.Point(0, 138)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(212, 39)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "NILAI"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.LightPink
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.Location = New System.Drawing.Point(0, 93)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(212, 39)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "ABSENSI"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Pink
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Button5)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1033, 50)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(451, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(256, 24)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "SMA HARAPAN BANGSA"
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.LightPink
        Me.Button5.FlatAppearance.BorderSize = 0
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button5.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.Button5.Location = New System.Drawing.Point(6, 12)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(128, 33)
        Me.Button5.TabIndex = 2
        Me.Button5.Text = "Pengaturan"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'FormSiswa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1033, 541)
        Me.Controls.Add(Me.PanelUtama)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "FormSiswa"
        Me.Text = "FormSiswa"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelUtama As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button5 As Button
End Class
