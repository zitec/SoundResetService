using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZitecSoundResetService
{
    public class SoundListener
    {
        private Settings _settings;

        private Stopwatch _noSoundStopWatch = new Stopwatch();
        private MMDevice  _soundDevice;

        private BrowserRestarter _browserRestarter;

        public SoundListener(Settings settings)
        {
            _settings = settings;
            _browserRestarter = new BrowserRestarter(_settings.BrowserWaitLoadTime, _settings.YoutubePlaylistFile);            
        }

        public void BeginListen()
        {
            Thread thread = new Thread(() => OnStart());

            thread.Name = "Sound listening thread";

            _settings.BrowserWaitLoadTime = 4000;
            _settings.RestartBrowserTimeout = 6000;

            thread.Start();
        }

        private void OnStart()
        {
            // no sound stop watch will hold the time that has passed after the last sound sample
            _noSoundStopWatch.Start();

            // the default sound device that will be sampled at a regular interval for the volume
            _soundDevice = GetDefaulfAudioDevice();

            while(true)
            {
                OnTick();
                Thread.Sleep(_settings.CheckInterval);
            }
        }

        private void OnTick()
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

        private MMDevice GetDefaulfAudioDevice()
        {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();

            MMDevice defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            Logger.LogLine("Got default device: {0}", defaultDevice.FriendlyName);

            return defaultDevice;
        }
    }
}
