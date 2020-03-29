using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Practice.NativeMethods
{
    public static class NativeMethods
    {
        [DllImport("User32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
    }
}
