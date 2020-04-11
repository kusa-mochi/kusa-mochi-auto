using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using KusaMochiAutoLibrary.ScriptReaders;
using KusaMochiAuto.Views;

namespace KusaMochiAuto
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return _fileInput ? null : Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        private async void PrismApplication_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length != 1) return;
            _fileInput = true;

            using (StreamReader reader = new StreamReader(e.Args[0]))
            {
                ScriptReader scriptReader = new ScriptReader();
                bool result = await scriptReader.ExecuteScript(reader.ReadToEnd());
            }
            Application.Current.Shutdown(0);

        }

        private bool _fileInput = false;
    }
}
