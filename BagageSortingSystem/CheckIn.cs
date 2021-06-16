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
        private static int _checkInOneCounter = 0;
        public static int CheckInOneCounter { get => _checkInOneCounter; set => _checkInOneCounter = value; }

        //CheckIn 2
        private static BagageItem[] _checkInTwo = new BagageItem[50];
        public static BagageItem[] CheckInTwo { get => _checkInTwo; set => _checkInTwo = value; }

        private static int _checkInTwoCounter = 0;
        public static int CheckInTwoCounter { get => _checkInTwoCounter; set => _checkInTwoCounter = value; }


        //Methods
        public static BagageItem[] GetLocation(int checkInNumber)
        {
            switch (checkInNumber)
            {
                case 1:
                    return CheckInOne;
                case 2:
                    return CheckInTwo;
                default:
                    return null;
            }
        }
        public static object GetLocationLock(int checkInNumber)
        {
            switch (checkInNumber)
            {
                case 1:
                    return CheckInLockOne;
                case 2:
                    return CheckInLockTwo;
                default:
                    return null;
            }
        }
        public static int GetLocationCounter(int checkInNumber)
        {
            switch (checkInNumber)
            {
                case 1:
                    return CheckInOneCounter;
                case 2:
                    return CheckInTwoCounter;
                default:
                    return -1;
            }
        }
        public static void AddToLocationCounter(int checkInNumber)
        {
            switch (checkInNumber)
            {
                case 1:
                    CheckInOneCounter++;
                    break;
                case 2:
                    CheckInTwoCounter++;
                    break;
                default:
                    break;
            }
        }
        public static void SubtractFromLocationCounter(int checkInNumber)
        {
            switch (checkInNumber)
            {
                case 1:
                    CheckInOneCounter--;
                    break;
                case 2:
                    CheckInTwoCounter--;
                    break;
                default:
                    break;
            }
        }

    }


}
