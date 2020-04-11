using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prism.Commands;
using Prism.Mvvm;

using KusaMochiAutoLibrary.NativeFunctions;
using KusaMochiAutoLibrary.EventArgs;
using KusaMochiAutoLibrary.Emulators;
using KusaMochiAutoLibrary.Recorders;

namespace Practice.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Mouse Pointer to 200,200

        private DelegateCommand _MouseMoveTo200200;

        public DelegateCommand MouseMoveTo200200 =>
            _MouseMoveTo200200 ?? (_MouseMoveTo200200 = new DelegateCommand(() =>
            {
                _mouse.MouseMoveTo(200, 200);
            }));

        #endregion

        #region Click after 3 sec

        private DelegateCommand _ClickAfter3Sec;
        public DelegateCommand ClickAfter3Sec =>
            _ClickAfter3Sec ?? (_ClickAfter3Sec = new DelegateCommand(() =>
            {
                Task<bool> result = ClickAfter3SecAsync();
            }));
        private async Task<bool> ClickAfter3SecAsync()
        {
            await Task.Delay(3000);
            _mouse.MouseClick();

            return true;
        }

        #endregion

        #region Right click after 3 sec

        private DelegateCommand _RightClickAfter3Sec;
        public DelegateCommand RightClickAfter3Sec =>
            _RightClickAfter3Sec ?? (_RightClickAfter3Sec = new DelegateCommand(() =>
            {
                Task<bool> result = RightClickAfter3SecAsync();
            }));

        private async Task<bool> RightClickAfter3SecAsync()
        {
            await Task.Delay(3000);
            _mouse.MouseRightClick();

            return true;
        }

        #endregion

        #region Drag 300,10 to 300,300

        private DelegateCommand _Drag300010To300300;
        public DelegateCommand Drag300010To300300 =>
            _Drag300010To300300 ?? (_Drag300010To300300 = new DelegateCommand(() =>
            {
                Task<bool> result = Drag300010To300300Async();
            }));
        private async Task<bool> Drag300010To300300Async()
        {
            _mouse.MouseMoveTo(300, 10);
            _mouse.MouseLeftDown();
            await Task.Delay(_timeInterval);

            for (int posY = 10; posY < 300; posY += 10)
            {
                _mouse.MouseMoveTo(300, posY);
                await Task.Delay(_timeInterval);
            }
            _mouse.MouseLeftUp();

            return true;
        }

        #endregion

        #region Wheel Down After 3 sec

        private DelegateCommand _WheelDownAfter3Sec;
        public DelegateCommand WheelDownAfter3Sec =>
            _WheelDownAfter3Sec ?? (_WheelDownAfter3Sec = new DelegateCommand(() =>
            {
                Task<bool> result = WheelDownAfter3SecAsync();
            }));

        private async Task<bool> WheelDownAfter3SecAsync()
        {
            await Task.Delay(3000);
            _mouse.MouseWheel(-100);

            return true;
        }

        #endregion

        #region Middle Button Press After 3 sec

        private DelegateCommand _MiddleButtonPressAfter3Sec;
        public DelegateCommand MiddleButtonPressAfter3Sec =>
            _MiddleButtonPressAfter3Sec ?? (_MiddleButtonPressAfter3Sec = new DelegateCommand(() =>
            {
                Task<bool> resul = MiddleButtonPressAfter3SecAsync();
            }));

        private async Task<bool> MiddleButtonPressAfter3SecAsync()
        {
            await Task.Delay(3000);
            _mouse.MouseMiddleDown();
            _mouse.MouseMiddleUp();

            return true;
        }

        #endregion

        #region Type Kusa Mochi

        private DelegateCommand _TypeKusaMochi;
        public DelegateCommand TypeKusaMochi =>
            _TypeKusaMochi ?? (_TypeKusaMochi = new DelegateCommand(() =>
            {
                Task<bool> result = TypeKusaMochiAsync();
            }));
        private async Task<bool> TypeKusaMochiAsync()
        {
            await Task.Delay(3000);

            _keyboard.KeyDown(Keys.ShiftKey);
            _keyboard.KeyDown(Keys.K);
            _keyboard.KeyUp(Keys.K);
            _keyboard.KeyUp(Keys.ShiftKey);
            _keyboard.KeyPress(Keys.U);
            _keyboard.KeyPress(Keys.S);
            _keyboard.KeyPress(Keys.A);
            _keyboard.KeyPress(Keys.Space);
            _keyboard.KeyDown(Keys.ShiftKey);
            _keyboard.KeyDown(Keys.M);
            _keyboard.KeyUp(Keys.M);
            _keyboard.KeyUp(Keys.ShiftKey);
            _keyboard.KeyPress(Keys.O);
            _keyboard.KeyPress(Keys.C);
            _keyboard.KeyPress(Keys.H);
            _keyboard.KeyPress(Keys.I);

            return true;
        }

        #endregion

        #region Type Windows key

        private DelegateCommand _TypeWindowsKey;
        public DelegateCommand TypeWindowsKey =>
            _TypeWindowsKey ?? (_TypeWindowsKey = new DelegateCommand(() =>
            {
                Task<bool> result = TypeWindowsKeyAsync();
            }));
        private async Task<bool> TypeWindowsKeyAsync()
        {
            await Task.Delay(3000);

            _keyboard.KeyPress(Keys.LWin);

            return true;
        }

        #endregion

        #region Record Events

        private string _RecordedText = "";
        public string RecordedText
        {
            get { return _RecordedText; }
            set
            {
                int maxLength = 500;
                value = value.Length < maxLength ? value : value.Substring(0, maxLength);
                SetProperty(ref _RecordedText, value);
            }
        }

        #endregion

        public MainWindowViewModel()
        {
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
        }

        ~MainWindowViewModel()
        {
            InputDetector.Finish();
        }

        private void OnMouseMove(object sender, Win32Point e)
        {
            RecordedText = "MouseMove(" + e.X.ToString("D4") + "," + e.Y.ToString("D4") + ")\n" + RecordedText;
        }

        private void OnMouseLeftButtonDown(object sender, Win32Point e)
        {
            RecordedText = "MouseLeftButtonDown(" + e.X.ToString("D4") + "," + e.Y.ToString("D4") + ")\n" + RecordedText;
        }

        private void OnMouseLeftButtonUp(object sender, Win32Point e)
        {
            RecordedText = "MouseLeftButtonUp(" + e.X.ToString("D4") + "," + e.Y.ToString("D4") + ")\n" + RecordedText;
        }

        private void OnMouseRightButtonDown(object sender, Win32Point e)
        {
            RecordedText = "MouseRightButtonDown(" + e.X.ToString("D4") + "," + e.Y.ToString("D4") + ")\n" + RecordedText;
        }

        private void OnMouseRightButtonUp(object sender, Win32Point e)
        {
            RecordedText = "MouseRightButtonUp(" + e.X.ToString("D4") + "," + e.Y.ToString("D4") + ")\n" + RecordedText;
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            RecordedText = "MouseWheel(" + e.Position.X.ToString("D4") + "," + e.Position.Y.ToString("D4") + "," + e.AmountOfMovement + ")\n" + RecordedText;
        }

        private void OnMouseMiddleButtonDown(object sender, Win32Point e)
        {
            RecordedText = "MouseMiddleButtonDown(" + e.X.ToString("D4") + "," + e.Y.ToString("D4") + ")\n" + RecordedText;
        }

        private void OnMouseMiddleButtonUp(object sender, Win32Point e)
        {
            RecordedText = "MouseMiddleButtonUp(" + e.X.ToString("D4") + "," + e.Y.ToString("D4") + ")\n" + RecordedText;
        }

        private void OnKeyDown(object sender, KeyboardEventArgs e)
        {
            RecordedText = "KeyDown(" + (int)e.key + ")\n" + RecordedText;
        }

        private void OnKeyUp(object sender, KeyboardEventArgs e)
        {
            RecordedText = "KeyUp(" + (int)e.key + ")\n" + RecordedText;
        }

        private void OnSystemKeyDown(object sender, KeyboardEventArgs e)
        {
            RecordedText = "SystemKeyDown(" + (int)e.key + ")\n" + RecordedText;
        }

        private void OnSystemKeyUp(object sender, KeyboardEventArgs e)
        {
            RecordedText = "SystemKeyUp(" + (int)e.key + ")\n" + RecordedText;
        }

        #region Common Fields

        private readonly MouseEmulator _mouse = new MouseEmulator();
        private readonly KeyboardEmulator _keyboard = new KeyboardEmulator();
        private readonly int _timeInterval = 8;

        #endregion
    }
}
