using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using OpenCvSharp;

namespace KusaMochiAutoLibrary.ScriptReaders
{
    public class ScriptReader
    {
        public ScriptReader()
        {
        }

        public async Task<bool> ExecuteScript(string script)
        {
            if (string.IsNullOrEmpty(script)) throw new ArgumentNullException("script");

            try
            {
                var result = await CSharpScript.RunAsync(
                    script,
                    ScriptOptions.Default
                        .WithImports(
                            "System",
                            "System.Collections.Generic",
                            "System.Windows",
                            "OpenCvSharp",
                            "KusaMochiAutoLibrary.ScriptReaders.ScriptRunner"
                            )
                        .WithReferences(
                            Assembly.GetAssembly(typeof(Point2d)),
                            Assembly.GetAssembly(typeof(List<Point2d>)),
                            Assembly.GetAssembly(typeof(System.Windows.Application)),
                            Assembly.GetAssembly(typeof(KusaMochiAutoLibrary.Emulators.MouseEmulator))
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
    }
}
