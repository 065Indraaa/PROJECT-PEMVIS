Imports MySql.Data.MySqlClient

Public Class DataKelas
    Dim conn As MySqlConnection
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader
    Dim i As Integer

    ' Koneksi Database
    Sub koneksi()
        conn = New MySqlConnection("server=localhost;user=root;password=;database=db_sekolah")
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
            cmd = New MySqlCommand("SELECT nama_kelas, wali_kelas, kapasitas FROM tb_kelas ORDER BY id_kelas", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr(0), dr(1), dr(2))
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' Bersihkan Form
    Sub Bersih()
        namakls.Clear()
        TextBox2.Clear()
        NumericUpDown1.Value = 30
        namakls.Focus()
    End Sub

    ' Form Load
    Private Sub DataKelas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilData()
        NumericUpDown1.Minimum = 10
        NumericUpDown1.Maximum = 50
        NumericUpDown1.Value = 30
    End Sub

    ' Button Tambah
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If namakls.Text = "" Then
            MsgBox("Nama Kelas harus diisi!")
            Return
        End If

        Try
            koneksi()
            cmd = New MySqlCommand("INSERT INTO tb_kelas (nama_kelas, wali_kelas, kapasitas) VALUES (@nama, @wali, @kapasitas)", conn)
            cmd.Parameters.AddWithValue("@nama", namakls.Text)
            cmd.Parameters.AddWithValue("@wali", TextBox2.Text)
            cmd.Parameters.AddWithValue("@kapasitas", NumericUpDown1.Value)
            i = cmd.ExecuteNonQuery()
            If i > 0 Then
                MsgBox("Data Berhasil Disimpan!")
                Bersih()
                TampilData()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' Button Edit
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If namakls.Text = "" Then
            MsgBox("Pilih data yang akan diedit!")
            Return
        End If

        Try
            koneksi()
            cmd = New MySqlCommand("UPDATE tb_kelas SET wali_kelas=@wali, kapasitas=@kapasitas WHERE nama_kelas=@nama", conn)
            cmd.Parameters.AddWithValue("@nama", namakls.Text)
            cmd.Parameters.AddWithValue("@wali", TextBox2.Text)
            cmd.Parameters.AddWithValue("@kapasitas", NumericUpDown1.Value)
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

    ' Button Hapus
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If namakls.Text = "" Then
            MsgBox("Pilih data yang akan dihapus!")
            Return
        End If

        If MsgBox("Yakin hapus data?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                koneksi()
                cmd = New MySqlCommand("DELETE FROM tb_kelas WHERE nama_kelas=@nama", conn)
                cmd.Parameters.AddWithValue("@nama", namakls.Text)
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

    ' DataGridView Click
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            namakls.Text = row.Cells(0).Value.ToString()
            TextBox2.Text = row.Cells(1).Value.ToString()
            NumericUpDown1.Value = Convert.ToInt32(row.Cells(2).Value)
        End If
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    End Sub
End Class