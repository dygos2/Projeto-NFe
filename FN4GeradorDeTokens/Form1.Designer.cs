namespace FN4GeradorDeTokens
{
    partial class Form1
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
            this.btnCalcular = new System.Windows.Forms.Button();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.grpResultado = new System.Windows.Forms.GroupBox();
            this.txtCnpj = new System.Windows.Forms.TextBox();
            this.txtIdDaEmpresa = new System.Windows.Forms.TextBox();
            this.lblCnpj = new System.Windows.Forms.Label();
            this.lblIdDaEmpresa = new System.Windows.Forms.Label();
            this.grpResultado.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(292, 29);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(75, 23);
            this.btnCalcular.TabIndex = 7;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // txtResultado
            // 
            this.txtResultado.Location = new System.Drawing.Point(6, 19);
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ReadOnly = true;
            this.txtResultado.Size = new System.Drawing.Size(544, 20);
            this.txtResultado.TabIndex = 0;
            // 
            // grpResultado
            // 
            this.grpResultado.Controls.Add(this.txtResultado);
            this.grpResultado.Location = new System.Drawing.Point(12, 68);
            this.grpResultado.Name = "grpResultado";
            this.grpResultado.Size = new System.Drawing.Size(556, 51);
            this.grpResultado.TabIndex = 10;
            this.grpResultado.TabStop = false;
            this.grpResultado.Text = "Resultado:";
            // 
            // txtCnpj
            // 
            this.txtCnpj.Location = new System.Drawing.Point(96, 31);
            this.txtCnpj.Name = "txtCnpj";
            this.txtCnpj.Size = new System.Drawing.Size(190, 20);
            this.txtCnpj.TabIndex = 9;
            this.txtCnpj.TextChanged += new System.EventHandler(this.txtCnpj_TextChanged);
            // 
            // txtIdDaEmpresa
            // 
            this.txtIdDaEmpresa.Location = new System.Drawing.Point(96, 6);
            this.txtIdDaEmpresa.Name = "txtIdDaEmpresa";
            this.txtIdDaEmpresa.Size = new System.Drawing.Size(53, 20);
            this.txtIdDaEmpresa.TabIndex = 8;
            this.txtIdDaEmpresa.TextChanged += new System.EventHandler(this.txtIdDaEmpresa_TextChanged);
            // 
            // lblCnpj
            // 
            this.lblCnpj.AutoSize = true;
            this.lblCnpj.Location = new System.Drawing.Point(12, 34);
            this.lblCnpj.Name = "lblCnpj";
            this.lblCnpj.Size = new System.Drawing.Size(37, 13);
            this.lblCnpj.TabIndex = 6;
            this.lblCnpj.Text = "CNPJ:";
            // 
            // lblIdDaEmpresa
            // 
            this.lblIdDaEmpresa.AutoSize = true;
            this.lblIdDaEmpresa.Location = new System.Drawing.Point(12, 9);
            this.lblIdDaEmpresa.Name = "lblIdDaEmpresa";
            this.lblIdDaEmpresa.Size = new System.Drawing.Size(78, 13);
            this.lblIdDaEmpresa.TabIndex = 5;
            this.lblIdDaEmpresa.Text = "Id da Empresa:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 129);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.grpResultado);
            this.Controls.Add(this.txtCnpj);
            this.Controls.Add(this.txtIdDaEmpresa);
            this.Controls.Add(this.lblCnpj);
            this.Controls.Add(this.lblIdDaEmpresa);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grpResultado.ResumeLayout(false);
            this.grpResultado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.GroupBox grpResultado;
        private System.Windows.Forms.TextBox txtCnpj;
        private System.Windows.Forms.TextBox txtIdDaEmpresa;
        private System.Windows.Forms.Label lblCnpj;
        private System.Windows.Forms.Label lblIdDaEmpresa;
    }
}

