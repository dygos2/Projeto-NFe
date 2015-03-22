<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDI
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDI))
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnselectprinter = New System.Windows.Forms.Button()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.btnStop)
        Me.Panel1.Controls.Add(Me.btnStart)
        Me.Panel1.Controls.Add(Me.btnselectprinter)
        Me.Panel1.Location = New System.Drawing.Point(0, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(427, 223)
        Me.Panel1.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(45, 129)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Status: Parado"
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(241, 21)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(137, 94)
        Me.Button1.TabIndex = 3
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.BackColor = System.Drawing.Color.Transparent
        Me.btnStop.BackgroundImage = CType(resources.GetObject("btnStop.BackgroundImage"), System.Drawing.Image)
        Me.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnStop.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnStop.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStop.ForeColor = System.Drawing.Color.Transparent
        Me.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStop.Location = New System.Drawing.Point(241, 157)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(137, 41)
        Me.btnStop.TabIndex = 2
        Me.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnStop.UseVisualStyleBackColor = False
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.Color.Transparent
        Me.btnStart.BackgroundImage = CType(resources.GetObject("btnStart.BackgroundImage"), System.Drawing.Image)
        Me.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStart.ForeColor = System.Drawing.Color.Transparent
        Me.btnStart.Location = New System.Drawing.Point(39, 155)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(132, 41)
        Me.btnStart.TabIndex = 1
        Me.btnStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnStart.UseVisualStyleBackColor = False
        '
        'btnselectprinter
        '
        Me.btnselectprinter.BackgroundImage = CType(resources.GetObject("btnselectprinter.BackgroundImage"), System.Drawing.Image)
        Me.btnselectprinter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnselectprinter.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnselectprinter.Location = New System.Drawing.Point(48, 21)
        Me.btnselectprinter.Name = "btnselectprinter"
        Me.btnselectprinter.Size = New System.Drawing.Size(106, 94)
        Me.btnselectprinter.TabIndex = 0
        Me.btnselectprinter.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnselectprinter.UseVisualStyleBackColor = True
        '
        'PrintDocument1
        '
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'MDI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(423, 220)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MaximizeBox = False
        Me.Name = "MDI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NFePrinter"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnselectprinter As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Public WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
