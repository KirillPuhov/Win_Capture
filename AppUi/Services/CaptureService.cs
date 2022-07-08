using Domain.Factories;
using Domain.Factories.Abstract_factory;
using Domain.Models;
using System.Threading.Tasks;

namespace AppUi.Services
{
    public sealed class CaptureService : ICaptureService
    {
        private readonly System.Windows.Window _window;

        public CaptureService() =>
            _window = System.Windows.Application.Current.MainWindow;

        public void Start(CaptureType type, string fileName, string path)
        {
            _window.Hide();

            IOutFile _output = GetFactory(type, fileName, path).GetOutFile();

            //задержка перед сохранением скриншота
            var t = Task.Run(async delegate
            {
                await Task.Delay(170);
                _output.Save();
            });
            t.Wait();

            _window.Show();
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
