using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    class ConveyorBelts
    {
        //Locks
        public static object ConveyorLockOne = new object();
        public static object ConveyorLockTwo = new object();
        public static object ConveyorLockThree = new object();


        //Conveyor One
        private static BagageItem[] _conveyorOne = new BagageItem[50];
        public static BagageItem[] ConveyorOne { get => _conveyorOne; set => _conveyorOne = value; }

        private static int _conveyorOneCounter = 0;
        public static int ConveyorOneCounter { get => _conveyorOneCounter; set => _conveyorOneCounter = value; }



    }

}
