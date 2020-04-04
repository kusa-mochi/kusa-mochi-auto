﻿using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prism.Commands;
using Prism.Mvvm;

using Practice.Model;
using Practice.NativeMethods;

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
            _mouse.MouseLeftButtonDown();
            await Task.Delay(_timeInterval);

            for (int posY = 10; posY < 300; posY += 10)
            {
                _mouse.MouseMoveTo(300, posY);
                await Task.Delay(_timeInterval);
            }
            _mouse.MouseLeftButtonUp();

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
            _keyboard.KeyInput(Keys.U);
            _keyboard.KeyInput(Keys.S);
            _keyboard.KeyInput(Keys.A);
            _keyboard.KeyInput(Keys.Space);
            _keyboard.KeyDown(Keys.ShiftKey);
            _keyboard.KeyDown(Keys.M);
            _keyboard.KeyUp(Keys.M);
            _keyboard.KeyUp(Keys.ShiftKey);
            _keyboard.KeyInput(Keys.O);
            _keyboard.KeyInput(Keys.C);
            _keyboard.KeyInput(Keys.H);
            _keyboard.KeyInput(Keys.I);

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
            RecordedText = "MouseLeftButtonDown()\n" + RecordedText;
        }

        private void OnMouseLeftButtonUp(object sender, Win32Point e)
        {
            RecordedText = "MouseLeftButtonUp()\n" + RecordedText;
        }

        private void OnMouseRightButtonDown(object sender, Win32Point e)
        {
            RecordedText = "MouseRightButtonDown()\n" + RecordedText;
        }

        private void OnMouseRightButtonUp(object sender, Win32Point e)
        {
            RecordedText = "MouseRightButtonUp()\n" + RecordedText;
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
