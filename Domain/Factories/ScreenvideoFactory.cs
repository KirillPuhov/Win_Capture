using Domain.Factories.Abstract_factory;
using Domain.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Domain.Factories
{
    public sealed class ScreenvideoFactory : OutFileFactory
    {
        private readonly IOutFile _output;

        private readonly string _fileName;
        private readonly string _path;

        private double _size;
        private dynamic _data;

        public ScreenvideoFactory(string fileName, string path)
        {
            //TODO: Проверка fileName и path

            _fileName = fileName;
            _path = path;

            Create();

            _output = new ScreenvideoFile(_fileName, _size, _data, DateTime.Now, _path);
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

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var bmp = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            var gr = Graphics.FromImage(bmp);
            gr.CopyFromScreen(0, 0, 0, 0, new Size(bmp.Width, bmp.Height));
        }
    }
}
