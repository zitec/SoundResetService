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
using System.Timers;

namespace ZitecSoundResetService
{
    public partial class SoundResetService : ServiceBase
    {
        private Settings _settings;

        private Timer     _timer            = new Timer();
        private Stopwatch _noSoundStopWatch = new Stopwatch();
        private MMDevice  _soundDevice;
        private BrowserRestarter _browserRestarter;

        public SoundResetService(Settings settings)
        {
            InitializeComponent();

            _settings = settings;
            _browserRestarter = new BrowserRestarter(_settings.BrowserWaitLoadTime, _settings.YoutubePlaylistFile);
        }

        protected override void OnStart(string[] args)
        {
            // no sound stop watch will hold the time that has passed after the last sound sample
            _noSoundStopWatch.Start();

            // the default sound device that will be sampled at a regular interval for the volume
            _soundDevice = GetDefaulfAudioDevice();

            // the object that will generate events at regulat intervals
            _timer.Interval = _settings.CheckInterval;
            _timer.Elapsed += OnTick;
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            float currentAudioLevel = _soundDevice.AudioMeterInformation.MasterPeakValue;

            // We have a minimum sound level
            if (currentAudioLevel > _settings.MinVolumeThreshold)
            {
                _noSoundStopWatch.Restart();
                return;
            }

            // the threshold of the sound has not been met
            if (currentAudioLevel <= _settings.MinVolumeThreshold)
            {
                // if no sound has been around from some while restart the things
                if (_noSoundStopWatch.ElapsedMilliseconds > _settings.RestartBrowserTimeout)
                {
                    _browserRestarter.RestartBrowser(); // restart the music
                    _noSoundStopWatch.Restart();        // restart the timer
                }
            }
        }

        protected override void OnStop()
        {
        }

        private MMDevice GetDefaulfAudioDevice()
        {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();

            MMDevice defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            Logger.LogLine("Got default device: {0}", defaultDevice.FriendlyName);

            return defaultDevice;
        }
    }
}
