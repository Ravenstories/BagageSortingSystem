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
        public BagageItem BagageItem { get; }

        public ConveyorEventArgs(BagageItem bagageItem)
        {
            BagageItem = bagageItem;
        }
    }
}
