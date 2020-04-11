using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using KusaMochiAutoLibrary.NativeFunctions;

namespace KusaMochiAutoLibrary.Emulators
{
    public class KeyboardEmulator
    {
        public bool KeyPress(Keys key)
        {
            return KeyPress((short)key);
        }

        public bool KeyPress(short key)
        {
            INPUT[] inputs = new INPUT[]
            {
                new INPUT
                {
                    type = NativeMethods.INPUT_KEYBOARD,
                    ui = new INPUT_UNION
                    {
                        keyboard = new KEYBDINPUT
                        {
                            wVk = key,
                            wScan = (short)NativeMethods.MapVirtualKey(key, 0),
                            dwFlags = NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.KEYEVENTF_KEYDOWN,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                },
                new INPUT{
                    type = NativeMethods.INPUT_KEYBOARD,
                    ui = new INPUT_UNION
                    {
                        keyboard = new KEYBDINPUT
                        {
                            wVk = key,
                            wScan = (short)NativeMethods.MapVirtualKey(key, 0),
                            dwFlags = NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.KEYEVENTF_KEYUP,
                            dwExtraInfo = IntPtr.Zero,
                            time = 0
                        }
                    }
                }
            };

            NativeMethods.SendInput(2, ref inputs[0], Marshal.SizeOf(inputs[0]));

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
                type = NativeMethods.INPUT_KEYBOARD,
                ui = new INPUT_UNION
                {
                    keyboard = new KEYBDINPUT
                    {
                        wVk = key,
                        wScan = (short)NativeMethods.MapVirtualKey(key, 0),
                        dwFlags = NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.KEYEVENTF_KEYDOWN,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };

            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));

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
                type = NativeMethods.INPUT_KEYBOARD,
                ui = new INPUT_UNION
                {
                    keyboard = new KEYBDINPUT
                    {
                        wVk = key,
                        wScan = (short)NativeMethods.MapVirtualKey(key, 0),
                        dwFlags = NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.KEYEVENTF_KEYUP,
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
