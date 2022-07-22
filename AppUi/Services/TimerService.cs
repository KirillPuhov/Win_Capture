using System;
using System.Windows;
using System.Windows.Threading;

namespace AppUi.Services
{
    public class TimerService : ITimerService
    {
        private DispatcherTimer _timer;

        public TimerService()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Normal, Application.Current.Dispatcher);
            _timer.Interval = new TimeSpan(0, 0, 0, 1);
        }

        public void Start(EventHandler handler)
        {
            _timer.Tick += handler;
            _timer?.Start();
        }

        public void Pause()
        {
            _timer?.Stop();
        }

        public void Resume()
        {
            _timer?.Start();
        }

        public void Stop()
        {
            _timer?.Stop();
            _timer = null;
        }
    }
}
