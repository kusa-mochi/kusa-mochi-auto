using System;
using System.Collections.Generic;
using System.Text;

using KusaMochiAutoLibrary.NativeFunctions;

namespace KusaMochiAutoLibrary.EventArgs
{
    public class MouseWheelEventArgs
    {
        public Win32Point Position { get; set; }
        public int AmountOfMovement { get; set; }
    }
}
