using System;

namespace Domain.Models
{
    public sealed class ScreenshotFile : IOutFile
    {
        private readonly string _fileName;
        private readonly double _size;
        private readonly dynamic _data;
        private readonly DateTime _dateOfCreation;
        private readonly string _extension = ".jpg";
        private readonly string _path;

        public ScreenshotFile(string fileName, double size, dynamic data, DateTime dateOfCreation, string path)
        {
            _fileName = fileName;
            _size = size;
            _data = data;
            _dateOfCreation = dateOfCreation;
            _path = path;
        }

        public string FileName => _fileName;

        public double Size => _size;

        public dynamic Data => _data;

        public DateTime DateOfCreation => _dateOfCreation;

        public string Extension => _extension;

        public string Path => _path;

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + _fileName.GetHashCode();
                hash = hash * 23 + _size.GetHashCode();
                hash = hash * 23 + _data.GetHashCode();
                hash = hash * 23 + _dateOfCreation.GetHashCode();
                hash = hash * 24 + _extension.GetHashCode();
                hash = hash * 23 + _path.GetHashCode();

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

            var x = obj as ScreenshotFile;

            if (_size != x._size || _dateOfCreation != x.DateOfCreation || _data != x._data || _fileName != x._fileName || _extension != x._extension || _path != x._path)
                return false;

            return true;
        }

        public static bool operator ==(ScreenshotFile screenshot1, ScreenshotFile screenshot2)
        {
            return Equals(screenshot1, screenshot2);
        }

        public static bool operator !=(ScreenshotFile screenshot1, ScreenshotFile screenshot2)
        {
            return !Equals(screenshot1, screenshot2);
        }

        public override string ToString()
        {
            return String.Format("(Name: {0}, Type:{1}, Size: {2}, Date of creation: {3}, Path: {4})", FileName, Extension, Size, DateOfCreation, Path);
        }
    }
}
