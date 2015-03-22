Imports System.Xml
Public Class frm_Show_logs

    Dim dt_pdf As DataTable
    Dim dc As DataColumn
    Dim dr As DataRow
    Private Sub frm_Show_logs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim ds As DataSet = New DataSet()
            ds.ReadXml(Application.StartupPath & "\XMLDOC.xml")

            If (ds.Tables("Pdf_Name").Rows.Count > 0) Then
                For pdf_count As Integer = 0 To ds.Tables("Pdf_Name").Rows().Count - 1
                    If (pdf_count = 0) Then
                        dt_pdf = New DataTable()
                        dc = New DataColumn("Num. NFe")

                        dt_pdf.Columns.Add(dc)
                        dc = New DataColumn("Serie NFe")
                        dt_pdf.Columns.Add(dc)
                        dc = New DataColumn("Chave")
                        dt_pdf.Columns.Add(dc)
                        dc = New DataColumn("Hora da Impressão")
                        dt_pdf.Columns.Add(dc)
                    End If
                    dr = dt_pdf.NewRow()
                    dr("Num. NFe") = ds.Tables("Pdf_Id").Rows(pdf_count)(0)
                    dr("Serie NFe") = ds.Tables("Pdf_Series").Rows(pdf_count)(0)
                    dr("Chave") = ds.Tables("Pdf_Name").Rows(pdf_count)(0)
                    dr("Hora da Impressão") = ds.Tables("Pdf_Print_DateTime").Rows(pdf_count)(0)
                    dt_pdf.Rows.Add(dr)
                Next

                DataGridView1.DataSource = dt_pdf.DefaultView
                DataGridView1.AllowUserToOrderColumns = True
                DataGridView1.AllowUserToResizeColumns = True

            Else
            End If
        Catch ex As Exception
            MsgBox("Sem registros para exibição.")
        End Try
    End Sub

    Private Sub frm_Show_logs_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim ptr As New MDI()
        ptr.Show()
        Me.Hide()

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class