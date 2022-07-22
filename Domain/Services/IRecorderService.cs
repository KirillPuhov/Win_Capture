using ScreenRecorderLib;

namespace Domain.Services
{
    public interface IRecorderService
    {
        void CreateRecording();

        void CreateDefaultRecording();

        void Pause();

        void Resume();

        void EndRecording();

        string Path { get; set; }

        string Error { get; set; }

        RecorderStatus Status { get; set; }
    }
}
