

Public Class serieVO
    Private _numero As Integer
    Private _notas As List(Of Integer)


    Public Property numero() As Integer
        Get
            Return _numero
        End Get
        Set(ByVal value As Integer)
            _numero = value
        End Set
    End Property

    Public Property notas() As List(Of Integer)
        Get
            Return _notas
        End Get
        Set(ByVal value As List(Of Integer))
            _notas = value
        End Set
    End Property

    Public Sub obterNotas(ByVal cnpj As String)
        Me.notas = DataAccess.series.obterNotasPorSerieECnpj(Me.numero, cnpj)
    End Sub

    Public Function obterNotasExistentesASeremInutilizadas(ByVal cnpj As String) As List(Of Integer)
        Return DataAccess.series.obterNotasExistentesASeremInutilizadas(Me.numero, cnpj)
    End Function
End Class
