using System;
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
