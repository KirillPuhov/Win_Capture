using AppUi.Services;
using AppUi.Window.Command;
using AppUi.Window.DI;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AppUi.Window.ViewModel
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        private readonly IDiContainer _container;

        private readonly IDialogService _dialogService;
        private readonly ICaptureService _captureService;

        private System.Windows.Window window = Application.Current.MainWindow;

        #region ctor

        public MainViewModel(IDiContainer container)
        {
            _container = container;

            _dialogService  = _container.Navigate<IDialogService>("DialogService");
            _captureService = _container.Navigate<ICaptureService>("CaptureService");
        }

        #endregion

        private RelayCommand _screenshot;
        public RelayCommand Screenshot
        {
            get
            {
                return _screenshot ??
                    (_screenshot = new RelayCommand(obj => 
                    {
                        _captureService.Start(CaptureType.Screenshot, "screenshot", @"C:\Users\micro\Documents");
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
