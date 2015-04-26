

Public Class Rotina

    Public Shared Sub rotinaBkp()
        Try

            Dim rotina = New rotinaVO
            IBatis.Instance.Update("rotinaBkp", rotina)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


End Class