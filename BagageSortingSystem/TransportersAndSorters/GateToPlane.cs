using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BagageSortingSystem.TransportersAndSorters
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
                Console.WriteLine("CheckIn To Conveyor is trying to sort");
                Transport();
                Thread.Sleep(4500);
                Console.WriteLine("Number of bagage on conveyor 1: " + ConveyorBelt.ConveyorCounter + "\n");
            }
        }

        public void Transport()
        {
            
            Console.WriteLine("Gate trying to add bagage to plane");

            BagageItem itemToMove = null;


            lock (gate.GateLock)
            {
                if (gate.BagageArray[0] == null)
                {
                    Monitor.Wait(gate.GateLock);
                }

                itemToMove = gate.RemoveFromBagageArray();
                Monitor.PulseAll(gate.GateLock);
            }


            Console.WriteLine("\nThis is the Bagage at CheckIn " + gate + ": ");
            Console.WriteLine("Number of bagage at Gate: " + gate.BagageArrayIndex + "\n");
        }
    }
}
