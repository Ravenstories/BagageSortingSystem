using System;
using System.Collections.Generic;
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

            
            if (ConveyorBelt.Conveyor.FirstOrDefault()  != null)
            {
                BagageItem itemToMove = ConveyorBelt.RemoveItem();

                return itemToMove;
            }
            else
            {
                    
                return null;

            }
            
        }

        public static void MoveItemToGate(BagageItem itemToMove)
        {
            Gate gate = gateController.GateArray[itemToMove.GateNumber];

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




