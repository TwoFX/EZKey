using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZKey
{
    static class Manager
    {
        public static void KeyDetected(int KeyCode)
        {
            System.Windows.MessageBox.Show(KeyCode.ToString());
        }
    }
}
