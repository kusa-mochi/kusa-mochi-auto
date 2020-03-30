using System;
using System.Collections.Generic;
using System.Text;

using Practice.NativeMethods;

namespace Practice.Model
{
    public class MouseEmulator
    {
        public bool MouseMoveTo(int x, int y)
        {
            NativeMethods.NativeMethods.SetCursorPos(x, y);
            return true;
        }

        public bool MouseClick()
        {
            NativeMethods.NativeMethods.Win32Point mousePosition = new NativeMethods.NativeMethods.Win32Point
            {
                X = 0,
                Y = 0
            };

            NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
            NativeMethods.NativeMethods.mouse_event(
                NativeMethods.NativeMethods.MOUSEEVENTFLAG_LEFTDOWN | NativeMethods.NativeMethods.MOUSEEVENTFLAG_LEFTUP,
                (uint)mousePosition.X,
                (uint)mousePosition.Y,
                0,
                0
                );

            return true;
        }

        public bool MouseRightClick()
        {
            NativeMethods.NativeMethods.Win32Point mousePosition = new NativeMethods.NativeMethods.Win32Point
            {
                X = 0,
                Y = 0
            };

            NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
            NativeMethods.NativeMethods.mouse_event(
                NativeMethods.NativeMethods.MOUSEEVENTFLAG_RIGHTDOWN | NativeMethods.NativeMethods.MOUSEEVENTFLAG_RIGHTUP,
                (uint)mousePosition.X,
                (uint)mousePosition.Y,
                0,
                0
                );

            return true;
        }

        public bool MouseLeftButtonDown()
        {
            NativeMethods.NativeMethods.Win32Point mousePosition = new NativeMethods.NativeMethods.Win32Point
            {
                X = 0,
                Y = 0
            };

            NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
            NativeMethods.NativeMethods.mouse_event(
                NativeMethods.NativeMethods.MOUSEEVENTFLAG_LEFTDOWN,
                (uint)mousePosition.X,
                (uint)mousePosition.Y,
                0,
                0
                );

            return true;
        }

        public bool MouseLeftButtonUp()
        {
            NativeMethods.NativeMethods.Win32Point mousePosition = new NativeMethods.NativeMethods.Win32Point
            {
                X = 0,
                Y = 0
            };

            NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);
            NativeMethods.NativeMethods.mouse_event(
                NativeMethods.NativeMethods.MOUSEEVENTFLAG_LEFTUP,
                (uint)mousePosition.X,
                (uint)mousePosition.Y,
                0,
                0
                );

            return true;
        }
    }
}
