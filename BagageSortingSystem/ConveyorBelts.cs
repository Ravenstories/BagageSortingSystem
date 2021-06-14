using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    public class ConveyorBelts
    {
        public static object ConveyorLockOne = new object();
        public static object ConveyorLockTwo = new object();
        public static object ConveyorLockThree = new object();

        private static BagageItem[] _conveyorOne = new BagageItem[50];
        public static BagageItem[] ConveyorOne { get => _conveyorOne; set => _conveyorOne = value; }


    }
    
}
