using Domain.Services;
using NAudio.Wave;
using SharpAvi;
using SharpAvi.Codecs;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Threading;

namespace Domain.Models
{
    public sealed class ScreenvideoFile : IOutFile
    {
        private readonly string _fileName;
        private readonly DateTime _dateOfCreation;
        private readonly string _extension = ".avi";
        private readonly string _path;

        private double _size;

        private readonly DispatcherTimer _recordingTimer;
        private readonly Stopwatch _recordingStopwatch = new Stopwatch();
        private RecorderService _recorder;
        private string _lastFileName;

        public ScreenvideoFile(string fileName, DateTime dateOfCreation, string path)
        {
            _fileName = fileName;
            _dateOfCreation = dateOfCreation;
            _path = path;

            _recordingTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

            InitDefaultSettings();
        }

        public string FileName => _fileName;

        public double Size => _size;

        public DateTime DateOfCreation => _dateOfCreation;

        public string Extension => _extension;

        public string Path => _path;

        public void doAction()
        {
            if (_isRecording)
                throw new InvalidOperationException("Запись уже идёт!");

            _isRecording = true;

            _recordingStopwatch.Reset();
            _recordingTimer.Start();

            _lastFileName = System.IO.Path.Combine(Path + "\\Win_Capture\\Video", FileName + $"{this.GetHashCode()}" + Extension);
            var _bitRate = Mp3LameAudioEncoder.SupportedBitRates.OrderBy(br => br).ElementAt(_audioQuality);
            _recorder = new RecorderService(_lastFileName,
                _encoder, _encodingQuality,
                _audioSourceIndex, _audioWaveFormat, _encodeAudio, _bitRate, _framesPerSecond);
            _size = _recorder.ScreenWidth * _recorder.ScreenHeight;

            _recordingStopwatch.Start();
        }

        public void stopAction()
        {
            if (!_isRecording)
                throw new InvalidOperationException("Запись не идёт!");

            try
            {
                _recorder?.Dispose();
                _recorder = null;
            }
            finally
            {
                _recordingTimer.Stop();
                _recordingStopwatch.Stop();

                _size = _recordingStopwatch.ElapsedMilliseconds * 
                        _encodingQuality;

                _isRecording = false;
            }

        }

        
        private int _audioSourceIndex;
        private int _encodingQuality;
        private int _framesPerSecond;
        private int _audioQuality;
        private bool _encodeAudio;
        private bool _isRecording;
        private FourCC _encoder;
        private SupportedWaveFormat _audioWaveFormat;
        

        private void InitDefaultSettings()
        {
            DirExist();

            _encoder = CodecIds.MotionJpeg;
            _encodingQuality = 70;
            _framesPerSecond = 30;

            _audioSourceIndex = -1;
            _audioWaveFormat = SupportedWaveFormat.WAVE_FORMAT_44M16;
            _encodeAudio = true;
            _audioQuality = (Mp3LameAudioEncoder.SupportedBitRates.Length + 1) / 2;

            _isRecording = false;
        }

        private void DirExist()
        {
            if (!Directory.Exists(Path + "\\Win_Capture\\Video"))
                Directory.CreateDirectory(Path + "\\Win_Capture\\Video");
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 18;

                hash = hash * 24 + _fileName.GetHashCode();
                hash = hash * 24 + _size.GetHashCode();
                hash = hash * 24 + _dateOfCreation.GetHashCode();
                hash = hash * 24 + _extension.GetHashCode();
                hash = hash * 24 + _path.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            if (this.GetHashCode() != obj.GetHashCode())
                return false;

            var x = obj as ScreenvideoFile;

            if (_size != x._size || _dateOfCreation != x.DateOfCreation || _fileName != x._fileName || _extension != x._extension || _path != x._path)
                return false;

            return true;
        }

        public static bool operator ==(ScreenvideoFile screenshot1, ScreenvideoFile screenshot2)
        {
            return Equals(screenshot1, screenshot2);
        }

        public static bool operator !=(ScreenvideoFile screenshot1, ScreenvideoFile screenshot2)
        {
            return !Equals(screenshot1, screenshot2);
        }

        public override string ToString()
        {
            return String.Format("(Name: {0}, Type:{1}, Size: {2}, Date of creation: {3}, Path: {4})", FileName, Extension, Size, DateOfCreation, Path);
        }
    }
}
