Public Class rotinaVO

  
    Private _rotina As Boolean


    Public Sub New()

    End Sub

    
    Public Property cfop() As Long
        Get
            Return _rotina
        End Get
        Set(ByVal value As Long)
            _rotina = value
        End Set
    End Property
End Class
