Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms

Public Class FormPrintPreview
    Private Sub FormPrintPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Koneksi ke database MySQL
        Dim conn As New MySqlConnection("server=localhost;userid=root;password=;database=db_absensi")
        Dim da As New MySqlDataAdapter("SELECT id, nama, kelas, tanggal, catatan, keterangan FROM absensi", conn)
        Dim ds As New DataSet()
        da.Fill(ds, "absensi")

        ' Tampilkan data di report
        ReportViewer1.LocalReport.ReportPath = Application.StartupPath & "\Report1.rdlc"
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", ds.Tables("absensi")))
        ReportViewer1.RefreshReport()
    End Sub
End Class
