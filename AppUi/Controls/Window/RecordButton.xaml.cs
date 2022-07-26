using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace AppUi.Controls.Window
{
    public partial class RecordButton : UserControl
    {
        private static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(RecordButton));

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

        public RecordButton()
        {
            InitializeComponent();
        }

        private bool _isRecording = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_isRecording)
            {
                Ellipse.Visibility = Visibility.Hidden;
                Rectangle.Visibility = Visibility.Visible;
                _isRecording = true;
            }
            else
            {
                Ellipse.Visibility = Visibility.Visible;
                Rectangle.Visibility = Visibility.Hidden;
                _isRecording = false;
            }
        }
    }
}
