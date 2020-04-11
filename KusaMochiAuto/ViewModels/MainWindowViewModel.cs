using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;

using Prism.Commands;
using Prism.Mvvm;

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
            set
            {
                SetProperty(ref _IsRecording, value);
                CanRecord = !value;
                CanStop = value;
            }
        }

        private bool _CanRecord = true;
        public bool CanRecord
        {
            get { return _CanRecord; }
            set { SetProperty(ref _CanRecord, value); }
        }

        private bool _CanStop = false;
        public bool CanStop
        {
            get { return _CanStop; }
            set { SetProperty(ref _CanStop, value); }
        }

        #endregion

        #region OpenCommand

        private DelegateCommand _OpenCommand;
        public DelegateCommand OpenCommand =>
            _OpenCommand ?? (_OpenCommand = new DelegateCommand(() =>
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == true)
                {
                    using (StreamReader reader = new StreamReader(dialog.FileName))
                    {
                        ScriptReader scriptReader = new ScriptReader();
                        string script = reader.ReadToEnd();
                        scriptReader.ExecuteScript(script);
                    }
                }
            }));

        #endregion

        #region RecordCommand

        private DelegateCommand _RecordCommand;
        public DelegateCommand RecordCommand =>
            _RecordCommand ?? (_RecordCommand = new DelegateCommand(
                () =>
                {
                    IsRecording = true;
                    //_recordedSource = "";
                    InputDetector.Initialize();
                    //InputDetector.MouseMove += OnMouseMove;
                    //InputDetector.MouseLeftButtonDown += OnMouseLeftButtonDown;
                    //InputDetector.MouseLeftButtonUp += OnMouseLeftButtonUp;
                    //InputDetector.MouseRightButtonDown += OnMouseRightButtonDown;
                    //InputDetector.MouseRightButtonUp += OnMouseRightButtonUp;
                    //InputDetector.MouseWheel += OnMouseWheel;
                    //InputDetector.MouseMiddleButtonDown += OnMouseMiddleButtonDown;
                    //InputDetector.MouseMiddleButtonUp += OnMouseMiddleButtonUp;
                    //InputDetector.KeyDown += OnKeyDown;
                    //InputDetector.KeyUp += OnKeyUp;
                    //InputDetector.SystemKeyDown += OnSystemKeyDown;
                    //InputDetector.SystemKeyUp += OnSystemKeyUp;
                }));

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
                    if (dialog.ShowDialog() == true)
                    {
                        using (StreamWriter writer = new StreamWriter(dialog.FileName))
                        {
                            writer.Write(recordedScript);
                        }

                    }
                }));

        #endregion

        #region Event Handlers

        #endregion

        #region Private Methods

        #endregion

        #region Fields

        #endregion

        public MainWindowViewModel()
        {

        }
    }
}
