using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using KusaMochiAutoLibrary.Emulators;

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
            CSharpScript.RunAsync(
                formattedScript,
                ScriptOptions.Default
                    .WithImports(
                        "System",
                        "KusaMochiAutoLibrary.Emulators"
                        )
                    .WithReferences(
                        Assembly.GetAssembly(typeof(MouseEmulator)),
                        Assembly.GetAssembly(typeof(KeyboardEmulator))
                        )
                );
        }

        private string FormatScript(string script)
        {
            string output = script;
            output = "MouseEmulator mouse = new MouseEmulator();" + output;
            output = "KeyboardEmulator keyboard = new KeyboardEmulator();" + output;
            output = "TimeEmulator timeEmulator = new TimeEmulator();" + output;

            string[] mouseMethods = new string[]
            {
                "MouseMoveTo",
                "MouseClick",
                "MouseRightClick",
                "MouseLeftDown",
                "MouseLeftUp",
                "MouseRightDown",
                "MouseRightUp",
                "MouseMiddleDown",
                "MouseMiddleUp",
                "MouseWheel"
            };
            foreach (string methodName in mouseMethods)
            {
                output = output.Replace(methodName + "(", "mouse." + methodName + "(");
            }

            string[] keyboardMethods = new string[]
            {
                "KeyPress",
                "KeyDown",
                "KeyUp"
            };
            foreach (string methodName in keyboardMethods)
            {
                output = output.Replace(methodName + "(", "keyboard." + methodName + "(");
            }

            string[] timeMethods = new string[]
            {
                "Wait"
            };
            foreach (string methodName in timeMethods)
            {
                output = output.Replace(methodName + "(", "timeEmulator." + methodName + "(");
            }

            return output;
        }
    }
}
