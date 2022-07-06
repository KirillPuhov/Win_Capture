using AppUi.Services;
using AppUi.Window;
using AppUi.Window.DI;
using AppUi.Window.ViewModel;

namespace AppUi
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RayeWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var _container = new Container();
            _container.Register<IDialogService, DialogService>(new DialogService(), "DialogService");

            DataContext = new MainViewModel(_container);
        }
    }
}
