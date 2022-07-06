using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppUi.Window.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        
        #region ctor

        public MainViewModel()
        {

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
