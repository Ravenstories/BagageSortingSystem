﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSorting_Engine
{
    class ArrayOfGates
    {
        private static Gate[] gateArray = new Gate[10];
        public static Gate[] GateArray { get => gateArray; set => gateArray = value; }
        private static int _arrayCounter = 0;
        public static int ArrayCounter { get => _arrayCounter; set => _arrayCounter = value; }

        public void CreateGates()
        {
            for (int i = 0; i < GateArray.Length; i++)
            {
                GateArray[i] = new Gate();
                GateArray[i].GateNumber = i;
                GateArray[i].StartThread();
            }
        }

        
    }
}
