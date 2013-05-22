Public Class listaDeEventoVO


    

    Private _listaDeEventos As List(Of FN4Common.eventoVO)


    Public Property ListaDeEventos() As List(Of FN4Common.eventoVO)
        Get
            Return _listaDeEventos
        End Get
        Set(ByVal value As List(Of FN4Common.eventoVO))
            _listaDeEventos = value
        End Set
    End Property


End Class
