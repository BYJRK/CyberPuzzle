using CyberPuzzle.Helpers;
using CyberPuzzle.ViewModel;
using System;
using System.Windows;

namespace CyberPuzzle
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            RandomHelper.Init(1335);

            var wnd = new MainWindow();
            wnd.Show();
        }
    }
}
