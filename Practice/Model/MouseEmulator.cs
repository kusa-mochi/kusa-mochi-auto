using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
            Win32Point mousePosition = new Win32Point
            {
                X = 0,
                Y = 0
            };
            NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);

            INPUT[] inputs = new INPUT[] {
                new INPUT{
                    type = NativeMethods.NativeMethods.INPUT_MOUSE,
                    ui = new INPUT_UNION{
                        mouse = new MOUSEINPUT{
                            dwFlags = NativeMethods.NativeMethods.MOUSEEVENTF_LEFTDOWN,
                            dx = mousePosition.X,
                            dy = mousePosition.Y,
                            mouseData = 0,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                },
                new INPUT{
                    type = NativeMethods.NativeMethods.INPUT_MOUSE,
                    ui = new INPUT_UNION{
                        mouse = new MOUSEINPUT{
                            dwFlags = NativeMethods.NativeMethods.MOUSEEVENTF_LEFTUP,
                            dx = mousePosition.X,
                            dy = mousePosition.Y,
                            mouseData = 0,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                }
            };

            NativeMethods.NativeMethods.SendInput(2, ref inputs[0], Marshal.SizeOf(inputs[0]));

            return true;
        }

        public bool MouseRightClick()
        {
            Win32Point mousePosition = new Win32Point
            {
                X = 0,
                Y = 0
            };
            NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);

            INPUT[] inputs = new INPUT[] {
                new INPUT{
                    type = NativeMethods.NativeMethods.INPUT_MOUSE,
                    ui = new INPUT_UNION{
                        mouse = new MOUSEINPUT{
                            dwFlags = NativeMethods.NativeMethods.MOUSEEVENTF_RIGHTDOWN,
                            dx = mousePosition.X,
                            dy = mousePosition.Y,
                            mouseData = 0,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                },
                new INPUT{
                    type = NativeMethods.NativeMethods.INPUT_MOUSE,
                    ui = new INPUT_UNION{
                        mouse = new MOUSEINPUT{
                            dwFlags = NativeMethods.NativeMethods.MOUSEEVENTF_RIGHTUP,
                            dx = mousePosition.X,
                            dy = mousePosition.Y,
                            mouseData = 0,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                }
            };

            NativeMethods.NativeMethods.SendInput(2, ref inputs[0], Marshal.SizeOf(inputs[0]));

            return true;
        }

        public bool MouseLeftButtonDown()
        {
            Win32Point mousePosition = new Win32Point
            {
                X = 0,
                Y = 0
            };
            NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);

            INPUT input = new INPUT
            {
                type = NativeMethods.NativeMethods.INPUT_MOUSE,
                ui = new INPUT_UNION
                {
                    mouse = new MOUSEINPUT
                    {
                        dwFlags = NativeMethods.NativeMethods.MOUSEEVENTF_LEFTDOWN,
                        dx = mousePosition.X,
                        dy = mousePosition.Y,
                        mouseData = 0,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
        }

        public bool MouseLeftButtonUp()
        {
            Win32Point mousePosition = new Win32Point
            {
                X = 0,
                Y = 0
            };
            NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);

            INPUT input = new INPUT
            {
                type = NativeMethods.NativeMethods.INPUT_MOUSE,
                ui = new INPUT_UNION
                {
                    mouse = new MOUSEINPUT
                    {
                        dwFlags = NativeMethods.NativeMethods.MOUSEEVENTF_LEFTUP,
                        dx = mousePosition.X,
                        dy = mousePosition.Y,
                        mouseData = 0,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
        }

        public bool MouseWheel(int amount)
        {
            Win32Point mousePosition = new Win32Point
            {
                X = 0,
                Y = 0
            };
            NativeMethods.NativeMethods.GetCursorPos(ref mousePosition);

            INPUT input = new INPUT
            {
                type = NativeMethods.NativeMethods.INPUT_MOUSE,
                ui = new INPUT_UNION
                {
                    mouse = new MOUSEINPUT
                    {
                        dwFlags = NativeMethods.NativeMethods.MOUSEEVENTF_WHEEL,
                        dx = mousePosition.X,
                        dy = mousePosition.Y,
                        mouseData = amount,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
        }
    }
}
