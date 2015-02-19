using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ZitecSoundResetService
{
    public partial class SoundResetService : ServiceBase
    {
        private Settings _settings;

        public SoundResetService(Settings settings)
        {
            InitializeComponent();

            _settings = settings;
        }

        public void Start()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            SoundListener listener = new SoundListener(_settings);

            listener.BeginListen();
        }

        protected override void OnStop()
        {
        }
    }
}
