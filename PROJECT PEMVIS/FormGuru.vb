Public Class FormGuru

    Private Sub LoadForm(frm As Form)
        PanelUtama.Controls.Clear()
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        PanelUtama.Controls.Add(frm)
        frm.Show()
    End Sub

    Private Sub FormGuru_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Misalnya, langsung tampilkan form absensi
        LoadForm(New FormCatatan())
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadForm(New FormCatatan())
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

End Class
