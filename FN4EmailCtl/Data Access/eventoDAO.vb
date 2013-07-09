Imports FN4Common

Public Class eventoDAO

    Public Shared Function obterEventosParaEnvioDeEmail() As List(Of eventoVO)
        Return IBatisNETHelper.Instance.QueryForList(Of eventoVO)("obterEventosEmail", Nothing)
    End Function
End Class
