Public Class produto_fiscalDAO
    Public Shared Sub inserirProduto(ByVal prod As produto_fiscalVO)
        IBatis.Instance.Insert("inserirProduto", prod)
    End Sub

    Public Shared Sub alterarProduto(ByVal prod As produto_fiscalVO)
        IBatis.Instance.Update("alterarProduto", prod)
    End Sub

    Public Shared Sub inseriMeta(ByVal prod As produto_fiscalVO)
        IBatis.Instance.Update("inseriMeta", prod)
    End Sub

End Class
