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

namespace EZKey
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private int lastCode = 0;
        private Rectangle[] keyLayout;
        private Dictionary<int, int> Layout;
        private int keyDex = 55;

        public MainWindow()
        {

            keyLayout = new Rectangle[keyDex];
            Layout = Manager.Layout;
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            InterceptKeys.Load();

            Manager.KeyDown += displayKeyDown;
            Manager.KeyUp += displayKeyUp;
            Manager.OptionChanged += setOptions;

            Manager.Background = Colors.White;
            Manager.Foreground = Colors.White;
            Manager.ForegroundPressed = Colors.Orange;
            Manager.Border = Colors.Black;
            Manager.BorderPressed = Colors.Black;
            Manager.Text = Colors.Black;
            Manager.TextPressed = Colors.White;

            Manager.Size = 
                Manager.Roundness = 
                Manager.BorderThickness = 
                Manager.BasicOffsetX = 
                Manager.OffsetX = 
                Manager.BasicOffsetY = 
                Manager.OffsetY = 
                0.25; 

            for (int i = 0; i < keyDex; i++)
            {
                if (i != 1 && i != 45)
                {
                    Rectangle c = new Rectangle(); // I shall call it "The conveniance variable" (because keyLayout[i] is way too fucking long) ... Totally abusing reference types :D
                    keyLayout[i] = c;
                    c.HorizontalAlignment = HorizontalAlignment.Left;
                    c.VerticalAlignment = VerticalAlignment.Top;
                    grid.Children.Add(c);
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
                    c.StrokeThickness = Manager.BorderThickness * 2.8;
                    c.Stroke = new SolidColorBrush(Manager.Border);
                    c.RadiusX = c.RadiusY = Manager.Roundness * 15.0;
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
                        c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + (i + 4) * Manager.OffsetX * 36.0, Manager.BasicOffsetY * 80.0 + o * Manager.OffsetY * 20.0 + (o - 1) * Manager.Size * 120.0, 0, 0);
                    }
                    else
                    {
                        switch (i)
                        {
                            case 47: // ~ Key
                                c.Height = c.Width = Manager.Size * 120.0;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0, 0, 0);
                                break;
                            case 48: // Backspace Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 * 2 + Manager.OffsetX * 36.0;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + (i + 4) * Manager.OffsetX * 36.0, Manager.BasicOffsetY * 80.0 + Manager.OffsetY * 20.0, 0, 0);
                                break;
                            case 49: // Tab Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 2;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0, Manager.BasicOffsetY * 80.0 + Manager.BasicOffsetY * 20.0 * 2 + Manager.Size * 120, 0, 0);
                                break;
                            case 50: // \ Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 2 + 3;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + (i + 4) * Manager.OffsetX * 36.0, Manager.BasicOffsetY * 80.0 + Manager.BasicOffsetY * 20.0 * 2 + Manager.Size * 120, 0, 0);
                                break;
                            case 51: // Caps Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 3;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0, Manager.BasicOffsetY * 80.0 + Manager.BasicOffsetY * 20.0 * 3 + Manager.Size * 120 * 2, 0, 0);
                                break;
                            case 52: // Enter Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 5 + 3;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + (i - 1) * Manager.OffsetX * 36.0, Manager.BasicOffsetY * 80.0 + Manager.BasicOffsetY * 20.0 * 3 + Manager.Size * 120 * 2, 0, 0);
                                break;
                            case 53: // Left Shift Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 5;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0, Manager.BasicOffsetY * 80.0 + Manager.BasicOffsetY * 20.0 * 4 + Manager.Size * 120 * 3, 0, 0);
                                break;
                            case 54: // Right Shift Key
                                c.Height = Manager.Size * 120.0;
                                c.Width = Manager.Size * 120.0 + Manager.OffsetX * 36.0 * 7 + 3;
                                c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + (i - 5) * Manager.OffsetX * 36.0, Manager.BasicOffsetY * 80.0 + Manager.BasicOffsetY * 20.0 * 4 + Manager.Size * 120 * 3, 0, 0);
                                break;
                        }
                    }
                }
            }
            //MessageBox.Show(keyLayout.Max(x => x == null ? 0 : x.Margin.Left + x.Width).ToString());
            //MessageBox.Show((keyLayout[48].Margin.Left + keyLayout[48].Width).ToString());
            this.grid.Width = keyLayout.Max(x => x == null ? 0 : x.Margin.Left + x.Width) + 15;
            this.grid.Height = keyLayout.Max(x => x == null ? 0 : x.Margin.Top + x.Height) + 15;

            Layout = Manager.Layout;
        }

        private void displayKeyDown(int KeyCode)
        {
            //this.Dispatcher.BeginInvoke(new Action(() => { keyLayout[Layout[KeyCode]].Fill = new SolidColorBrush(Manager.ForegroundPressed); }));
            keyLayout[Layout[KeyCode]].Fill = new SolidColorBrush(Manager.ForegroundPressed);
            //MessageBox.Show("Pressed!");
        }

        private void displayKeyUp(int KeyCode)
        {
            //this.Dispatcher.BeginInvoke(new Action(() => { keyLayout[Layout[KeyCode]].Fill = new SolidColorBrush(Manager.Foreground); }));
            keyLayout[Layout[KeyCode]].Fill = new SolidColorBrush(Manager.Foreground);
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
            else if (e.ChangedButton == MouseButton.Middle)
                new Options().Show();
        }

        private void grid_MouseEnter(object sender, MouseEventArgs e)
        {
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
    }
}