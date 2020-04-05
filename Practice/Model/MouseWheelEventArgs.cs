using System;
using System.Collections.Generic;
using System.Text;

using Practice.NativeMethods;

namespace Practice.Model
{
    public class MouseWheelEventArgs
    {
        public Win32Point Position { get; set; }
        public int AmountOfMovement { get; set; }
    }
}
