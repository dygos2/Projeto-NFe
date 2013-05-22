Public Class notaAInutilizar
    Private _NFe_ide_nNF As Long
    Private _serie As Integer


    Public Property numero As Long
        Get
            Return _NFe_ide_nNF
        End Get
        Set(ByVal value As Long)
            _NFe_ide_nNF = value
        End Set
    End Property

    Public Property serie As Integer
        Get
            Return _serie
        End Get
        Set(ByVal value As Integer)
            _serie = value
        End Set
    End Property
End Class
