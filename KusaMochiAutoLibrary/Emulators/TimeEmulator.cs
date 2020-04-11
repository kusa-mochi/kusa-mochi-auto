using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KusaMochiAutoLibrary.Emulators
{
    public class TimeEmulator
    {
        public void Wait(int t)
        {
            Thread.Sleep(t);
        }
    }
}
