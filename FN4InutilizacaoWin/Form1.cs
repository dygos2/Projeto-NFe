using System.Windows.Forms;
using FN4InutilizacaoCtl;

namespace FN4InutilizacaoWin
{
    public partial class Form1 : Form
    {
        private InutilizacaoMonitor _mon = new InutilizacaoMonitor();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            _mon.Run();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            _mon.Pause();
        }
    }
}
