Public Class FormGuru
    Private Sub LoadForm(childForm As Form)
        Panel2.Controls.Clear() ' 
        childForm.TopLevel = False
        childForm.FormBorderStyle = FormBorderStyle.None
        childForm.Dock = DockStyle.Fill
        Panel2.Controls.Add(childForm)
        childForm.Show()

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