using System;
using System.Threading.Tasks;

namespace AppUi.Services
{
    public sealed class HelperService : IHelper
    {
        private readonly System.Windows.Window _window;

        public HelperService() =>
            _window = System.Windows.Application.Current.MainWindow;

        public void PauseBeforeAction(Action action)
        {
            var _task = Task.Run(async delegate
            {
                await Task.Delay(170);
                action.Invoke();
            });
            _task.Wait();
        }

        public void WindowHide()
        {
            _window.Hide();
        }

        public void WindowShow()
        {
            _window.Show();
        }
    }
}
