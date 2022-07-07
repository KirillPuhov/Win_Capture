namespace AppUi.Services
{
    public interface ICaptureService
    {
        void Start(CaptureType type, string fileName, string path);
    }
}
