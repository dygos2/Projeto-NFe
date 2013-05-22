Imports FN4Common

Public Class bkpDAO

    Public Shared Sub incluirbkp(ByVal bkp As bkpVO)
        Try
            IBatisNETHelper.Instance.Insert("inserirBkp", bkp)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
End Class
