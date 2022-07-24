using Domain.Models;

namespace AppUi.Services
{
    public interface ICaptureService
    {
        void HideWindowAndStart(CaptureType type, string fileName, string path);

        void Start(CaptureType type, string fileName, string path);

        void Stop();

        void Pause();

        void Resume();

        IOutFile GetOutFile();
    }
}
