using AppUi.Services;
using AppUi.Window.Command;
using AppUi.Window.DI;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppUi.Window.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IDiContainer _container;

        private readonly IDialogService _dialogService;
        private readonly ICaptureService _captureService;

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
                        _captureService.Start(CaptureType.Screenshot, "screenshot", @"C:\Users\micro\Desktop");
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
