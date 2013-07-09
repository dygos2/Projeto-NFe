Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports FN4Common
Imports FN4Common.Helpers

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class FN4Cadastro
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function CadastrarEmpresa(ByVal cnpj As String, ByVal nome As String, ByVal nomeFantasia As String, ByVal logradouro As String, ByVal numero As String, ByVal bairro As String, ByVal cep As String, ByVal complemento As String, ByVal codigoMunicipio As Integer, ByVal nomeMunicipio As String, ByVal uf As String, ByVal ie As String, ByVal crt As Integer, ByVal homologacao As Integer, ByVal email As String, ByVal fone As String) As retornoCadastroVO
        Try
            Dim empresa As New empresaVO

            empresa.cnpj = cnpj
            empresa.nome = nome
            empresa.logradouro = logradouro
            empresa.numero = numero
            empresa.bairro = bairro
            empresa.codigoMunicipio = codigoMunicipio
            empresa.nomeMunicipio = nomeMunicipio
            empresa.uf = uf
            empresa.ie = ie
            empresa.crt = crt
            empresa.homologacao = homologacao
            empresa.email = email
            empresa.cep = cep
            empresa.nomeFantasia = nomeFantasia
            empresa.complemento = complemento
            empresa.fone = fone

            Dim resultado As String = empresaDAO.inserirEmpresa(empresa)

            If resultado Is Nothing Then Return Nothing

            Dim idEmpresa As Integer = Convert.ToInt32(resultado.Split("$")(0))
            Dim token As String = resultado.Split("$")(1)

            Dim retorno As New retornoCadastroVO

            retorno.idEmpresa = idEmpresa
            retorno.token = token

            Return retorno
        Catch ex As Exception
            Dim retorno As New retornoCadastroVO
            retorno.mensagem = "[Erro]: ao cadastrar empresa. Mensagem: " & Geral.ObterExceptionMessagesEmCascata(ex)

            Return retorno
        End Try
    End Function

    <WebMethod()> _
    Public Function AtualizarEmpresa(ByVal cnpj As String, ByVal token As String, ByVal nome As String, ByVal nomeFantasia As String, ByVal logradouro As String, ByVal numero As Integer, ByVal bairro As String, ByVal cep As String, ByVal complemento As String, ByVal codigoMunicipio As Integer, ByVal nomeMunicipio As String, ByVal uf As String, ByVal ie As String, ByVal crt As Integer, ByVal homologacao As Integer, ByVal email As String, ByVal fone As String) As retornoCadastroVO
        Dim retorno As New retornoCadastroVO

        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

            If empresa Is Nothing Then Return Nothing

            If Not Seguranca.CompararMD5(empresa.validador, token) Then
                retorno.mensagem = "Token enviado para a empresa é inválido."

                Return retorno
            End If

            empresa.nome = nome
            empresa.logradouro = logradouro
            empresa.numero = numero
            empresa.bairro = bairro
            empresa.codigoMunicipio = codigoMunicipio
            empresa.nomeMunicipio = nomeMunicipio
            empresa.uf = uf
            empresa.ie = ie
            empresa.crt = crt
            empresa.homologacao = homologacao
            empresa.email = email
            empresa.cep = cep
            empresa.nomeFantasia = nomeFantasia
            empresa.complemento = complemento
            empresa.fone = fone

            empresaDAO.alterarEmpresa(empresa)

            retorno.idEmpresa = empresa.idEmpresa
            retorno.mensagem = "Empresa atualizada com sucesso."

            Return retorno
        Catch ex As Exception
            retorno.mensagem = "[Erro]: ao atualizar empresa. Mensagem: " & Geral.ObterExceptionMessagesEmCascata(ex)

            Return retorno
        End Try
    End Function

    <WebMethod()>
    Public Function ConsultarEmpresa(ByVal cnpj As String) As retornoCadastroVO
        Dim retorno As New retornoCadastroVO

        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

            If empresa Is Nothing Then
                retorno.mensagem = "Nenhuma empresa encontrada com este CNPJ."

                Return retorno
            End If

            retorno.idEmpresa = empresa.idEmpresa
            retorno.token = empresa.token

            Return retorno
        Catch ex As Exception
            retorno.mensagem = "[Erro]: ao consultar empresa. Mensagem: " & Geral.ObterExceptionMessagesEmCascata(ex)

            Return retorno
        End Try
    End Function

End Class