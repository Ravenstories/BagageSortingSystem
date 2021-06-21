using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.Events
{
    public class ConveyorEventArgs : EventArgs
    {
        public ObservableCollection<BagageItem> PassengerList { get; }
        public BagageItem BagageItem { get; }

        public ConveyorEventArgs(ObservableCollection<BagageItem> passengerList, BagageItem bagageItem)
        {
            PassengerList = passengerList;
            BagageItem = bagageItem;
        }
    }
}
