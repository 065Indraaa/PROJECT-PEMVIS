Imports MySql.Data.MySqlClient

Module ModuleKoneksi
    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public dr As MySqlDataReader

    Public Sub bukaKoneksi()
        Try
            Dim cs As String = "server=localhost;user id=root;password=;database=db_sekolah"
            conn = New MySqlConnection(cs)
            If conn.State = ConnectionState.Closed Then conn.Open()
        Catch ex As Exception
            MsgBox("Koneksi gagal: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
End Module
