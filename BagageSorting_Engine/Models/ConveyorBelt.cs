using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagageSorting_Engine.Events;
using BagageSorting_Engine.ViewModels;

namespace BagageSorting_Engine.Models
{
    public class ConveyorBelt
    {
        //Locks
        public static object ConveyorLock = new object();

        //Conveyor One
        private static BagageItem[] _conveyor = new BagageItem[50];
        public BagageItem[] Conveyor { get => _conveyor; set => _conveyor = value; }

        private static int _conveyorCounter = 0;
        public static int ConveyorCounter { get => _conveyorCounter; set => _conveyorCounter = value; }

        


    }

}
