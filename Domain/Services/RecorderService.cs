using ScreenRecorderLib;

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

    public sealed class RecorderSettings 
    {
        private readonly RecorderOptions _options;

        public RecorderSettings(AudioBitrate bitrate, AudioChannels channels, bool isAudioEnabled, int fps)
        {
             _options = new RecorderOptions
             {
                 AudioOptions = new AudioOptions
                 {
                     Bitrate = bitrate,
                     Channels = channels,
                     IsAudioEnabled = isAudioEnabled,
                 },
                 VideoEncoderOptions = new VideoEncoderOptions
                 {
                     Bitrate = 8000 * 1000,
                     Framerate = fps,
                     IsFixedFramerate = true,

                     Encoder = new H264VideoEncoder
                     {
                         BitrateMode = H264BitrateControlMode.CBR,
                         EncoderProfile = H264Profile.Main,
                     },

                     IsFragmentedMp4Enabled = true,
                     IsThrottlingDisabled = false,
                     IsHardwareEncodingEnabled = true,
                     IsLowLatencyEnabled = false,
                     IsMp4FastStartEnabled = false
                 },
                 MouseOptions = new MouseOptions
                 {
                     //Displays a colored dot under the mouse cursor when the left mouse button is pressed.	
                     IsMouseClicksDetected = true,
                     MouseLeftClickDetectionColor = "#FFFF00",
                     MouseRightClickDetectionColor = "#FFFF00",
                     MouseClickDetectionRadius = 30,
                     MouseClickDetectionDuration = 100,
                     IsMousePointerEnabled = true,
                     /* Polling checks every millisecond if a mouse button is pressed.
                        Hook is more accurate, but may affect mouse performance as every mouse update must be processed.*/
                     MouseClickDetectionMode = MouseDetectionMode.Hook
                 },
             };
        }

        public RecorderOptions GetOptions()
        {
            return _options;
        }
    }
}
