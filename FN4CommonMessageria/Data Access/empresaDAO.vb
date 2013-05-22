Imports System.Security.Cryptography
Imports FN4CommonMessageria
Imports FN4CommonMessageria.Helpers

Public Class empresaDAO
    Public Shared Sub alterarGadgets(ByVal empresa As empresaVO)
        IBatisNETHelper.Instance.Update("alterarGadgets", empresa)
    End Sub

    Public Shared Function obterEmpresa(ByVal cnpj As String, ByVal cpf As String) As FN4CommonMessageria.empresaVO
        Try
            Dim ht As New Hashtable
            ht.Add("cnpj", cnpj)
            ht.Add("cpf", cpf)

            Return IBatisNETHelper.Instance.QueryForObject("obterEmpresa", ht)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function inserirEmpresa(ByVal empresa As empresaVO) As String
        Dim retorno As String = IBatisNETHelper.Instance.Insert("inserirEmpresa", empresa)

        Dim empresaNova As empresaVO = empresaDAO.obterEmpresa(empresa.cnpj, String.Empty)

        empresaNova.token = Seguranca.GerarMD5(String.Format("{0}{1}", empresaNova.idEmpresa, empresaNova.cnpj))

        IBatisNETHelper.Instance.Update("alterarToken", empresaNova)

        Return String.Format("{0}${1}", empresaNova.idEmpresa, empresaNova.token)
    End Function

    Public Shared Sub alterarEmpresa(ByVal empresa As empresaVO)
        IBatisNETHelper.Instance.Update("alterarEmpresa", empresa)
    End Sub

    Public Shared Sub alterarToken(ByVal empresa As empresaVO)
        IBatisNETHelper.Instance.Update("alterarToken", empresa)
    End Sub

    Public Shared Sub alterarFrest(ByVal empresa As empresaVO)
        IBatisNETHelper.Instance.Update("alterarFrest", empresa)
    End Sub

    Public Shared Sub alterarPrest(ByVal empresa As empresaVO)
        IBatisNETHelper.Instance.Update("alterarPrest", empresa)
    End Sub

    Public Shared Function obterEmpresasComUrlDePostBack() As List(Of empresaVO)
        Return IBatisNETHelper.Instance.QueryForList(Of empresaVO)("obterEmpresasComUrlDePostBack", Nothing)
    End Function
End Class
