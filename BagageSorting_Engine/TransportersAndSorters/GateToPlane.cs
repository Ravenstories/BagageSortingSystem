using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.Events;


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
            BagageItem itemToMove;
            
            if (gate.BagageArray[0] == null)
            {
                Monitor.Wait(gate.GateLock);
            }

            itemToMove = gate.RemoveFromBagageArray();



            return itemToMove;
            
        }
    }
}
