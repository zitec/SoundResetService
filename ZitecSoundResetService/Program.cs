using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

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
                SetStartup(true);
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

        private static void SetStartup(bool startWithWindows)
        {
            Assembly  assembly = Assembly.GetExecutingAssembly();
            AppDomain domain   = AppDomain.CurrentDomain;

            string assemblyName = assembly.GetName().Name;

            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (startWithWindows)
                rk.SetValue(assemblyName, assembly.Location);
            else
                rk.DeleteValue(assemblyName, false);
        }
    }
}
