using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppUi.Window.ViewModel
{
    public sealed class ConfigurationViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
