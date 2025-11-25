Imports MySql.Data.MySqlClient

Public Class FormDataMapel
    Dim conn As MySqlConnection
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader
    Dim i As Integer

    Sub koneksi()
        conn = New MySqlConnection("server=localhost;user=root;password=;database=db_sekolah")
        Try
            conn.Open()
        Catch ex As Exception
            MsgBox("Koneksi Gagal: " & ex.Message)
        End Try
    End Sub

    '========================
    ' LOAD COMBO GURU & KELAS & SEMESTER
    '========================
    Sub LoadCombo()
        ' Combo Guru
        ComboBox1.Items.Clear()
        Try
            koneksi()
            cmd = New MySqlCommand("SELECT id_guru, nama_lengkap FROM tb_guru ORDER BY nama_lengkap", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                ' Format: "id_guru - nama_lengkap"
                ComboBox1.Items.Add(dr("id_guru").ToString() & " - " & dr("nama_lengkap").ToString())
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox("Error load guru: " & ex.Message)
        Finally
            conn.Close()
        End Try

        ' Combo Kelas (tb_kelas)
        ComboKelas.Items.Clear()
        Try
            koneksi()
            cmd = New MySqlCommand("SELECT id_kelas, nama_kelas FROM tb_kelas ORDER BY nama_kelas", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                ' Format: "id_kelas - nama_kelas"
                ComboKelas.Items.Add(dr("id_kelas").ToString() & " - " & dr("nama_kelas").ToString())
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox("Error load kelas: " & ex.Message)
        Finally
            conn.Close()
        End Try

        ' Combo Semester
        ComboSemester.Items.Clear()
        ComboSemester.Items.Add("1")
        ComboSemester.Items.Add("2")
    End Sub

    '========================
    ' TAMPILKAN DATA MAPEL
    '========================
    Sub TampilData()
        DataGridView1.Rows.Clear()

        ' Pastikan ada kolom IdKelas tersembunyi di DataGrid (index 5)
        If DataGridView1.Columns.Count < 6 Then
            Dim col As New DataGridViewTextBoxColumn()
            col.Name = "IdKelas"
            col.HeaderText = "IdKelas"
            col.Visible = False
            DataGridView1.Columns.Add(col)
        End If

        Try
            koneksi()
            cmd = New MySqlCommand("
                SELECT m.kode_mapel, 
                       m.nama_mapel, 
                       g.nama_lengkap AS nama_guru, 
                       m.semester, 
                       k.nama_kelas, 
                       m.id_kelas
                FROM tb_mata_pelajaran m
                LEFT JOIN tb_guru g ON m.id_guru = g.id_guru
                LEFT JOIN tb_kelas k ON m.id_kelas = k.id_kelas
                ORDER BY m.id_mapel", conn)

            dr = cmd.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(
                    dr("kode_mapel").ToString(),
                    dr("nama_mapel").ToString(),
                    dr("nama_guru").ToString(),
                    dr("semester").ToString(),
                    dr("nama_kelas").ToString(),
                    dr("id_kelas")  ' kolom ke-6 (hidden), untuk mapping kembali
                )
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox("Error tampil data: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '========================
    ' BERSIHKAN FORM
    '========================
    Sub Bersih()
        TextBox1.Clear() ' Kode Mapel
        TextBox2.Clear() ' Nama Mapel
        ComboBox1.SelectedIndex = -1  ' Guru
        ComboSemester.SelectedIndex = -1
        ComboKelas.SelectedIndex = -1
        TextBox5.Clear() ' Text Cari
        TextBox1.Focus()
    End Sub

    '========================
    ' FORM LOAD
    '========================
    Private Sub FormDataMapel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo()
        TampilData()
    End Sub

    '========================
    ' BUTTON SIMPAN
    '========================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or
           ComboSemester.SelectedIndex = -1 Or ComboKelas.SelectedIndex = -1 Then

            MsgBox("Kode, Nama Mapel, Semester dan Kelas harus diisi!")
            Return
        End If

        ' Ambil id_guru (boleh kosong)
        Dim idGuru As Object = Nothing
        If ComboBox1.SelectedIndex >= 0 Then
            idGuru = ComboBox1.Text.Split("-"c)(0).Trim()
        End If

        ' Ambil id_kelas dari ComboKelas (wajib)
        Dim idKelas As Integer = Integer.Parse(ComboKelas.Text.Split("-"c)(0).Trim())

        Try
            koneksi()
            cmd = New MySqlCommand("
                INSERT INTO tb_mata_pelajaran
                (kode_mapel, nama_mapel, id_guru, semester, id_kelas)
                VALUES (@kode, @nama, @idguru, @semester, @idkelas)", conn)

            cmd.Parameters.AddWithValue("@kode", TextBox1.Text)
            cmd.Parameters.AddWithValue("@nama", TextBox2.Text)

            If idGuru Is Nothing Then
                cmd.Parameters.Add("@idguru", MySqlDbType.Int32).Value = DBNull.Value
            Else
                cmd.Parameters.AddWithValue("@idguru", idGuru)
            End If

            cmd.Parameters.AddWithValue("@semester", ComboSemester.Text)
            cmd.Parameters.AddWithValue("@idkelas", idKelas)

            cmd.ExecuteNonQuery()

            MsgBox("Data mata pelajaran berhasil disimpan!", MsgBoxStyle.Information)
            TampilData()
            Bersih()
        Catch ex As Exception
            MsgBox("Error simpan: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '========================
    ' BUTTON EDIT
    '========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MsgBox("Pilih data yang akan diedit!")
            Return
        End If

        If ComboSemester.SelectedIndex = -1 Or ComboKelas.SelectedIndex = -1 Then
            MsgBox("Semester dan Kelas harus dipilih!")
            Return
        End If

        Dim idGuru As Object = Nothing
        If ComboBox1.SelectedIndex >= 0 Then
            idGuru = ComboBox1.Text.Split("-"c)(0).Trim()
        End If

        Dim idKelas As Integer = Integer.Parse(ComboKelas.Text.Split("-"c)(0).Trim())

        Try
            koneksi()
            cmd = New MySqlCommand("
                UPDATE tb_mata_pelajaran
                SET nama_mapel=@nama, 
                    id_guru=@idguru, 
                    semester=@semester, 
                    id_kelas=@idkelas
                WHERE kode_mapel=@kode", conn)

            cmd.Parameters.AddWithValue("@kode", TextBox1.Text)
            cmd.Parameters.AddWithValue("@nama", TextBox2.Text)

            If idGuru Is Nothing Then
                cmd.Parameters.Add("@idguru", MySqlDbType.Int32).Value = DBNull.Value
            Else
                cmd.Parameters.AddWithValue("@idguru", idGuru)
            End If

            cmd.Parameters.AddWithValue("@semester", ComboSemester.Text)
            cmd.Parameters.AddWithValue("@idkelas", idKelas)

            i = cmd.ExecuteNonQuery()
            If i > 0 Then
                MsgBox("Data mata pelajaran berhasil diupdate!", MsgBoxStyle.Information)
                Bersih()
                TampilData()
            End If
        Catch ex As Exception
            MsgBox("Error update: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '========================
    ' BUTTON HAPUS
    '========================
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Then
            MsgBox("Pilih data yang akan dihapus!")
            Return
        End If

        If MsgBox("Yakin ingin menghapus data mata pelajaran ini?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                koneksi()
                cmd = New MySqlCommand("DELETE FROM tb_mata_pelajaran WHERE kode_mapel=@kode", conn)
                cmd.Parameters.AddWithValue("@kode", TextBox1.Text)
                i = cmd.ExecuteNonQuery()
                If i > 0 Then
                    MsgBox("Data mata pelajaran berhasil dihapus!", MsgBoxStyle.Information)
                    Bersih()
                    TampilData()
                End If
            Catch ex As Exception
                MsgBox("Error hapus: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End If
    End Sub

    '========================
    ' BUTTON CANCEL
    '========================
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Bersih()
    End Sub

    '========================
    ' BUTTON CARI
    '========================
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            cmd = New MySqlCommand("
                SELECT m.kode_mapel, 
                       m.nama_mapel, 
                       g.nama_lengkap AS nama_guru, 
                       m.semester, 
                       k.nama_kelas,
                       m.id_kelas
                FROM tb_mata_pelajaran m
                LEFT JOIN tb_guru g ON m.id_guru = g.id_guru
                LEFT JOIN tb_kelas k ON m.id_kelas = k.id_kelas
                WHERE m.kode_mapel LIKE @cari OR m.nama_mapel LIKE @cari
                ORDER BY m.id_mapel", conn)

            cmd.Parameters.AddWithValue("@cari", "%" & TextBox5.Text & "%")
            dr = cmd.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(
                    dr("kode_mapel").ToString(),
                    dr("nama_mapel").ToString(),
                    dr("nama_guru").ToString(),
                    dr("semester").ToString(),
                    dr("nama_kelas").ToString(),
                    dr("id_kelas")
                )
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox("Error cari: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '========================
    ' KLIK DATAGRID
    '========================
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            TextBox1.Text = row.Cells(0).Value.ToString() ' kode_mapel
            TextBox2.Text = row.Cells(1).Value.ToString() ' nama_mapel
            ComboSemester.Text = row.Cells(3).Value.ToString() ' semester

            ' Set ComboKelas berdasarkan id_kelas (kolom ke-6)
            Dim idKelasValue As String = ""
            If Not IsDBNull(row.Cells(5).Value) AndAlso row.Cells(5).Value IsNot Nothing Then
                idKelasValue = row.Cells(5).Value.ToString()
            End If

            If idKelasValue <> "" Then
                For idx As Integer = 0 To ComboKelas.Items.Count - 1
                    If ComboKelas.Items(idx).ToString().StartsWith(idKelasValue & " -") Then
                        ComboKelas.SelectedIndex = idx
                        Exit For
                    End If
                Next
            Else
                ComboKelas.SelectedIndex = -1
            End If

            ' Untuk guru, kita hanya isi text tampilannya saja (nama_guru)
            ComboBox1.Text = row.Cells(2).Value.ToString()
        End If
    End Sub

End Class
