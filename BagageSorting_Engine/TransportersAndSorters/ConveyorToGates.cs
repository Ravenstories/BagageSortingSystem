﻿using System;
using System.Threading;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.Controllers;

namespace BagageSorting_Engine.TransportersAndSorters
{
    
    public class ConveyorToGates
    {
        static Controller_Gates gateController = new Controller_Gates();

        public static BagageItem GrabItemFromConveyor()
        {
            //Take item from conveyor, removes and return it. 

            BagageItem itemToMove = null;
            while (ConveyorBelt.Conveyor.Count == 0)
            {
                Monitor.PulseAll(ConveyorBelt.ConveyorLock);
                Monitor.Wait(ConveyorBelt.ConveyorLock);
            }
            itemToMove = ConveyorBelt.Conveyor.Dequeue();
            Monitor.PulseAll(ConveyorBelt.ConveyorLock);
            return itemToMove;
        }

        public static void MoveItemToGate(BagageItem itemToMove)
        {
            //Looks at the gate number, the itemToMove has and sort it there.
            Gate gate = gateController.GateArray[itemToMove.GateNumber];

            while (!gate.AddToBagageArray(itemToMove))
            {
                Monitor.PulseAll(gate.GateLock);
                Thread.Sleep(1000);
            }
            itemToMove.TimeSorted = DateTime.Now;
            //Debug.WriteLine(itemToMove.Name + " should be at gate " + itemToMove.GateNumber);
            Monitor.PulseAll(gate.GateLock);
            
            Thread.Sleep(100);
        }
    }
}




