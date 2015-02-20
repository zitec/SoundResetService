using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZitecSoundResetService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            try
            {
                StartSoundListener();
            }
            catch(Exception e)
            {
                Logger.LogLine("Exception at {0}", e.ToString());
            }
        }

        private static void StartSoundListener()
        {
            SoundListener listener = new SoundListener(Settings.Default);

            listener.BeginListen();
        }
    }
}
