using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace smi_wrapper
{
    internal class JsonWriter
    {
        internal JsonWriter(AppSettings settings, WriteLog writeLog)
        {
            _appSettings = settings;
            _writeLog = writeLog;
        }

        private AppSettings _appSettings;
        private WriteLog _writeLog;

        internal void Write(List<DataStorage> inputData, DateTime currentDate)
        {
            string jsonString = JsonConvert.SerializeObject(inputData, Formatting.Indented);
            if (!Directory.Exists(_appSettings.OutJsonPath))
            {
                Directory.CreateDirectory(_appSettings.OutJsonPath);
            }
            //DateTime currentDate = DateTime.Now;
            string jsonName = $"{currentDate:yyyyMMdd-HHmm}.json";
            string fullJsonPath = Path.Combine(_appSettings.OutJsonPath, jsonName);
            try
            {
                using (StreamWriter writer = File.AppendText(fullJsonPath))
                {
                    writer.WriteLine(jsonString);
                }
            }
            catch (Exception ex)
            {
                _writeLog.Write(LogFacility.ERROR, $"Не удалось записать {fullJsonPath}. {ex.Message}");
            }
        }

        
    }
}
