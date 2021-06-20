using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using BagageSorting_Engine.Factories;

namespace BagageSorting_Engine.Models
{
    public class IncomingPassengers : BaseNotificationClass
    {
        //Lock
        public static object PassengerLock = new object();

        //List to contain all passengers
        private static List<BagageItem> passengersToCheckInList = new List<BagageItem>();
        public List<BagageItem> PassengersToCheckInList
        { 
            get => passengersToCheckInList;
            set 
            { 
                passengersToCheckInList = value;
                OnPropertyChanged();
            }
        }

        //List of premade Bagage so the system have something to start with
        public void AddBagageToList()
        {
            PassengersToCheckInList.Add(BagageFactory.GetBagageItem(11111));
            PassengersToCheckInList.Add(BagageFactory.GetBagageItem(11112));
            PassengersToCheckInList.Add(BagageFactory.GetBagageItem(11113));
            PassengersToCheckInList.Add(BagageFactory.GetBagageItem(11114));
            PassengersToCheckInList.Add(BagageFactory.GetBagageItem(11115));
            PassengersToCheckInList.Add(BagageFactory.GetBagageItem(11116));
            PassengersToCheckInList.Add(BagageFactory.GetBagageItem(11117));
            PassengersToCheckInList.Add(BagageFactory.GetBagageItem(11118));
            PassengersToCheckInList.Add(BagageFactory.GetBagageItem(11119));
        }

        
    }
}
