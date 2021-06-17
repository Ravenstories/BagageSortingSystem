using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BagageSortingSystem.TransportersAndSorters
{
    
    class PassengersToCheckIn : IStartProcess, IItemsAtLocation
    {
        public void StartProcess()
        {
            while (true)
            {
                IncomingPassengers incomingPassengers = new IncomingPassengers();

                Thread.Sleep(2200);
                SortToCheckIn(incomingPassengers.PassengersToCheckInList);
                                
            }
        }

        public void SortToCheckIn(List<BagageItem> bagage)
        {
            Console.WriteLine("Passenger Sorter is trying to sort");
            //Lock one object at the time an move a component. 
            BagageItem itemToMove = null;

            lock (IncomingPassengers.PassengerLockOne) //Need bagage lock
            {
                if (bagage.FirstOrDefault() == null)
                {
                    Console.WriteLine("Passenger couldnt find any bagage to sort");
                    Monitor.Wait(IncomingPassengers.PassengerLockOne);
                }

                itemToMove = bagage.FirstOrDefault();
                bagage.Remove(itemToMove);

                Monitor.PulseAll(IncomingPassengers.PassengerLockOne);
            }
            
            //Get a random checkIn and save the number in variable so that we can find it to see what bagage it contains. 
            System.Random randomCheckIn = new System.Random();
            int rndCheckInNumber = randomCheckIn.Next(0, Program.CheckInArray.Length);

            CheckIn checkIn = Program.CheckInArray[rndCheckInNumber];

            lock (checkIn.CheckInLock)
            {
                while (!checkIn.AddToBagageArray(itemToMove))
                {
                    Monitor.Wait(checkIn.CheckInLock);
                }

                Monitor.PulseAll(checkIn.CheckInLock);
            }

            Thread.Sleep(100);

            Console.WriteLine("\nThis is the Bagage at CheckIn " + rndCheckInNumber + ": ");
            ItemsAtLocation(checkIn.BagageArray);
            Console.WriteLine("Number of bagage at CheckIn: " + checkIn.BagageArrayIndex + "\n");

            Thread.Sleep(100);
        }

        public void ItemsAtLocation(BagageItem[] conveyorArray)
        {
            for (int i = 0; i < conveyorArray.Length; i++)
            {
                if (conveyorArray[i] != null)
                {
                    Console.WriteLine(conveyorArray[i].Name + ", " + conveyorArray[i].PassengerNumber);
                }
            }
        }
        
    }
}




