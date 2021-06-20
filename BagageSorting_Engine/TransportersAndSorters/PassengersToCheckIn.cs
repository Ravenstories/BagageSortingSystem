using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.TransportersAndSorters
{
    class PassengersToCheckIn : BaseNotificationClass, IStartProcess, IItemsAtLocation
    {
        public void StartProcess()
        {
            
            IncomingPassengers incomingPassengers = new IncomingPassengers();
            incomingPassengers.AddBagageToList();
            while (true)
            {

                Thread.Sleep(Random.rndNum.Next(2000, 10000));
                SortToCheckIn(incomingPassengers.PassengersToCheckInList);
                Thread.Sleep(Random.rndNum.Next(2000, 10000));

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
                    Monitor.Wait(IncomingPassengers.PassengerLock);
                }

                itemToMove = bagage.FirstOrDefault();
                bagage.Remove(itemToMove);
                OnPropertyChanged();
                
               

                Monitor.PulseAll(IncomingPassengers.PassengerLock);
            }
            
            //Sorting the bagage to a random CheckIn, to simulate people arriving at different gates.
            CheckIn checkIn = Controller_CheckIn.CheckInArray[Random.rndNum.Next(0, Controller_CheckIn.CheckInArray.Length)];

            lock (checkIn.CheckInLock)
            {
                while (!checkIn.AddToBagageArray(itemToMove))
                {
                    Monitor.Wait(checkIn.CheckInLock);
                }

                Monitor.PulseAll(checkIn.CheckInLock);
            }
            /*if (checkIn.IsOpen == true)
            {
            }
            else
            {
                checkIn = ArrayOfCheckIns.CheckInArray[0];
                lock (checkIn.CheckInLock)
                {
                    while (!checkIn.AddToBagageArray(itemToMove))
                    {
                        Monitor.Wait(checkIn.CheckInLock);
                    }
                    Monitor.PulseAll(checkIn.CheckInLock);
                }
                if (checkIn.IsOpen == true)
                {
            }
                }*/

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




