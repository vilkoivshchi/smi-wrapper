using System.IO;
using System;

namespace smi_wrapper
{
    internal class AppSettings
    {

        public int LogRotateTime { get => _logRotateTime; set { _logRotateTime = value; } }
        public string ConfigPath { get => _configPath; set { _configPath = value; } }
        public string OutJsonPath { get => _outJsonPath; set { _outJsonPath = value; } }

        private int _logRotateTime = 5;
        private string _configPath = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), "settings.json");
        private string _outJsonPath = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), "json/"); 
    }
}
