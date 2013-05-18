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
        public delegate void OptionChangedHandler();
        public static OptionChangedHandler OptionChanged;
        public static KeyHandler KeyUp, KeyDown;
        public static Color Background, Foreground, ForegroundPressed, Text, TextPressed, Border, BorderPressed;
        public static double BorderThickness, Roundness, Size, OffsetX, OffsetY, BasicOffsetX, BasicOffsetY, tOffsetY, FontSize; // Is Roundness a word? Whatever..
        public static bool lockMaskEnabled;
        public static int lockKey;
        public static string lockSymbol;
        public static Dictionary<int, int> Layout = LanguagePacks.enUS;
        public static string[] Lables = LanguagePacks.enUSlbl;

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

        public static void TriggerOptionChanged()
        {
            if (OptionChanged != null)
            {
                OptionChanged();
            }
        }
    }
}