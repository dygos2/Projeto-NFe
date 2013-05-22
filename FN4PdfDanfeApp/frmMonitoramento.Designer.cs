namespace FN4PdfDanfeApp
{
    partial class frmMonitoramento
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMonitoramento));
            this.btnMonitorar = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmIniciarMonitoramento = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRestaurar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSair = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMonitorar
            // 
            this.btnMonitorar.Location = new System.Drawing.Point(27, 79);
            this.btnMonitorar.Name = "btnMonitorar";
            this.btnMonitorar.Size = new System.Drawing.Size(75, 23);
            this.btnMonitorar.TabIndex = 0;
            this.btnMonitorar.Text = "Monitorar";
            this.btnMonitorar.UseVisualStyleBackColor = true;
            this.btnMonitorar.Click += new System.EventHandler(this.btnMonitorar_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(24, 43);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(57, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Tag = "Status: {0}";
            this.lblStatus.Text = "Status: {0}";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.contextMenu;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Monitoramento de Geração de DANFE";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmIniciarMonitoramento,
            this.tsmRestaurar,
            this.tsmSair});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(194, 70);
            // 
            // tsmIniciarMonitoramento
            // 
            this.tsmIniciarMonitoramento.Name = "tsmIniciarMonitoramento";
            this.tsmIniciarMonitoramento.Size = new System.Drawing.Size(193, 22);
            this.tsmIniciarMonitoramento.Text = "Iniciar Monitoramento";
            this.tsmIniciarMonitoramento.Click += new System.EventHandler(this.tsmIniciarMonitoramento_Click);
            // 
            // tsmRestaurar
            // 
            this.tsmRestaurar.Name = "tsmRestaurar";
            this.tsmRestaurar.Size = new System.Drawing.Size(193, 22);
            this.tsmRestaurar.Text = "Minimizar";
            this.tsmRestaurar.Click += new System.EventHandler(this.tsmRestaurar_Click);
            // 
            // tsmSair
            // 
            this.tsmSair.Name = "tsmSair";
            this.tsmSair.Size = new System.Drawing.Size(193, 22);
            this.tsmSair.Text = "Sair";
            this.tsmSair.Click += new System.EventHandler(this.tsmSair_Click);
            // 
            // frmMonitoramento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 113);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnMonitorar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMonitoramento";
            this.Text = "Monitoramento de Geração de DANFE";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMonitoramento_FormClosing);
            this.Resize += new System.EventHandler(this.frmMonitoramento_Resize);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMonitorar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmIniciarMonitoramento;
        private System.Windows.Forms.ToolStripMenuItem tsmRestaurar;
        private System.Windows.Forms.ToolStripMenuItem tsmSair;
    }
}

