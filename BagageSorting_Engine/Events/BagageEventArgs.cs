using System;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.Events
{
    public class BagageEventArgs : EventArgs
    {
        public BagageItem BagageItem { get; }

        public BagageEventArgs(BagageItem bagageItem)
        {
            BagageItem = bagageItem;
        }
        
    }
}
