using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;

namespace EZKey
{
    static class Manager
    {
        public delegate void KeyHandler(int KeyCode);
        public static KeyHandler KeyUp, KeyDown;
        public static Color Background, Foreground, ForegroundPressed, Text, TextPressed, Border, BorderPressed;
        public static double BorderThickness, Roundness; // Is Roundness a word? Whatever..
        public static int Size, OffsetX, OffsetY, BasicOffsetX, BasicOffsetY;

        public static void TriggerKeyDown(int KeyCode)
        {
            if (KeyDown != null)
            {
                KeyDown(KeyCode);
            }
        }

        public static void TriggerKeyUp(int KeyCode)
        {
            if (KeyUp != null)
            {
                KeyUp(KeyCode);
            }
        }
    }
}
