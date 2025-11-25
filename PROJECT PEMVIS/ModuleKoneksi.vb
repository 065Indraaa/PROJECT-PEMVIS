Imports MySql.Data.MySqlClient

Module ModuleKoneksi
    Private Const ConnectionString As String = "server=localhost;user id=root;password=;database=db_sekolah"

    Public Function OpenConnection() As MySqlConnection
        Dim connection = New MySqlConnection(ConnectionString)
        connection.Open()
        Return connection
    End Function

    Public Function TryOpenConnection() As MySqlConnection
        Try
            Return OpenConnection()
        Catch ex As Exception
            MsgBox("Koneksi gagal: " & ex.Message, MsgBoxStyle.Critical)
            Throw
        End Try
    End Function
End Module
