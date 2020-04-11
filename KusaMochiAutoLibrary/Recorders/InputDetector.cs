using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using KusaMochiAutoLibrary.NativeFunctions;
using KusaMochiAutoLibrary.EventArgs;

namespace KusaMochiAutoLibrary.Recorders
{
    public static class InputDetector
    {
        #region Fields

        private static readonly NativeMethods.LowLevelMouseKeyboardProc _mouseProc = MouseInputCallback;
        private static readonly NativeMethods.LowLevelMouseKeyboardProc _keyboardProc = KeyboardInputCallback;
        private static IntPtr _mouseHookId = IntPtr.Zero;
        private static IntPtr _keyboardHookId = IntPtr.Zero;
        private static TimeIntervalCounter _timeCounter = new TimeIntervalCounter();
        private static double _mouseMoveTimeInterval = 33.0;
        private static IScriptGenerator _scriptGenerator = null;

        #endregion

        #region Properties

        public static string RecordedScript
        {
            get
            {
                return _scriptGenerator.GetScript();
            }
        }

        #endregion

        #region Events

        public static event EventHandler<Win32Point> MouseMove;
        public static event EventHandler<Win32Point> MouseLeftDown;
        public static event EventHandler<Win32Point> MouseRightDown;
        public static event EventHandler<Win32Point> MouseLeftUp;
        public static event EventHandler<Win32Point> MouseRightUp;
        public static event EventHandler<MouseWheelEventArgs> MouseWheel;
        public static event EventHandler<Win32Point> MouseMiddleDown;
        public static event EventHandler<Win32Point> MouseMiddleUp;
        public static event EventHandler<KeyboardEventArgs> KeyDown;
        public static event EventHandler<KeyboardEventArgs> KeyUp;
        public static event EventHandler<KeyboardEventArgs> SystemKeyDown;
        public static event EventHandler<KeyboardEventArgs> SystemKeyUp;

        #endregion

        public static void Initialize(IScriptGenerator scriptGenerator)
        {
            _scriptGenerator = scriptGenerator;
            _mouseHookId = SetHook(_mouseProc, NativeMethods.HookType.WH_MOUSE_LL);
            _keyboardHookId = SetHook(_keyboardProc, NativeMethods.HookType.WH_KEYBOARD_LL);
            _timeCounter.Start();
        }

        public static void Finish()
        {
            UnsetHook(_mouseHookId);
            UnsetHook(_keyboardHookId);
        }

        private static IntPtr SetHook(NativeMethods.LowLevelMouseKeyboardProc proc, NativeMethods.HookType hookType)
        {
            using (Process currentProcess = Process.GetCurrentProcess())
            using (ProcessModule currentModule = currentProcess.MainModule)
            {
                return NativeMethods.SetWindowsHookEx(
                    (int)hookType,
                    proc,
                    NativeMethods.GetModuleHandle(currentModule.ModuleName),
                    0
                    );
            }
        }

        private static bool UnsetHook(IntPtr hookId)
        {
            return NativeMethods.UnhookWindowsHookEx(hookId);
        }

        private static IntPtr MouseInputCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return NativeMethods.CallNextHookEx(_mouseHookId, nCode, wParam, lParam);
            }

            MSLLHOOKSTRUCT param = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);

            Win32Point mousePosition = new Win32Point
            {
                X = 0,
                Y = 0
            };
            NativeMethods.GetCursorPos(ref mousePosition);

            switch ((NativeMethods.MouseMessage)wParam)
            {
                case NativeMethods.MouseMessage.WM_LBUTTONDOWN:
                    //_recordedScript += $"MouseLeftDown({mousePosition.X},{mousePosition.Y});\n";
                    _scriptGenerator.MouseLeftDown(mousePosition.X, mousePosition.Y);
                    MouseLeftDown?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.MouseMessage.WM_LBUTTONUP:
                    //_recordedScript += $"MouseLeftUp({mousePosition.X},{mousePosition.Y});\n";
                    _scriptGenerator.MouseLeftUp(mousePosition.X, mousePosition.Y);
                    MouseLeftUp?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.MouseMessage.WM_MOUSEMOVE:
                    if (_timeCounter.CurrentCount > _mouseMoveTimeInterval)
                    {
                        //_recordedScript += $"MouseMoveTo({mousePosition.X},{mousePosition.Y});\n";
                        _scriptGenerator.MouseMove(mousePosition.X, mousePosition.Y);
                        MouseMove?.Invoke(null, mousePosition);
                        _timeCounter.Restart();
                    }
                    break;
                case NativeMethods.MouseMessage.WM_MOUSEWHEEL:
                    int wheelAmount = (param.mouseData >> 16) / 120;
                    //_recordedScript += $"MouseWheel({mousePosition.X},{mousePosition.Y},{wheelAmount});\n";
                    _scriptGenerator.MouseWheel(mousePosition.X, mousePosition.Y, wheelAmount);
                    MouseWheel?.Invoke(null, new MouseWheelEventArgs
                    {
                        Position = mousePosition,
                        AmountOfMovement = wheelAmount
                    });
                    break;
                case NativeMethods.MouseMessage.WM_RBUTTONDOWN:
                    //_recordedScript += $"MouseRightDown({mousePosition.X},{mousePosition.Y});\n";
                    _scriptGenerator.MouseRightDown(mousePosition.X, mousePosition.Y);
                    MouseRightDown?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.MouseMessage.WM_RBUTTONUP:
                    //_recordedScript += $"MouseRightUp({mousePosition.X},{mousePosition.Y});\n";
                    _scriptGenerator.MouseRightUp(mousePosition.X, mousePosition.Y);
                    MouseRightUp?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.MouseMessage.WM_MBUTTONDOWN:
                    //_recordedScript += $"MouseMiddleDown({mousePosition.X},{mousePosition.Y});\n";
                    _scriptGenerator.MouseMiddleDown(mousePosition.X, mousePosition.Y);
                    MouseMiddleDown?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.MouseMessage.WM_MBUTTONUP:
                    //_recordedScript += $"MouseMiddleDown({mousePosition.X},{mousePosition.Y});\n";
                    _scriptGenerator.MouseMiddleUp(mousePosition.X, mousePosition.Y);
                    MouseMiddleUp?.Invoke(null, mousePosition);
                    break;
                default:
                    break;
            }

            return NativeMethods.CallNextHookEx(_mouseHookId, nCode, wParam, lParam);
        }

        private static IntPtr KeyboardInputCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return NativeMethods.CallNextHookEx(_keyboardHookId, nCode, wParam, lParam);
            }

            KBDLLHOOKSTRUCT param = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
            KeyboardEventArgs args = new KeyboardEventArgs
            {
                key = (Keys)param.scanCode
            };

            switch ((NativeMethods.KeyboardMessage)wParam)
            {
                case NativeMethods.KeyboardMessage.WM_KEYDOWN:
                    //_recordedScript += $"KeyDown({(int)args.key});\n";
                    _scriptGenerator.KeyDown(args.key);
                    KeyDown?.Invoke(null, args);
                    break;
                case NativeMethods.KeyboardMessage.WM_KEYUP:
                    //_recordedScript += $"KeyUp({(int)args.key});\n";
                    _scriptGenerator.KeyUp(args.key);
                    KeyUp?.Invoke(null, args);
                    break;
                case NativeMethods.KeyboardMessage.WM_SYSKEYDOWN:
                    //_recordedScript += $"SystemKeyDown({(int)args.key});\n";
                    _scriptGenerator.SystemKeyDown(args.key);
                    SystemKeyDown?.Invoke(null, args);
                    break;
                case NativeMethods.KeyboardMessage.WM_SYSKEYUP:
                    //_recordedScript += $"SystemKeyUp({(int)args.key});\n";
                    _scriptGenerator.SystemKeyUp(args.key);
                    SystemKeyUp?.Invoke(null, args);
                    break;
                default:
                    break;
            }

            return NativeMethods.CallNextHookEx(_keyboardHookId, nCode, wParam, lParam);
        }
    }
}
