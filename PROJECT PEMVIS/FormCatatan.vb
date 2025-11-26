Imports MySql.Data.MySqlClient

Public Class FormCatatan
    Dim conn As MySqlConnection
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader

    Sub koneksi()
        conn = New MySqlConnection("server=localhost;user=root;password=;database=db_sekolah")
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
        Catch ex As Exception
            MsgBox("Koneksi gagal: " & ex.Message)
        End Try
    End Sub

    Private Sub FormCatatan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilAbsensi()
    End Sub

    Sub TampilAbsensi()
        DataGridView1.Rows.Clear()

        Try
            koneksi()
            cmd = New MySqlCommand("
                SELECT id_absensi, nama_lengkap, nama_kelas, tanggal, catatan, keterangan
                FROM v_absensi
                ORDER BY tanggal DESC
            ", conn)
            dr = cmd.ExecuteReader()

            While dr.Read()
                DataGridView1.Rows.Add(
                    dr("id_absensi"),
                    dr("nama_lengkap"),
                    dr("nama_kelas"),
                    CDate(dr("tanggal")).ToString("dd-MM-yyyy"),
                    If(IsDBNull(dr("catatan")), "", dr("catatan").ToString()),
                    dr("keterangan").ToString()
                )
            End While
            dr.Close()

        Catch ex As Exception
            MsgBox("Error memuat absensi: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    'Klik Cetak
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MsgBox("Fitur Cetak akan dibuat setelah format laporan tersedia.")
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        'Tidak ada aksi untuk saat ini
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        'Tidak ada aksi
    End Sub
End Class
