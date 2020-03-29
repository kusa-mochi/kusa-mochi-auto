using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Practice.NativeMethods
{
    public static class NativeMethods
    {
        public const int MOUSEEVENTFLAG_LEFTDOWN = 0x02;
        public const int MOUSEEVENTFLAG_LEFTUP = 0x04;
        public const int MOUSEEVENTFLAG_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTFLAG_RIGHTUP = 0x10;

        [StructLayout(LayoutKind.Sequential)]
        public struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        [DllImport("User32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("User32.dll")]
        public static extern bool GetCursorPos(ref Win32Point pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
    }
}
