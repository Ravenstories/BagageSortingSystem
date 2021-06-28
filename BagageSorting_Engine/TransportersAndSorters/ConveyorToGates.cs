using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.TransportersAndSorters
{
    
    public class ConveyorToGates
    {
        static Controller_Gates gateController = new Controller_Gates();

        public static BagageItem GrapItemFromConveyor()
        {
            //Lock one object at the time an move a component. 

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
            Gate gate = gateController.GateArray[itemToMove.GateNumber];

            lock (gate.GateLock)
            {
                while (!gate.AddToBagageArray(itemToMove))
                {
                    Monitor.PulseAll(gate.GateLock);
                    Monitor.Wait(gate.GateLock);
                }
                itemToMove.TimeSorted = DateTime.Now;
                Debug.WriteLine(itemToMove.Name + " should be at gate " + itemToMove.GateNumber);
                Monitor.PulseAll(gate.GateLock);
            }
            Thread.Sleep(100);
        }
    }
}




