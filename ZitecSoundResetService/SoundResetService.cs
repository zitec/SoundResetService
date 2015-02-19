using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ZitecSoundResetService
{
    public partial class SoundResetService : ServiceBase
    {
        private Settings _settings;

        private Timer     _timer            = new Timer();
        private Stopwatch _noSoundStopWatch = new Stopwatch();

        public SoundResetService(Settings settings)
        {
            InitializeComponent();

            _settings = settings;
            
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {
        }
    }
}
