using System.Collections.Generic;

namespace BagageSorting_Engine.Models
{
    public class ConveyorBelt
    {
        //Locks
        public static object ConveyorLock = new object();

        //Conveyor One
        private static Queue<BagageItem> _conveyor = new Queue<BagageItem>();
        public static Queue<BagageItem> Conveyor { get => _conveyor; set => _conveyor = value; }

        private static int _conveyorCounter = 0;
        public static int ConveyorCounter { get => _conveyorCounter; set => _conveyorCounter = value; }

    }
}
