using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KusaMochiAutoLibrary
{
    public static class InputDetector
    {
        private static readonly NativeMethods.LowLevelMouseKeyboardProc _mouseProc = MouseInputCallback;
        private static readonly NativeMethods.LowLevelMouseKeyboardProc _keyboardProc = KeyboardInputCallback;
        private static IntPtr _mouseHookId = IntPtr.Zero;
        private static IntPtr _keyboardHookId = IntPtr.Zero;
        private static TimeIntervalCounter _timeCounter = new TimeIntervalCounter();
        private static double _mouseMoveTimeInterval = 33.0;

        #region Events

        public static event EventHandler<Win32Point> MouseMove;
        public static event EventHandler<Win32Point> MouseLeftButtonDown;
        public static event EventHandler<Win32Point> MouseRightButtonDown;
        public static event EventHandler<Win32Point> MouseLeftButtonUp;
        public static event EventHandler<Win32Point> MouseRightButtonUp;
        public static event EventHandler<MouseWheelEventArgs> MouseWheel;
        public static event EventHandler<Win32Point> MouseMiddleButtonDown;
        public static event EventHandler<Win32Point> MouseMiddleButtonUp;
        public static event EventHandler<KeyboardEventArgs> KeyDown;
        public static event EventHandler<KeyboardEventArgs> KeyUp;
        public static event EventHandler<KeyboardEventArgs> SystemKeyDown;
        public static event EventHandler<KeyboardEventArgs> SystemKeyUp;

        #endregion

        public static void Initialize()
        {
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
                    MouseLeftButtonDown?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.MouseMessage.WM_LBUTTONUP:
                    MouseLeftButtonUp?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.MouseMessage.WM_MOUSEMOVE:
                    if (_timeCounter.CurrentCount > _mouseMoveTimeInterval)
                    {
                        MouseMove?.Invoke(null, mousePosition);
                        _timeCounter.Restart();
                    }
                    break;
                case NativeMethods.MouseMessage.WM_MOUSEWHEEL:
                    MouseWheel?.Invoke(null, new MouseWheelEventArgs
                    {
                        Position = mousePosition,
                        AmountOfMovement = param.mouseData >> 16
                    });
                    break;
                case NativeMethods.MouseMessage.WM_RBUTTONDOWN:
                    MouseRightButtonDown?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.MouseMessage.WM_RBUTTONUP:
                    MouseRightButtonUp?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.MouseMessage.WM_MBUTTONDOWN:
                    MouseMiddleButtonDown?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.MouseMessage.WM_MBUTTONUP:
                    MouseMiddleButtonUp?.Invoke(null, mousePosition);
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
                    KeyDown?.Invoke(null, args);
                    break;
                case NativeMethods.KeyboardMessage.WM_KEYUP:
                    KeyUp?.Invoke(null, args);
                    break;
                case NativeMethods.KeyboardMessage.WM_SYSKEYDOWN:
                    SystemKeyDown?.Invoke(null, args);
                    break;
                case NativeMethods.KeyboardMessage.WM_SYSKEYUP:
                    SystemKeyUp?.Invoke(null, args);
                    break;
                default:
                    break;
            }

            return NativeMethods.CallNextHookEx(_keyboardHookId, nCode, wParam, lParam);
        }
    }
}
