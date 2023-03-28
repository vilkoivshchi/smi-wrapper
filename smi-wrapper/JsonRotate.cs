using System;
using System.IO;

namespace smi_wrapper
{
    internal class JsonRotate
    {
        internal JsonRotate(AppSettings settings)
        {
            _appSettings = settings;
        }

        private AppSettings _appSettings;

        internal void Sweep(DateTime currentTime)
        {
            string[] files = Directory.GetFiles(_appSettings.OutJsonPath);

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastAccessTime < currentTime.AddMinutes(-_appSettings.LogRotateTime))
                {
                    fi.Delete();
                }
            }
        }

    }
}
