using Prism.Commands;
using Prism.Mvvm;

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

        #region コマンド

        private DelegateCommand _RecordCommand;
        public DelegateCommand RecordCommand =>
            _RecordCommand ?? (_RecordCommand = new DelegateCommand(
                () =>
                {
                    IsRecording = true;
                }));

        private DelegateCommand _StopCommand;
        public DelegateCommand StopCommand =>
            _StopCommand ?? (_StopCommand = new DelegateCommand(
                () =>
                {
                    IsRecording = false;
                }));

        #endregion

        public MainWindowViewModel()
        {

        }
    }
}
