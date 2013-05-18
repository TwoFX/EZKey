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

namespace EZKey
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {

        bool initialized = false;


        public Options()
        {
            InitializeComponent();
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
            lblMaskKey.Content = Manager.Lables[Manager.Layout[Manager.lockKey]];
            initialized = true;
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
    }
}