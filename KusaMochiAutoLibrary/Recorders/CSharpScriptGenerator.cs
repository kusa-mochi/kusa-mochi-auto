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
            CheckTotalWait();
            _script += $"MouseMoveTo({x},{y});\n";
        }

        public void MouseLeftDown(int x, int y)
        {
            CheckTotalWait();
            _script += $"MouseDown({x},{y});\n";
        }

        public void MouseRightDown(int x, int y)
        {
            CheckTotalWait();
            _script += $"MouseRightDown({x},{y});\n";
        }

        public void MouseLeftUp(int x, int y)
        {
            CheckTotalWait();
            _script += $"MouseUp({x},{y});\n";
        }

        public void MouseRightUp(int x, int y)
        {
            CheckTotalWait();
            _script += $"MouseRightUp({x},{y});\n";
        }

        public void MouseWheel(int x, int y, int amount)
        {
            CheckTotalWait();
            _script += $"MouseWheel({x},{y},{amount});\n";
        }

        public void MouseMiddleDown(int x, int y)
        {
            CheckTotalWait();
            _script += $"MouseMiddleDown({x},{y});\n";
        }

        public void MouseMiddleUp(int x, int y)
        {
            CheckTotalWait();
            _script += $"MouseMiddleUp({x},{y});\n";
        }

        public void KeyDown(Keys key)
        {
            CheckTotalWait();
            _script += $"KeyDown({(int)key});\n";
        }

        public void KeyUp(Keys key)
        {
            CheckTotalWait();
            _script += $"KeyUp({(int)key});\n";
        }

        public void SystemKeyDown(Keys key)
        {
            CheckTotalWait();
            _script += $"SystemKeyDown({(int)key});\n";
        }

        public void SystemKeyUp(Keys key)
        {
            CheckTotalWait();
            _script += $"SystemKeyUp({(int)key});\n";
        }

        public void Wait(int t)
        {
            _currentWait += t;
            _lastIsWait = true;
        }

        public void Reset()
        {
            _script = "";
        }

        public string GetScript()
        {
            return _script;
        }

        private void CheckTotalWait()
        {
            _lastIsWait = false;
            if (_currentWait > 0)
            {
                _script += $"Wait({_currentWait});\n";
                _currentWait = 0;
            }
        }

        private string _script = "";
        private int _currentWait = 0;
        private bool _lastIsWait = false;
    }
}
