namespace AppUi.Services
{
    public interface IDialogService
    {
        string Result { get; }

        bool ShowDialog();

        void ShowInfo(string info);

        void ShowError(string error);

        void OpenFolder();
    }
}
