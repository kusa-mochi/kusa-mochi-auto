using Prism.Ioc;
using KusaMochiAuto.Views;
using System.Windows;

namespace KusaMochiAuto
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        private void PrismApplication_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 0) return;


        }
    }
}
