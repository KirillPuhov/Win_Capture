using AppUi.Window;
using AppUi.Window.ViewModel;

namespace AppUi.View
{
    public partial class ConfigurationWindow : RayeWindow
    {
        public ConfigurationWindow()
        {
            InitializeComponent();

            DataContext = new ConfigurationViewModel();
        }
    }
}
