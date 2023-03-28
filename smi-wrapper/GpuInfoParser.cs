
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace smi_wrapper
{
    internal class GpuInfoParser
    {
        internal List<GpuInfo> Parse(string[] gpuInfoSource)
        {
            List<GpuInfo> gpuInfo = new();
            for(int i = 0; i < gpuInfoSource.Length; i++)
            {

                string pattern = @"[\d]{1,}(?=\:)|(?<=\:[\s]).{1,}(?=[\s]\()|(?<=UUID:[\s]GPU-).{1,}(?=\))";
                MatchCollection matches = Regex.Matches(gpuInfoSource[i], pattern);
                if (matches.Count > 0)
                {
                    int.TryParse(matches[0].Value, out int gpuNum);

                    GpuInfo currentGpu = new(gpuNum, matches[1].Value, matches[2].Value);
                    gpuInfo.Add(currentGpu);
                }
            }
            return gpuInfo;
        }
        
    }
}
