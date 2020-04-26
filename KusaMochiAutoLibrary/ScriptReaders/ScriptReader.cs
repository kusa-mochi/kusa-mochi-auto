using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using OpenCvSharp;
using KusaMochiAutoLibrary.Emulators;
using KusaMochiAutoLibrary.ImageRecognition;
using KusaMochiAutoLibrary.External;

namespace KusaMochiAutoLibrary.ScriptReaders
{
    public class ScriptReader
    {
        public ScriptReader()
        {
        }

        public async Task<bool> ExecuteScript(string script)
        {
            string formattedScript = FormatScript(script);
            try
            {
                var result = await CSharpScript.RunAsync(
                    formattedScript,
                    ScriptOptions.Default
                        .WithImports(
                            "System",
                            "System.Collections.Generic",
                            "System.Windows",
                            "OpenCvSharp",
                            "KusaMochiAutoLibrary.Emulators",
                            "KusaMochiAutoLibrary.ImageRecognition",
                            "KusaMochiAutoLibrary.External"
                            )
                        .WithReferences(
                            Assembly.GetAssembly(typeof(Point2d)),
                            Assembly.GetAssembly(typeof(List<Point2d>)),
                            Assembly.GetAssembly(typeof(MouseEmulator)),
                            Assembly.GetAssembly(typeof(KeyboardEmulator)),
                            Assembly.GetAssembly(typeof(TimeEmulator)),
                            Assembly.GetAssembly(typeof(ImageRecognizer)),
                            Assembly.GetAssembly(typeof(ProgramRunner))
                            )
                    );
            }
            catch (CompilationErrorException ex)
            {
                Console.WriteLine("[Script error]");
                Console.WriteLine(ex.Message);
                MessageBox.Show($"[script error]\n{ex.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        private string FormatScript(string script)
        {
            string output = script;
            output = "MouseEmulator mouse = new MouseEmulator();" + output;
            output = "KeyboardEmulator keyboard = new KeyboardEmulator();" + output;
            output = "TimeEmulator timeEmulator = new TimeEmulator();" + output;
            output = "ImageRecognizer imageRecognizer = new ImageRecognizer();" + output;
            output = "ProgramRunner programRunner = new ProgramRunner();" + output;

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

            string[] recognizeMethods = new string[]
            {
                "IsImageFound",
                "GetImagePosition"
            };
            foreach (string methodName in recognizeMethods)
            {
                output = output.Replace(methodName + "(", "imageRecognizer." + methodName + "(");
            }

            string[] externalMethods = new string[]
            {
                "Run"
            };
            foreach (string methodName in externalMethods)
            {
                output = output.Replace(methodName + "(", "programRunner." + methodName + "(");
            }

            return output;
        }
    }
}
