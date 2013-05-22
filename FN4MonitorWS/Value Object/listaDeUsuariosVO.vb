Imports FN4Common

Public Class listaDeUsuariosVO
    Private _listaDeUsuarios As List(Of FN4Common.usuarioVO)
    Private _empresas As List(Of FN4Common.empresaVO)
    Private _quantidadeDeRegistros As Integer

    Public Sub New()

    End Sub
    Public Property listaDeUsuarios() As List(Of FN4Common.usuarioVO)
        Get
            Return _listaDeUsuarios
        End Get
        Set(ByVal value As List(Of FN4Common.usuarioVO))
            _listaDeUsuarios = value
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

    Public Property empresas As List(Of empresaVO)
        Get
            Return _empresas
        End Get
        Set(ByVal value As List(Of empresaVO))
            _empresas = value
        End Set
    End Property
End Class
