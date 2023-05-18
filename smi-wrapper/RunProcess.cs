using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace smi_wrapper
{
    internal class RunProcess
    {
        internal RunProcess(string command, string arguments, WriteLog logWriter, JsonWriter jsonWriter, JsonRotate jsonRotate, GpuInfoProcRun gpuInfoProcRun)
        {
            _command = command;
            _arguments = arguments;
            _writeLog = logWriter;
            _jsonWriter = jsonWriter;
            _jsonRotate = jsonRotate;
            _gpuInfoProcRun = gpuInfoProcRun;
        }

        private string _command = string.Empty;
        private string _arguments = string.Empty;
        private WriteLog _writeLog;
        private JsonWriter _jsonWriter;
        private JsonRotate _jsonRotate;
        private DateTime _prevousDateTime = DateTime.Now;
        private List<DataStorage> _currentDataList = new();
        private GpuInfoProcRun _gpuInfoProcRun;
        private List<GpuInfo> _gpuInfoList = new();
        private GpuInfoParser _gpuInfoParser = new();

        internal void Run()
        {
            _writeLog.Write(LogFacility.INFO, $"Try to detect adapters");
            FindGpu();
            Process measureProc = new();
            measureProc.StartInfo.FileName = _command;
            measureProc.StartInfo.Arguments = _arguments;
            measureProc.StartInfo.UseShellExecute = false;
            measureProc.StartInfo.RedirectStandardOutput = true;
            measureProc.StartInfo.RedirectStandardError = true;
            measureProc.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            measureProc.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            _writeLog.Write(LogFacility.INFO, $"Try to launch {_command} {_arguments}");
            measureProc.Start();
            measureProc.BeginOutputReadLine();
            measureProc.BeginErrorReadLine();
            measureProc.WaitForExit();
        }

        private void FindGpu()
        {
            string[] gpus = _gpuInfoProcRun.Run();
            _gpuInfoList = _gpuInfoParser.Parse(gpus);
        }


        internal void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                if (outLine.Data[0] != '#')
                {
                    string trimmed = Regex.Replace(outLine.Data.Trim(), @"\s+", " ");
                    string[] splitted = trimmed.Split(' ');
                    string eventYear = splitted[0].Substring(0, 4);
                    string eventMonth = splitted[0].Substring(4, 2);
                    string eventDay = splitted[0].Substring(6, 2);
                    DateTime.TryParse($"{eventYear}/{eventMonth}/{eventDay} {splitted[1]}", out DateTime currentDate);
                    int.TryParse(splitted[2], out int gpuNum);
                    int.TryParse(splitted[3], out int smUsage);
                    int.TryParse(splitted[4], out int memUsage);
                    int.TryParse(splitted[5], out int encoderUsage);
                    int.TryParse(splitted[6], out int decoderUsage);
                    int.TryParse(splitted[7], out int frameBufferSize);
                    int.TryParse(splitted[8], out int bar1Size);
                    int.TryParse(splitted[8], out int rxPci);
                    int.TryParse(splitted[10], out int txPci);
                    string model = string.Empty;
                    string uuid = string.Empty;
                    for(int i = 0; i < _gpuInfoList.Count; i++)
                    {
                        if(gpuNum == _gpuInfoList[i].GpuNum)
                        {
                            model = _gpuInfoList[i].GpuModel;
                            uuid = _gpuInfoList[i].GpuUuid;
                        }
                    }
                    DataStorage currentData = new (currentDate, gpuNum, smUsage, memUsage, encoderUsage, decoderUsage, frameBufferSize, bar1Size, rxPci, txPci, model, uuid);
                    
                    DateTime currentDateTime = DateTime.Now;
                    
                    if (currentDateTime.Minute > _prevousDateTime.Minute || currentDateTime.Hour > _prevousDateTime.Hour || currentDateTime.Day > _prevousDateTime.Day || currentDateTime.Month > _prevousDateTime.Month || currentDateTime.Year > _prevousDateTime.Year)
                    {
                        _jsonWriter.Write(_currentDataList, _prevousDateTime);
                        _currentDataList.Clear();
                        _prevousDateTime = currentDateTime;
                        _jsonRotate.Sweep(_prevousDateTime);
                        FindGpu();
                    }
                    _currentDataList.Add(currentData);
                }
            }   
        }
    }
}
