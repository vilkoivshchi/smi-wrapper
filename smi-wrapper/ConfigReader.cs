using Newtonsoft.Json.Linq;
using System.IO;

namespace smi_wrapper
{
    internal class ConfigReader
    {
        internal ConfigReader(WriteLog logWriter, string configPath = "")
        {
            _configPath = configPath;
            _logWriter = logWriter;
        }
        private string _configPath = string.Empty;
        private WriteLog _logWriter;

        internal AppSettings Read()
        {
            AppSettings settings = new();
            if (_configPath == string.Empty)
            {
                _configPath = settings.ConfigPath;
            }
            if (File.Exists(_configPath))
            {
                string configText = File.ReadAllText(_configPath);
                JObject config = JObject.Parse(configText);
                JToken logRotateTime = config.SelectToken("jsonRotateTime");
                if (logRotateTime != null)
                {
                    if (int.TryParse((string)logRotateTime, out int logRotateTimeFromConfig))
                    {
                        settings.LogRotateTime = logRotateTimeFromConfig;
                    }
                }
                JToken outJsonStore = config.SelectToken("filesStoreFolder");
                if (outJsonStore != null)
                {
                    string jsonStorePath = (string)outJsonStore;
                    string trimmedStore = jsonStorePath.Trim();
                    if (!Directory.Exists(trimmedStore))
                    {
                        Directory.CreateDirectory(trimmedStore);
                    }
                    settings.OutJsonPath = trimmedStore;
                }
            }
            else
            {
                _logWriter.Write(LogFacility.ERROR, $"Can't find {_configPath}, using default settings!");
            }
            
            return settings;
        }
    }
}
