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
            if (_isClosingUsingCrossButton)
            {
                Properties.KusaMochiAutoSettings.Default.Reload();
            }
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        public Keys StopKey
        {
            get
            {
                short savedKeyValue = Properties.KusaMochiAutoSettings.Default.StopScriptKey;
                Keys savedKey = (Keys)savedKeyValue;
                StopKeyName = savedKey.ToString();
                return savedKey;
            }
            set
            {
                Properties.KusaMochiAutoSettings.Default.StopScriptKey = (short)value;
                StopKeyName = value.ToString();
            }
        }

        private string _StopKeyName = ((Keys)Properties.KusaMochiAutoSettings.Default.StopScriptKey).ToString();
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
            StopKey = e.key;
        }

        private DelegateCommand _StopKeyTextBoxLostFocusCommand;
        public DelegateCommand StopKeyTextBoxLostFocusCommand =>
            _StopKeyTextBoxLostFocusCommand ?? (_StopKeyTextBoxLostFocusCommand = new DelegateCommand(() =>
            {
                InputDetector.Finish();
            }));

        private DelegateCommand _OkCommand;
        public DelegateCommand OkCommand =>
            _OkCommand ?? (_OkCommand = new DelegateCommand(() =>
            {
                Properties.KusaMochiAutoSettings.Default.Save();
                Prism.Services.Dialogs.DialogResult result = new Prism.Services.Dialogs.DialogResult(ButtonResult.OK);
                _isClosingUsingCrossButton = false;
                RequestClose?.Invoke(result);
            }));

        private DelegateCommand _CancelCommand;
        public DelegateCommand CancelCommand =>
            _CancelCommand ?? (_CancelCommand = new DelegateCommand(() =>
            {
                Properties.KusaMochiAutoSettings.Default.Reload();
                Prism.Services.Dialogs.DialogResult result = new Prism.Services.Dialogs.DialogResult(ButtonResult.Cancel);
                _isClosingUsingCrossButton = false;
                RequestClose?.Invoke(result);
            }));

        private bool _isClosingUsingCrossButton = true;
    }
}
