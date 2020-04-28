using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using KusaMochiAutoLibrary.NativeFunctions;

namespace KusaMochiAutoLibrary.Emulators
{
    public class MouseEmulator
    {
        public bool MouseMoveTo(int x, int y)
        {
            NativeMethods.SetCursorPos(x, y);
            return true;
        }

        public bool MouseClick()
        {
            Win32Point p = GetMousePosition();
            return MouseClick(p.X, p.Y);
        }

        public bool MouseClick(int x, int y)
        {
            MouseMoveTo(x, y);

            INPUT[] inputs = new INPUT[] {
                new INPUT{
                    type = NativeMethods.INPUT_MOUSE,
                    ui = new INPUT_UNION{
                        mouse = new MOUSEINPUT{
                            dwFlags = NativeMethods.MOUSEEVENTF_LEFTDOWN,
                            dx = x,
                            dy = y,
                            mouseData = 0,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                },
                new INPUT{
                    type = NativeMethods.INPUT_MOUSE,
                    ui = new INPUT_UNION{
                        mouse = new MOUSEINPUT{
                            dwFlags = NativeMethods.MOUSEEVENTF_LEFTUP,
                            dx = x,
                            dy = y,
                            mouseData = 0,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                }
            };

            NativeMethods.SendInput(2, ref inputs[0], Marshal.SizeOf(inputs[0]));

            return true;
        }

        public bool MouseRightClick()
        {
            Win32Point p = GetMousePosition();
            return MouseRightClick(p.X, p.Y);
        }

        public bool MouseRightClick(int x, int y)
        {
            MouseMoveTo(x, y);

            INPUT[] inputs = new INPUT[] {
                new INPUT{
                    type = NativeMethods.INPUT_MOUSE,
                    ui = new INPUT_UNION{
                        mouse = new MOUSEINPUT{
                            dwFlags = NativeMethods.MOUSEEVENTF_RIGHTDOWN,
                            dx = x,
                            dy = y,
                            mouseData = 0,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                },
                new INPUT{
                    type = NativeMethods.INPUT_MOUSE,
                    ui = new INPUT_UNION{
                        mouse = new MOUSEINPUT{
                            dwFlags = NativeMethods.MOUSEEVENTF_RIGHTUP,
                            dx = x,
                            dy = y,
                            mouseData = 0,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                }
            };

            NativeMethods.SendInput(2, ref inputs[0], Marshal.SizeOf(inputs[0]));

            return true;
        }

        public bool MouseMiddleClick()
        {
            Win32Point p = GetMousePosition();
            return MouseMiddleClick(p.X, p.Y);
        }

        public bool MouseMiddleClick(int x, int y)
        {
            MouseMoveTo(x, y);

            INPUT[] inputs = new INPUT[] {
                new INPUT{
                    type = NativeMethods.INPUT_MOUSE,
                    ui = new INPUT_UNION{
                        mouse = new MOUSEINPUT{
                            dwFlags = NativeMethods.MOUSEEVENTF_MIDDLEDOWN,
                            dx = x,
                            dy = y,
                            mouseData = 0,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                },
                new INPUT{
                    type = NativeMethods.INPUT_MOUSE,
                    ui = new INPUT_UNION{
                        mouse = new MOUSEINPUT{
                            dwFlags = NativeMethods.MOUSEEVENTF_MIDDLEUP,
                            dx = x,
                            dy = y,
                            mouseData = 0,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                }
            };

            NativeMethods.SendInput(2, ref inputs[0], Marshal.SizeOf(inputs[0]));

            return true;
        }

        public bool MouseLeftDown()
        {
            Win32Point p = GetMousePosition();
            return MouseLeftDown(p.X, p.Y);
        }

        public bool MouseLeftDown(int x, int y)
        {
            MouseMoveTo(x, y);

            INPUT input = new INPUT
            {
                type = NativeMethods.INPUT_MOUSE,
                ui = new INPUT_UNION
                {
                    mouse = new MOUSEINPUT
                    {
                        dwFlags = NativeMethods.MOUSEEVENTF_LEFTDOWN,
                        dx = x,
                        dy = y,
                        mouseData = 0,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
        }

        public bool MouseLeftUp()
        {
            Win32Point p = GetMousePosition();
            return MouseLeftUp(p.X, p.Y);
        }

        public bool MouseLeftUp(int x, int y)
        {
            MouseMoveTo(x, y);

            INPUT input = new INPUT
            {
                type = NativeMethods.INPUT_MOUSE,
                ui = new INPUT_UNION
                {
                    mouse = new MOUSEINPUT
                    {
                        dwFlags = NativeMethods.MOUSEEVENTF_LEFTUP,
                        dx = x,
                        dy = y,
                        mouseData = 0,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
        }

        public bool MouseRightDown()
        {
            Win32Point p = GetMousePosition();
            return MouseRightDown(p.X, p.Y);
        }

        public bool MouseRightDown(int x, int y)
        {
            MouseMoveTo(x, y);

            INPUT input = new INPUT
            {
                type = NativeMethods.INPUT_MOUSE,
                ui = new INPUT_UNION
                {
                    mouse = new MOUSEINPUT
                    {
                        dwFlags = NativeMethods.MOUSEEVENTF_RIGHTDOWN,
                        dx = x,
                        dy = y,
                        mouseData = 0,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
        }

        public bool MouseRightUp()
        {
            Win32Point p = GetMousePosition();
            return MouseRightUp(p.X, p.Y);
        }

        public bool MouseRightUp(int x, int y)
        {
            MouseMoveTo(x, y);

            INPUT input = new INPUT
            {
                type = NativeMethods.INPUT_MOUSE,
                ui = new INPUT_UNION
                {
                    mouse = new MOUSEINPUT
                    {
                        dwFlags = NativeMethods.MOUSEEVENTF_RIGHTUP,
                        dx = x,
                        dy = y,
                        mouseData = 0,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
        }

        public bool MouseWheel(int amount)
        {
            Win32Point p = GetMousePosition();
            return MouseWheel(p.X, p.Y, amount);
        }

        public bool MouseWheel(int x, int y, int amount)
        {
            MouseMoveTo(x, y);

            INPUT input = new INPUT
            {
                type = NativeMethods.INPUT_MOUSE,
                ui = new INPUT_UNION
                {
                    mouse = new MOUSEINPUT
                    {
                        dwFlags = NativeMethods.MOUSEEVENTF_WHEEL,
                        dx = x,
                        dy = y,
                        mouseData = amount * 120,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
        }

        public bool MouseMiddleDown()
        {
            Win32Point p = GetMousePosition();
            return MouseMiddleDown(p.X, p.Y);
        }

        public bool MouseMiddleDown(int x, int y)
        {
            MouseMoveTo(x, y);

            INPUT input = new INPUT
            {
                type = NativeMethods.INPUT_MOUSE,
                ui = new INPUT_UNION
                {
                    mouse = new MOUSEINPUT
                    {
                        dwFlags = NativeMethods.MOUSEEVENTF_MIDDLEDOWN,
                        dx = x,
                        dy = y,
                        mouseData = 0,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
        }

        public bool MouseMiddleUp()
        {
            Win32Point p = GetMousePosition();
            return MouseMiddleUp(p.X, p.Y);
        }

        public bool MouseMiddleUp(int x, int y)
        {
            MouseMoveTo(x, y);

            INPUT input = new INPUT
            {
                type = NativeMethods.INPUT_MOUSE,
                ui = new INPUT_UNION
                {
                    mouse = new MOUSEINPUT
                    {
                        dwFlags = NativeMethods.MOUSEEVENTF_MIDDLEUP,
                        dx = x,
                        dy = y,
                        mouseData = 0,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
        }

        private Win32Point GetMousePosition()
        {
            Win32Point p = new Win32Point { X = 0, Y = 0 };
            NativeMethods.GetCursorPos(ref p);
            return p;
        }
    }
}
