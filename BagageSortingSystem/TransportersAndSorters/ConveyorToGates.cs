using System;
using System.Threading;

namespace BagageSortingSystem.TransportersAndSorters
{
    
    class ConveyorToGates : IStartProcess, IMoveArray, IItemsAtLocation, ISortArray
    {
        public void StartProcess()
        {
            while (true)
            {
                Thread.Sleep(2200);
                Console.WriteLine("Conveyor To Gate is trying to sort");
                Sorting(ConveyorBelt.Conveyor);
            }
        }

        public void Sorting(BagageItem[] conveyor)
        {
            //Lock one object at the time an move a component. 
            BagageItem itemToMove = null;
            lock (ConveyorBelt.ConveyorLock)
            {
                if (conveyor[0] == null)
                {
                    Monitor.Wait(ConveyorBelt.ConveyorLock);
                }
                
                itemToMove = conveyor[0];
                MoveArray(conveyor);
                ConveyorBelt.ConveyorCounter--;

                Monitor.PulseAll(ConveyorBelt.ConveyorLock);
            }

            Gate gate = Program.GateArray[itemToMove.TerminalNumber];
            
            lock (gate.GateLock)
            {
                while (!gate.AddToBagageArray(itemToMove))
                {
                    Monitor.Wait(gate.GateLock);
                }
                
                Monitor.PulseAll(gate.GateLock);
            }

            Thread.Sleep(100);

            Console.WriteLine("\nThis is the Bagage at Gate " + itemToMove.TerminalNumber + ": ");
            ItemsAtLocation(gate.BagageArray);
            Console.WriteLine("Number of bagage at Gate: " + Gates.GateOneCounter + "\n");

        }
        public void ItemsAtLocation(BagageItem[] locationArray)
        {
            for (int i = 0; i < locationArray.Length; i++)
            {
                if (locationArray[i] != null)
                {
                    Console.WriteLine(locationArray[i].Name + ", " + locationArray[i].PassengerNumber);
                }
            }
        }
        public BagageItem[] MoveArray(BagageItem[] locationArray)
        {
            for (int i = 1; i < locationArray.Length; i++)
            {
                locationArray[i - 1] = locationArray[i];
            }
            locationArray[locationArray.Length - 1] = null;
            return locationArray;
        }
    }
}




