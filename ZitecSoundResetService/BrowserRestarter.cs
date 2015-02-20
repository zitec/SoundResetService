using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZitecSoundResetService
{
    class BrowserRestarter
    {
        private string IE_PROCESS      = "iexplore";
        private string CHROME_PROCESS  = "chrome";
        private string FIREFOX_PROCESS = "firefox";

        private int _browserWaitLoadTime;
        private string[] _youtubeLinks;

        private Random _random = new Random();

        /// <summary>
        /// Constructs an instance of a BrowserRestarter object
        /// </summary>
        /// <param name="browserWaitLoadTime">The time to wait after the process has been started</param>
        /// <param name="playlistFile">The file containing the playlists</param>
        public BrowserRestarter(int browserWaitLoadTime, string playlistFile)
        {
            playlistFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, playlistFile);

            _browserWaitLoadTime = browserWaitLoadTime;            
            _youtubeLinks = File.ReadAllLines(playlistFile);

            Logger.LogLine("Loaded {0} links from {1} file.", _youtubeLinks.Length, playlistFile);
        }

        public void RestartBrowser()
        {
            KillAllBrowsers(); // kill all the chrome, firefox and ie browser instances,

            StartBrowserAtRandomLink(); // starts a browser at a random link
        }

        private void KillAllBrowsers()
        {
            Process[] ieProcesses      = Process.GetProcessesByName(IE_PROCESS     );
            Process[] chromeProcesses  = Process.GetProcessesByName(CHROME_PROCESS );
            Process[] firefoxProcesses = Process.GetProcessesByName(FIREFOX_PROCESS);

            IEnumerable<Process> allBrowsers = ieProcesses.Concat(chromeProcesses).Concat(firefoxProcesses);

            foreach (Process process in allBrowsers)
            {
                process.Kill();
            }
        }

        private void StartBrowserAtRandomLink()
        {
            string randomLink = GetRandomYoutubeLink();

            Logger.LogLine("Starting browser at: {0}", randomLink);

            Process.Start(randomLink);

            // wait for the browser to stream some muisc
            Thread.Sleep(_browserWaitLoadTime);
        }

        private string GetRandomYoutubeLink()
        {
            int randomIndex = _random.Next(_youtubeLinks.Length);

            return _youtubeLinks[randomIndex];
        }
    }
}
