Imports MySql.Data.MySqlClient

Public Class FormInputNilai
    Dim conn As MySqlConnection
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader

    '=====================================
    ' KONEKSI DATABASE
    '=====================================
    Sub koneksi()
        conn = New MySqlConnection("server=localhost;user=root;password=;database=db_sekolah")
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
        Catch ex As Exception
            MsgBox("Koneksi Gagal: " & ex.Message)
        End Try
    End Sub

    '=====================================
    ' FORM LOAD
    '=====================================
    Private Sub FormInputNilai_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboSiswa()
        LoadComboSemester()
        LoadComboMapel()
        TampilData()
    End Sub

    '=====================================
    ' LOAD DATA COMBO
    '=====================================
    Sub LoadComboSiswa()
        Try
            koneksi()
            ComboSiswa.Items.Clear()
            cmd = New MySqlCommand("
                SELECT nis, nama_lengkap 
                FROM tb_siswa 
                ORDER BY nama_lengkap", conn)
            dr = cmd.ExecuteReader()
            While dr.Read()
                ComboSiswa.Items.Add(dr("nis").ToString() & " - " & dr("nama_lengkap").ToString())
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox("Error Load Siswa: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Sub LoadComboSemester()
        ComboSemester.Items.Clear()
        ComboSemester.Items.Add("1")
        ComboSemester.Items.Add("2")
    End Sub

    Sub LoadComboMapel()
        Try
            koneksi()
            ComboMapel.Items.Clear()
            cmd = New MySqlCommand("
                SELECT kode_mapel, nama_mapel 
                FROM tb_mata_pelajaran
                ORDER BY nama_mapel", conn)
            dr = cmd.ExecuteReader()
            While dr.Read()
                ComboMapel.Items.Add(dr("kode_mapel").ToString() & " - " & dr("nama_mapel").ToString())
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox("Error Load Mapel: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '=====================================
    ' LOAD DATA GRID
    '=====================================
    Sub TampilData(Optional keyword As String = "")
        DataGridView1.Rows.Clear()
        If Not DataGridView1.Columns.Contains("IdNilai") Then
            Dim col As New DataGridViewTextBoxColumn()
            col.Name = "IdNilai"
            col.Visible = False
            DataGridView1.Columns.Add(col)
        End If

        Try
            koneksi()
            Dim sql As String = "
                SELECT id_nilai, nama_lengkap, nis, nama_kelas, semester, nama_mapel,
                       nilai_pengetahuan, nilai_keterampilan, kehadiran
                FROM v_nilai_lengkap"

            If keyword <> "" Then
                sql &= " WHERE nama_lengkap LIKE @cari 
                         OR nis LIKE @cari 
                         OR nama_mapel LIKE @cari"
            End If

            cmd = New MySqlCommand(sql, conn)
            If keyword <> "" Then
                cmd.Parameters.AddWithValue("@cari", "%" & keyword & "%")
            End If

            dr = cmd.ExecuteReader()
            While dr.Read()
                DataGridView1.Rows.Add(
                    dr("nama_lengkap").ToString(),
                    dr("nis").ToString(),
                    dr("nama_kelas").ToString(),
                    dr("semester").ToString(),
                    dr("nama_mapel").ToString(),
                    dr("nilai_pengetahuan").ToString(),
                    dr("nilai_keterampilan").ToString(),
                    If(IsDBNull(dr("kehadiran")), "", dr("kehadiran")),
                    dr("id_nilai")
                )
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox("Error Tampil: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '=====================================
    ' PILIH SISWA → AUTO KELAS
    '=====================================
    Private Sub ComboSiswa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboSiswa.SelectedIndexChanged
        If ComboSiswa.SelectedIndex < 0 Then Exit Sub
        Dim nis As String = ComboSiswa.Text.Split("-"c)(0).Trim()

        Try
            koneksi()
            cmd = New MySqlCommand("
                SELECT s.nis, k.nama_kelas
                FROM tb_siswa s
                LEFT JOIN tb_kelas k ON s.id_kelas = k.id_kelas
                WHERE s.nis=@nis", conn)
            cmd.Parameters.AddWithValue("@nis", nis)
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                TextBox2.Text = dr("nis").ToString()
                TextBox3.Text = dr("nama_kelas").ToString()
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox("Error pilih siswa: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '=====================================
    ' SIMPAN NILAI
    '=====================================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboSiswa.SelectedIndex < 0 Or ComboMapel.SelectedIndex < 0 Or ComboSemester.SelectedIndex < 0 Then
            MsgBox("Lengkapi semua data!")
            Exit Sub
        End If

        Dim np As Integer = CInt(TextBox6.Text)
        Dim nk As Integer = CInt(TextBox8.Text)

        Try
            koneksi()
            cmd = New MySqlCommand("
                INSERT INTO tb_nilai
                (nis, kode_mapel, semester, nilai_pengetahuan, nilai_keterampilan)
                VALUES (@nis, @mapel, @sem, @np, @nk)", conn)

            cmd.Parameters.AddWithValue("@nis", TextBox2.Text)
            cmd.Parameters.AddWithValue("@mapel", ComboMapel.Text.Split("-"c)(0).Trim())
            cmd.Parameters.AddWithValue("@sem", ComboSemester.Text)
            cmd.Parameters.AddWithValue("@np", np)
            cmd.Parameters.AddWithValue("@nk", nk)

            cmd.ExecuteNonQuery()
            MsgBox("Nilai berhasil disimpan")
            TampilData()
        Catch ex As Exception
            MsgBox("Error Simpan: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '=====================================
    ' DELETE NILAI
    '=====================================
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If DataGridView1.CurrentRow Is Nothing Then Exit Sub
        If MsgBox("Yakin hapus?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

        Try
            koneksi()
            cmd = New MySqlCommand("DELETE FROM tb_nilai WHERE id_nilai=@id", conn)
            cmd.Parameters.AddWithValue("@id", CInt(DataGridView1.CurrentRow.Cells("IdNilai").Value))
            cmd.ExecuteNonQuery()
            MsgBox("Hapus sukses")
            TampilData()
        Catch ex As Exception
            MsgBox("Error Hapus: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '=====================================
    ' SEARCH
    '=====================================
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TampilData(TextBox10.Text)
    End Sub

    '=====================================
    ' CLOSE
    '=====================================
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub
End Class
