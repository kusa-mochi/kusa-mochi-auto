using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using KusaMochiAutoLibrary.NativeFunctions;
using KusaMochiAutoLibrary.EventArgs;

namespace KusaMochiAutoLibrary.Recorders
{
    public class CSharpScriptGenerator : IScriptGenerator
    {
        public void MouseMove(int x, int y)
        {
            _script += $"MouseMoveTo({x},{y});\n";
        }

        public void MouseLeftDown(int x, int y)
        {
            _script += $"MouseLeftDown({x},{y});\n";
        }

        public void MouseRightDown(int x, int y)
        {
            _script += $"MouseRightDown({x},{y});\n";
        }

        public void MouseLeftUp(int x, int y)
        {
            _script += $"MouseLeftUp({x},{y});\n";
        }

        public void MouseRightUp(int x, int y)
        {
            _script += $"MouseRightUp({x},{y});\n";
        }

        public void MouseWheel(int x, int y, int amount)
        {
            _script += $"MouseWheel({x},{y},{amount});\n";
        }

        public void MouseMiddleDown(int x, int y)
        {
            _script += $"MouseMiddleDown({x},{y});\n";
        }

        public void MouseMiddleUp(int x, int y)
        {
            _script += $"MouseMiddleUp({x},{y});\n";
        }

        public void KeyDown(Keys key)
        {
            _script += $"KeyDown({(int)key});\n";
        }

        public void KeyUp(Keys key)
        {
            _script += $"KeyUp({(int)key});\n";
        }

        public void SystemKeyDown(Keys key)
        {
            _script += $"SystemKeyDown({(int)key});\n";
        }

        public void SystemKeyUp(Keys key)
        {
            _script += $"SystemKeyUp({(int)key});\n";
        }

        public void Reset()
        {
            _script = "";
        }

        public string GetScript()
        {
            return _script;
        }

        private string _script = "";
    }
}
