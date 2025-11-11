<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDataGuru
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.KodeGuru = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NamaGuru = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mapel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.JenisKelamin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Alamat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NoTlpn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MySqlCommand1 = New MySql.Data.MySqlClient.MySqlCommand()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Controls.Add(Me.Button6)
        Me.GroupBox1.Controls.Add(Me.TextBox8)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(-1, 1)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(611, 324)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        AddHandler Me.GroupBox1.Enter, AddressOf Me.GroupBox1_Enter
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.MistyRose
        Me.GroupBox3.Controls.Add(Me.TextBox5)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.TextBox4)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.TextBox3)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.RadioButton2)
        Me.GroupBox3.Controls.Add(Me.RadioButton1)
        Me.GroupBox3.Controls.Add(Me.TextBox2)
        Me.GroupBox3.Controls.Add(Me.Button2)
        Me.GroupBox3.Controls.Add(Me.TextBox1)
        Me.GroupBox3.Controls.Add(Me.Button1)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Location = New System.Drawing.Point(66, 47)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(471, 143)
        Me.GroupBox3.TabIndex = 17
        Me.GroupBox3.TabStop = False
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(301, 72)
        Me.TextBox5.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(121, 20)
        Me.TextBox5.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(245, 72)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "No Tlpn"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(96, 44)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(121, 20)
        Me.TextBox4.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 46)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Mata Pelajaran"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(301, 17)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(121, 39)
        Me.TextBox3.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(245, 23)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Alamat"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(175, 94)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(79, 17)
        Me.RadioButton2.TabIndex = 8
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Perempuan"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(96, 94)
        Me.RadioButton1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(68, 17)
        Me.RadioButton1.TabIndex = 7
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Laki-Laki"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(96, 69)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(121, 20)
        Me.TextBox2.TabIndex = 6
        AddHandler Me.TextBox2.TextChanged, AddressOf Me.TextBox2_TextChanged
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.RosyBrown
        Me.Button2.Location = New System.Drawing.Point(292, 108)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(55, 21)
        Me.Button2.TabIndex = 10
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = False
        AddHandler Me.Button2.Click, AddressOf Me.Button2_Click
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(95, 20)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(121, 20)
        Me.TextBox1.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.RosyBrown
        Me.Button1.Location = New System.Drawing.Point(367, 108)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(55, 21)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "Simpan"
        Me.Button1.UseVisualStyleBackColor = False
        AddHandler Me.Button1.Click, AddressOf Me.Button1_Click
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 93)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Jenis Kelamin"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 70)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Nama Lengkap"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Kode Guru"
        '
        'DataGridView1
        '
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.MistyRose
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.KodeGuru, Me.NamaGuru, Me.Mapel, Me.JenisKelamin, Me.Alamat, Me.NoTlpn})
        Me.DataGridView1.GridColor = System.Drawing.Color.MistyRose
        Me.DataGridView1.Location = New System.Drawing.Point(35, 218)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 28
        Me.DataGridView1.Size = New System.Drawing.Size(545, 102)
        Me.DataGridView1.TabIndex = 16
        AddHandler Me.DataGridView1.CellClick, AddressOf Me.DataGridView1_CellClick
        '
        'KodeGuru
        '
        Me.KodeGuru.HeaderText = "KodeGuru"
        Me.KodeGuru.Name = "KodeGuru"
        '
        'NamaGuru
        '
        Me.NamaGuru.HeaderText = "NamaGuru"
        Me.NamaGuru.Name = "NamaGuru"
        '
        'Mapel
        '
        Me.Mapel.HeaderText = "Mapel"
        Me.Mapel.Name = "Mapel"
        '
        'JenisKelamin
        '
        Me.JenisKelamin.HeaderText = "JenisKelamin"
        Me.JenisKelamin.Name = "JenisKelamin"
        '
        'Alamat
        '
        Me.Alamat.HeaderText = "Alamat"
        Me.Alamat.Name = "Alamat"
        '
        'NoTlpn
        '
        Me.NoTlpn.HeaderText = "NoTlpn"
        Me.NoTlpn.Name = "NoTlpn"
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.RosyBrown
        Me.Button6.Location = New System.Drawing.Point(182, 190)
        Me.Button6.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(48, 25)
        Me.Button6.TabIndex = 15
        Me.Button6.Text = "Cari"
        Me.Button6.UseVisualStyleBackColor = False
        AddHandler Me.Button6.Click, AddressOf Me.Button6_Click
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(35, 194)
        Me.TextBox8.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(128, 20)
        Me.TextBox8.TabIndex = 14
        AddHandler Me.TextBox8.TextChanged, AddressOf Me.TextBox8_TextChanged
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.RosyBrown
        Me.Button4.Location = New System.Drawing.Point(445, 194)
        Me.Button4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(55, 21)
        Me.Button4.TabIndex = 12
        Me.Button4.Text = "Hapus"
        Me.Button4.UseVisualStyleBackColor = False
        AddHandler Me.Button4.Click, AddressOf Me.Button4_Click
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.RosyBrown
        Me.Button3.Location = New System.Drawing.Point(504, 194)
        Me.Button3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(53, 21)
        Me.Button3.TabIndex = 11
        Me.Button3.Text = "Edit"
        Me.Button3.UseVisualStyleBackColor = False
        AddHandler Me.Button3.Click, AddressOf Me.Button3_Click
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(209, 5)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 31)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "DATA GURU"
        '
        'MySqlCommand1
        '
        Me.MySqlCommand1.CacheAge = 0
        Me.MySqlCommand1.Connection = Nothing
        Me.MySqlCommand1.EnableCaching = False
        Me.MySqlCommand1.Transaction = Nothing
        '
        'FormDataGuru
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(606, 322)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "FormDataGuru"
        Me.Text = "FormDataGuru"
        AddHandler Me.Load, AddressOf Me.FormDataGuru_Load
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button6 As Button
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents MySqlCommand1 As MySql.Data.MySqlClient.MySqlCommand
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents KodeGuru As DataGridViewTextBoxColumn
    Friend WithEvents NamaGuru As DataGridViewTextBoxColumn
    Friend WithEvents Mapel As DataGridViewTextBoxColumn
    Friend WithEvents JenisKelamin As DataGridViewTextBoxColumn
    Friend WithEvents Alamat As DataGridViewTextBoxColumn
    Friend WithEvents NoTlpn As DataGridViewTextBoxColumn
End Class
