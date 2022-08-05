using Domain.Models;

namespace AppUi.Services
{
    public interface ICaptureService
    {
        void HideWindowAndStart(CaptureType type, string fileName = null, string path = null);

        void Start(CaptureType type, string fileName = null, string path = null);

        void Stop();

        void Pause();

        void Resume();

        IOutFile GetOutFile();
    }
}
