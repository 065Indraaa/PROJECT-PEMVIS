Imports MySql.Data.MySqlClient

Public Class FormInputNilai
    Dim conn As MySqlConnection
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader

    '=====================================
    ' KONEKSI
    '=====================================
    Sub koneksi()
        conn = New MySqlConnection("server=localhost;user=root;password=;database=db_sekolah")
        Try
            conn.Open()
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
        TampilData()
    End Sub

    '=====================================
    ' LOAD DATA COMBO & GRID
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

    Sub LoadComboMapel(ByVal idKelas As Integer)
        Try
            koneksi()
            ComboMapel.Items.Clear()
            cmd = New MySqlCommand("
                SELECT kode_mapel, nama_mapel
                FROM tb_mata_pelajaran
                WHERE id_kelas=@idK
                ORDER BY nama_mapel", conn)
            cmd.Parameters.AddWithValue("@idK", idKelas)
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

    Sub TampilData(Optional keyword As String = "")
        DataGridView1.Rows.Clear()

        ' Tambah kolom IdNilai (hidden) jika belum ada
        If Not DataGridView1.Columns.Contains("IdNilai") Then
            Dim col As New DataGridViewTextBoxColumn()
            col.Name = "IdNilai"
            col.HeaderText = "IdNilai"
            col.Visible = False
            DataGridView1.Columns.Add(col)
        End If

        Try
            koneksi()

            Dim sql As String = "
                SELECT 
                    id_nilai,
                    nama_lengkap,
                    nis,
                    nama_kelas,
                    semester,
                    nama_mapel,
                    nilai_pengetahuan,
                    nilai_keterampilan,
                    kehadiran
                FROM v_nilai_lengkap"

            If keyword <> "" Then
                sql &= " WHERE nama_lengkap LIKE @cari 
                         OR nis LIKE @cari 
                         OR nama_kelas LIKE @cari 
                         OR nama_mapel LIKE @cari"
            End If

            sql &= " ORDER BY nama_lengkap, nama_mapel"

            cmd = New MySqlCommand(sql, conn)
            If keyword <> "" Then
                cmd.Parameters.AddWithValue("@cari", "%" & keyword & "%")
            End If

            dr = cmd.ExecuteReader()
            While dr.Read()
                Dim idNilai As Integer = CInt(dr("id_nilai"))
                Dim nama As String = dr("nama_lengkap").ToString()
                Dim nis As String = dr("nis").ToString()
                Dim kelas As String = dr("nama_kelas").ToString()
                Dim sem As String = dr("semester").ToString()
                Dim mapel As String = dr("nama_mapel").ToString()
                Dim nPen As String = dr("nilai_pengetahuan").ToString()
                Dim nKet As String = dr("nilai_keterampilan").ToString()
                Dim hadir As String = ""
                If Not IsDBNull(dr("kehadiran")) Then
                    hadir = dr("kehadiran").ToString()
                End If

                DataGridView1.Rows.Add(
                    nama,
                    nis,
                    kelas,
                    sem,
                    mapel,
                    nPen,
                    nKet,
                    hadir,
                    idNilai
                )
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox("Error Tampil Nilai: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '=====================================
    ' HELPER FUNGSI
    '=====================================
    Private Function CekNilaiValid(input As String, ByRef nilaiOut As Integer) As Boolean
        If Not Integer.TryParse(input, nilaiOut) Then Return False
        If nilaiOut < 0 Or nilaiOut > 100 Then Return False
        Return True
    End Function

    Private Function HitungPredikat(nilai As Integer) As String
        If nilai >= 90 Then
            Return "A"
        ElseIf nilai >= 80 Then
            Return "B"
        ElseIf nilai >= 70 Then
            Return "C"
        ElseIf nilai >= 60 Then
            Return "D"
        Else
            Return "E"
        End If
    End Function

    Private Function GetIdKelasByNama(namaKelas As String) As Integer?
        Try
            koneksi()
            cmd = New MySqlCommand("SELECT id_kelas FROM tb_kelas WHERE nama_kelas=@nama LIMIT 1", conn)
            cmd.Parameters.AddWithValue("@nama", namaKelas)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return CInt(result)
            End If
        Catch ex As Exception
            MsgBox("Error GetIdKelas: " & ex.Message)
        Finally
            conn.Close()
        End Try
        Return Nothing
    End Function

    '=====================================
    ' EVENT COMBO SISWA
    '=====================================
    Private Sub ComboSiswa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboSiswa.SelectedIndexChanged
        If ComboSiswa.SelectedIndex < 0 Then Exit Sub

        Dim nis As String = ComboSiswa.Text.Split("-"c)(0).Trim()

        Try
            koneksi()
            cmd = New MySqlCommand("
                SELECT s.nis, k.nama_kelas, k.id_kelas
                FROM tb_siswa s
                LEFT JOIN tb_kelas k ON s.id_kelas = k.id_kelas
                WHERE s.nis=@nis", conn)
            cmd.Parameters.AddWithValue("@nis", nis)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                TextBox2.Text = dr("nis").ToString()
                TextBox3.Text = dr("nama_kelas").ToString()
                Dim idKelas As Integer = If(IsDBNull(dr("id_kelas")), 0, CInt(dr("id_kelas")))
                dr.Close()

                If idKelas > 0 Then
                    LoadComboMapel(idKelas)
                Else
                    ComboMapel.Items.Clear()
                End If
            Else
                dr.Close()
            End If
        Catch ex As Exception
            MsgBox("Error pilih siswa: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '=====================================
    ' SIMPAN NILAI (INSERT)
    '=====================================
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ' Event ini dihubungkan lewat AddHandler di Designer
        If ComboSiswa.SelectedIndex < 0 Or ComboMapel.SelectedIndex < 0 Or ComboSemester.SelectedIndex < 0 Then
            MsgBox("Data belum lengkap!")
            Exit Sub
        End If

        Dim nPen, nKet As Integer
        If Not CekNilaiValid(TextBox6.Text.Trim(), nPen) Then
            MsgBox("Nilai Pengetahuan harus angka 0-100.")
            Exit Sub
        End If
        If Not CekNilaiValid(TextBox8.Text.Trim(), nKet) Then
            MsgBox("Nilai Keterampilan harus angka 0-100.")
            Exit Sub
        End If

        Dim predPen As String = HitungPredikat(nPen)
        Dim predKet As String = HitungPredikat(nKet)

        TextBox7.Text = predPen
        TextBox9.Text = predKet

        Dim nis As String = TextBox2.Text.Trim()
        Dim kodeMapel As String = ComboMapel.Text.Split("-"c)(0).Trim()
        Dim sem As String = ComboSemester.Text.Trim()

        Try
            koneksi()
            cmd = New MySqlCommand("
                INSERT INTO tb_nilai
                (nis, kode_mapel, semester, nilai_pengetahuan, predikat_pengetahuan,
                 nilai_keterampilan, predikat_keterampilan, kehadiran)
                VALUES
                (@nis, @mapel, @sem, @np, @pp, @nk, @pk, @kh)", conn)

            cmd.Parameters.AddWithValue("@nis", nis)
            cmd.Parameters.AddWithValue("@mapel", kodeMapel)
            cmd.Parameters.AddWithValue("@sem", sem)
            cmd.Parameters.AddWithValue("@np", nPen)
            cmd.Parameters.AddWithValue("@pp", predPen)
            cmd.Parameters.AddWithValue("@nk", nKet)
            cmd.Parameters.AddWithValue("@pk", predKet)
            cmd.Parameters.Add("@kh", MySqlDbType.Int32).Value = DBNull.Value   ' belum ada input kehadiran

            cmd.ExecuteNonQuery()
            MsgBox("Nilai berhasil disimpan.")
            TampilData()
        Catch ex As Exception
            MsgBox("Error Simpan Nilai: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '=====================================
    ' EDIT NILAI (UPDATE)
    '=====================================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.CurrentRow Is Nothing Then
            MsgBox("Pilih data di tabel terlebih dahulu.")
            Exit Sub
        End If

        Dim idObj = DataGridView1.CurrentRow.Cells("IdNilai").Value
        If idObj Is Nothing OrElse IsDBNull(idObj) Then
            MsgBox("IdNilai tidak ditemukan.")
            Exit Sub
        End If
        Dim idNilai As Integer = CInt(idObj)

        If ComboSiswa.SelectedIndex < 0 Or ComboMapel.SelectedIndex < 0 Or ComboSemester.SelectedIndex < 0 Then
            MsgBox("Data belum lengkap!")
            Exit Sub
        End If

        Dim nPen, nKet As Integer
        If Not CekNilaiValid(TextBox6.Text.Trim(), nPen) Then
            MsgBox("Nilai Pengetahuan harus angka 0-100.")
            Exit Sub
        End If
        If Not CekNilaiValid(TextBox8.Text.Trim(), nKet) Then
            MsgBox("Nilai Keterampilan harus angka 0-100.")
            Exit Sub
        End If

        Dim predPen As String = HitungPredikat(nPen)
        Dim predKet As String = HitungPredikat(nKet)

        TextBox7.Text = predPen
        TextBox9.Text = predKet

        Dim nis As String = TextBox2.Text.Trim()
        Dim kodeMapel As String = ComboMapel.Text.Split("-"c)(0).Trim()
        Dim sem As String = ComboSemester.Text.Trim()

        Try
            koneksi()
            cmd = New MySqlCommand("
                UPDATE tb_nilai
                SET nis=@nis,
                    kode_mapel=@mapel,
                    semester=@sem,
                    nilai_pengetahuan=@np,
                    predikat_pengetahuan=@pp,
                    nilai_keterampilan=@nk,
                    predikat_keterampilan=@pk,
                    kehadiran=@kh
                WHERE id_nilai=@id", conn)

            cmd.Parameters.AddWithValue("@id", idNilai)
            cmd.Parameters.AddWithValue("@nis", nis)
            cmd.Parameters.AddWithValue("@mapel", kodeMapel)
            cmd.Parameters.AddWithValue("@sem", sem)
            cmd.Parameters.AddWithValue("@np", nPen)
            cmd.Parameters.AddWithValue("@pp", predPen)
            cmd.Parameters.AddWithValue("@nk", nKet)
            cmd.Parameters.AddWithValue("@pk", predKet)
            cmd.Parameters.Add("@kh", MySqlDbType.Int32).Value = DBNull.Value

            cmd.ExecuteNonQuery()
            MsgBox("Nilai berhasil diupdate.")
            TampilData()
        Catch ex As Exception
            MsgBox("Error Update Nilai: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '=====================================
    ' HAPUS NILAI (DELETE)
    '=====================================
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If DataGridView1.CurrentRow Is Nothing Then
            MsgBox("Pilih data yang akan dihapus.")
            Exit Sub
        End If

        Dim idObj = DataGridView1.CurrentRow.Cells("IdNilai").Value
        If idObj Is Nothing OrElse IsDBNull(idObj) Then
            MsgBox("IdNilai tidak ditemukan.")
            Exit Sub
        End If
        Dim idNilai As Integer = CInt(idObj)

        If MsgBox("Yakin hapus nilai ini?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If

        Try
            koneksi()
            cmd = New MySqlCommand("DELETE FROM tb_nilai WHERE id_nilai=@id", conn)
            cmd.Parameters.AddWithValue("@id", idNilai)
            cmd.ExecuteNonQuery()
            MsgBox("Nilai berhasil dihapus.")
            TampilData()
        Catch ex As Exception
            MsgBox("Error Hapus Nilai: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    '=====================================
    ' CARI NILAI (FILTER)
    '=====================================
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TampilData(TextBox10.Text.Trim())
    End Sub

    '=====================================
    ' CLICK PADA GRID → ISI FORM
    '=====================================
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

        Dim nama As String = row.Cells("Nama").Value.ToString()
        Dim nis As String = row.Cells("NIS").Value.ToString()
        Dim kelas As String = row.Cells("Kelas").Value.ToString()
        Dim sem As String = row.Cells("Semester").Value.ToString()
        Dim mapelNama As String = row.Cells("MataPelajaran").Value.ToString()
        Dim nPenText As String = row.Cells("NilaiPengetahuan").Value.ToString()
        Dim nKetText As String = row.Cells("NilaiKeterampilan").Value.ToString()

        TextBox2.Text = nis
        TextBox3.Text = kelas
        ComboSemester.Text = sem
        TextBox6.Text = nPenText
        TextBox8.Text = nKetText

        Dim nPen, nKet As Integer
        If Integer.TryParse(nPenText, nPen) Then TextBox7.Text = HitungPredikat(nPen)
        If Integer.TryParse(nKetText, nKet) Then TextBox9.Text = HitungPredikat(nKet)

        ' Set ComboSiswa (NIS - Nama)
        Dim displaySiswa As String = nis & " - " & nama
        For i As Integer = 0 To ComboSiswa.Items.Count - 1
            If ComboSiswa.Items(i).ToString() = displaySiswa Then
                ComboSiswa.SelectedIndex = i
                Exit For
            End If
        Next

        ' Reload mapel sesuai kelas & set ComboMapel
        Dim idKelas As Integer? = GetIdKelasByNama(kelas)
        If idKelas.HasValue AndAlso idKelas.Value > 0 Then
            LoadComboMapel(idKelas.Value)
            For i As Integer = 0 To ComboMapel.Items.Count - 1
                If ComboMapel.Items(i).ToString().EndsWith(" - " & mapelNama) Then
                    ComboMapel.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    '=====================================
    ' STUB EVENT (dari Designer AddHandler)
    '=====================================
    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs)
        'kosong, tidak masalah
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)
        'kosong, tidak masalah
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)
        'kosong, tidak masalah
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)
        'kosong, tidak masalah
    End Sub

End Class
