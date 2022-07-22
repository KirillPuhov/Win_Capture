using ScreenRecorderLib;
using Settings;

namespace Domain.Services
{
    public sealed class RecorderService : IRecorderService
    {
        private Recorder _rec;

        private readonly RecorderOptions _options;


        private string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
            }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
            }
        }

        private RecorderStatus _status;
        public RecorderStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
            }
        }


        public RecorderService(RecorderSettings options, string path)
        {
            this._options = options.GetOptions();
            this._path = path;

        }

        public void CreateRecording()
        {
            _rec = Recorder.CreateRecorder(_options);

            _rec.OnRecordingComplete += Rec_OnRecordingComplete;
            _rec.OnRecordingFailed += Rec_OnRecordingFailed;
            _rec.OnStatusChanged += Rec_OnStatusChanged;

            _rec.Record(_path);
        }

        public void CreateDefaultRecording()
        {
            _rec = Recorder.CreateRecorder();
            _rec.Record(_path);
        }

        public void Pause()
        {
            _rec.Pause();
        }

        public void Resume()
        {
            _rec.Resume();
        }

        public void EndRecording()
        {
            _rec.Stop();
        }

        private void Rec_OnRecordingComplete(object sender, RecordingCompleteEventArgs e)
        {
            Path = e.FilePath;
        }

        private void Rec_OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            Error = e.Error;
        }

        private void Rec_OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            Status = e.Status;
        }
    }
}
