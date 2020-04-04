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
        private static NativeMethods.NativeMethods.LowLevelMouseProc _proc = InputCallback;
        private static IntPtr _hookId = IntPtr.Zero;

        #region Events

        public static event EventHandler<Win32Point> MouseMove;

        #endregion

        public static void Initialize()
        {
            _hookId = SetHook(_proc);
        }

        public static void Finish()
        {
            UnsetHook(_hookId);
        }

        private static IntPtr SetHook(NativeMethods.NativeMethods.LowLevelMouseProc proc)
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

            switch ((NativeMethods.NativeMethods.MouseMessage)wParam)
            {
                case NativeMethods.NativeMethods.MouseMessage.WM_LBUTTONDOWN:
                    break;
                case NativeMethods.NativeMethods.MouseMessage.WM_LBUTTONUP:
                    break;
                case NativeMethods.NativeMethods.MouseMessage.WM_MOUSEMOVE:
                    Win32Point mousePosition = new Win32Point
                    {
                        X = 0,
                        Y = 0
                    };
                    NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
                    MouseMove?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.NativeMethods.MouseMessage.WM_MOUSEWHEEL:
                    break;
                case NativeMethods.NativeMethods.MouseMessage.WM_RBUTTONDOWN:
                    break;
                case NativeMethods.NativeMethods.MouseMessage.WM_RBUTTONUP:
                    break;
                default:
                    break;
            }

            return NativeMethods.NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
    }
}
