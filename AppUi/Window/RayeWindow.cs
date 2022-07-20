using AppUi.Controls.Window;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;

namespace AppUi.Window
{
    public class RayeWindow : System.Windows.Window
    {
        public static readonly DependencyProperty HeaderIconProperty = DependencyProperty.Register("HeaderIcon", typeof(UIElement), typeof(RayeWindow));
        public UIElement HeaderIcon
        {
            get
            {
                return (UIElement)GetValue(HeaderIconProperty);
            }
            set
            {
                SetValue(HeaderIconProperty, value);
            }
        }

        public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.Register("HeaderContent", typeof(object), typeof(RayeWindow));
        public object HeaderContent
        {
            get
            {
                return GetValue(HeaderContentProperty);
            }
            set
            {
                SetValue(HeaderContentProperty, value);
            }
        }

        public static readonly DependencyProperty IsHeaderTextVisibleProperty = DependencyProperty.Register("IsHeaderTextVisible", typeof(bool), typeof(RayeWindow), new PropertyMetadata(true));
        public bool IsHeaderTextVisible
        {
            get
            {
                return (bool)GetValue(IsHeaderTextVisibleProperty);
            }
            set
            {
                SetValue(IsHeaderTextVisibleProperty, value);
            }
        }

        public static readonly DependencyProperty MinimizeBoxProperty = DependencyProperty.Register("MinimizeBox", typeof(bool), typeof(RayeWindow), new PropertyMetadata(true));
        public bool MinimizeBox
        {
            get { return (bool)GetValue(MinimizeBoxProperty); }
            set { SetValue(MinimizeBoxProperty, value); }
        }

        public static readonly DependencyProperty MaximizeBoxProperty = DependencyProperty.Register("MaximizeBox", typeof(bool), typeof(RayeWindow), new PropertyMetadata(true));
        public bool MaximizeBox
        {
            get { return (bool)GetValue(MaximizeBoxProperty); }
            set { SetValue(MaximizeBoxProperty, value); }
        }

        private TaskbarIcon _taskbar;

        public RayeWindow() : base()
        {
            base.Style = (Style)FindResource("RayeWindowStyle");
            _taskbar = new TaskbarIcon();
        }

        public override void OnApplyTemplate()
        {
            var windowControlBox = GetTemplateChild("WindowControlBox") as WindowControlBox;
            windowControlBox.OnMinimize = OnMinimize;
            windowControlBox.OnMaximize = OnMaximize;
            windowControlBox.OnClose = OnClose;

            base.OnApplyTemplate();
        }

        private void OnMinimize(object sender, RoutedEventArgs e)
        {
            base.WindowState = WindowState.Minimized;
        }

        private void OnMaximize(object sender, RoutedEventArgs e)
        {
            _taskbar.Visibility = Visibility.Visible;
            _taskbar.TrayLeftMouseDown += TaskbarIcon_TrayLeftMouseDown;

            if (WindowState == WindowState.Normal)
            {
                base.Hide();
            }
        }

        private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            base.Show();
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            base.Close();
        }
    }
}
