Imports MySql.Data.MySqlClient

Public Class FormLogin
    Dim conn As MySqlConnection
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader

    ' Koneksi Database
    Sub bukaKoneksi()
        Try
            conn = New MySqlConnection("server=localhost;user=root;password=;database=db_sekolah")
            conn.Open()
        Catch ex As Exception
            MsgBox("Koneksi Gagal: " & ex.Message)
        End Try
    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.PasswordChar = "●"
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txtUsername.Text.Trim() = "" Or txtPassword.Text.Trim() = "" Then
            MsgBox("Username dan password tidak boleh kosong!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            bukaKoneksi()
            Dim query As String =
                "SELECT u.*, r.nama_role FROM tb_users u " &
                "INNER JOIN tb_role r ON u.id_role = r.id_role " &
                "WHERE u.username=@user AND u.password=@pass AND u.status='Aktif'"

            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@user", txtUsername.Text)
            cmd.Parameters.AddWithValue("@pass", txtPassword.Text)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                Dim role As String = dr("nama_role").ToString()
                Dim nama As String = dr("nama_lengkap").ToString()

                MsgBox("Selamat datang, " & nama & " (" & role & ")", MsgBoxStyle.Information)

                Select Case role
                    Case "Admin"
                        FormAdmin.Show()
                    Case "Guru"
                        FormGuru.Show()
                    Case "Siswa"
                        FormSiswa.Show()
                    Case Else
                        MsgBox("Role tidak dikenali!")
                End Select

                Me.Hide()
            Else
                MsgBox("Username atau password salah atau akun nonaktif!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
End Class
