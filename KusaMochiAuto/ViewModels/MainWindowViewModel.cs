using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using KusaMochiAutoLibrary.Recorders;
using KusaMochiAutoLibrary.EventArgs;
using KusaMochiAutoLibrary.NativeFunctions;
using KusaMochiAutoLibrary.ScriptReaders;

namespace KusaMochiAuto.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Change Notification Properties

        private bool _IsRecording = false;
        public bool IsRecording
        {
            get { return _IsRecording; }
            set { SetProperty(ref _IsRecording, value); }
        }

        private bool _IsRunningScript = false;
        public bool IsRunningScript
        {
            get { return _IsRunningScript; }
            set { SetProperty(ref _IsRunningScript, value); }
        }

        #endregion

        #region OpenCommand

        private DelegateCommand _OpenCommand;
        public DelegateCommand OpenCommand =>
            _OpenCommand ?? (_OpenCommand = new DelegateCommand(() =>
                {
                    IsRunningScript = true;
                    Task<bool> result = OpenCommandAsync();
                },
                () => !IsRecording && !IsRunningScript)
                    .ObservesProperty(() => IsRecording)
                    .ObservesProperty(() => IsRunningScript)
            );
        private async Task<bool> OpenCommandAsync()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".cs";
            dialog.Filter = "C# files|*.cs|All files|*.*";
            if (dialog.ShowDialog() == true)
            {
                using (StreamReader reader = new StreamReader(dialog.FileName))
                {
                    ScriptReader scriptReader = new ScriptReader();
                    string script = reader.ReadToEnd();
                    await scriptReader.ExecuteScript(script);
                }
            }

            IsRunningScript = false;

            return true;
        }

        #endregion

        #region RecordCommand

        private DelegateCommand _RecordCommand;
        public DelegateCommand RecordCommand =>
            _RecordCommand ?? (_RecordCommand = new DelegateCommand(
                () =>
                {
                    IsRecording = true;
                    InputDetector.Initialize(new CSharpScriptGenerator());
                },
                () => !IsRecording && !IsRunningScript)
                    .ObservesProperty(() => IsRecording)
                    .ObservesProperty(() => IsRunningScript)
            );

        #endregion

        #region StopCommand

        private DelegateCommand _StopCommand;
        public DelegateCommand StopCommand =>
            _StopCommand ?? (_StopCommand = new DelegateCommand(
                () =>
                {
                    IsRecording = false;
                    string recordedScript = InputDetector.RecordedScript;
                    InputDetector.Finish();
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.DefaultExt = ".cs";
                    dialog.AddExtension = true;
                    dialog.Filter = "C# files|*.cs|All files|*.*";
                    if (dialog.ShowDialog() == true)
                    {
                        using (StreamWriter writer = new StreamWriter(dialog.FileName))
                        {
                            writer.Write(recordedScript);
                        }

                    }
                },
                () => IsRecording && !IsRunningScript)
                    .ObservesProperty(() => IsRecording)
                    .ObservesProperty(() => IsRunningScript)
            );

        #endregion

        private DelegateCommand _SettingCommand;
        public DelegateCommand SettingCommand =>
                _SettingCommand ?? (_SettingCommand = new DelegateCommand(() =>
                {
                    _dialogService.ShowDialog("SettingDialog", null, null);
                },
                () => !IsRecording && !IsRunningScript)
                    .ObservesProperty(() => IsRecording)
                    .ObservesProperty(() => IsRunningScript)
            );

        #region Event Handlers

        #endregion

        #region Private Methods

        #endregion

        #region Fields

        private IDialogService _dialogService = null;

        #endregion

        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
    }
}
