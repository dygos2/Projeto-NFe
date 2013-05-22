using System.ServiceProcess;
using FN4CancelamentoCtl;

namespace FN4CancelamentoSvc
{
    partial class CancelamentoService : ServiceBase
    {
        private readonly CancelamentoMonitor _mon = new CancelamentoMonitor();
        public CancelamentoService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _mon.Run();
        }

        protected override void OnStop()
        {
            _mon.Pause();
        }
    }
}
