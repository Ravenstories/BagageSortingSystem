using System;
using System.Threading;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.TransportersAndSorters
{
    
    class ConveyorToGates : IStartProcess, IMoveArray, IItemsAtLocation, ISortArray
    {
        public void StartProcess()
        {
            while (true)
            {
                Thread.Sleep(Random.rndNum.Next(2000, 10000));
                Sorting(ConveyorBelt.Conveyor);
                Thread.Sleep(Random.rndNum.Next(2000, 10000));

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

            Gate gate = Controller_Gates.GateArray[itemToMove.GateNumber];
            

            lock (gate.GateLock)
            {
                while (!gate.AddToBagageArray(itemToMove))
                {
                    Monitor.Wait(gate.GateLock);
                }
                
                Monitor.PulseAll(gate.GateLock);
            }

            Thread.Sleep(100);


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




