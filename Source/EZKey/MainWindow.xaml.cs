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

        public MainWindow()
        {

            keyLayout = new Rectangle[47];
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

            for (int i = 0; i < 47; i++)
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
            for (int i = 0; i < 47; i++)
            {
                if (i != 1 && i != 45)
                {
                    Rectangle c = keyLayout[i]; // I shall call it "The conveniance variable" (because keyLayout[i] is way too fucking long) [once again]
                    c.Fill = new SolidColorBrush(Manager.Foreground);
                    c.StrokeThickness = Manager.BorderThickness * 2.8;
                    c.Stroke = new SolidColorBrush(Manager.Border);
                    c.Height = c.Width = Manager.Size * 120.0;
                    c.RadiusX = c.RadiusY = Manager.Roundness * 15.0;
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
                    c.Margin = new Thickness(Manager.BasicOffsetX * 80.0 + i * Manager.OffsetX * 36.0, Manager.BasicOffsetY * 80.0 + o * Manager.OffsetY * 20.0 + (o - 1) * Manager.Size * 120.0, 0, 0);
                }
            }

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

        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void grid_MouseEnter(object sender, MouseEventArgs e)
        {
            lblMini.Visibility = Visibility.Visible;
            lblClose.Visibility = Visibility.Visible;
        }

        private void grid_MouseLeave(object sender, MouseEventArgs e)
        {
            lblMini.Visibility = Visibility.Hidden;
            lblClose.Visibility = Visibility.Hidden;
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
