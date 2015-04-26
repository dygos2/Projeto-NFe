Public Class Funcs

    Public Shared Function verificaPendencia(ByVal cliente As clientesVO) As String
        Dim pendencia = ""

        If IsDBNull(cliente.token) Or IsDBNull(cliente.ct_integra) Then
            pendencia = "1"
        ElseIf IsDBNull(cliente.cert_fim) Then
            pendencia += "2"
        ElseIf IsDBNull(cliente.ct_fiscal) Or IsDBNull(cliente.ipi_padrao) Then
            pendencia += "3"
        End If


        Return pendencia
    End Function

    Public Shared Function retornaTemplate(ByVal pendencia As String) As String

        Dim template = ""

        Select Case pendencia

            Case "1"
                'id template etapa 1
                template = templatesDao.obterTemplate(54)
            Case "2"
                'id template etapa 2
                template = templatesDao.obterTemplate(55)
            Case "3"
                'id template etapa 3
                template = templatesDao.obterTemplate(56)
            Case "12"
                'id template etapa 1 e 2
                template = templatesDao.obterTemplate(57)
            Case "13"
                'id template etapa 1 e 3
                template = templatesDao.obterTemplate(58)
            Case "23"
                'id template etapa 2 e 3
                template = templatesDao.obterTemplate(59)
            Case "123"
                'id template etapa 1, 2 e 3
                template = templatesDao.obterTemplate(60)

        End Select

        Return template
    End Function
End Class
