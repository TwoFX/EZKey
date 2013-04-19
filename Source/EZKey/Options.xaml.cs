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
    }
}
