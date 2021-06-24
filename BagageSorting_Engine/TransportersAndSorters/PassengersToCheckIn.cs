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
        public IncomingPassengers incomingPassengers = new IncomingPassengers();

        public BagageItem GrabItemFromPassengerList()
        {
            //Lock one object at the time an move a component. 
            
            BagageItem itemToMove = null;

            if (incomingPassengers.PassengerList.Count() == 0)
            {
                    Monitor.Wait(IncomingPassengers.PassengerLock);

            }

            itemToMove = incomingPassengers.PassengerList.FirstOrDefault();

            

            return itemToMove;

            //Debug.WriteLine(itemToMove.Name + " have moved to checkIn");

            
        }


         public bool MoveToCheckIn(BagageItem itemToMove)
         {

            //Sorting the bagage to a random CheckIn, to simulate people arriving at different gates.
            CheckIn checkIn = Controller_CheckIn.CheckInArray[Random.rndNum.Next(0, Controller_CheckIn.CheckInArray.Length)];

           
            if (checkIn.IsOpen == true)
            {
                lock (checkIn.CheckInLock)
                {
                    while (!checkIn.AddToBagageArray(itemToMove))
                    {
                        Monitor.Wait(checkIn.CheckInLock);
                    }
                    Monitor.PulseAll(checkIn.CheckInLock);

                    //Event
                    incomingPassengers.RemoveBagageFromList(itemToMove);
                }

                Debug.WriteLine(itemToMove.Name + " have moved to Check In " + checkIn.CheckInNumber);
                
                return checkIn.IsOpen;
            }
            else
            {
                Thread.Sleep(2000);

                return checkIn.IsOpen;
            }
         }
    }      
            
}




