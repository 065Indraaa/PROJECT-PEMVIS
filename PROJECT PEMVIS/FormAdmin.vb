Public Class FormAdmin
    Private Sub LoadForm(frm As Form)
        PanelUtama.Controls.Clear()
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        PanelUtama.Controls.Add(frm)
        frm.Show()
    End Sub

    Private Sub FormAdmin_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click()
        LoadForm(New FormDataGuru())
    End Sub

    Private Sub Button6_Click()
        LoadForm(New FormDataMapel())
    End Sub

    Private Sub Label1_Click()

    End Sub
End Class