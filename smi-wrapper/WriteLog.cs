using System;
using System.IO;

namespace smi_wrapper
{
    public class WriteLog 
    {
        string logPath = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), "logs/");
        string fullLogPath = string.Empty;

        public void Write(LogFacility logLevel, string text)
        {
            if (!Directory.Exists(logPath))
            {
               Directory.CreateDirectory(logPath);
            }
            DateTime dateTimeNow = DateTime.Now;
            string filename = $"{dateTimeNow:yyyyMMdd}.log";
            fullLogPath = Path.Combine(logPath, filename);
            DateTime now = DateTime.Now;
            try
            {
                using(StreamWriter writer = File.AppendText(fullLogPath))
                {
                    writer.WriteLine($"{now:yyyy-MM-dd HH:mm:ss} {logLevel}: {text}");
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось записать {fullLogPath}. {ex.Message}");
            }
            
        }
    }
        
}
