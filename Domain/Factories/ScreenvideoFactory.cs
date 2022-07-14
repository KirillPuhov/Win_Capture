using Domain.Factories.Abstract_factory;
using Domain.Models;
using System;

namespace Domain.Factories
{
    public sealed class ScreenvideoFactory : OutFileFactory
    {
        private readonly IOutFile _output;

        private readonly string _fileName;
        private readonly string _path;

        public ScreenvideoFactory(string fileName, string path)
        {
            //TODO: Проверка fileName и path

            _fileName = fileName;
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
