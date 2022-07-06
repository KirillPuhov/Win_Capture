using AppUi.Window.DI;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppUi.Window.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region ctor

        public MainViewModel(IDiContainer container)
        {
            //_depndence = container.Navigate<IDiContainer>("Container");
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
