﻿Imports System.ComponentModel
Imports System.Configuration.Install

Public Class ProjectInstaller

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent

    End Sub

    Private Sub ServiceInstaller1_AfterInstall(ByVal sender As System.Object, ByVal e As System.Configuration.Install.InstallEventArgs) Handles ServiceInstaller1.AfterInstall

    End Sub
End Class
