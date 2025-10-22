Public Class FormGuru

    ' Method untuk menampilkan form di dalam panel
    Private Sub LoadForm(frm As Form)
        PanelUtama.Controls.Clear()
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        PanelUtama.Controls.Add(frm)
        frm.Show()
    End Sub

    ' Tombol 1: buka FormCatatan
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadForm(New FormCatatan())
    End Sub

    ' Tombol 2: contoh nanti bisa diarahkan ke form lain
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Contoh: LoadForm(New FormDataSiswa())
    End Sub

    ' Tombol 3: contoh
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Contoh: LoadForm(New FormNilai())
    End Sub

    ' Tombol 5: contoh
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Contoh: LoadForm(New FormProfilGuru())
    End Sub

    ' Saat form utama guru dibuka
    Private Sub FormGuru_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Misalnya langsung tampilkan FormCatatan pertama kali
        LoadForm(New FormCatatan())
    End Sub

End Class
