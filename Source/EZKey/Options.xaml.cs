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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using FilePath = System.IO.Path;

namespace EZKey
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {

        private bool initialized = false;


        public Options()
        {
            commonInit();
            initialized = true;
        }

        public Options(double left, double top)
        {
            commonInit();
            this.Left = left;
            this.Top = top;
            initialized = true;
        }

        private void commonInit()
        {
            InitializeComponent();

            // Set Sliders/Boxes to current values
            sldrRoundness.Value = Manager.Roundness;
            sldrSize.Value = Manager.Size;
            sldrFontSize.Value = Manager.FontSize;
            sldrStrokeThickness.Value = Manager.BorderThickness;
            sldrOffsetX.Value = Manager.OffsetX;
            sldrOffsetY.Value = Manager.OffsetY;
            sldrFOffset.Value = Manager.tOffsetY;
            tbBackground.Text = Manager.Background.ToString();
            tbForeground.Text = Manager.Foreground.ToString();
            tbFGPressed.Text = Manager.ForegroundPressed.ToString();
            tbStrokeClr.Text = Manager.Border.ToString();
            tbStrokeKeyStroke.Text = Manager.BorderPressed.ToString();
            lblMaskKey.Content = Manager.Lables[Manager.Layout[Manager.lockKey]];
            tbFont.Text = Manager.Text.ToString();
            tbFontPressed.Text = Manager.TextPressed.ToString();
            cbMask.IsChecked = Manager.lockMaskEnabled;
            cbItalic.IsChecked = Manager.FontS == FontStyles.Italic;
            cbBold.IsChecked = Manager.FontW == FontWeights.Bold;
            tbFontFamily.Text = Manager.Font.ToString();

            // Button Colors
            btnStrokeKeyStroke.Background = new SolidColorBrush(Manager.BorderPressed);
            btnBackground.Background = new SolidColorBrush(Manager.Background);
            btnForeground.Background = new SolidColorBrush(Manager.Foreground);
            btnFgKs.Background = new SolidColorBrush(Manager.ForegroundPressed);
            btnStroke.Background = new SolidColorBrush(Manager.Border);
            btnFont.Background = new SolidColorBrush(Manager.Text);
            btnFontPressed.Background = new SolidColorBrush(Manager.TextPressed);

            // Fill the combobox
            foreach (string entry in Manager.comboBoxItems)
            {
                cbConfigs.Items.Add(entry);
            }
            cbConfigs.SelectedIndex = Manager.currentTheme;
        }



        private System.Windows.Media.Color DToM(System.Drawing.Color input)
        {
            return System.Windows.Media.Color.FromArgb(input.A, input.R, input.G, input.B);
        }

        private System.Drawing.Color MToD(System.Windows.Media.Color input)
        {
            return System.Drawing.Color.FromArgb(input.A, input.R, input.G, input.B);
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized)
            {
                Manager.Roundness = e.NewValue;
                Manager.TriggerOptionChanged();
            }
        }

        private void sldrSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized)
            {
                Manager.Size = e.NewValue;
                Manager.TriggerOptionChanged();
            }
        }

        private void sldrFontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized)
            {
                Manager.FontSize = e.NewValue;
                Manager.TriggerOptionChanged();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized)
            {
                Manager.BorderThickness = e.NewValue;
                Manager.TriggerOptionChanged();
            }
        }

        private void sldrOffsetX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized)
            {
                Manager.OffsetX = e.NewValue;
                Manager.TriggerOptionChanged();
            }
        }

        private void sldrOffsetY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized)
            {
                Manager.OffsetY = e.NewValue;
                Manager.TriggerOptionChanged();
            }
        }

        private void sldrFOffset_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized)
            {
                Manager.tOffsetY = e.NewValue;
                Manager.TriggerOptionChanged();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (initialized)
            {
                Color? clr = ConvertColor(tbBackground.Text);
                if (clr != null)
                {
                    Manager.Background = (Color)clr;
                    btnBackground.Background = new SolidColorBrush((Color)clr);
                    Manager.TriggerOptionChanged();
                }
            }
        }

        private Color? ConvertColor(string i)
        {
            if (Regex.IsMatch(i, "^#?([0-9a-fA-F]{3,8})$"))
            {
                int r = i.Length - i.Count(x => x == '#');
                string c = i.ToCharArray().Contains('#') ? i : "#" + i;
                if (r == 3 || r == 4 || r == 6 || r == 8)
                {
                    return (Color?)ColorConverter.ConvertFromString(c);
                }
            }
            return null;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (initialized)
            {
                Color? clr = ConvertColor(tbForeground.Text);
                if (clr != null)
                {
                    Manager.Foreground = (Color)clr;
                    btnForeground.Background = new SolidColorBrush((Color)clr);
                    Manager.TriggerOptionChanged();
                }
            }
        }

        private void tbFGPressed_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (initialized)
            {
                Color? clr = ConvertColor(tbFGPressed.Text);
                if (clr != null)
                {
                    Manager.ForegroundPressed = (Color)clr;
                    btnFgKs.Background = new SolidColorBrush((Color)clr);
                    Manager.TriggerOptionChanged();
                }
            }
        }

        private void tbStrokeClr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (initialized)
            {
                Color? clr = ConvertColor(tbStrokeClr.Text);
                if (clr != null)
                {
                    Manager.Border = (Color)clr;
                    btnStroke.Background = new SolidColorBrush((Color)clr);
                    Manager.TriggerOptionChanged();
                }
            }
        }

        private void cbMask_Checked(object sender, RoutedEventArgs e)
        {
            Manager.lockMaskEnabled = true;
            tbMask.IsEnabled = true;
        }

        private void cbMask_Unchecked(object sender, RoutedEventArgs e)
        {
            Manager.lockMaskEnabled = false;
            tbMask.IsEnabled = false;
        }

        private void tbMask_TextChanged(object sender, TextChangedEventArgs e)
        {
            Manager.lockSymbol = tbMask.Text;
        }

        private void btnSetLockKey_Click(object sender, RoutedEventArgs e)
        {
            Manager.KeyDown += setHotkey;
            btnSetLockKey.Content = "Press a button";
        }

        private void setHotkey(int keyCode)
        {
            Manager.KeyDown -= setHotkey;
            Manager.lockKey = keyCode;
            lblMaskKey.Content = Manager.Lables[Manager.Layout[Manager.lockKey]];
            btnSetLockKey.Content = "Set";
        }

        Color getColor(Color preC)
        {
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            cd.Color = MToD(preC);
            cd.FullOpen = true;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            return DToM(cd.Color);
        }

        private void btnBackground_Click(object sender, RoutedEventArgs e)
        {
            Color clr = getColor(Manager.Background);
            Manager.Background = clr;
            btnBackground.Background = new SolidColorBrush(clr);
            tbBackground.Text = clr.ToString();
        }
        private void btnForeground_Click(object sender, RoutedEventArgs e)
        {
            Color clr = getColor(Manager.Foreground);
            Manager.Foreground = clr;
            btnForeground.Background = new SolidColorBrush(clr);
            tbForeground.Text = clr.ToString();
        }

        private void btnFgKs_Click(object sender, RoutedEventArgs e)
        {
            Color clr = getColor(Manager.ForegroundPressed);
            Manager.ForegroundPressed = clr;
            btnFgKs.Background = new SolidColorBrush(clr);
            tbFGPressed.Text = clr.ToString();
        }

        private void btnStroke_Click(object sender, RoutedEventArgs e)
        {
            Color clr = getColor(Manager.Border);
            Manager.Border = clr;
            btnStroke.Background = new SolidColorBrush(clr);
            tbStrokeClr.Text = clr.ToString();
        }

        private void tbStrokeKeyStroke_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (initialized)
            {
                Color? clr = ConvertColor(tbStrokeKeyStroke.Text);
                if (clr != null)
                {
                    Manager.BorderPressed = (Color)clr;
                    btnStrokeKeyStroke.Background = new SolidColorBrush((Color)clr);
                    Manager.TriggerOptionChanged();
                }
            }
        }

        private void btnStrokeKeyStroke_Click(object sender, RoutedEventArgs e)
        {
            Color clr = getColor(Manager.BorderPressed);
            Manager.BorderPressed = clr;
            btnStrokeKeyStroke.Background = new SolidColorBrush(clr);
            tbStrokeKeyStroke.Text = clr.ToString();
        }

        private void tbFont_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (initialized)
            {
                Color? clr = ConvertColor(tbFont.Text);
                if (clr != null)
                {
                    Manager.Text = (Color)clr;
                    btnFont.Background = new SolidColorBrush((Color)clr);
                    Manager.TriggerOptionChanged();
                }
            }
        }

        private void btnFont_Click(object sender, RoutedEventArgs e)
        {
            Color clr = getColor(Manager.Text);
            Manager.Text = clr;
            btnFont.Background = new SolidColorBrush(clr);
            tbFont.Text = clr.ToString();
        }

        private void tbFontPressed_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (initialized)
            {
                Color? clr = ConvertColor(tbFontPressed.Text);
                if (clr != null)
                {
                    Manager.TextPressed = (Color)clr;
                    btnFontPressed.Background = new SolidColorBrush((Color)clr);
                    Manager.TriggerOptionChanged();
                }
            }
        }

        private void btnFontPressed_Click(object sender, RoutedEventArgs e)
        {
            Color clr = getColor(Manager.TextPressed);
            Manager.TextPressed = clr;
            btnFontPressed.Background = new SolidColorBrush(clr);
            tbFontPressed.Text = clr.ToString();
        }

        private void btnFontFamily_Click(object sender, RoutedEventArgs e)
        {
            Manager.Font = new FontFamily(tbFontFamily.Text);
            Manager.TriggerOptionChanged();
        }

        private void cbBold_Checked(object sender, RoutedEventArgs e)
        {
            Manager.FontW = FontWeights.Bold;
            Manager.TriggerOptionChanged();
        }

        private void cbBold_Unchecked(object sender, RoutedEventArgs e)
        {
            Manager.FontW = FontWeights.Normal;
            Manager.TriggerOptionChanged();
        }

        private void cbItalic_Checked(object sender, RoutedEventArgs e)
        {
            Manager.FontS = FontStyles.Italic;
            Manager.TriggerOptionChanged();
        }

        private void cbItalic_Unchecked(object sender, RoutedEventArgs e)
        {
            Manager.FontS = FontStyles.Normal;
            Manager.TriggerOptionChanged();
        }

        private void btnLoadConf_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "EZKey Config Files|*.ezc|All Files|*.*";
            ofd.InitialDirectory = Manager.ThemePath;
            if (ofd.ShowDialog() == true)
            {
                if (Manager.cfgPaths.Values.Contains(ofd.FileName)) // Did the user load a file that we already know?
                {
                    findConfigItemAndSelect(ofd.FileName);
                }
                else
                {
                    addConfigItemAndSelect(ofd.FileName);
                }
            }
        }

        private void btnSaveConf_Click(object sender, RoutedEventArgs e) // = Save AS
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "EZKey Config Files|*.ezc|All Files|*.*";
            sfd.InitialDirectory = Manager.ThemePath;
            if (sfd.ShowDialog() == true)
            {
                saveToLocation(sfd.FileName);
            }
        }

        private void saveToLocation(string path)
        {
            saveConfigFile(path);
            if (Manager.cfgPaths.Values.Contains(path)) // Did the user save to a file that we already know?
            {
                findConfigItemAndSelect(path);
            }
            else
            {
                addConfigItemAndSelect(path);
            }
        }

        private void findConfigItemAndSelect(string path)
        {
            int foundIndex = Manager.cfgPaths.FirstOrDefault(x => x.Value == path).Key;
            cbConfigs.SelectedIndex = foundIndex;
            Manager.currentTheme = foundIndex;
        }

        private void addConfigItemAndSelect(string path)
        {
            int newIndex = addConfigItem(path);
            Manager.currentTheme = newIndex;
            cbConfigs.SelectedIndex = newIndex;
        }

        private int addConfigItem(string path)
        {
            string displayName = System.IO.Path.GetFileNameWithoutExtension(path);
            int displayIndex = Manager.comboBoxItems.Count;

            Manager.cfgPaths.Add(displayIndex, path);

            Manager.comboBoxItems.Add(displayName);
            cbConfigs.Items.Add(displayName);
            cbConfigs.SelectedIndex = displayIndex;
            

            return displayIndex;
        }

        private void saveConfigFile(string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName);
            foreach (System.Reflection.FieldInfo field in typeof(Manager).GetFields())
            {
                if (typeof(Manager).GetFields().Any(x => x.Name == "d" + field.Name) &&
                    !(typeof(Manager).GetField("d" + field.Name).GetValue(null).Equals(field.GetValue(null))))
                {
                    string l = field.Name + " ";
                    if (field.FieldType == typeof(string) ||
                        field.FieldType == typeof(Color) ||
                        field.FieldType == typeof(FontFamily) ||
                        field.FieldType == typeof(int))
                        l += field.GetValue(null).ToString();
                    else if (field.FieldType == typeof(bool))
                        l += (bool)field.GetValue(null) ? "#t" : "#f";
                    else if (field.FieldType == typeof(FontWeight))
                        l += (FontWeight)field.GetValue(null) == FontWeights.Bold ? "#t" : "#f";
                    else if (field.FieldType == typeof(FontStyle))
                        l += (FontStyle)field.GetValue(null) == FontStyles.Italic ? "#t" : "#f";
                    else if (field.FieldType == typeof(double))
                    {
                        double m = (double)field.GetValue(null);
                        l += m.ToString(new System.Globalization.CultureInfo("en-US"));
                    }
                    else continue; // Should never be the case anymore
                    sw.WriteLine(l);
                }
            }
            sw.Close();
        }

        private void parseConfigFile(string fileName)
        {
            Manager.ApplyConfig(
                    File.ReadAllLines(fileName)
                    .Select<string, string[]>(x => x
                    .Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries)));
        }

        private void cbConfigs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbConfigs.SelectedIndex == 0)
            {
                Manager.LoadStandards();
            }
            else
            {
                parseConfigFile(Manager.cfgPaths[cbConfigs.SelectedIndex]);
            }
            Manager.currentTheme = cbConfigs.SelectedIndex;
            if (this.IsLoaded)
            {
                new Options(this.Left, this.Top).Show();
                this.Close();
            }
        }

        private void btnsaveSelec_Click(object sender, RoutedEventArgs e) // = Save to selected config
        {
            saveToLocation(Manager.cfgPaths[Manager.currentTheme]);
        }
    }
}