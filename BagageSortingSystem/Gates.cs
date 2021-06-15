using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    class Gates
    {
        //Locks
        public static object GateLockOne = new object();
        public static object GateLockTwo = new object();


        //Gate1        
        private static BagageItem[] _gateOne = new BagageItem[50];
        public static BagageItem[] GateOne { get => _gateOne; set => _gateOne = value; }

        private static int _gateOneCounter = 0;
        public static int GateOneCounter { get => _gateOneCounter; set => _gateOneCounter = value; }


        //Gate2
        private static BagageItem[] _gateTwo = new BagageItem[50];
        public static BagageItem[] GateTwo { get => _gateTwo; set => _gateTwo = value; }

        private static int _gateTwoCounter = 0;
        public static int GateTwoCounter { get => _gateTwoCounter; set => _gateTwoCounter = value; }
    }
}
