﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;

namespace KusaMochiAutoLibrary.ScriptReaders
{
    public class ScriptReader
    {
        public ScriptReader()
        {
        }

        public void ExecuteScript(string script)
        {
            string formattedScript = FormatScript(script);
        }

        private string FormatScript(string script)
        {
            // TODO
            return null;
        }
    }
}
