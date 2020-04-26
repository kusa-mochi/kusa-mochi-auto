using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KusaMochiAutoLibrary.External
{
    public class ProgramRunner
    {
        public void Run(string filePath, string args = null)
        {
            ProcessStartInfo pInfo =
                args == null ?
                new ProcessStartInfo(filePath) :
                new ProcessStartInfo(filePath, args);
            Process.Start(pInfo);
        }
    }
}
