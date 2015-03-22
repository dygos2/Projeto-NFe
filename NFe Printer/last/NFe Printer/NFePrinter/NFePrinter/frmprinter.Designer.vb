<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmprinter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmprinter))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbip = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnselectpath = New System.Windows.Forms.Button()
        Me.txtpdfpath = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnsave = New System.Windows.Forms.Button()
        Me.cmbprintername = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.MonthCalendar1)
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbip)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnselectpath)
        Me.GroupBox1.Controls.Add(Me.txtpdfpath)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnsave)
        Me.GroupBox1.Controls.Add(Me.cmbprintername)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(498, 399)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parâmetros da configuração"
        '
        'cmbip
        '
        Me.cmbip.FormattingEnabled = True
        Me.cmbip.Location = New System.Drawing.Point(179, 32)
        Me.cmbip.Name = "cmbip"
        Me.cmbip.Size = New System.Drawing.Size(189, 21)
        Me.cmbip.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(142, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Selecione o seu computador"
        '
        'btnselectpath
        '
        Me.btnselectpath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnselectpath.Location = New System.Drawing.Point(374, 114)
        Me.btnselectpath.Name = "btnselectpath"
        Me.btnselectpath.Size = New System.Drawing.Size(30, 20)
        Me.btnselectpath.TabIndex = 5
        Me.btnselectpath.Text = "..."
        Me.btnselectpath.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnselectpath.UseVisualStyleBackColor = True
        '
        'txtpdfpath
        '
        Me.txtpdfpath.Location = New System.Drawing.Point(179, 114)
        Me.txtpdfpath.Name = "txtpdfpath"
        Me.txtpdfpath.Size = New System.Drawing.Size(189, 20)
        Me.txtpdfpath.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 117)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(145, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Selecione o caminho do PDF"
        '
        'btnsave
        '
        Me.btnsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.Location = New System.Drawing.Point(36, 201)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(166, 123)
        Me.btnsave.TabIndex = 2
        Me.btnsave.Text = "Atualizar e iniciar"
        Me.btnsave.UseVisualStyleBackColor = True
        '
        'cmbprintername
        '
        Me.cmbprintername.FormattingEnabled = True
        Me.cmbprintername.Location = New System.Drawing.Point(179, 72)
        Me.cmbprintername.Name = "cmbprintername"
        Me.cmbprintername.Size = New System.Drawing.Size(189, 21)
        Me.cmbprintername.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Selecione a impressora"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(33, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Período dos retornos"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(169, 156)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(84, 17)
        Me.RadioButton1.TabIndex = 9
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Apenas hoje"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(259, 156)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(96, 17)
        Me.RadioButton2.TabIndex = 10
        Me.RadioButton2.Text = "Entre as datas:"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Location = New System.Drawing.Point(259, 185)
        Me.MonthCalendar1.MaxSelectionCount = 30
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 11
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(324, 359)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(97, 23)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Reiniciar Envios"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmprinter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Bisque
        Me.ClientSize = New System.Drawing.Size(538, 452)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmprinter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NFePrinter"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnsave As System.Windows.Forms.Button
    Friend WithEvents cmbprintername As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnselectpath As System.Windows.Forms.Button
    Friend WithEvents txtpdfpath As System.Windows.Forms.TextBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cmbip As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents MonthCalendar1 As System.Windows.Forms.MonthCalendar
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
