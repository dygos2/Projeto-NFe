﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On



'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.1.
'
Namespace localhost.HelpDesk
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="RecepcaoServiceSoap", [Namespace]:="http://fisconet4.net.org/")>  _
    Partial Public Class RecepcaoService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private ProcessarRequisicaoOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.FN4MonitorWS.My.MySettings.Default.FN4MonitorWS_localhost_HelpDesk_RecepcaoService
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event ProcessarRequisicaoCompleted As ProcessarRequisicaoCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://fisconet4.net.org/ProcessarRequisicao", RequestNamespace:="http://fisconet4.net.org/", ResponseNamespace:="http://fisconet4.net.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ProcessarRequisicao(ByVal xmlEnvio As String, ByVal CNPJEmpresa As String, ByVal nomeResponsavel As String, ByVal emailResponsavel As String) As String
            Dim results() As Object = Me.Invoke("ProcessarRequisicao", New Object() {xmlEnvio, CNPJEmpresa, nomeResponsavel, emailResponsavel})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ProcessarRequisicaoAsync(ByVal xmlEnvio As String, ByVal CNPJEmpresa As String, ByVal nomeResponsavel As String, ByVal emailResponsavel As String)
            Me.ProcessarRequisicaoAsync(xmlEnvio, CNPJEmpresa, nomeResponsavel, emailResponsavel, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ProcessarRequisicaoAsync(ByVal xmlEnvio As String, ByVal CNPJEmpresa As String, ByVal nomeResponsavel As String, ByVal emailResponsavel As String, ByVal userState As Object)
            If (Me.ProcessarRequisicaoOperationCompleted Is Nothing) Then
                Me.ProcessarRequisicaoOperationCompleted = AddressOf Me.OnProcessarRequisicaoOperationCompleted
            End If
            Me.InvokeAsync("ProcessarRequisicao", New Object() {xmlEnvio, CNPJEmpresa, nomeResponsavel, emailResponsavel}, Me.ProcessarRequisicaoOperationCompleted, userState)
        End Sub
        
        Private Sub OnProcessarRequisicaoOperationCompleted(ByVal arg As Object)
            If (Not (Me.ProcessarRequisicaoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ProcessarRequisicaoCompleted(Me, New ProcessarRequisicaoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub ProcessarRequisicaoCompletedEventHandler(ByVal sender As Object, ByVal e As ProcessarRequisicaoCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ProcessarRequisicaoCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
End Namespace
