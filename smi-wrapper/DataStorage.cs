using System;

namespace smi_wrapper
{
    internal class DataStorage
    {
        internal DataStorage(DateTime eventDateTime, int gpuNum, int smUsage, int memUsage, int encoderUsage, int decoderUsage, int frameBufferSize, int bar1Size, int rxPci, int txPci, string gpuModel = "", string gpuUid = "")
        {
            EventDateTime = eventDateTime;
            GpuNum = gpuNum;
            SmUsage = smUsage;
            MemUsage = memUsage;
            EncoderUsage = encoderUsage;
            DecoderUsage = decoderUsage;
            FrameBufferSize = frameBufferSize;
            Bar1Size = bar1Size;
            RxPci = rxPci;
            TxPci = txPci;
            GpuModel = gpuModel;
            GpuUid = gpuUid;
        }

        public DateTime EventDateTime { get; set; }
        public int GpuNum { get; set; }
        public string GpuModel { get => _gpuModel; set { _gpuModel = value; } }
        public string GpuUid { get => _gpuUuid; set { _gpuUuid = value; } }
        public int SmUsage { get; set; }
        public int MemUsage { get; set; }
        public int EncoderUsage { get; set; }
        public int DecoderUsage { get; set; }
        public int FrameBufferSize { get; set; }
        public int Bar1Size { get; set; }
        public int RxPci { get; set; }
        public int TxPci { get; set; }

        private string _gpuModel = string.Empty;
        private string _gpuUuid = string.Empty;
    }
}
