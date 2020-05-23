using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using KusaMochiAutoLibrary.ScriptReaders;

namespace KusaMochiAutoScriptRunner
{
    public class Program
    {
        static void Main(string[] args)
        {
            //if (args.Length != 2)
            //{
            //    throw new InvalidOperationException();
            //}

            //Program p = new Program();
            //p.Start(args[1]).Wait();


            Thread.Sleep(1000);
            MessageBox.Show("あばばばぼぼぼ");
        }

        private async Task<bool> Start(string filePath)
        {
            Debug.Print("begin the sub process to run a script.");
            bool output = false;

            ScriptReader reader = new ScriptReader();
            using (StreamReader sr = new StreamReader(filePath))
            {
                output = await reader.ExecuteScript(sr.ReadToEnd());
            }

            return output;
        }
    }
}
