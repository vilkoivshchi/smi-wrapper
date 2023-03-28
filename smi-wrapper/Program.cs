
namespace smi_wrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string command = "nvidia-smi";
            string arguments = "dmon -o DT -s umt";
            string gpuInfoArguments = "-L";
            //List<string> dataLabels = new List<string> {"DateTime", "GPU #", "sm %", "mem usage %", "encoder %", "decoder %", "framebufer MB", "bar1 MB", "rxpci MB/s", "txpci MB/s"};
            //AppSettings applicationSettings = new AppSettings();
            
            WriteLog logWriter = new();
            ConfigReader configReader = new(logWriter);
            AppSettings applicationSettings = configReader.Read();
            JsonWriter jsonWriter = new(applicationSettings, logWriter);
            JsonRotate jsonRotate = new(applicationSettings);
            GpuInfoProcRun gpuInfoProcRun = new(command, gpuInfoArguments);
            RunProcess runProcess = new(command, arguments, logWriter, jsonWriter, jsonRotate, gpuInfoProcRun);
            runProcess.Run();
        }
    }
}