using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.Events
{
    public class PlaneEventArgs : EventArgs
    {
        public PlaneItem PlaneItem { get; }

        public PlaneEventArgs(PlaneItem planeItem)
        {
            PlaneItem = planeItem;
        }
    }
}
