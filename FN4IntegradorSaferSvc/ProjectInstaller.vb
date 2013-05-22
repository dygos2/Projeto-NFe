﻿

Public Class ProjectInstaller

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent

        'Rotina para permitir mais de uma instancia do mesmo servico
        'Gera o service name dinamicamente mantendo a raiz interna

        'Fisconet 4 - Servico de Envio de NFe 
        Dim name As String = New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).Name.Replace(".exe", "").ToUpper
        Me.ServiceInstaller1.ServiceName += name
        Me.ServiceInstaller1.DisplayName += " (" & name & ")"
    End Sub

End Class
