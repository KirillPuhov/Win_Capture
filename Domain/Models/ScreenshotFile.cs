using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Domain.Models
{
    public sealed class ScreenshotFile : IOutFile
    {
        private const string APP_FOLDERS = @"\Win_Capture\Screenshots\";

        private readonly string _fileName;
        private readonly DateTime _dateOfCreation;
        private readonly string _extension = ".png";
        private readonly string _path;

        private double _size;

        public ScreenshotFile(string fileName, DateTime dateOfCreation, string path)
        {
            _fileName = fileName;
            _dateOfCreation = dateOfCreation;
            _path = path;

            DirExist();
        }

        public string FileName => _fileName + $"{this.GetHashCode()}";

        public double Size => _size;

        public DateTime DateOfCreation => _dateOfCreation;

        public string Extension => _extension;

        public string Path => _path;

        public void doAction()
        {
            using (var _bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            {
                using (var _graphic = Graphics.FromImage(_bitmap))
                {
                    _graphic.CopyFromScreen(Point.Empty, Point.Empty, _bitmap.Size);
                }

                _size = _bitmap.Size.Width * _bitmap.Size.Height;

                _bitmap.Save(Path + APP_FOLDERS + FileName + Extension, ImageFormat.Png);
            }
        }

        private void DirExist()
        {
            if (!Directory.Exists(Path + APP_FOLDERS))
                Directory.CreateDirectory(Path + APP_FOLDERS);
        }

        public void stopAction() { }

        public void doPause() { }

        public void doResume() { }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + _fileName.GetHashCode();
                hash = hash * 23 + _size.GetHashCode();
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

            if (_size != x._size || _dateOfCreation != x.DateOfCreation || _fileName != x._fileName || _extension != x._extension || _path != x._path)
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
            return string.Format("(Name: {0}, Type:{1}, Size: {2}, Date of creation: {3}, Path: {4})", FileName, Extension, Size, DateOfCreation, Path);
        }
    }
}
