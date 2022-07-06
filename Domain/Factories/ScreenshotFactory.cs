using Domain.Factories.Abstract_factory;
using Domain.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Domain.Factories
{
    public sealed class ScreenshotFactory : OutFileFactory
    {
        private readonly IOutFile _output;

        private readonly string  _fileName;
        private readonly string _path;

        private double  _size;
        private dynamic _data;

        public ScreenshotFactory(string fileName, string path)
        {
            //TODO: Проверка fileName и path

            _fileName = fileName;
            _path     = path;

            Create();

            _output = new ScreenshotFile(_fileName, _size, _data, DateTime.Now, _path);
        }

        public override string GetInfo()
        {
            return _output.ToString();
        }

        public override IOutFile GetOutFile()
        {
            return _output;
        }

        protected override void Create()
        {
            // получаем размеры окна рабочего стола
            Rectangle _bounds = Screen.GetBounds(Point.Empty);

            // создаем пустое изображения размером с экран устройства
            using (var _bitmap = new Bitmap(_bounds.Width, _bounds.Height))
            {
                // создаем объект на котором можно рисовать
                using (var _graphic = Graphics.FromImage(_bitmap))
                {
                    // перерисовываем экран на наш графический объект
                    _graphic.CopyFromScreen(Point.Empty, Point.Empty, _bounds.Size);
                }

                _size = _bitmap.Size.Height*_bitmap.Size.Width;
                _data = _bitmap;
            }
        }
    }
}
