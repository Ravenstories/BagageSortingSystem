using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BagageSorting_Engine.Models;
using System.Collections.ObjectModel;

namespace BagageSorting_Engine.Events
{
    public class BagageEventArgs : EventArgs
    {
        //public TrulyObservableCollection<BagageItem> PassengerList { get; }
        public BagageItem BagageItem { get; }

        public BagageEventArgs(BagageItem bagageItem)
        {
            BagageItem = bagageItem;
        }
        
    }
}
