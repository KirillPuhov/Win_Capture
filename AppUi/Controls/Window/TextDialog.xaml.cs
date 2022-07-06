using AppUi.Window;

namespace AppUi.Controls.Window
{
    /// <summary>
    /// Логика взаимодействия для TextDialog.xaml
    /// </summary>
    public partial class TextDialog : RayeWindow
    {
        public string Header { get; set; }
        public string Body { get; set; }

        public TextDialog(string header, string body)
        {
            InitializeComponent();

            this.Header = header;
            this.Body = body;
        }
    }
}
