Public Class FormSiswa
    Private Sub LoadForm(frm As Form)
        PanelUtama.Controls.Clear()
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        PanelUtama.Controls.Add(frm)
        frm.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        LoadForm(New FormPengaturanNew())
    End Sub
End Class