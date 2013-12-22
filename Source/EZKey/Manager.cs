/*
 * Copyright (C) 2013 Markus Himmel
 * 
 * This file is part of EZKey Keyboard Visualization.
 * 
 * EZKey is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * EZKey is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with EZKey.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Threading.Tasks;
using System.Reflection;

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
        public static FontFamily Font;
        public static FontWeight FontW;
        public static FontStyle FontS;

        public static Color dBackground, dForeground, dForegroundPressed, dText, dTextPressed, dBorder, dBorderPressed;
        public static double dBorderThickness, dRoundness, dSize, dOffsetX, dOffsetY, dBasicOffsetX, dBasicOffsetY, dtOffsetY, dFontSize; // Is Roundness a word? Whatever..
        public static bool dlockMaskEnabled;
        public static int dlockKey;
        public static string dlockSymbol;
        public static Dictionary<int, int> dLayout = LanguagePacks.enUS;
        public static string[] dLables = LanguagePacks.enUSlbl;
        public static FontFamily dFont;
        public static FontWeight dFontW;
        public static FontStyle dFontS;

        public static string ThemePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + @"\Themes\";
        public static int currentTheme = 0;
        public static List<string> comboBoxItems = new List<string>() { "<default>" };
        public static Dictionary<int, string> cfgPaths;


        public static void InitStandards()
        {
            Manager.dBackground = (Color)ColorConverter.ConvertFromString("#FFDDDDDD");
            Manager.dForeground = Colors.White;
            Manager.dForegroundPressed = Colors.Orange;
            Manager.dBorder = Colors.Black;
            Manager.dBorderPressed = Colors.Black;
            Manager.dText = Colors.Black;
            Manager.dTextPressed = Colors.Black;

            Manager.dlockMaskEnabled = false;
            Manager.dlockKey = 0x78;
            Manager.dlockSymbol = "?";

            Manager.dFont = new FontFamily("Segoe UI");
            Manager.dFontW = FontWeights.Bold;
            Manager.dFontS = FontStyles.Normal;

            Manager.dSize =
                Manager.dRoundness =
                Manager.dBorderThickness =
                Manager.dBasicOffsetX =
                Manager.dOffsetX =
                Manager.dBasicOffsetY =
                Manager.dOffsetY =
                Manager.dtOffsetY =
                Manager.dFontSize =
                0.25; 
        }

        public static void LoadStandards()
        {
            foreach (FieldInfo field in typeof(Manager).GetFields())
            {
                if (typeof(Manager).GetFields().Any(x => x.Name == "d" + field.Name))
                {
                    field.SetValue(null, typeof(Manager).GetField("d" + field.Name).GetValue(null));
                }
            }
        }

        public static void SetField(string name, object value)
        {
            typeof(Manager).GetField(name).SetValue(null, value);
            TriggerOptionChanged();
        }

        public static object GetField(string name)
        {
            return typeof(Manager).GetField(name).GetValue(null);
        }

        public static void ApplyConfig(IEnumerable<string[]> values)
        {
            LoadStandards();
            foreach (string[] fieldS in values)
            {
                FieldInfo field = typeof(Manager).GetField(fieldS[0]);
                if (field.FieldType == typeof(string))
                    field.SetValue(null, fieldS[1]);
                else if (field.FieldType == typeof(double))
                    field.SetValue(null, double.Parse(fieldS[1], new System.Globalization.CultureInfo("en-US")));
                else if (field.FieldType == typeof(Color))
                    field.SetValue(null, ColorConverter.ConvertFromString(fieldS[1]));
                else if (field.FieldType == typeof(bool))
                    field.SetValue(null, fieldS[1] == "#t");
                else if (field.FieldType == typeof(FontFamily))
                    field.SetValue(null, new FontFamily(fieldS[1]));
                else if (field.FieldType == typeof(FontWeight))
                    field.SetValue(null, fieldS[1] == "#t" ? FontWeights.Bold : FontWeights.Normal);
                else if (field.FieldType == typeof(FontStyle))
                    field.SetValue(null, fieldS[1] == "#t" ? FontStyles.Italic : FontStyles.Normal);
                else if (field.FieldType == typeof(int))
                    field.SetValue(null, int.Parse(fieldS[1]));
            }
            TriggerOptionChanged();
        }

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