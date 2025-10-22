Public Class FormGuru
    Private Sub LoadForm(FormGuru As Form)
        Panel2.Controls.Clear() ' 
        FormGuru.TopLevel = False
        FormGuru.FormBorderStyle = FormBorderStyle.None
        FormGuru = DockStyle.Fill
        Panel2.Controls.Add(FormGuru)
        FormGuru.Show()

    End Sub


    Private Sub Button1_Click()
        LoadForm(New FormCatatan())
    End Sub

    Private Sub Button3_Click()

    End Sub

    Private Sub Button2_Click()

    End Sub

    Private Sub Button5_Click()

    End Sub

    Private Sub FormGuru_Load()

    End Sub
End Class