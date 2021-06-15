using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    class CheckIn
    {
        //Locks
        public static object CheckInLockOne = new object();
        public static object CheckInLockTwo = new object();


        //CheckIn 1
        private static BagageItem[] _checkInOne = new BagageItem[50];
        public static BagageItem[] CheckInOne { get => _checkInOne; set => _checkInOne = value; }

        //CheckIn 2
        private static BagageItem[] _checkInTwo = new BagageItem[50];
        public static BagageItem[] CheckInTwo { get => _checkInTwo; set => _checkInTwo = value; }
    }
}
