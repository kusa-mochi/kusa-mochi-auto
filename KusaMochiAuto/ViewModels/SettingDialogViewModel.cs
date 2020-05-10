using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;
using Prism.Services.Dialogs;

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
    }
}
