using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace FN4IntegracaoPostBackSvc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new IntegracaoPostBackMonitorService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
