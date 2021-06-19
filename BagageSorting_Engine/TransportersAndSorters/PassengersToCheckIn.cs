using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BagageSorting_Engine.TransportersAndSorters
{
    
    class PassengersToCheckIn : IStartProcess, IItemsAtLocation
    {
        public void StartProcess()
        {
            while (true)
            {
                IncomingPassengers incomingPassengers = new IncomingPassengers();

                Thread.Sleep(Random.rndNum.Next(2000, 10000));
                SortToCheckIn(incomingPassengers.PassengersToCheckInList);
            }
        }

        public void SortToCheckIn(List<BagageItem> bagage)
        {
            //Lock one object at the time an move a component. 
            BagageItem itemToMove = null;

            lock (IncomingPassengers.PassengerLock) 
            {
                if (bagage.FirstOrDefault() == null)
                {
                    Console.WriteLine("No passenger at gate... waiting.");
                    Monitor.Wait(IncomingPassengers.PassengerLock);
                }

                itemToMove = bagage.FirstOrDefault();
                bagage.Remove(itemToMove);

                Monitor.PulseAll(IncomingPassengers.PassengerLock);
            }
            
            //Sorting the bagage to a random CheckIn, to simulate people arriving at different gates.
            CheckIn checkIn = ArrayOfCheckIns.CheckInArray[Random.rndNum.Next(0, ArrayOfCheckIns.CheckInArray.Length)];

            if (checkIn.IsOpen == true)
            {
                lock (checkIn.CheckInLock)
                {
                    while (!checkIn.AddToBagageArray(itemToMove))
                    {
                        Monitor.Wait(checkIn.CheckInLock);
                    }

                    Monitor.PulseAll(checkIn.CheckInLock);
                }
            }
            else
            {
                checkIn = ArrayOfCheckIns.CheckInArray[0];
                if (checkIn.IsOpen == true)
                {
                    lock (checkIn.CheckInLock)
                    {
                        while (!checkIn.AddToBagageArray(itemToMove))
                        {
                            Monitor.Wait(checkIn.CheckInLock);
                        }
                        Monitor.PulseAll(checkIn.CheckInLock);
                    }
                }
            }

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




