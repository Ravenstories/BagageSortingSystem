using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.Events;
using System.Diagnostics;

namespace BagageSorting_Engine.TransportersAndSorters
{
    public class PassengersToCheckIn : BaseNotificationClass
    {
        IncomingPassengers incomingPassengers = new IncomingPassengers();
        Controller_CheckIn controller_CheckIn = new Controller_CheckIn();
        IncomingPassengers IncomingPassengers { get => incomingPassengers; set => incomingPassengers = value; }
        Controller_CheckIn Controller_CheckIn { get => controller_CheckIn; set => controller_CheckIn = value; }

        public BagageItem GrabItemFromPassengerList()
        {
            //Lock one object at the time an move a component. 
            
            BagageItem itemToMove = null;
            if (IncomingPassengers.PassengerList.Count() == 0)
            {
                Monitor.PulseAll(IncomingPassengers.PassengerLock);
                Monitor.Wait(IncomingPassengers.PassengerLock);
            }
            itemToMove = IncomingPassengers.PassengerList.FirstOrDefault();
            return itemToMove;
        }

        public bool MoveToCheckIn(BagageItem itemToMove)
        {
            //Sorting the bagage to a random CheckIn, to simulate people arriving at different gates.
            CheckIn checkIn = Controller_CheckIn.CheckInArray[Random.rndNum.Next(0, Controller_CheckIn.CheckInArray.Length)];
           
            if (checkIn.IsOpen)
            {
                lock (checkIn.CheckInLock)
                {
                    while (!checkIn.AddToBagageArray(itemToMove))
                    {
                        Monitor.PulseAll(checkIn.CheckInLock);
                        Monitor.Wait(checkIn.CheckInLock);
                    }
                    Monitor.PulseAll(checkIn.CheckInLock);
                    IncomingPassengers.RemoveBagageFromList(itemToMove);
                }

                Debug.WriteLine(itemToMove.Name + " have moved to Check In " + checkIn.CheckInNumber);
                return checkIn.IsOpen;
            }
            else
            {
                return checkIn.IsOpen;
            }
        }
    }
}




