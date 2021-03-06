﻿/*
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
        private string[] lbls = Manager.Lables;
        private int keyDex = 71; // Highest index of keys
        private bool lockMode = false;

        public MainWindow()
        {

            keyLayout = new Rectangle[keyDex];
            lblLayout = new Label[keyDex];
            Layout = Manager.Layout;
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
                if (i != 1 && i != 45)
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

        private void setOptions()
        {
            for (int i = 0; i < keyDex; i++)
            {
                if (i != 1 && i != 45)
                {
                    Rectangle c = keyLayout[i]; // I shall call it "The conveniance variable" (because keyLayout[i] is way too fucking long) [once again]
                    c.Fill = new SolidColorBrush(Manager.Foreground);
                    c.StrokeThickness = Manager.BorderThickness * 4 * Manager.Size * 4;
                    c.Stroke = new SolidColorBrush(Manager.Border);
                    c.RadiusX = c.RadiusY = Manager.Roundness * 15.0 * Manager.Size * 4;
                    if (i < 47)
                    {
                        c.Height = c.Width = Manager.Size * 120.0;
                        int o;
                        switch (i % 4)
                        {
                            case 0:
                                o = 1;
                                break;
                            case 1:
                                o = 4;
                                break;
                            default:
                                o = i % 4;
                                break;
                        }
                        c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + (i + 4) * Manager.OffsetX * 36.0 * Manager.Size * 4, Manager.BasicOffsetY * 80.0 + o * Manager.OffsetY * 20.0 * Manager.Size * 4 + o * Manager.Size * 120.0 + Manager.tOffsetY * 25 * Manager.Size * 4 , 0, 0);
                    }
                    else if (i < 58)
                    {
                        switch (i)
                        {
                            case 47: // ~ Key
                                c.Height = c.Width = Manager.Size * 120.0;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * Manager.Size * 4 + Manager.Size * 120 + Manager.tOffsetY * 25 * Manager.Size * 4 , 0, 0);
                                break;
                            case 48: // Backspace Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120 + Manager.OffsetX * 36.0 * 4 * Manager.Size * 4;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + (i + 4) * Manager.OffsetX * 36.0 * Manager.Size * 4, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * Manager.Size * 4 + Manager.Size * 120 + Manager.tOffsetY * 25 * Manager.Size * 4, 0, 0);
                                break;
                            case 49: // Tab Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 2 * Manager.Size * 4;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 2 * Manager.Size * 4 + Manager.Size * 120 * 2 + Manager.tOffsetY * 25 * Manager.Size * 4 , 0, 0);
                                break;
                            case 50: // \ Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 2 * Manager.Size * 4;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + (i + 4) * Manager.OffsetX * 36.0 * Manager.Size * 4, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 2 * Manager.Size * 4 + Manager.Size * 120 * 2 + Manager.tOffsetY * 25 * Manager.Size * 4 , 0, 0);
                                break;
                            case 51: // Caps Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 3 * Manager.Size * 4;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 3 * Manager.Size * 4 + Manager.Size * 120 * 3 + Manager.tOffsetY * 25 * Manager.Size * 4 , 0, 0);
                                break;
                            case 52: // Enter Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 5 * Manager.Size * 4;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + (i - 1) * Manager.OffsetX * 36.0 * Manager.Size * 4, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 3 * Manager.Size * 4 + Manager.Size * 120 * 3 + Manager.tOffsetY * 25 * Manager.Size * 4 , 0, 0);
                                break;
                            case 53: // Left Shift Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 5 * Manager.Size * 4;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 4 * Manager.Size * 4 + Manager.Size * 120 * 4 + Manager.tOffsetY * 25 * Manager.Size * 4 , 0, 0);
                                break;
                            case 54: // Right Shift Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 7 * Manager.Size * 4;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + (i - 5) * Manager.OffsetX * 36.0 * Manager.Size * 4, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 4 * Manager.Size * 4 + Manager.Size * 120 * 4 + Manager.tOffsetY * 25 * Manager.Size * 4 , 0, 0);
                                break;
                            case 55: // Left Ctrl Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 2 * Manager.Size * 4;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 5 * Manager.Size * 4 + Manager.Size * 120 * 5 + Manager.tOffsetY * 25 * Manager.Size * 4 , 0, 0);
                                break;
                            case 56: // Space Bar
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 44 * Manager.Size * 4;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + 6 * Manager.OffsetX * 36.0 * Manager.Size * 4, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 5 * Manager.Size * 4 + Manager.Size * 120 * 5 + Manager.tOffsetY * 25 * Manager.Size * 4 , 0, 0);
                                break;
                            case 57: // Right Ctrl Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 2 * Manager.Size * 4;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + (i - 3) * Manager.OffsetX * 36.0 * Manager.Size * 4, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0 * 5 * Manager.Size * 4 + Manager.Size * 120 * 5 + Manager.tOffsetY * 25 * Manager.Size * 4 , 0, 0);
                                break;
                        }
                    }
                    else if (i < 70) // F1 - F12
                    {
                        c.Height = c.Width = Manager.Size * 120.0;
                        c.Margin = new Thickness(Manager.BasicOffsetY * 80.0 + (8 + i * 4 - 232 + Math.Floor((i - 58) / 4.0) * 2) * Manager.OffsetX * 36.0 * Manager.Size * 4, Manager.BasicOffsetY * 80.0, 0, 0);
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
                    l.FontSize = Manager.FontSize * 46.0;

                    FormattedText measurer;
                    do
                    {
                        measurer = new FormattedText(l.Content.ToString(), System.Globalization.CultureInfo.CurrentUICulture,
                            System.Windows.FlowDirection.LeftToRight, new Typeface(Manager.Font, Manager.FontS, Manager.FontW, FontStretches.Medium),
                            l.FontSize, new SolidColorBrush(Manager.Text));

                        l.FontSize -= 0.01;
                    } while (measurer.Height > c.Height - 12 || measurer.Width > c.Width - 12);

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
                new Options().Show();
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
            new Options().Show();
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