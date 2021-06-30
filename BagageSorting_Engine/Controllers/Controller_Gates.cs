using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.Controllers
{
    public class Controller_Gates
    {
        private static Gate[] gateArray = new Gate[10];
        public Gate[] GateArray { get => gateArray; set => gateArray = value; }

        private static int _arrayCounter = 0;
        public static int ArrayCounter { get => _arrayCounter; set => _arrayCounter = value; }

        public void CreateGates()
        {
            for (int i = 0; i < GateArray.Length; i++)
            {
                GateArray[i] = new Gate();
                GateArray[i].GateNumber = i;
                Thread.Sleep(20);
            }
        }
    }
}
