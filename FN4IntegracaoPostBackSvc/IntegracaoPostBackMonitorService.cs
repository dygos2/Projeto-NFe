using System.ServiceProcess;
using FN4IntegracaoPostBackCtl;

namespace FN4IntegracaoPostBackSvc
{
    partial class IntegracaoPostBackMonitorService : ServiceBase
    {
        private readonly IntegracaoPostBackMonitor _mon = new IntegracaoPostBackMonitor();
        public IntegracaoPostBackMonitorService()
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
