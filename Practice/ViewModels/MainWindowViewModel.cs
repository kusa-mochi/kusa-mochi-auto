using Prism.Commands;
using Prism.Mvvm;

using Practice.NativeMethods;

namespace Practice.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private DelegateCommand _MouseMoveTo200200;
        public DelegateCommand MouseMoveTo200200 =>
            _MouseMoveTo200200 ?? (_MouseMoveTo200200 = new DelegateCommand(ExecuteMouseMoveTo200200));

        void ExecuteMouseMoveTo200200()
        {
            NativeMethods.NativeMethods.SetCursorPos(200, 200);
        }

        public MainWindowViewModel()
        {

        }
    }
}
