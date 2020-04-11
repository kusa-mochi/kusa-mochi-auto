using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using KusaMochiAutoLibrary.EventArgs;
using KusaMochiAutoLibrary.NativeFunctions;

namespace KusaMochiAutoLibrary.Recorders
{
    public interface IScriptGenerator
    {
        public void MouseMove(int x, int y);
        public void MouseLeftDown(int x, int y);
        public void MouseRightDown(int x, int y);
        public void MouseLeftUp(int x, int y);
        public void MouseRightUp(int x, int y);
        public void MouseWheel(int x, int y, int amount);
        public void MouseMiddleDown(int x, int y);
        public void MouseMiddleUp(int x, int y);
        public void KeyDown(Keys key);
        public void KeyUp(Keys key);
        public void SystemKeyDown(Keys key);
        public void SystemKeyUp(Keys key);
        public void Reset();
        public string GetScript();
    }
}
