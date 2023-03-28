

namespace smi_wrapper
{
    internal class GpuInfo
    {
        internal GpuInfo(int gpuNum, string gpuModel, string gpuUuid)
        {
            _gpuNum = gpuNum;
            _gpuModel = gpuModel;
            _gpuUuid = gpuUuid;
        }

        public int GpuNum { get => _gpuNum; set { _gpuNum = value; } }
        public string GpuModel { get => _gpuModel; set { _gpuModel = value; } }
        public string GpuUuid { get => _gpuUuid; set { _gpuUuid = value; } }

        private int _gpuNum = -1;
        private string _gpuModel = string.Empty;
        private string _gpuUuid = string.Empty;
        
    }
}
