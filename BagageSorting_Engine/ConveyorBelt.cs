using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSorting_Engine
{
    class ConveyorBelt
    {
        //Locks
        public static object ConveyorLock = new object();

        //Conveyor One
        private static BagageItem[] _conveyor = new BagageItem[50];
        public static BagageItem[] Conveyor { get => _conveyor; set => _conveyor = value; }

        private static int _conveyorCounter = 0;
        public static int ConveyorCounter { get => _conveyorCounter; set => _conveyorCounter = value; }



    }

}
