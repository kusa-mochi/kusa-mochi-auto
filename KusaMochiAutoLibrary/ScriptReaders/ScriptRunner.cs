using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using OpenCvSharp;

using KusaMochiAutoLibrary.Emulators;
using KusaMochiAutoLibrary.External;
using KusaMochiAutoLibrary.ImageRecognition;

namespace KusaMochiAutoLibrary.ScriptReaders
{
    /// <summary>
    /// Emulators wrapper for executing scripts.
    /// </summary>
    public class ScriptRunner
    {
        private static MouseEmulator _sMouseEmulator = new MouseEmulator();
        private static KeyboardEmulator _sKeyboardEmulator = new KeyboardEmulator();
        private static TimeEmulator _sTimeEmulator = new TimeEmulator();
        private static ImageRecognizer _sImageRecognizer = new ImageRecognizer();
        private static ProgramRunner _sProgramRunner = new ProgramRunner();

        public static void MouseMoveTo(int x, int y)
        {
            _sMouseEmulator.MouseMoveTo(x, y);
        }

        public static void Click()
        {
            _sMouseEmulator.MouseClick();
        }

        public static void Click(int x, int y)
        {
            _sMouseEmulator.MouseClick(x, y);
        }

        public static void RightClick()
        {
            _sMouseEmulator.MouseRightClick();
        }

        public static void RightClick(int x, int y)
        {
            _sMouseEmulator.MouseRightClick(x, y);
        }

        public static void MiddleClick()
        {
            _sMouseEmulator.MouseMiddleClick();
        }

        public static void MiddleClick(int x, int y)
        {
            _sMouseEmulator.MouseMiddleClick(x, y);
        }

        public static void MouseDown()
        {
            _sMouseEmulator.MouseLeftDown();
        }

        public static void MouseDown(int x, int y)
        {
            _sMouseEmulator.MouseLeftDown(x, y);
        }

        public static void MouseRightDown()
        {
            _sMouseEmulator.MouseRightDown();
        }

        public static void MouseRightDown(int x, int y)
        {
            _sMouseEmulator.MouseRightDown(x, y);
        }

        public static void MouseMiddleDown()
        {
            _sMouseEmulator.MouseMiddleDown();
        }

        public static void MouseMiddleDown(int x, int y)
        {
            _sMouseEmulator.MouseMiddleDown(x, y);
        }

        public static void MouseUp()
        {
            _sMouseEmulator.MouseLeftUp();
        }

        public static void MouseUp(int x, int y)
        {
            _sMouseEmulator.MouseLeftUp(x, y);
        }

        public static void MouseRightUp()
        {
            _sMouseEmulator.MouseRightUp();
        }

        public static void MouseRightUp(int x, int y)
        {
            _sMouseEmulator.MouseRightUp(x, y);
        }

        public static void MouseMiddleUp()
        {
            _sMouseEmulator.MouseMiddleUp();
        }

        public static void MouseMiddleUp(int x, int y)
        {
            _sMouseEmulator.MouseMiddleUp(x, y);
        }

        public static void MouseWheel(int amount)
        {
            _sMouseEmulator.MouseWheel(amount);
        }

        public static void MouseWheel(int x, int y, int amount)
        {
            _sMouseEmulator.MouseWheel(x, y, amount);
        }

        public static void KeyPress(Keys key)
        {
            _sKeyboardEmulator.KeyPress(key);
        }

        public static void KeyPress(short key)
        {
            _sKeyboardEmulator.KeyPress(key);
        }

        public static void KeyDown(Keys key)
        {
            _sKeyboardEmulator.KeyDown(key);
        }

        public static void KeyDown(short key)
        {
            _sKeyboardEmulator.KeyDown(key);
        }

        public static void KeyUp(Keys key)
        {
            _sKeyboardEmulator.KeyUp(key);
        }

        public static void KeyUp(short key)
        {
            _sKeyboardEmulator.KeyUp(key);
        }

        public static void Wait(int t)
        {
            _sTimeEmulator.Wait(t);
        }

        public static List<Point2d> GetImagePosition(string imageFilePath, double threshold = -1.0)
        {
            return _sImageRecognizer.GetImagePosition(imageFilePath, threshold);
        }

        public static void Run(string filePath, string args = null)
        {
            _sProgramRunner.Run(filePath, args);
        }
    }
}
