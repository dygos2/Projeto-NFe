Imports FN4Common

Public Class notaDAO


    Public Shared Function obterNotasParaZip() As List(Of notaVO)

        Return IBatisNETHelper.Instance.QueryForList(Of notaVO)("obterNotasZip", Nothing)

    End Function

    Public Shared Function obterNotasTotaisParaZip() As List(Of notaVO)

        Return IBatisNETHelper.Instance.QueryForList(Of notaVO)("obterTodasNotasZip", Nothing)

    End Function

End Class
