using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSorting_Engine.Events
{
    public class CheckInOpenEvent : EventArgs
    {
        public bool IsOpen;
        public int CheckInNumber; 

        public CheckInOpenEvent(int checkInNumber, bool isOpen)
        {
            CheckInNumber = checkInNumber;
            IsOpen = isOpen;

        }
    }
}
