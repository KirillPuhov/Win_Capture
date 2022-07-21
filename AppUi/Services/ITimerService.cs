using System;

namespace AppUi.Services
{
    public interface ITimerService
    {
        void Start(EventHandler handler);

        void Stop();
    }
}
