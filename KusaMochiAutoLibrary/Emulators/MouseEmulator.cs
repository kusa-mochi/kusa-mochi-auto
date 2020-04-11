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
                        mouseData = amount,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
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
    }
}
