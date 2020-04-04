using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

using Practice.NativeMethods;

namespace Practice.Model
{
    public static class InputDetector
    {
        private static NativeMethods.NativeMethods.LowLevelMouseKeyboardProc _proc = InputCallback;
        private static IntPtr _hookId = IntPtr.Zero;

        #region Events

        public static event EventHandler<Win32Point> MouseMove;
        public static event EventHandler<Win32Point> MouseLeftButtonDown;
        public static event EventHandler<Win32Point> MouseRightButtonDown;
        public static event EventHandler<Win32Point> MouseLeftButtonUp;
        public static event EventHandler<Win32Point> MouseRightButtonUp;

        #endregion

        public static void Initialize()
        {
            _hookId = SetHook(_proc);
        }

        public static void Finish()
        {
            UnsetHook(_hookId);
        }

        private static IntPtr SetHook(NativeMethods.NativeMethods.LowLevelMouseKeyboardProc proc)
        {
            using (Process currentProcess = Process.GetCurrentProcess())
            using (ProcessModule currentModule = currentProcess.MainModule)
            {
                return NativeMethods.NativeMethods.SetWindowsHookEx(
                    NativeMethods.NativeMethods.WH_MOUSE_LL,
                    proc,
                    NativeMethods.NativeMethods.GetModuleHandle(currentModule.ModuleName),
                    0
                    );
            }
        }

        private static bool UnsetHook(IntPtr hookId)
        {
            return NativeMethods.NativeMethods.UnhookWindowsHookEx(hookId);
        }

        private static IntPtr InputCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return NativeMethods.NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
            }

            Win32Point mousePosition = new Win32Point
            {
                X = 0,
                Y = 0
            };

            switch ((NativeMethods.NativeMethods.MouseKeyboardMessage)wParam)
            {
                case NativeMethods.NativeMethods.MouseKeyboardMessage.WM_LBUTTONDOWN:
                    NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
                    MouseLeftButtonDown?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.NativeMethods.MouseKeyboardMessage.WM_LBUTTONUP:
                    NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
                    MouseLeftButtonUp?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.NativeMethods.MouseKeyboardMessage.WM_MOUSEMOVE:
                    NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
                    MouseMove?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.NativeMethods.MouseKeyboardMessage.WM_MOUSEWHEEL:
                    break;
                case NativeMethods.NativeMethods.MouseKeyboardMessage.WM_RBUTTONDOWN:
                    NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
                    MouseRightButtonDown?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.NativeMethods.MouseKeyboardMessage.WM_RBUTTONUP:
                    NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
                    MouseRightButtonUp?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.NativeMethods.MouseKeyboardMessage.WM_KEYDOWN:
                    break;
                case NativeMethods.NativeMethods.MouseKeyboardMessage.WM_KEYUP:
                    break;
                case NativeMethods.NativeMethods.MouseKeyboardMessage.WM_SYSKEYDOWN:
                    break;
                case NativeMethods.NativeMethods.MouseKeyboardMessage.WM_SYSKEYUP:
                    break;
                default:
                    break;
            }

            return NativeMethods.NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
    }
}
