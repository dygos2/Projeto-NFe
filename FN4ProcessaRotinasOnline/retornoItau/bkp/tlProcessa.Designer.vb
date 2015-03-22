<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tlProcessa
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnProcBoleto = New System.Windows.Forms.Button()
        Me.btnProcRetorno = New System.Windows.Forms.Button()
        Me.btnParaProc = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnProcBoleto
        '
        Me.btnProcBoleto.Location = New System.Drawing.Point(92, 12)
        Me.btnProcBoleto.Name = "btnProcBoleto"
        Me.btnProcBoleto.Size = New System.Drawing.Size(72, 35)
        Me.btnProcBoleto.TabIndex = 0
        Me.btnProcBoleto.Text = "Processar Boleto"
        Me.btnProcBoleto.UseVisualStyleBackColor = True
        '
        'btnProcRetorno
        '
        Me.btnProcRetorno.Location = New System.Drawing.Point(11, 12)
        Me.btnProcRetorno.Name = "btnProcRetorno"
        Me.btnProcRetorno.Size = New System.Drawing.Size(72, 35)
        Me.btnProcRetorno.TabIndex = 1
        Me.btnProcRetorno.Text = "Processar Franquias"
        Me.btnProcRetorno.UseVisualStyleBackColor = True
        '
        'btnParaProc
        '
        Me.btnParaProc.Location = New System.Drawing.Point(173, 12)
        Me.btnParaProc.Name = "btnParaProc"
        Me.btnParaProc.Size = New System.Drawing.Size(72, 35)
        Me.btnParaProc.TabIndex = 2
        Me.btnParaProc.Text = "Iniciar Processos"
        Me.btnParaProc.UseVisualStyleBackColor = True
        '
        'tlProcessa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(258, 65)
        Me.Controls.Add(Me.btnParaProc)
        Me.Controls.Add(Me.btnProcRetorno)
        Me.Controls.Add(Me.btnProcBoleto)
        Me.Name = "tlProcessa"
        Me.Text = "Processamentos"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnProcBoleto As System.Windows.Forms.Button
    Friend WithEvents btnProcRetorno As System.Windows.Forms.Button
    Friend WithEvents btnParaProc As System.Windows.Forms.Button
End Class
