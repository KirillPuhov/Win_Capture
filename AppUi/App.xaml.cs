using System;
using System.Windows;

namespace AppUi
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Environment.SetEnvironmentVariable("IsRecord", "False");
        }
    }
}
