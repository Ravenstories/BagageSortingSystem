using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.Events;
using System.Diagnostics;

namespace BagageSorting_Engine.TransportersAndSorters
{
    public class GateToPlane
    {
        private Gate gate;
        public GateToPlane(Gate gate)
        {
            this.gate = gate;
        }

        public BagageItem Transport()
        {
            BagageItem itemToMove = null;
            
            while (gate.BagageArray[0] == null)
            {
                Monitor.PulseAll(gate.GateLock);
                Monitor.Wait(gate.GateLock);
            }

            itemToMove = gate.RemoveFromBagageArray();
            itemToMove.TimeBoarded = DateTime.Now;
            Debug.WriteLine(itemToMove.Name + "Have boarded a plane \n");
            Monitor.PulseAll(gate.GateLock);
            return itemToMove;
        }
    }
}
