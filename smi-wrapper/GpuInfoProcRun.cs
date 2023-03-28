using System;
using System.Diagnostics;

namespace smi_wrapper
{
    internal class GpuInfoProcRun
    {
        internal GpuInfoProcRun(string appName, string appArgs)
        {
            _appName = appName;
            _appArgs = appArgs;
        }

        private string _appName;
        private string _appArgs;

        internal string[] Run()
        {
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = _appName;
            proc.StartInfo.Arguments = _appArgs;
            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();
            string[] outputArray = { };
            if (OperatingSystem.IsLinux())
            {
                outputArray = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                outputArray = output.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            }
            proc.WaitForExit();
            return outputArray;
        }
    }
}
