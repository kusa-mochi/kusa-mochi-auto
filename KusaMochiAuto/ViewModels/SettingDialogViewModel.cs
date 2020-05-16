using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using KusaMochiAutoLibrary.Recorders;
using KusaMochiAutoLibrary.EventArgs;

namespace KusaMochiAuto.ViewModels
{
    public class SettingDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "kusa-mochi-auto settings";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() { return true; }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        private string _StopKeyName = Keys.Escape.ToString();
        public string StopKeyName
        {
            get { return _StopKeyName; }
            set { SetProperty(ref _StopKeyName, value); }
        }

        private DelegateCommand _StopKeyTextBoxGotFocusCommand;
        public DelegateCommand StopKeyTextBoxGotFocusCommand =>
            _StopKeyTextBoxGotFocusCommand ?? (_StopKeyTextBoxGotFocusCommand = new DelegateCommand(() =>
            {
                InputDetector.Initialize();
                InputDetector.KeyDown += OnKeyDown;
            }));

        private void OnKeyDown(object sender, KusaMochiAutoLibrary.EventArgs.KeyboardEventArgs e)
        {
            StopKeyName = e.key.ToString();
        }

        private DelegateCommand _StopKeyTextBoxLostFocusCommand;
        public DelegateCommand StopKeyTextBoxLostFocusCommand =>
            _StopKeyTextBoxLostFocusCommand ?? (_StopKeyTextBoxLostFocusCommand = new DelegateCommand(() =>
            {
                InputDetector.Finish();
            }));
    }
}
