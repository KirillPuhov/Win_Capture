using AppUi.Services;
using AppUi.Window;
using AppUi.Window.DI;
using AppUi.Window.ViewModel;

namespace AppUi.View
{
    public partial class MainWindow : RayeWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var _container = new Container();
            _container.Register<IDialogService, DialogService>(new DialogService(), "DialogService");
            _container.Register<ICaptureService, CaptureService>(new CaptureService(), "CaptureService");
            _container.Register<ITimerService, TimerService>(new TimerService(), "TimerService");
            _container.Register<IRecentService, RecentService>(new RecentService(), "RecentService");

            DataContext = new MainViewModel(_container);
        }
    }
}
