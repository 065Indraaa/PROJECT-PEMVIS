Imports MySql.Data.MySqlClient

Public Class FormDataSiswa
    Dim conn As MySqlConnection
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim i As Integer

    ' Koneksi Database
    Sub koneksi()
        conn = New MySqlConnection("server=localhost;user=root;password=;database=db_sekolah;Allow Zero Datetime=True;Convert Zero Datetime=True;")
        Try
            conn.Open()
        Catch ex As Exception
            MsgBox("Koneksi Gagal: " & ex.Message)
        End Try
    End Sub

    ' Tampilkan Data
    Sub TampilData()
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            cmd = New MySqlCommand("SELECT s.nis, s.nama_lengkap, s.jenis_kelamin, s.tanggal_lahir, k.nama_kelas, s.alamat, s.nama_ayah, s.nama_ibu FROM tb_siswa s LEFT JOIN tb_kelas k ON s.id_kelas = k.id_kelas ORDER BY s.id_siswa", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr(0), dr(1), dr(2), dr(3), dr(4), dr(5), dr(6), dr(7))
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' Load ComboBox Kelas
    Sub LoadKelas()
        ComboBox1.Items.Clear()
        Try
            koneksi()
            cmd = New MySqlCommand("SELECT nama_kelas FROM tb_kelas ORDER BY nama_kelas", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                ComboBox1.Items.Add(dr("nama_kelas").ToString())
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' Get ID Kelas dari Nama Kelas
    Function GetIdKelas(namaKelas As String) As Integer
        Dim idKelas As Integer = 0
        Try
            koneksi()
            cmd = New MySqlCommand("SELECT id_kelas FROM tb_kelas WHERE nama_kelas=@nama", conn)
            cmd.Parameters.AddWithValue("@nama", namaKelas)
            dr = cmd.ExecuteReader
            If dr.Read Then
                idKelas = Convert.ToInt32(dr("id_kelas"))
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
        Return idKelas
    End Function

    ' Bersihkan Form
    Sub Bersih()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox1.SelectedIndex = -1
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        TextBox1.Focus()
    End Sub

    ' Form Load
    Private Sub FormDataSiswa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadKelas()
        TampilData()
    End Sub

    ' Button Simpan (menggunakan stored procedure)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("NIS dan Nama harus diisi!")
            Return
        End If

        Dim jk As String = ""
        If RadioButton1.Checked Then
            jk = "Laki-Laki"
        ElseIf RadioButton2.Checked Then
            jk = "Perempuan"
        Else
            MsgBox("Pilih jenis kelamin!")
            Return
        End If

        If ComboBox1.SelectedIndex = -1 Then
            MsgBox("Pilih kelas!")
            Return
        End If

        Try
            Dim idKelas As Integer = GetIdKelas(ComboBox1.Text)

            koneksi()
            cmd = New MySqlCommand("sp_insert_siswa", conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@p_nis", TextBox1.Text)
            cmd.Parameters.AddWithValue("@p_nama_lengkap", TextBox2.Text)
            cmd.Parameters.AddWithValue("@p_jenis_kelamin", jk)
            cmd.Parameters.AddWithValue("@p_id_kelas", idKelas)
            cmd.Parameters.AddWithValue("@p_alamat", TextBox5.Text)
            cmd.Parameters.AddWithValue("@p_nama_ayah", TextBox6.Text)
            cmd.Parameters.AddWithValue("@p_nama_ibu", TextBox7.Text)

            ' ===== TTL STRING (VARCHAR) =====
            cmd.Parameters.AddWithValue("@p_tanggal_lahir", TextBox3.Text)

            cmd.ExecuteNonQuery()

            Dim namaDepan As String = TextBox2.Text.Split(" "c)(0)

            MsgBox("Data Berhasil Disimpan!" & vbCrLf &
                   "Username: " & TextBox1.Text & vbCrLf &
                   "Password: " & namaDepan)

            Bersih()
            TampilData()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' Button Edit
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MsgBox("Pilih data yang akan diedit!")
            Return
        End If

        Dim jk As String = ""
        If RadioButton1.Checked Then
            jk = "Laki-Laki"
        ElseIf RadioButton2.Checked Then
            jk = "Perempuan"
        End If

        Try
            Dim idKelas As Integer = GetIdKelas(ComboBox1.Text)

            koneksi()
            cmd = New MySqlCommand("UPDATE tb_siswa SET nama_lengkap=@nama, jenis_kelamin=@jk, tanggal_lahir=@ttl, id_kelas=@kelas, alamat=@alamat, nama_ayah=@ayah, nama_ibu=@ibu WHERE nis=@nis", conn)

            cmd.Parameters.AddWithValue("@nis", TextBox1.Text)
            cmd.Parameters.AddWithValue("@nama", TextBox2.Text)
            cmd.Parameters.AddWithValue("@jk", jk)

            ' ===== TTL STRING (VARCHAR) =====
            cmd.Parameters.AddWithValue("@ttl", TextBox3.Text)

            cmd.Parameters.AddWithValue("@kelas", idKelas)
            cmd.Parameters.AddWithValue("@alamat", TextBox5.Text)
            cmd.Parameters.AddWithValue("@ayah", TextBox6.Text)
            cmd.Parameters.AddWithValue("@ibu", TextBox7.Text)

            i = cmd.ExecuteNonQuery()

            If i > 0 Then
                MsgBox("Data Berhasil Diupdate!")
                Bersih()
                TampilData()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' Button Cancel
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Bersih()
    End Sub

    ' Button Hapus
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Then
            MsgBox("Pilih data yang akan dihapus!")
            Return
        End If

        If MsgBox("Yakin hapus data siswa ini?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                koneksi()
                cmd = New MySqlCommand("DELETE FROM tb_siswa WHERE nis=@nis", conn)
                cmd.Parameters.AddWithValue("@nis", TextBox1.Text)
                i = cmd.ExecuteNonQuery()

                If i > 0 Then
                    MsgBox("Data Berhasil Dihapus!")
                    Bersih()
                    TampilData()
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                conn.Close()
            End Try
        End If
    End Sub

    ' Button Close
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    ' Button Cari
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        DataGridView1.Rows.Clear()
        Try
            koneksi()
            cmd = New MySqlCommand("SELECT s.nis, s.nama_lengkap, s.jenis_kelamin, s.tanggal_lahir, k.nama_kelas, s.alamat, s.nama_ayah, s.nama_ibu FROM tb_siswa s LEFT JOIN tb_kelas k ON s.id_kelas = k.id_kelas WHERE s.nis LIKE @cari OR s.nama_lengkap LIKE @cari", conn)
            cmd.Parameters.AddWithValue("@cari", "%" & TextBox8.Text & "%")

            dr = cmd.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr(0), dr(1), dr(2), dr(3), dr(4), dr(5), dr(6), dr(7))
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' DataGridView Click
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            TextBox1.Text = row.Cells(0).Value.ToString()
            TextBox2.Text = row.Cells(1).Value.ToString()

            If row.Cells(2).Value.ToString() = "Laki-Laki" Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If

            ' TTL sekarang string penuh
            TextBox3.Text = row.Cells(3).Value.ToString()

            ComboBox1.Text = row.Cells(4).Value.ToString()
            TextBox5.Text = row.Cells(5).Value.ToString()
            TextBox6.Text = row.Cells(6).Value.ToString()
            TextBox7.Text = row.Cells(7).Value.ToString()
        End If
    End Sub

End Class
