using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    class Gates
    {
        //Gate1        
        private static BagageItem[] _gateOne = new BagageItem[50];
        public static BagageItem[] GateOne { get => _gateOne; set => _gateOne = value; }


        //Gate2
        private static BagageItem[] _gateTwo = new BagageItem[50];
        public static BagageItem[] GateTwo { get => _gateTwo; set => _gateTwo = value; }

    }
}
