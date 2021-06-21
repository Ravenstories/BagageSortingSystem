using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.Events
{
    public class PassengerEventArgs : EventArgs
    {
        public List<BagageItem> Passenger { get; }

        public PassengerEventArgs(List<BagageItem> passenger)
        {
            Passenger = passenger;
        }
    }
}
