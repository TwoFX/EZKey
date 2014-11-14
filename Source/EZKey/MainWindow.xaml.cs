/*
 * Copyright (C) 2013-2014 Markus Himmel
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
//using System.Drawing;
//using ColorPicker;
using System.IO;

namespace EZKey
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private int lastCode = 0;
        private Rectangle[] keyLayout;
        private Label[] lblLayout;
        private Dictionary<int, int> Layout;
        private int[] rowOrder, rowOffset;
        private string[] lbls = Manager.Lables;
        private int keyDex = 71; // Highest index of keys
        private bool lockMode = false;
        private Options options;

        public MainWindow()
        {

            keyLayout = new Rectangle[keyDex];
            lblLayout = new Label[keyDex];
            Layout = Manager.Layout;
            rowOrder = new int[] { 1, 4, 2, 3 };
            rowOffset = new int[] { 0, 0, 2, 3, 1 };
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            InterceptKeys.Load();

            Manager.KeyDown += displayKeyDown;
            Manager.KeyUp += displayKeyUp;
            Manager.OptionChanged += setOptions;

            Manager.cfgPaths = new Dictionary<int, string>();

            foreach (string file in Directory.EnumerateFiles(Manager.ThemePath, "*.ezc", SearchOption.AllDirectories))
            {
                Manager.comboBoxItems.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                Manager.cfgPaths.Add(Manager.comboBoxItems.Count - 1, file);
            }

            Manager.InitStandards();
            Manager.LoadStandards();

            for (int i = 0; i < keyDex; i++)
            {
                if (i != 1 && i != 53)
                {
                    Rectangle c = new Rectangle(); // I shall call it "The conveniance variable" (because keyLayout[i] is way too fucking long) ... Totally abusing reference types :D
                    keyLayout[i] = c;
                    c.HorizontalAlignment = HorizontalAlignment.Left;
                    c.VerticalAlignment = VerticalAlignment.Top;
                    grid.Children.Add(c);
                    Label l = new Label();
                    lblLayout[i] = l;
                    l.FontSize = Manager.FontSize * 46.0;
                    l.HorizontalAlignment = HorizontalAlignment.Left;
                    l.VerticalAlignment = VerticalAlignment.Top;
                    l.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    l.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    grid.Children.Add(l);
                }
                
            }
            setOptions();
        }

        /// <summary>
        /// Returns the height/width and margin of a key rectangle for a certain key ID.
        /// </summary>
        private Tuple<double, double, Thickness> squareKey(int keyId)
        {
            double size = Manager.Size * 120.0;
            int row = rowOrder[keyId % 4];
            int rsColun = (int)Math.Floor(keyId / 4.0);
            
            double x = Manager.BasicOffsetX * 80.0 + // Basic Offset
                rowOffset[row] * Manager.Size * 30 + // Row Specific Offset
                (keyId / 4) * (Manager.Size * 120) + // Preceding keys
                (keyId / 4) * (Manager.OffsetX * 30); // Preceding offsets

            double y = Manager.BasicOffsetY * 80.0 +
                row * Manager.OffsetY * Manager.Size * 80.0 + // Preceding offsets
                row * Manager.Size * 120 + // Preceding keys
                Manager.tOffsetY * Manager.Size * 100; // F-Keys

            return new Tuple<double, double, Thickness>(size, size, new Thickness(x, y, 0, 0));
        }

        private Tuple<double, double, Thickness> paddedKey(int keyId, bool padRight)
        {
            var baseKey = squareKey(keyId);
            double width, x;

            if (padRight)
            {
                x = baseKey.Item3.Left;
                //double rightBaseline = Manager.BasicOffsetX * 80.0 + 24 * Manager.Size * (5 + 336 * Manager.OffsetX);
                double rightBaseline = Manager.BasicOffsetX * 80.0 + 1800 * Manager.Size + 390 * Manager.OffsetX;
                width = rightBaseline - baseKey.Item3.Left;
            }
            else
            {
                x = Manager.BasicOffsetY * 80.0;
                width = baseKey.Item3.Left + baseKey.Item1 - Manager.BasicOffsetY * 80.0; 
            }

            return new Tuple<double, double, Thickness>(baseKey.Item1, width, new Thickness(x, baseKey.Item3.Top, 0, 0));
        }

        private void setOptions()
        {
            double rightBaseline = Manager.BasicOffsetX * 80.0 + 1800 * Manager.Size + 390 * Manager.OffsetX;
            for (int i = 0; i < keyDex; i++)
            {
                if (i != 1 && i != 53)
                {
                    Rectangle c = keyLayout[i]; // I shall call it "The conveniance variable" (because keyLayout[i] is way too fucking long) [once again]
                    c.Fill = new SolidColorBrush(Manager.Foreground);
                    c.StrokeThickness = Manager.BorderThickness * 4 * Manager.Size * 4;
                    c.Stroke = new SolidColorBrush(Manager.Border);
                    c.RadiusX = c.RadiusY = Manager.Roundness * 15.0 * Manager.Size * 4;
                    if (i < 55)
                    {
                        var dims =  new int[] {0, 2, 3, 5, 49, 51, 52, 54}.Contains(i) ? paddedKey(i, i > 5) : squareKey(i);
                        c.Height = dims.Item1;
                        c.Width = dims.Item2;
                        c.Margin = dims.Item3;
                    }
                    else if (i < 58)
                    {
                        switch (i)
                        {
                            case 55: // Left Ctrl Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 5 * Manager.Size * 4 + Manager.Size * 120 * 5 + Manager.tOffsetY * 25 * Manager.Size * 4, 0, 0);
                                break;
                            case 56: // Space Bar
                                c.Height = Manager.Size * 120.0;
                                c.Width = rightBaseline - (Manager.BasicOffsetY * 80 + 2 * Manager.Size * 120 + 2 * Manager.OffsetX * 30);
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + Manager.Size * 120 + Manager.OffsetX * 30, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 5 * Manager.Size * 4 + Manager.Size * 120 * 5 + Manager.tOffsetY * 25 * Manager.Size * 4, 0, 0);
                                break;
                            case 57: // Right Ctrl Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0;
                                c.Margin = new Thickness(rightBaseline - Manager.Size * 120, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 5 * Manager.Size * 4 + Manager.Size * 120 * 5 + Manager.tOffsetY * 25 * Manager.Size * 4, 0, 0);
                                break;
                        }
                    }
                    else if (i < 70) // F1 - F12
                    {
                        c.Height = c.Width = Manager.Size * 120.0;
                        
                        double x = Manager.BasicOffsetX * 80.0 + // Basic Offset
                                    ((i - 58) / 4) * (Manager.Size * 120.0 * 0.5) + // F-Key Spacing
                                    (i - 56) * (Manager.Size * 120) + // Preceding keys
                                    (i - 56) * (Manager.OffsetX * 30); // Preceding offsets
                        double y = Manager.BasicOffsetY * 80.0;
                        c.Margin = new Thickness(x, y, 0, 0);
                    }
                    else // Escape Key
                    {
                        c.Height = c.Width = Manager.Size * 120.0;
                        c.Margin = new Thickness(Manager.BasicOffsetX * 80.0, Manager.BasicOffsetY * 80.0, 0, 0);
                    }
                    Label l = lblLayout[i];
                    l.Content = lockMode && Manager.lockMaskEnabled ? Manager.lockSymbol : lbls[i];
                    l.Width = c.Width;
                    l.Height = c.Height;
                    l.FontFamily = Manager.Font;
                    l.FontWeight = Manager.FontW;
                    l.FontStyle = Manager.FontS;
                    

                    double finalFontSize = Manager.FontSize * 46.0;

                    FormattedText measurer;
                    SolidColorBrush brush = new SolidColorBrush(Manager.Text);
                    Typeface face = new Typeface(Manager.Font, Manager.FontS, Manager.FontW, FontStretches.Medium);
                    do
                    {
                        measurer = new FormattedText(l.Content.ToString(), System.Globalization.CultureInfo.CurrentUICulture,
                            System.Windows.FlowDirection.LeftToRight, face,
                            finalFontSize, brush);

                        finalFontSize -= 0.25;
                    } while ((measurer.Height > c.Height - 12 || measurer.Width > c.Width - 12) && finalFontSize > 0.25);

                    l.FontSize = finalFontSize;

                    l.Foreground = new SolidColorBrush(Manager.Text);
                    l.Margin = new Thickness(c.Margin.Left, c.Margin.Top, 0, 0);
                }
            }
            this.grid.Width = keyLayout.Max(x => x == null ? 0 : x.Margin.Left + x.Width) + 25;
            this.grid.Height = keyLayout.Max(x => x == null ? 0 : x.Margin.Top + x.Height) + 25;
            this.grid.Background = new SolidColorBrush(Manager.Background);
            lblOptions.Width = this.grid.Width;
            //lblOptions.Content = new FormattedText("Backspace", System.Globalization.CultureInfo.CurrentUICulture, System.Windows.FlowDirection.LeftToRight,
                //new Typeface(Manager.Font, Manager.FontS, Manager.FontW, FontStretches.Medium), Manager.FontSize * 46.0, new SolidColorBrush(Manager.Text)).Width - keyLayout[48].Width;
            Layout = Manager.Layout;
        }

        private void displayKeyDown(int KeyCode)
        {
            if (KeyCode == Manager.lockKey)
            {
                lockMode = !lockMode;
                setOptions();
            }
            else if (Layout.Keys.Contains(KeyCode) && !lockMode)
            {
                lblLayout[Layout[KeyCode]].Foreground = new SolidColorBrush(Manager.TextPressed);
                keyLayout[Layout[KeyCode]].Fill = new SolidColorBrush(Manager.ForegroundPressed);
                keyLayout[Layout[KeyCode]].Stroke = new SolidColorBrush(Manager.BorderPressed);
            }
        }

        private void displayKeyUp(int KeyCode)
        {
            if (Layout.Keys.Contains(KeyCode) && !lockMode)
            {
                lblLayout[Layout[KeyCode]].Foreground = new SolidColorBrush(Manager.Text);
                keyLayout[Layout[KeyCode]].Fill = new SolidColorBrush(Manager.Foreground);
                keyLayout[Layout[KeyCode]].Stroke = new SolidColorBrush(Manager.Border);
            }

        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                if (options == null)
                {
                    options = new Options();
                    options.Closed += (__, _) => options = null;
                }
                options.Show();
            }
        }

        private void grid_MouseEnter(object sender, MouseEventArgs e)
        {
            lblOptions.Foreground = new SolidColorBrush(Color.FromRgb((byte)(byte.MaxValue - Manager.Background.R),
                (byte)(byte.MaxValue - Manager.Background.G),
                (byte)(byte.MaxValue - Manager.Background.B)));
            lblOptions.Visibility = Visibility.Visible;
        }

        private void grid_MouseLeave(object sender, MouseEventArgs e)
        {
            lblOptions.Visibility = Visibility.Hidden;
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (options == null)
            {
                options = new Options();
                options.Closed += (__, _) => options = null;
            }
            options.Show();
        }

        private void lblLayout_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void lblLoad_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void lblSave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Microsoft.Win32.SaveFileDialog sfg = new Microsoft.Win32.SaveFileDialog();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            InterceptKeys.UnLoad();
            Application.Current.Shutdown();
        }
    }
}