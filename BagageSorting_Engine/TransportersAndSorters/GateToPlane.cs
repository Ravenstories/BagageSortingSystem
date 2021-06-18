using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BagageSorting_Engine.TransportersAndSorters
{
    class GateToPlane
    {
        private Gate gate;
        public GateToPlane(Gate gate)
        {
            this.gate = gate;
        }

        public void StartProcess()
        {
            while (true)
            {
                
                Transport();
                Thread.Sleep(Random.rndNum.Next(2000, 10000));
            }
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
                Console.WriteLine(itemToMove.Name + " Have been transported to Plane");
                Monitor.PulseAll(gate.GateLock);
            }
            
        }
    }
}
