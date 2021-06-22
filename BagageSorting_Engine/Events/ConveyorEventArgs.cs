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
        public ObservableCollection<BagageItem> Conveyor { get; }
        public BagageItem BagageItem { get; }

        public ConveyorEventArgs(ObservableCollection<BagageItem> conveyor, BagageItem bagageItem)
        {
            Conveyor = conveyor;
            BagageItem = bagageItem;
        }
    }
}
