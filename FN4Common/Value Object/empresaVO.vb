Imports System.IO

Public Class empresaVO
    Private _idEmpresa As Integer
    Private _cnpj As String
    Private _nome As String
    Private _nomeFantasia As String
    Private _logradouro As String
    Private _numero As String
    Private _complemento As String
    Private _bairro As String
    Private _codigoMunicipio As Integer
    Private _nomeMunicipio As String
    Private _uf As String
    Private _cep As String
    Private _fone As String
    Private _ie As String
    Private _iest As String
    Private _im As String
    Private _cnae As String
    Private _crt As Integer
    Private _homologacao As Integer
    Private _receberEmailNota As Boolean
    Private _email As String
    Private _token As String
    Private _logotipo As String
    Private _gadget1 As Integer
    Private _gadget2 As Integer
    Private _gadget3 As Integer
    Private _delimitador As String
    Private _separador As String
    Private _urlPostBack As String
    Private _frest As Integer
    Private _prest As Integer
    Private _habilitado_stat As Integer
    Private _envio_auto_canc As Integer
    Public Property envio_auto_canc() As Integer
        Get
            Return _envio_auto_canc
        End Get
        Set(ByVal value As Integer)
            _envio_auto_canc = value
        End Set
    End Property
    Public Property habilitado_stat() As Integer
        Get
            Return _habilitado_stat
        End Get
        Set(ByVal value As Integer)
            _habilitado_stat = value
        End Set
    End Property

    Public Property idEmpresa() As Integer
        Get
            Return _idEmpresa
        End Get
        Set(ByVal value As Integer)
            _idEmpresa = value
        End Set
    End Property

    Public Property cnpj() As String
        Get
            Return _cnpj
        End Get
        Set(ByVal value As String)
            _cnpj = value
        End Set
    End Property

    Public Property nome() As String
        Get
            Return _nome
        End Get
        Set(ByVal value As String)
            _nome = value
        End Set
    End Property

    Public Property nomeFantasia() As String
        Get
            Return _nomeFantasia
        End Get
        Set(ByVal value As String)
            _nomeFantasia = value
        End Set
    End Property

    Public Property logradouro() As String
        Get
            Return _logradouro
        End Get
        Set(ByVal value As String)
            _logradouro = value
        End Set
    End Property

    Public Property numero() As String
        Get
            Return _numero
        End Get
        Set(ByVal value As String)
            _numero = value
        End Set
    End Property

    Public Property complemento() As String
        Get
            Return _complemento
        End Get
        Set(ByVal value As String)
            _complemento = value
        End Set
    End Property

    Public Property bairro() As String
        Get
            Return _bairro
        End Get
        Set(ByVal value As String)
            _bairro = value
        End Set
    End Property

    Public Property codigoMunicipio() As Integer
        Get
            Return _codigoMunicipio
        End Get
        Set(ByVal value As Integer)
            _codigoMunicipio = value
        End Set
    End Property

    Public Property nomeMunicipio() As String
        Get
            Return _nomeMunicipio
        End Get
        Set(ByVal value As String)
            _nomeMunicipio = value
        End Set
    End Property

    Public Property uf() As String
        Get
            Return _uf
        End Get
        Set(ByVal value As String)
            _uf = value
        End Set
    End Property

    Public Property cep() As String
        Get
            Return _cep
        End Get
        Set(ByVal value As String)
            _cep = value
        End Set
    End Property

    Public Property fone() As String
        Get
            Return _fone
        End Get
        Set(ByVal value As String)
            _fone = value
        End Set
    End Property

    Public Property ie() As String
        Get
            Return _ie
        End Get
        Set(ByVal value As String)
            _ie = value
        End Set
    End Property

    Public Property iest() As String
        Get
            Return _iest
        End Get
        Set(ByVal value As String)
            _iest = value
        End Set
    End Property

    Public Property im() As String
        Get
            Return _im
        End Get
        Set(ByVal value As String)
            _im = value
        End Set
    End Property

    Public Property cnae() As String
        Get
            Return _cnae
        End Get
        Set(ByVal value As String)
            _cnae = value
        End Set
    End Property

    Public Property crt() As Integer
        Get
            Return _crt
        End Get
        Set(ByVal value As Integer)
            _crt = value
        End Set
    End Property

    Public Property homologacao As Integer
        Get
            Return _homologacao
        End Get
        Set(ByVal value As Integer)
            _homologacao = value
        End Set
    End Property

    Public Property receberEmailNota As Boolean
        Get
            Return _receberEmailNota
        End Get
        Set(ByVal value As Boolean)
            _receberEmailNota = value
        End Set
    End Property

    Public Property email As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

    Public Property token As String
        Get
            Return _token
        End Get
        Set(ByVal value As String)
            _token = value
        End Set
    End Property

    Public Property logotipo As String
        Get
            Dim pathLogo As String = Path.Combine(Geral.Parametro("pastaLogos"), cnpj & ".jpg")

            If File.Exists(pathLogo) Then
                Return pathLogo
            Else
                pathLogo = pathLogo.Replace(cnpj, "default")
            End If

            Return pathLogo
        End Get
        Set(ByVal value As String)
            _logotipo = value
        End Set
    End Property

    Public Property gadget1 As Integer
        Get
            Return _gadget1
        End Get
        Set(ByVal value As Integer)
            _gadget1 = value
        End Set
    End Property

    Public Property gadget2 As Integer
        Get
            Return _gadget2
        End Get
        Set(ByVal value As Integer)
            _gadget2 = value
        End Set
    End Property

    Public Property gadget3 As Integer
        Get
            Return _gadget3
        End Get
        Set(ByVal value As Integer)
            _gadget3 = value
        End Set
    End Property

    Public Property delimitador As String
        Get
            Return _delimitador
        End Get
        Set(ByVal value As String)
            _delimitador = value
        End Set
    End Property

    Public Property separador As String
        Get
            Return _separador
        End Get
        Set(ByVal value As String)
            _separador = value
        End Set
    End Property

    Public ReadOnly Property validador As String

        Get
            If _idEmpresa > 0 And Not String.IsNullOrEmpty(_cnpj) Then
                Return String.Format("{0}{1}", _idEmpresa, _cnpj)
            End If

            Return String.Empty
        End Get
    End Property

    Public Property urlPostBack As String
        Get
            Return _urlPostBack
        End Get
        Set(ByVal value As String)
            _urlPostBack = value
        End Set
    End Property

    Public Property frest As Integer
        Get
            Return _frest
        End Get
        Set(ByVal value As Integer)
            _frest = value
        End Set
    End Property

    Public Property prest As Integer
        Get
            Return _prest
        End Get
        Set(ByVal value As Integer)
            _prest = value
        End Set
    End Property
End Class
