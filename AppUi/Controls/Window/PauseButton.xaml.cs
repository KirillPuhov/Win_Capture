using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppUi.Controls.Window
{
    public partial class PauseButton : UserControl
    {
        private static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(PauseButton));

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }

            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public PauseButton()
        {
            InitializeComponent();
        }

        private bool _isPause = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_isPause)
            {
                Pause.Visibility = Visibility.Hidden;
                Resume.Visibility = Visibility.Visible;
                _isPause = true;
            }
            else
            {
                Pause.Visibility = Visibility.Visible;
                Resume.Visibility = Visibility.Hidden;
                _isPause = false;
            }
        }
    }
}
