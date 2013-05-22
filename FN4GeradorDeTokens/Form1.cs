using System.Windows.Forms;
using FN4Common.Helpers;

namespace FN4GeradorDeTokens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, System.EventArgs e)
        {
            var entrada = string.Format("{0}{1}", txtIdDaEmpresa.Text, txtCnpj.Text);

            txtResultado.Text = Seguranca.GerarMD5(entrada);
        }

        private void txtIdDaEmpresa_TextChanged(object sender, System.EventArgs e)
        {
            LimparResultado();
        }

        private void LimparResultado()
        {
            txtResultado.Text = string.Empty;
        }

        private void txtCnpj_TextChanged(object sender, System.EventArgs e)
        {
            LimparResultado();
        }
    }
}
