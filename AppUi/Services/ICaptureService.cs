using Domain.Models;

namespace AppUi.Services
{
    public interface ICaptureService
    {
        void TaskStart(CaptureType type, string fileName, string path);

        void Start(CaptureType type, string fileName, string path);

        void Stop(IOutFile file);

        IOutFile GetOutFile();
    }
}
