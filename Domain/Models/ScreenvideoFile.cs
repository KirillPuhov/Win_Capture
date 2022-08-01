using Domain.Services;
using Settings;
using System;
using System.IO;

namespace Domain.Models
{
    public sealed class ScreenvideoFile : IOutFile
    {
        private readonly string _fileName;
        private readonly DateTime _dateOfCreation;
        private readonly string _extension = ".mp4";

        private string _path;
        private double _size = 1920*1080;

        private RecorderLogger _recorderLogger;

        public ScreenvideoFile(string fileName, DateTime dateOfCreation, string path)
        {
            _fileName       = fileName;
            _dateOfCreation = dateOfCreation;
            _path           = path;

            InitDefaultSettings();
        }

        public string FileName         => _fileName + $"{this.GetHashCode()}";

        public double Size             => _size;

        public DateTime DateOfCreation => _dateOfCreation;

        public string Extension        => _extension;

        public string Path             => _path;

        public void doAction()
        {
            _recorderLogger.CreateRecording();
        }

        public void doPause()
        {
            _recorderLogger.Pause();
        }

        public void doResume()
        {
            _recorderLogger.Resume();
        }

        public void stopAction()
        {
            _recorderLogger.EndRecording();
        }

        private void InitDefaultSettings()
        {
            DirExist();

            string _videoPath = System.IO.Path.Combine(Path + "\\Win_Capture\\Video", FileName + Extension);
            var _settings     = new RecorderSettings();

            _recorderLogger   = new RecorderLogger(new RecorderService(_settings, _videoPath));
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
