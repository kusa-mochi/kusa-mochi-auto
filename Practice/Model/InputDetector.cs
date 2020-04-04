﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Practice.NativeMethods;

namespace Practice.Model
{
    public static class InputDetector
    {
        private static NativeMethods.NativeMethods.LowLevelMouseKeyboardProc _mouseProc = MouseInputCallback;
        private static NativeMethods.NativeMethods.LowLevelMouseKeyboardProc _keyboardProc = KeyboardInputCallback;
        private static IntPtr _mouseHookId = IntPtr.Zero;
        private static IntPtr _keyboardHookId = IntPtr.Zero;

        #region Events

        public static event EventHandler<Win32Point> MouseMove;
        public static event EventHandler<Win32Point> MouseLeftButtonDown;
        public static event EventHandler<Win32Point> MouseRightButtonDown;
        public static event EventHandler<Win32Point> MouseLeftButtonUp;
        public static event EventHandler<Win32Point> MouseRightButtonUp;
        public static event EventHandler<KeyboardEventArgs> KeyDown;
        public static event EventHandler<KeyboardEventArgs> KeyUp;
        public static event EventHandler<KeyboardEventArgs> SystemKeyDown;
        public static event EventHandler<KeyboardEventArgs> SystemKeyUp;

        #endregion

        public static void Initialize()
        {
            _mouseHookId = SetHook(_mouseProc, NativeMethods.NativeMethods.HookType.WH_MOUSE_LL);
            _keyboardHookId = SetHook(_keyboardProc, NativeMethods.NativeMethods.HookType.WH_KEYBOARD_LL);
        }

        public static void Finish()
        {
            UnsetHook(_mouseHookId);
            UnsetHook(_keyboardHookId);
        }

        private static IntPtr SetHook(NativeMethods.NativeMethods.LowLevelMouseKeyboardProc proc, NativeMethods.NativeMethods.HookType hookType)
        {
            using (Process currentProcess = Process.GetCurrentProcess())
            using (ProcessModule currentModule = currentProcess.MainModule)
            {
                return NativeMethods.NativeMethods.SetWindowsHookEx(
                    (int)hookType,
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

        private static IntPtr MouseInputCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return NativeMethods.NativeMethods.CallNextHookEx(_mouseHookId, nCode, wParam, lParam);
            }

            Win32Point mousePosition = new Win32Point
            {
                X = 0,
                Y = 0
            };

            switch ((NativeMethods.NativeMethods.MouseMessage)wParam)
            {
                case NativeMethods.NativeMethods.MouseMessage.WM_LBUTTONDOWN:
                    NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
                    MouseLeftButtonDown?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.NativeMethods.MouseMessage.WM_LBUTTONUP:
                    NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
                    MouseLeftButtonUp?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.NativeMethods.MouseMessage.WM_MOUSEMOVE:
                    NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
                    MouseMove?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.NativeMethods.MouseMessage.WM_MOUSEWHEEL:
                    break;
                case NativeMethods.NativeMethods.MouseMessage.WM_RBUTTONDOWN:
                    NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
                    MouseRightButtonDown?.Invoke(null, mousePosition);
                    break;
                case NativeMethods.NativeMethods.MouseMessage.WM_RBUTTONUP:
                    NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
                    MouseRightButtonUp?.Invoke(null, mousePosition);
                    break;
                default:
                    break;
            }

            return NativeMethods.NativeMethods.CallNextHookEx(_mouseHookId, nCode, wParam, lParam);
        }

        private static IntPtr KeyboardInputCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return NativeMethods.NativeMethods.CallNextHookEx(_keyboardHookId, nCode, wParam, lParam);
            }

            KBDLLHOOKSTRUCT param = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
            KeyboardEventArgs args = new KeyboardEventArgs {
                key = (Keys)param.scanCode
            };

            switch ((NativeMethods.NativeMethods.KeyboardMessage)wParam)
            {
                case NativeMethods.NativeMethods.KeyboardMessage.WM_KEYDOWN:
                    KeyDown?.Invoke(null, args);
                    break;
                case NativeMethods.NativeMethods.KeyboardMessage.WM_KEYUP:
                    KeyUp?.Invoke(null, args);
                    break;
                case NativeMethods.NativeMethods.KeyboardMessage.WM_SYSKEYDOWN:
                    SystemKeyDown?.Invoke(null, args);
                    break;
                case NativeMethods.NativeMethods.KeyboardMessage.WM_SYSKEYUP:
                    SystemKeyUp?.Invoke(null, args);
                    break;
                default:
                    break;
            }

            return NativeMethods.NativeMethods.CallNextHookEx(_keyboardHookId, nCode, wParam, lParam);
        }
    }
}