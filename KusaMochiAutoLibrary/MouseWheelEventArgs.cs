using System;
using System.Collections.Generic;
using System.Text;

namespace KusaMochiAutoLibrary
{
    public class MouseWheelEventArgs
    {
        public Win32Point Position { get; set; }
        public int AmountOfMovement { get; set; }
    }
}
