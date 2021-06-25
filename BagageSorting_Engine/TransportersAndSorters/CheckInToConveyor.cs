using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.ViewModels;

namespace BagageSorting_Engine.TransportersAndSorters
{
    public class CheckInToConveyor 
    {

        private CheckIn checkIn; 
        BagageItem itemToMove;
        public CheckInToConveyor(CheckIn checkIn)
        {
            this.checkIn = checkIn;
        }

        public BagageItem GrapItemFromCheckIn()
        {
            

            if (checkIn.BagageArray[0] == null)
            {
                Monitor.Wait(checkIn.CheckInLock);
            }

            itemToMove = checkIn.RemoveFromBagageArray();
            return itemToMove;
            
        }

        public void MoveItemToConveyor(BagageItem itemToMove)
        {
            
            // This might be very redundant
            if (this.itemToMove == null)
            {
                Monitor.Wait(ConveyorBelt.ConveyorLock);
            }

            Debug.WriteLine(this.itemToMove.Name + " Is trying to be moved to Conveyor");


            //Add Bagage To Conveyor
            ConveyorBelt.Conveyor.Add(this.itemToMove);

            //Shows items at Conveyor Location
            //ItemsAtLocation(ConveyorBelt.Conveyor);

            Monitor.PulseAll(ConveyorBelt.ConveyorLock);
            Thread.Sleep(100);

            
        }
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




