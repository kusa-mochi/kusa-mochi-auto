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
        #region 変更通知プロパティ

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
                    _recordedSource = "";
                    InputDetector.Initialize();
                    InputDetector.MouseMove += OnMouseMove;
                    InputDetector.MouseLeftButtonDown += OnMouseLeftButtonDown;
                    InputDetector.MouseLeftButtonUp += OnMouseLeftButtonUp;
                    InputDetector.MouseRightButtonDown += OnMouseRightButtonDown;
                    InputDetector.MouseRightButtonUp += OnMouseRightButtonUp;
                    InputDetector.MouseWheel += OnMouseWheel;
                    InputDetector.MouseMiddleButtonDown += OnMouseMiddleButtonDown;
                    InputDetector.MouseMiddleButtonUp += OnMouseMiddleButtonUp;
                    InputDetector.KeyDown += OnKeyDown;
                    InputDetector.KeyUp += OnKeyUp;
                    InputDetector.SystemKeyDown += OnSystemKeyDown;
                    InputDetector.SystemKeyUp += OnSystemKeyUp;
                }));

        #endregion

        #region StopCommand

        private DelegateCommand _StopCommand;
        public DelegateCommand StopCommand =>
            _StopCommand ?? (_StopCommand = new DelegateCommand(
                () =>
                {
                    IsRecording = false;
                    InputDetector.Finish();
                    SaveFileDialog dialog = new SaveFileDialog();
                    if (dialog.ShowDialog() == true)
                    {
                        using (StreamWriter writer = new StreamWriter(dialog.FileName))
                        {
                            System.Windows.MessageBox.Show(_recordedSource);
                            writer.Write(_recordedSource);
                        }

                    }
                }));

        #endregion

        #region Event Handlers

        private void OnMouseMove(object sender, Win32Point e)
        {
            _recordedSource += "MouseMove(" + e.X + "," + e.Y + ")\n";
        }

        private void OnMouseLeftButtonDown(object sender, Win32Point e)
        {
            _recordedSource += "MouseLDown(" + e.X + "," + e.Y + ")\n";
        }

        private void OnMouseLeftButtonUp(object sender, Win32Point e)
        {
            _recordedSource += "MouseLUp(" + e.X + "," + e.Y + ")\n";
        }

        private void OnMouseRightButtonDown(object sender, Win32Point e)
        {
            _recordedSource += "MouseRDown(" + e.X + "," + e.Y + ")\n";
        }

        private void OnMouseRightButtonUp(object sender, Win32Point e)
        {
            _recordedSource += "MouseRUp(" + e.X + "," + e.Y + ")\n";
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _recordedSource += "MouseWheel(" + e.Position.X + "," + e.Position.Y + "," + e.AmountOfMovement + ")\n";
        }

        private void OnMouseMiddleButtonDown(object sender, Win32Point e)
        {
            _recordedSource += "MouseMDown(" + e.X + "," + e.Y + ")\n";
        }

        private void OnMouseMiddleButtonUp(object sender, Win32Point e)
        {
            _recordedSource += "MouseMUp(" + e.X + "," + e.Y + ")\n";
        }

        private void OnKeyDown(object sender, KeyboardEventArgs e)
        {
            _recordedSource += "KeyDown(" + (int)e.key + ")\n";
        }

        private void OnKeyUp(object sender, KeyboardEventArgs e)
        {
            _recordedSource += "KeyUp(" + (int)e.key + ")\n";
        }

        private void OnSystemKeyDown(object sender, KeyboardEventArgs e)
        {
            _recordedSource += "SystemKeyDown(" + (int)e.key + ")\n";
        }

        private void OnSystemKeyUp(object sender, KeyboardEventArgs e)
        {
            _recordedSource += "SystemKeyUp(" + (int)e.key + ")\n";
        }

        #endregion

        #region Private Methods

        #endregion

        #region Fields

        private string _recordedSource = "";

        #endregion

        public MainWindowViewModel()
        {

        }
    }
}
