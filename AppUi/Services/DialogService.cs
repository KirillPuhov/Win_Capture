using AppUi.Controls.Window;
using Domain.Models;
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
            _ownerWindow = Application.Current.MainWindow;
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
            else
            {
                Result = _applicationSettings.FolderDirectory;

                return false;
            }
        }

        public void ShowError(string error)
        {
            MessageBox.Show(error, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowInfo(string info)
        {
            OpenView(new TextDialog("Info", info));
        }

        public void OpenFolder()
        {
            string path = _applicationSettings.FolderDirectory + "\\Win_Capture";
            Process.Start("explorer.exe", path);
        }

        public void OpenView(System.Windows.Window window)
        {
            var _view = window;
            _view.Owner = _ownerWindow;
            _view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _view.ShowDialog();
        }

        public void OpenFile(IOutFile file)
        {
            string _path = null;
            if (file.Extension.Equals(".png"))
            {
                _path = file.Path + "\\Win_Capture\\Screenshots\\" + file.FileName + file.Extension;
            }
            else
            {
                _path = file.Path + "\\Win_Capture\\Video\\" + file.FileName + file.Extension;
            }

            Process.Start(_path);
        }
    }
}
