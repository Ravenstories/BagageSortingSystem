using System;
using System.Diagnostics;
using System.Threading;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.TransportersAndSorters
{
    public class CheckInToConveyor 
    {
        private CheckIn checkIn; 
        BagageItem itemToMove;

        //Get the checkin that the thread works with
        public CheckInToConveyor(CheckIn checkIn)
        {
            this.checkIn = checkIn;
        }

        //Grabs and item from checkin and store it in itemToMove and returns it.
        public BagageItem GrabItemFromCheckIn()
        {
            while (checkIn.BagageArray[0] == null)
            {
                Thread.Sleep(5000);
                Monitor.Wait(checkIn.CheckInLock);
            }

            itemToMove = checkIn.RemoveFromBagageArray();
            Monitor.PulseAll(checkIn.CheckInLock);
            return itemToMove;
        }

        public void MoveItemToConveyor(BagageItem itemToMove)
        {
            while (this.itemToMove == null)
            {
                Monitor.PulseAll(ConveyorBelt.ConveyorLock);
                Monitor.Wait(ConveyorBelt.ConveyorLock);
            }

            //Add Bagage To Conveyor
            itemToMove.TimeCheckIn = DateTime.Now;
            ConveyorBelt.Conveyor.Enqueue(itemToMove);
            //Debug.WriteLine(itemToMove.Name + "Should be in conveyor");
            Monitor.PulseAll(ConveyorBelt.ConveyorLock);
        }

        //Legacy Code to check items on conveyor
        public void ItemsAtLocation(BagageItem[] conveyorArray)
        {
            Debug.WriteLine("Items at static conveyor: ");
            for (int i = 0; i < conveyorArray.Length; i++)
            {
                if (conveyorArray[i] != null)
                {
                    Debug.WriteLine(conveyorArray[i].Name + ", " + conveyorArray[i].PassengerNumber);
                }
            }
            Debug.WriteLine("\n");
        }
    }
}




