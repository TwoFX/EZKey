﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZKey
{
    static class LanguagePacks
    {
        public static Dictionary<int, int> enUS = new Dictionary<int, int>()
        {
            { 0x31, 0 }, // 1 Key
            // No KeyID=1 on enUS Layout
            { 0x51, 2 }, // Q Key
            { 0x41, 3 }, // A Key
            { 0x32, 4 }, // 2 Key
            { 0x5A, 5 }, // Z Key
            { 0x57, 6 }, // W Key
            { 0x53, 7 }, // S Key
            { 0x33, 8 }, // 3 Key
            { 0x58, 9 }, // X Key
            { 0x45, 10 }, // E Key
            { 0x44, 11 }, // D Key
            { 0x34, 12 }, // 4 Key
            { 0x43, 13 }, // C Key
            { 0x52, 14 }, // R Key
            { 0x46, 15 }, // F Key
            { 0x35, 16 }, // 5 Key
            { 0x56, 17 }, // V Key
            { 0x54, 18 }, // T Key
            { 0x47, 19 }, // G Key
            { 0x36, 20 }, // 6 Key
            { 0x42, 21 }, // B Key
            { 0x59, 22 }, // Y Key
            { 0x48, 23 }, // H Key
            { 0x37, 24 }, // 7 Key
            { 0x4E, 25 }, // N Key
            { 0x55, 26 }, // U Key
            { 0x4A, 27 }, // J Key
            { 0x38, 28 }, // 8 Key
            { 0x4D, 29 }, // M Key
            { 0x49, 30 }, // I Key
            { 0x4B, 31 }, // K Key
            { 0x39, 32 }, // 9 Key
            { 0xBC, 33 }, // , Key
            { 0x4F, 34 }, // O Key
            { 0x4C, 35 }, // L Key
            { 0x30, 36 }, // 0 Key
            { 0xBE, 37 }, // . Key
            { 0x50, 38 }, // P Key
            { 0xBA, 39 }, // ; Key
            { 0xBD, 40 }, // - Key
            { 0xBF, 41 }, // / Key
            { 0xDB, 42 }, // [ Key
            { 0xDE, 43 }, // ' Key
            { 0xBB, 44 }, // + Key
            // No KeyID=45 on enUS Layout
            { 0xDD, 46 }, // ] Key
            { 0xC0, 47 }, // ~ Key
            { 0x08, 48 }, // Backspace Key
            { 0x09, 49 }, // Tab Key
            { 0xDC, 50 }, // \ Key
            { 0x14, 51 }, // Caps Key
            { 0x0D, 52 }, // Enter Key
            { 0xA0, 53 }, // Left Shift Key
            { 0xA1, 54 }, // Right Shift Key
            { 0xA2, 55 }, // Left Ctrl Key
            { 0x20, 56 }, // Space Bar
            { 0xA3, 57 }, // Right Ctrl Key
            { 0x70, 58 }, // F1 Key
            { 0x71, 59 }, // F2 Key
            { 0x72, 60 }, // F3 Key
            { 0x73, 61 }, // F4 Key
            { 0x74, 62 }, // F5 Key
            { 0x75, 63 }, // F6 Key
            { 0x76, 64 }, // F7 Key
            { 0x77, 65 }, // F8 Key
            { 0x78, 66 }, // F9 Key
            { 0x79, 67 }, // F10 Key
            { 0x7A, 68 }, // F11 Key
            { 0x7B, 69 }, // F12 Key
            { 0x1B, 70 } // Escape Key
        };

        public static string[] enUSlbl = new string[] {
            "1", " ", "Q", "A", "2", "Z", "W", "S", "3", "X",
            "E", "D", "4", "C", "R", "F", "5", "V", "T", "G",
            "6", "B", "Y", "H", "7", "N", "U", "J", "8", "M",
            "I", "K", "9", ",", "O", "L", "0", ".", "P", ";",
            "-", "/", "[", "'", "+", " ", "]", "~", "Backspace", "Tab",
            "\\", "Caps", "Enter", "Shift", "Shift", "Ctrl", "Space", "Ctrl",
            "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11",
            "F12", "Esc"
        };
    }
}