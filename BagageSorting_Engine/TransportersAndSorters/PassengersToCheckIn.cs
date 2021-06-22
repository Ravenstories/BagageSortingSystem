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
        ProgramSession session = new ProgramSession();
        public void StartProcess()
        {
            
            IncomingPassengers incomingPassengers = new IncomingPassengers();
            //incomingPassengers.AddBagageToList();
            while (true)
            {

                Thread.Sleep(Random.rndNum.Next(2000, 10000));
                SortToCheckIn(ProgramSession.PassengerList);
                Thread.Sleep(Random.rndNum.Next(2000, 10000));

            }
        }

        public void SortToCheckIn(TrulyObservableCollection<BagageItem> bagage)
        {
            //Lock one object at the time an move a component. 
            BagageItem itemToMove = null;

            lock (IncomingPassengers.PassengerLock) 
            {
                if (bagage.Count() == 0)
                {
                    Monitor.Wait(IncomingPassengers.PassengerLock);
                    
                }

                itemToMove = bagage.FirstOrDefault();

                //Event

                session.RemoveItemFromPassengerList(itemToMove);

                //Debug.WriteLine(itemToMove.Name + " have moved to checkIn");

                Monitor.PulseAll(IncomingPassengers.PassengerLock);
            }
            
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
            }
            else
            {
                Thread.Sleep(2000);
            }

            Thread.Sleep(100);
        }

        public void ItemsAtLocation(TrulyObservableCollection<BagageItem> bagage)
        {
            for (int i = 0; i < bagage.Count; i++)
            {
                if (bagage[i] != null)
                {
                    Debug.WriteLine(bagage[i].Name + ", " + bagage[i].PassengerNumber);
                }
            }
        }
        
    }
}




