using System;
using System.Collections.Generic;
using System.Text;

namespace ECTentti02
{
    class JobChangedEventArgs : EventArgs
    {
        public Job Job { get; set; }
    }
}
