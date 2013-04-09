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
        private Dictionary<int, Rectangle> findFromCode;
        private Color xyzColor;
        private int s, t;
        private double m;

        public MainWindow()
        {

            keyLayout = new Rectangle[47];
            findFromCode = new Dictionary<int, Rectangle>();
            m = 1.0;
            s = 30;
            t = 5;
         

            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            //this.WindowStyle = WindowStyle.None;
            InterceptKeys.Load();

            Manager.KeyDown += DisplayKeyDown;
            Manager.KeyUp += DisplayKeyUp;

            Manager.Background = Colors.White;
            Manager.Foreground = Colors.White;
            Manager.ForegroundPressed = Colors.Orange;
            Manager.Border = Colors.Black;
            Manager.BorderPressed = Colors.Black;
            Manager.Text = Colors.Black;
            Manager.TextPressed = Colors.White;

            Manager.Size = 30;
            Manager.Roundness = 3.75;
            Manager.BorderThickness = 0.7;
            Manager.BasicOffsetX = 20;
            Manager.OffsetX = 9;
            Manager.BasicOffsetX = 20;
            Manager.OffsetY = 5;

            

            for (int i = 0; i < 47; i++)
            {
                if (i != 1 && i != 45)
                {
                    Rectangle c = new Rectangle(); // I shall call it "The conveniance variable" (because keyLayout[i] is way too fucking long)
                    keyLayout[i] = c;
                    c.HorizontalAlignment = HorizontalAlignment.Left;
                    c.VerticalAlignment = VerticalAlignment.Top;
                    grid.Children.Add(c);
                }
            }

            setOptions();
            
            
            

            //keyLayout.Add(new Rectangle());
            //keyLayout[0].HorizontalAlignment = HorizontalAlignment.Left;
            //keyLayout[0].VerticalAlignment = VerticalAlignment.Top;
            //keyLayout[0].Fill = Brushes.White;
            //keyLayout[0].StrokeThickness = 0.7;
            //keyLayout[0].Stroke = Brushes.Black;
            //keyLayout[0].Height = 30;
            //keyLayout[0].Width = 30;
            //keyLayout[0].RadiusX = 30 / 8.0;
            //keyLayout[0].RadiusY = 30 / 8.0;
            //keyLayout[0].Margin = new Thickness(70, 70, 0, 0);
            //keyLayout[0].StrokeThickness = 30 / 20.0;
            //grid.Children.Add(keyLayout[0]);
        }

        private void setOptions()
        {
            for (int i = 0; i < 47; i++)
            {
                if (i != 1 && i != 45)
                {
                    Rectangle c = keyLayout[i]; // I shall call it "The conveniance variable" (because keyLayout[i] is way too fucking long) [once again]
                    c.Fill = new SolidColorBrush(Manager.Foreground);
                    c.StrokeThickness = Manager.BorderThickness;
                    c.Stroke = new SolidColorBrush(Manager.Border);
                    c.Height = c.Width = Manager.Size;
                    c.RadiusX = c.RadiusY = Manager.Roundness;
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
                    c.Margin = new Thickness(Manager.BasicOffsetX + i * Manager.OffsetX, Manager.BasicOffsetY + o * Manager.OffsetY + (o - 1) * Manager.Size, 0, 0);
                }
            }
        }

        private void DisplayKeyDown(int KeyCode)
        {
            keyLayout[0].Fill = Brushes.Black;
        }

        private void DisplayKeyUp(int KeyCode)
        {
            keyLayout[0].Fill = Brushes.White;
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
