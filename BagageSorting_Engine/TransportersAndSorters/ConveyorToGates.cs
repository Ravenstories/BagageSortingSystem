using System;
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

            lock (ConveyorBelt.ConveyorLock)
            {
                if (ConveyorBelt.Conveyor[0] == null)
                {
                    Monitor.Wait(ConveyorBelt.ConveyorLock);
                }
                
                BagageItem itemToMove = ConveyorBelt.Conveyor[0];
                
                //MoveArray();

                //Move Array
                for (int i = 1; i < ConveyorBelt.Conveyor.Length; i++)
                {
                    ConveyorBelt.Conveyor[i - 1] = ConveyorBelt.Conveyor[i];
                }
                ConveyorBelt.Conveyor[ConveyorBelt.Conveyor.Length - 1] = null;
                ConveyorBelt.ConveyorCounter--;


                return itemToMove;
            }
        }

        public static void MoveItemToGate(BagageItem itemToMove)
        {
            Gate gate = gateController.GateArray[itemToMove.GateNumber];

            lock (Gate.GateLock)
            {
                while (!gate.AddToBagageArray(itemToMove))
                {
                    Monitor.Wait(Gate.GateLock);
                }
                
                Monitor.PulseAll(Gate.GateLock);
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




