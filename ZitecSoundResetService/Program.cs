using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZitecSoundResetService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var service =  new SoundResetService(Settings.Default);

            ServiceBase[] ServicesToRun;

            ServicesToRun = new ServiceBase[] 
            { 
               service,
            };

            service.Start();

            while(true)
            {
                Thread.Sleep(1000);
                Application.DoEvents();
            }
            //ServiceBase.Run(ServicesToRun);
        }
    }
}
