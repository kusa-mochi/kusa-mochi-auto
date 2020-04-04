using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Practice.NativeMethods;

namespace Practice.Model
{
    public class KeyboardEmulator
    {
        public bool KeyInput(Keys key)
        {
            return KeyInput((short)key);
        }

        public bool KeyInput(short key)
        {
            INPUT[] inputs = new INPUT[]
            {
                new INPUT
                {
                    type = NativeMethods.NativeMethods.INPUT_KEYBOARD,
                    ui = new INPUT_UNION
                    {
                        keyboard = new KEYBDINPUT
                        {
                            wVk = key,
                            wScan = (short)NativeMethods.NativeMethods.MapVirtualKey(key, 0),
                            dwFlags = NativeMethods.NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.NativeMethods.KEYEVENTF_KEYDOWN,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                },
                new INPUT{
                    type = NativeMethods.NativeMethods.INPUT_KEYBOARD,
                    ui = new INPUT_UNION
                    {
                        keyboard = new KEYBDINPUT
                        {
                            wVk = key,
                            wScan = (short)NativeMethods.NativeMethods.MapVirtualKey(key, 0),
                            dwFlags = NativeMethods.NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.NativeMethods.KEYEVENTF_KEYUP,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                }
            };

            NativeMethods.NativeMethods.SendInput(2, ref inputs[0], Marshal.SizeOf(inputs[0]));

            return true;
        }

        public bool KeyDown(Keys key)
        {
            return KeyDown((short)key);
        }

        public bool KeyDown(short key)
        {
            INPUT input = new INPUT
            {
                type = NativeMethods.NativeMethods.INPUT_KEYBOARD,
                ui = new INPUT_UNION
                {
                    keyboard = new KEYBDINPUT
                    {
                        wVk = key,
                        wScan = (short)NativeMethods.NativeMethods.MapVirtualKey(key, 0),
                        dwFlags = NativeMethods.NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.NativeMethods.KEYEVENTF_KEYDOWN,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

            return true;
        }

        public bool KeyUp(Keys key)
        {
            return KeyUp((short)key);
        }

        public bool KeyUp(short key)
        {
            INPUT input = new INPUT
            {
                type = NativeMethods.NativeMethods.INPUT_KEYBOARD,
                ui = new INPUT_UNION
                {
                    keyboard = new KEYBDINPUT
                    {
                        wVk = key,
                        wScan = (short)NativeMethods.NativeMethods.MapVirtualKey(key, 0),
                        dwFlags = NativeMethods.NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.NativeMethods.KEYEVENTF_KEYUP,
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
