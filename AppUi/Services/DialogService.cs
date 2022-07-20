using AppUi.Controls.Window;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;

namespace AppUi.Services
{
    public sealed class DialogService : IDialogService
    {
        private readonly System.Windows.Window _ownerWindow;

        private string _result;
        public string Result
        {
            get { return _result; }
            set 
            { 
                _result = value; 
            }
        }

        public DialogService() => 
                _ownerWindow = Application.Current.MainWindow.Owner;

        public bool ShowDialog()
        {
            var _fileDialog = new OpenFileDialog();
            if (_fileDialog.ShowDialog() == true)
            {
                Result = _fileDialog.FileName;

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

        public void StartProcess(string application, string argument)
        {
            Process.Start(application, argument);
        }
    }
}
