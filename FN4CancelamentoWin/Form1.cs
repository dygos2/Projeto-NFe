using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FN4CancelamentoCtl;

namespace FN4CancelamentoWin
{
    public partial class Form1 : Form
    {
        private readonly CancelamentoMonitor _mon = new CancelamentoMonitor();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _mon.Run();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _mon.Pause();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
        }
    }
}
