using Domain.Factories;
using Domain.Factories.Abstract_factory;
using Domain.Models;

namespace AppUi.Services
{
    public sealed class CaptureService : ICaptureService
    {
        public void Start(CaptureType type, string fileName, string path)
        {
            IOutFile _output = GetFactory(type, fileName, path).GetOutFile();
            _output.Save();
        }

        private OutFileFactory GetFactory(CaptureType outputType, string fileName, string path)
        {
            switch (outputType)
            {
                case CaptureType.Screenshot:
                    return new ScreenshotFactory(fileName, path);

                case CaptureType.Screenvideo:
                    return new ScreenvideoFactory(fileName, path);
                default:
                    return new ScreenshotFactory(fileName, path);
            }
        }
    }

    public enum CaptureType
    {
        Screenshot,
        Screenvideo
    }
}
