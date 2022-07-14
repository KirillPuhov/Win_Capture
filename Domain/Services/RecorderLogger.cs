using ScreenRecorderLib;
using System;
using System.IO;

namespace Domain.Services
{
    public sealed class RecorderLogger : IRecorderService
    {
        private const string FILE_NAME = "recorder.log";
        private string  FILEPATH = System.IO.Path.Combine(Environment.CurrentDirectory, FILE_NAME);

        private readonly IRecorderService _recorderService;


        public string Path
        {
            get { return _recorderService.Path; }
            set
            {
                _recorderService.Path = value;
            }
        }


        public string Error
        {
            get { return _recorderService.Error; }
            set
            {
                _recorderService.Error = value;
            }
        }

        public RecorderStatus Status
        {
            get { return _recorderService.Status; }
            set
            {
                _recorderService.Status = value;
            }
        }


        public RecorderLogger(IRecorderService recorderService)
        {
            _recorderService = recorderService;
            SetLog(string.Format("[{0}] Create new video file: {1}\n", DateTime.Now, Path));
        }

        public void CreateDefaultRecording()
        {
            _recorderService.CreateDefaultRecording();
            SetLog(string.Format("[{0}] Start recording with default settings\n", DateTime.Now));
        }

        public void CreateRecording()
        {
            _recorderService.CreateRecording();
            SetLog(string.Format("[{0}] Start recording\n", DateTime.Now));
        }

        public void EndRecording()
        {
            _recorderService.EndRecording();
            SetLog(string.Format("[{0}] Stop recording\n", DateTime.Now));
        }

        private void SetLog(string message)
        {
            if (!File.Exists(FILEPATH))
                File.WriteAllText(FILEPATH, message);
            else
                File.AppendAllText(FILEPATH, message);
        }
    }
}
