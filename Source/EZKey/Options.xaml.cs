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
    }
}
