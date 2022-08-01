using Domain.Models;

namespace AppUi.Services
{
    public interface IDialogService
    {
        string Result { get; }

        bool ShowDialog();

        void ShowInfo(string info);

        void ShowError(string error);

        void OpenFolder();

        void OpenView(System.Windows.Window window);

        void OpenFile(IOutFile file);
    }
}
