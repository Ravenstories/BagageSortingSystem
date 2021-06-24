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
        //Event Handler
        public event EventHandler BagageMovedToCheckOutList;

        private Gate gate;
        public GateToPlane(Gate gate)
        {
            this.gate = gate;
        }

        

        public void Transport()
        {
            BagageItem itemToMove = null;

            
            lock (gate.GateLock)
            {
                if (gate.BagageArray[0] == null)
                {
                    Monitor.Wait(gate.GateLock);
                }

                itemToMove = gate.RemoveFromBagageArray();

                //Global CheckOut List Here

                //Event Here
                BagageMovedToCheckOutList?.Invoke(this, new PassengerEventArgs(itemToMove));


                Monitor.PulseAll(gate.GateLock);
            }
            
        }
    }
}
