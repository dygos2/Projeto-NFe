Imports System.Xml
Public Class frm_Show_logs

    Dim dt_pdf As DataTable
    Dim dc As DataColumn
    Dim dr As DataRow
    Private Sub frm_Show_logs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ds As DataSet = New DataSet()
        ds.ReadXml(Application.StartupPath & "\XMLDOC.xml")
       
        If (ds.Tables("Pdf_Name").Rows.Count > 0) Then
            For pdf_count As Integer = 0 To ds.Tables("Pdf_Name").Rows().Count - 1
                If (pdf_count = 0) Then
                    dt_pdf = New DataTable()
                    dc = New DataColumn("Pdf Id")

                    dt_pdf.Columns.Add(dc)
                    dc = New DataColumn("Pdf Series")
                    dt_pdf.Columns.Add(dc)
                    dc = New DataColumn("Pdf Name")
                    dt_pdf.Columns.Add(dc)
                    dc = New DataColumn("Pdf Print DateTime")
                    dt_pdf.Columns.Add(dc)
                End If
                dr = dt_pdf.NewRow()
                dr("Pdf Id") = ds.Tables("Pdf_Id").Rows(pdf_count)(0)
                dr("Pdf Series") = ds.Tables("Pdf_Series").Rows(pdf_count)(0)
                dr("Pdf Name") = ds.Tables("Pdf_Name").Rows(pdf_count)(0)
                dr("Pdf Print DateTime") = ds.Tables("Pdf_Print_DateTime").Rows(pdf_count)(0)
                dt_pdf.Rows.Add(dr)
            Next

            DataGridView1.DataSource = dt_pdf.DefaultView
            DataGridView1.AllowUserToOrderColumns = True
            DataGridView1.AllowUserToResizeColumns = True



        Else
        End If

    End Sub

    Private Sub frm_Show_logs_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If (Me.DialogResult = DialogResult.Abort) Then

            Icon = My.Resources.TrayIcon
            Me.Hide()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class