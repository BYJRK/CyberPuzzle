using CyberPuzzle.Helpers;
using CyberPuzzle.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace CyberPuzzle
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        static App()
        {
            Services = ConfigureServices();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            RandomHelper.Init();

            var wnd = new MainWindow();
            wnd.Show();
        }

        public static IServiceProvider Services { get; private set; }

        #region IoC

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<Level, Level>();

            return services.BuildServiceProvider();
        }

        #endregion
    }
}
