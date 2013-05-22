Imports System.Web.Services
Imports System.ComponentModel
Imports FN4Common.DataAccess
Imports FN4Common
Imports FN4Common.Helpers


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://fisconet4.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class FN4UsuariosService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function obterListaDeUsuarios(ByVal idUsuarioAtual As Integer, ByVal registroInicial As Integer, ByVal registrosPorPagina As Integer, ByVal cnpj As String, ByVal token As String) As listaDeUsuariosVO
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

            If empresa Is Nothing Then Return Nothing

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing

            Dim x As New listaDeUsuariosVO
            x.quantidadeDeRegistros = IBatisNETHelper.Instance.QueryForObject("obterQuantidadeDeUsuarios", Nothing)

            Dim ht As New Hashtable
            ht.Add("registroInicial", registroInicial)
            ht.Add("registrosPorPagina", registrosPorPagina)
            ht.Add("idUsuario", idUsuarioAtual)

            x.listaDeUsuarios = IBatisNETHelper.Instance.QueryForList(Of usuarioVO)("obterListaDeUsuarios", ht)
            x.empresas = IBatisNETHelper.Instance.QueryForList(Of empresaVO)("obterEmpresasPermitidas", idUsuarioAtual)

            Return x
        Catch ex As Exception
            Log.registrarErro("Erro ao obter lista de usuários:" & ex.Message, "WebService")
            Return Nothing
        End Try
    End Function

    <WebMethod()>
    Public Function obterListaDeEmpresas(ByVal idUsuario As Integer, ByVal cnpj As String, ByVal token As String) As List(Of empresaVO)
        Try

            Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)
            If empresa Is Nothing Then Return Nothing

            Log.registrarInfo("O cliente id " & idUsuario & " - empresa " & empresa.nome & " (" & empresa.cnpj & ") entrou no monitor", "WebService")

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing
            Dim empresas As List(Of empresaVO) = IBatisNETHelper.Instance.QueryForList(Of empresaVO)("obterEmpresasPermitidas", idUsuario)

            Return empresas
        Catch ex As Exception
            Throw ex
            Log.registrarInfo("erro na busca das empresas: " & ex.Message, "WebService")
            Return Nothing

        End Try
    End Function


    <WebMethod()> _
    Public Function inserirUsuario(ByVal nome As String, ByVal email As String, ByVal senha As String, ByVal tipo As Integer, ByVal cnpj As String, ByVal token As String) As Integer
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

            If empresa Is Nothing Then Return 1

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return 1

            Dim user As New usuarioVO
            user.nome = nome
            user.email = email
            user.senha = senha
            user.tipo = tipo

            IBatisNETHelper.Instance.Insert("inserirUsuario", user)

            Dim idUsuario As Integer = usuarios.obterIdDeUsuario(user)

            Dim ht As New Hashtable
            ht.Add("idEmpresa", empresa.idEmpresa)
            ht.Add("idUsuario", idUsuario)

            IBatisNETHelper.Instance.Insert("inserirEmpresa_Usuarios", ht)
            Log.registrarInfo("A empresa " & empresa.nome & " (" & empresa.cnpj & ") inseriu um novo usuário - " & nome & " / email - " & email & " / tipo -" & tipo, "WebService")

        Catch ex As Exception
            Log.registrarErro("Erro ao inserir usuario: " & ex.Message, "WebService")
            Return 1
        End Try
    End Function
    <WebMethod()> _
    Public Function alterarUsuario(ByVal nome As String, ByVal email As String, ByVal senha As String, ByVal tipo As Integer, ByVal id As Integer, ByVal cnpj As String, ByVal token As String) As Integer
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

            If empresa Is Nothing Then Return 1

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return 1

            Dim user As New usuarioVO
            user.nome = nome
            user.email = email
            user.senha = senha
            user.tipo = tipo
            user.idUsuario = id

            IBatisNETHelper.Instance.Update("alterarUsuario", user)
            Log.registrarInfo("A empresa " & empresa.nome & " (" & empresa.cnpj & ") alterou um usuário - " & nome & " / email - " & email & " / tipo -" & tipo, "WebService")
            Return 0
        Catch ex As Exception
            Log.registrarErro("Erro ao alterar usuário. " & ex.Message, "WebService")
            Return 1
        End Try
    End Function

    <WebMethod()> _
    Public Function excluirUsuario(ByVal id As Integer, ByVal cnpj As String, ByVal token As String) As Integer
        Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

        If empresa Is Nothing Then Return 1

        If Not Seguranca.CompararMD5(empresa.validador, token) Then Return 1

        Try
            IBatisNETHelper.Instance.Delete("excluirUsuario", id)

            Dim ht As New Hashtable()
            ht.Add("idEmpresa", empresa.idEmpresa)
            ht.Add("idUsuario", id)

            IBatisNETHelper.Instance.Delete("excluirEmpresa_Usuarios", ht)

            Return 0
        Catch ex As Exception
            Log.registrarErro("Erro ao excluir usuário: " & ex.Message, "WebService")
            Return 1
        End Try
    End Function
End Class