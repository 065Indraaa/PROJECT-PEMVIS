Imports MySql.Data.MySqlClient


Public Class FormCatatan
    Dim conn As New MySqlConnection("server=localhost;port=3307;username=root;password=;database=db_absensi")
    Dim i As Integer
    Dim dr As MySqlDataReader

    Public Sub tampil()
        DataGridView1.Rows.Clear()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM db_absensi", conn)
            dr = cmd.ExecuteReader
            While dr.Read
                DataGridView1.Rows.Add(dr.Item("id"), dr.Item("nama"), dr.Item("kelas"), dr.Item("tanggal"), "", dr.Item("keterangan"))
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Try
                If dr IsNot Nothing AndAlso Not dr.IsClosed Then dr.Close()
            Catch
            End Try
            If conn.State <> ConnectionState.Closed Then conn.Close()
        End Try
    End Sub

    Public Sub simpan()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("INSERT INTO db_absensi (id, nama, kelas, tanggal, keterangan) VALUES (@id,@nama,@kelas,@tanggal,@keterangan);", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@id", ComboBox3.Text)
            cmd.Parameters.AddWithValue("@nama", ComboBox1.Text)
            cmd.Parameters.AddWithValue("@kelas", ComboBox2.Text)
            cmd.Parameters.AddWithValue("@tanggal", DateTimePicker1.Text)
            cmd.Parameters.AddWithValue("@catatan", TextBox1.Text)
            cmd.Parameters.AddWithValue("@keterangan", ComboBox4.Text)
            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MessageBox.Show("Data berhasil Di Simpan", "Tambah Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Data Gagal Di Simpan", "Tambah Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub FormCatatan_Load(sender As Object, e As EventArgs)
        tampil()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        simpan()
        tampil()
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ComboBox3.Text = DataGridView1.CurrentRow.Cells(0).Value
        ComboBox1.Text = DataGridView1.CurrentRow.Cells(1).Value
        ComboBox2.Text = DataGridView1.CurrentRow.Cells(2).Value
        DateTimePicker1.Text = DataGridView1.CurrentRow.Cells(3).Value
        TextBox1.Text = DataGridView1.CurrentRow.Cells(4).Value
        ComboBox4.Text = DataGridView1.CurrentRow.Cells(5).Value
        Button1.Enabled = False
    End Sub

    Public Sub Ubah()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("UPDATE db_absensi SET id=@id, nama=@nama, kelas=@kalas, tanggal=@tanggal, catatan=@catatan, keterangan=@keterangan WHERE id=@id;", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@id", ComboBox3.Text)
            cmd.Parameters.AddWithValue("@nama", ComboBox1.Text)
            cmd.Parameters.AddWithValue("@kelas", ComboBox2.Text)
            cmd.Parameters.AddWithValue("@tanggal", DateTimePicker1.Text)
            cmd.Parameters.AddWithValue("@catatan", TextBox1.Text)
            cmd.Parameters.AddWithValue("@keterangan", ComboBox4.Text)
            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MessageBox.Show("Data berhasil Di Ubah", "Ubah Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Data Gagal Di Ubah", "Ubah Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
            tampil()
        End Try
    End Sub

    Public Sub hapus()
        If MsgBox("Apakah anda yakin ingin menghapus data ini?", MsgBoxStyle.Question + vbYesNo, "Hapus Data") = vbYes Then
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("delete from absensi where id=@id", conn)
                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@id", ComboBox3.Text)
                i = cmd.ExecuteNonQuery
                If i > 0 Then
                    MessageBox.Show("Data Berhasil Dihapus", "Hapus Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Data Gagal Dihapus", "Hapus Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                conn.Close()
                tampil()
                Reset()
            End Try
        Else
            Return
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        'Tambahkan kode di sini sesuai kebutuhan
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Ubah()
        tampil()
    End Sub

    Private Sub Reset()
        ComboBox3.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        DateTimePicker1.Value = DateTime.Now
        TextBox1.Clear()
        ComboBox4.Text = ""
        Button1.Enabled = True
    End Sub
End Class