using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace BagageSortingSystem.TransportersAndSorters
{
    #region Sort CheckIn
    class CheckInToConveyor : IStartProcess, IItemsAtLocation
    {
        private CheckIn checkIn; 
        public CheckInToConveyor(CheckIn checkIn)
        {
            this.checkIn = checkIn;
        }

        public void StartProcess()
        {
            while (true)
            {
                Console.WriteLine("CheckIn To Conveyor is trying to sort");
                Transport(ConveyorBelt.Conveyor);
                Thread.Sleep(4500);
                ItemsAtLocation(ConveyorBelt.Conveyor);
                Console.WriteLine("Number of bagage on conveyor 1: " + ConveyorBelt.ConveyorCounter + "\n");
            }
        }
                
        public void Transport(BagageItem[] conveyor)
        {
            /// Posible solution is to have a loop runing throug the checkIn array and see if 0 is null, in not, then move it to conveyor and the go to the next. 
            /// Look into the idea that the Thread will have a counter that goes up when it encounters a null. If it reaches number of null equeal to array.length, it's safe to assume it's done. 
            /// If it encounters something that's not null, the counter will reset. 
            /// How will it wake again? 
            
            Console.WriteLine("Check In sorter, trying to sort");

            BagageItem itemToMove = null;
            
            
            lock (checkIn.CheckInLock)
            {
                if (checkIn.BagageArray[0] == null)
                {
                    Monitor.Wait(checkIn.CheckInLock);
                }

                itemToMove = checkIn.RemoveFromBagageArray();
                Monitor.PulseAll(checkIn.CheckInLock);
            }

            lock (ConveyorBelt.ConveyorLock)
            {
                // This might be very redundant
                if (itemToMove == null)
                {
                    Monitor.Wait(ConveyorBelt.ConveyorLock);
                }
                
                conveyor[ConveyorBelt.ConveyorCounter] = itemToMove;
                ConveyorBelt.ConveyorCounter++;

                Monitor.PulseAll(ConveyorBelt.ConveyorLock);
                Thread.Sleep(100);
                
            }

            Console.WriteLine("\nThis is the Bagage at CheckIn " + checkIn + ": ");
            ItemsAtLocation(checkIn.BagageArray);
            Console.WriteLine("Number of bagage at Gate: " + checkIn.BagageArrayIndex + "\n");
        }
        public void ItemsAtLocation(BagageItem[] conveyorArray)
        {
            Console.WriteLine("\nThis is the Bagage on Conveyor One: \n");
            
            for (int i = 0; i < conveyorArray.Length; i++)
            {
                if (conveyorArray[i] != null)
                {
                    Console.WriteLine(conveyorArray[i].Name + ", " + conveyorArray[i].PassengerNumber);
                }
            }
        }

    }

#endregion
#region Sort from conveyor to Gates

#endregion
#region Passengerbagage to CheckIn Array
    #endregion

    public class Random
    {
        
    }
}




