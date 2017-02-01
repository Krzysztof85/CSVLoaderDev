using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVLoader
{
    public class PercentageEventArgs : EventArgs
    {
        public int Percentage { get; private set; }

        public PercentageEventArgs(int percentage)
        {
            Percentage = percentage;
        }
    }
}
