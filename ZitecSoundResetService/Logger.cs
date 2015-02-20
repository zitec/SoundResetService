using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZitecSoundResetService
{
    public static class Logger
    {
        private static StreamWriter _fileStream = File.AppendText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"));

        static Logger()
        {
            _fileStream.AutoFlush = true;
        }

        public static void LogLine(string message)
        {
            _fileStream.WriteLine(message);
        }

        public static void LogLine(string format, params object[] arg)
        {
            _fileStream.WriteLine(format, arg);
        }
    }
}
