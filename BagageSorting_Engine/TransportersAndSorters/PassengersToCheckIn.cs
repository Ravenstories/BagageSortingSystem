using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.Events;
using BagageSorting_Engine.ViewModels;
using System.Diagnostics;

namespace BagageSorting_Engine.TransportersAndSorters
{
    public class PassengersToCheckIn : BaseNotificationClass, IStartProcess
    {
        public IncomingPassengers incomingPassengers = new IncomingPassengers();
        BagageItem itemToMove = null;

        public void StartProcess()
        {
            
            //incomingPassengers.AddBagageToList();
            while (true)
            {

                Thread.Sleep(Random.rndNum.Next(2000, 10000));
                GrabItemFromPassengerList();

                if (itemToMove != null)
                {
                    Thread.Sleep(Random.rndNum.Next(500, 1000));
                    MoveToCheckIn();
                }
            }
        }

        public void GrabItemFromPassengerList()
        {
            //Lock one object at the time an move a component. 

            lock (IncomingPassengers.PassengerLock)
            {
                if (incomingPassengers.PassengerList.Count() == 0)
                {
                    Monitor.Wait(IncomingPassengers.PassengerLock);

                }

                itemToMove = incomingPassengers.PassengerList.FirstOrDefault();

                //Event
                incomingPassengers.RemoveBagageFromList(itemToMove);


                //Debug.WriteLine(itemToMove.Name + " have moved to checkIn");

                Monitor.PulseAll(IncomingPassengers.PassengerLock);
            }
        }


         public void MoveToCheckIn()
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
                    
                    //ItemsAtLocation(bagage);
                }
                Debug.WriteLine(itemToMove.Name + " have moved to Check In " + checkIn.CheckInNumber);
            }
            else
            {
                Thread.Sleep(2000);
            }

            Thread.Sleep(100);
         }
    }      
            
}




