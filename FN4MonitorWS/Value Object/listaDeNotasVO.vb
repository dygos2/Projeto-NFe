Public Class listaDeNotasVO

    Private _listaDeNotas As List(Of FN4Common.notaVO)
    Private _quantidadeDeRegistros As Integer

    Public Sub New()

    End Sub
    Public Property listaDeNotas() As List(Of FN4Common.notaVO)
        Get
            Return _listaDeNotas
        End Get
        Set(ByVal value As List(Of FN4Common.notaVO))
            _listaDeNotas = value
        End Set
    End Property

    Public Property quantidadeDeRegistros() As Integer

        Get
            Return _quantidadeDeRegistros
        End Get
        Set(ByVal value As Integer)
            _quantidadeDeRegistros = value
        End Set
    End Property

End Class
