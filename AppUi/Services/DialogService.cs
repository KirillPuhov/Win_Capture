using AppUi.Controls.Window;
using Settings;
using System.Diagnostics;
using System.Windows;

namespace AppUi.Services
{
    public sealed class DialogService : IDialogService
    {
        private readonly System.Windows.Window _ownerWindow;

        private ApplicationSettings _applicationSettings;

        private string _result;
        public string Result
        {
            get { return _result; }
            private set 
            { 
                _result = value;
                _applicationSettings.FolderDirectory = _result;
            }
        }

        public DialogService()
        {
            _ownerWindow = Application.Current.MainWindow.Owner;
            _applicationSettings = new ApplicationSettings();
        }

        public bool ShowDialog()
        {
            var _directoryDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (_directoryDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Result = _directoryDialog.SelectedPath;

                return true;
            }

            return false;
        }

        public void ShowError(string error)
        {
            MessageBox.Show(error, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowInfo(string info)
        {
            var _info = new TextDialog("Info", info);
            _info.Owner = _ownerWindow;
            _info.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _info.ShowDialog();
        }

        public void OpenFolder()
        {
            Process.Start("explorer.exe", _applicationSettings.FolderDirectory + "\\Win_Capture");
        }
    }
}
