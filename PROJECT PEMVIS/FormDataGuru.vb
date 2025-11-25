Imports MySql.Data.MySqlClient

Public Class FormDataGuru
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader
    Dim i As Integer

    ' Tampilkan Data
    Sub TampilData()
        DataGridView1.Rows.Clear()
        Try
            Using conn = TryOpenConnection()
                cmd = New MySqlCommand("SELECT nip, nama_lengkap, mata_pelajaran, jenis_kelamin, alamat, no_telepon FROM v_guru_lengkap ORDER BY id_guru", conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    DataGridView1.Rows.Add(dr(0), dr(1), dr(2), dr(3), dr(4), dr(5))
                End While
                dr.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ' Bersihkan Form
    Sub Bersih()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox4.Clear()
        TextBox3.Clear()
        TextBox5.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        TextBox1.Focus()
    End Sub

    ' Form Load
    Private Sub FormDataGuru_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilData()
    End Sub

    ' Button Simpan
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Data belum lengkap!")
            Return
        End If

        Dim jk As String = ""
        If RadioButton1.Checked Then jk = "Laki-Laki"
        If RadioButton2.Checked Then jk = "Perempuan"

        Try
            Using conn = TryOpenConnection()
                ' Gunakan stored procedure untuk auto create user
                cmd = New MySqlCommand("sp_insert_guru", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@p_nip", TextBox1.Text)
                cmd.Parameters.AddWithValue("@p_nama_lengkap", TextBox2.Text)
                cmd.Parameters.AddWithValue("@p_mata_pelajaran", TextBox4.Text)
                cmd.Parameters.AddWithValue("@p_jenis_kelamin", jk)
                cmd.Parameters.AddWithValue("@p_alamat", TextBox3.Text)
                cmd.Parameters.AddWithValue("@p_no_telepon", TextBox5.Text)
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Data Berhasil Disimpan!" & vbCrLf &
                   "Username: " & TextBox1.Text & vbCrLf &
                   "Password: " & TextBox2.Text.Split(" "c)(0),
                   MsgBoxStyle.Information)
            Bersih()
            TampilData()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    ' Button Edit
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("Pilih data yang akan diedit!")
            Return
        End If

        Dim jk As String = ""
        If RadioButton1.Checked Then jk = "Laki-Laki"
        If RadioButton2.Checked Then jk = "Perempuan"

        Try
            Using conn = TryOpenConnection()
                cmd = New MySqlCommand("UPDATE tb_guru SET nama_lengkap=@nama, mata_pelajaran=@mapel, jenis_kelamin=@jk, alamat=@alamat, no_telepon=@telp WHERE nip=@nip", conn)
                cmd.Parameters.AddWithValue("@nip", TextBox1.Text)
                cmd.Parameters.AddWithValue("@nama", TextBox2.Text)
                cmd.Parameters.AddWithValue("@mapel", TextBox4.Text)
                cmd.Parameters.AddWithValue("@jk", jk)
                cmd.Parameters.AddWithValue("@alamat", TextBox3.Text)
                cmd.Parameters.AddWithValue("@telp", TextBox5.Text)
                i = cmd.ExecuteNonQuery()
                If i > 0 Then
                    MsgBox("Data Berhasil Diupdate!")
                    Bersih()
                    TampilData()
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ' Button Hapus
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Then
            MsgBox("Pilih data yang akan dihapus!")
            Return
        End If

        If MsgBox("Yakin hapus data?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Using conn = TryOpenConnection()
                    cmd = New MySqlCommand("DELETE FROM tb_guru WHERE nip=@nip", conn)
                    cmd.Parameters.AddWithValue("@nip", TextBox1.Text)
                    i = cmd.ExecuteNonQuery()
                    If i > 0 Then
                        MsgBox("Data Berhasil Dihapus!")
                        Bersih()
                        TampilData()
                    End If
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    ' Button Cancel
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Bersih()
    End Sub

    ' Button Cari
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        DataGridView1.Rows.Clear()
        Try
            Using conn = TryOpenConnection()
                cmd = New MySqlCommand("SELECT nip, nama_lengkap, mata_pelajaran, jenis_kelamin, alamat, no_telepon FROM v_guru_lengkap WHERE nip LIKE @cari OR nama_lengkap LIKE @cari", conn)
                cmd.Parameters.AddWithValue("@cari", "%" & TextBox8.Text & "%")
                dr = cmd.ExecuteReader
                While dr.Read
                    DataGridView1.Rows.Add(dr(0), dr(1), dr(2), dr(3), dr(4), dr(5))
                End While
                dr.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ' DataGridView Click
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBox1.Text = row.Cells(0).Value.ToString()
            TextBox2.Text = row.Cells(1).Value.ToString()
            TextBox4.Text = row.Cells(2).Value.ToString()
            If row.Cells(3).Value.ToString() = "Laki-Laki" Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If
            TextBox3.Text = row.Cells(4).Value.ToString()
            TextBox5.Text = row.Cells(5).Value.ToString()
        End If
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
    End Sub
End Class