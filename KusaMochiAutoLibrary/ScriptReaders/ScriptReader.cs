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

            ScriptAdjustmentUnit[] adjustments = new ScriptAdjustmentUnit[] {
                new ScriptAdjustmentUnit()
                {
                    ClassName = "MouseEmulator",
                    Methods = new string[]
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
                    }
                },
                new ScriptAdjustmentUnit()
                {
                    ClassName = "KeyboardEmulator",
                    Methods = new string[]
                    {
                        "KeyPress",
                        "KeyDown",
                        "KeyUp"
                    }
                },
                new ScriptAdjustmentUnit()
                {
                    ClassName = "TimeEmulator",
                    Methods = new string[]
                    {
                        "Wait"
                    }
                },
                new ScriptAdjustmentUnit()
                {
                    ClassName = "ImageRecognizer",
                    Methods = new string[]
                    {
                        "GetImagePosition"
                    }
                },
                new ScriptAdjustmentUnit()
                {
                    ClassName = "ProgramRunner",
                    Methods = new string[]
                    {
                        "Run"
                    }
                }
            };

            foreach (ScriptAdjustmentUnit adjustment in adjustments)
            {
                output = AdjustScript(output, adjustment.ClassName, adjustment.Methods);
            }

            return output;
        }

        private string AdjustScript(string script, string className, string[] methodNames)
        {
            string instanceName = $"{className}Instance";
            script = $"{className} {instanceName} = new {className}();{script}";
            foreach (string methodName in methodNames)
            {
                script = script.Replace($"{methodName}(", $"{instanceName}.{methodName}(");
            }

            return script;
        }
    }
}
