Imports FN4Common
Imports System.Data.Odbc


Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim notas As List(Of notaVO)
        Dim nota As notaVO

        notas = IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasImpressao", Nothing)

        For Each nota In notas
            TextBox1.Text += nota.NFe_ide_nNF & " - copias solicitadas : " & nota.impressaoSolicitada & vbCrLf
        Next

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim conn As New OdbcConnection("dsn=nfe")
        Dim comm As New OdbcCommand("SELECT * FROM NOTAS")
        comm.Connection = conn
        Dim dr As OdbcDataReader
        conn.Open()


        dr = comm.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read
            TextBox1.Text += dr.Item("NFe_ide_nNF").ToString & " - copias solicitadas : " & dr.Item("impressaoSolicitada").ToString & vbCrLf

        End While
    End Sub
End Class
