using System;

namespace AppUi.Services
{
    public interface IHelper
    {
        void WindowHide();

        void WindowShow();

        void PauseBeforeAction(Action action);
    }
}
