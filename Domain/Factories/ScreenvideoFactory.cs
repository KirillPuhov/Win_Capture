using Domain.Factories.Abstract_factory;
using Domain.Models;
using System;

namespace Domain.Factories
{
    public sealed class ScreenvideoFactory : OutFileFactory
    {
        private readonly string FILENAME = "video";
        private readonly string FILE_PATH = Environment.GetEnvironmentVariable("userprofile") + @"\Documents";

        private readonly IOutFile _output;

        private readonly string _fileName;
        private readonly string _path;

        public ScreenvideoFactory(string fileName, string path)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                _fileName = FILENAME;
            else
                _fileName = fileName;


            if (string.IsNullOrWhiteSpace(path))
                _path = FILE_PATH;
            else
                _path = path;

            _output = new ScreenvideoFile(_fileName, DateTime.Now, _path);
        }

        public override string GetInfo()
        {
            return _output.ToString();
        }

        public override IOutFile GetOutFile()
        {
            return _output;
        }
    }
}
