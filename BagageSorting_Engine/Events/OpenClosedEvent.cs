using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSorting_Engine.Events
{
    public class OpenClosedEvent : EventArgs
    {
        public bool OpenClosed;
        public int Number; 

        public OpenClosedEvent(int number, bool openClosed)
        {
            Number = number;
            OpenClosed = openClosed;

        }
    }
}
