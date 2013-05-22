using System.ServiceProcess;
using FN4InutilizacaoCtl;

namespace FN4InutilizacaoSvc
{
    partial class InutilizacaoService : ServiceBase
    {
        private readonly InutilizacaoMonitor _mon = new InutilizacaoMonitor();
        public InutilizacaoService()
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
