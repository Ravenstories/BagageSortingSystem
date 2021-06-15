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


        //Methods
        public static BagageItem[] GetGate(int gateNumber)
        {
            switch (gateNumber)
            {
                case 1:
                    return GateOne;
                case 2:
                    return GateTwo;
                default:
                    return null;
            }
        }
        public static object GetGateLock(int gateNumber)
        {
            switch (gateNumber)
            {
                case 1:
                    return GateLockOne;
                case 2:
                    return GateLockTwo;
                default:
                    return null;
            }
        }
        public static int GetGateCounter(int gateNumber)
        {
            switch (gateNumber)
            {
                case 1:
                    return GateOneCounter;
                case 2:
                    return GateTwoCounter;
                default:
                    return -1;
            }
        }
        public static void AddToGateCounter(int gateNumber)
        {
            switch (gateNumber)
            {
                case 1:
                    GateOneCounter++;
                    break;
                case 2:
                    GateTwoCounter++;
                    break;
                default:
                    break;
            }
        }
        public static void SubtractFromGateCounter(int gateNumber)
        {
            switch (gateNumber)
            {
                case 1:
                    GateOneCounter--;
                    break;
                case 2:
                    GateTwoCounter--;
                    break;
                default:
                    break;
            }
        }


    }
}
