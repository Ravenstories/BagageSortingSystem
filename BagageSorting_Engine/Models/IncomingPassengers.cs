using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using BagageSorting_Engine.Events;
using BagageSorting_Engine.Factories;

namespace BagageSorting_Engine.Models
{
    public class IncomingPassengers : BaseNotificationClass
    {
        //Lock
        public static object PassengerLock = new object();

        //List to contain all passengers
        private static List<BagageItem> passengerList = new List<BagageItem>();
        public List<BagageItem> PassengerList
        { 
            get => passengerList;
            set 
            { 
                passengerList = value;
            }
        }

        //List of premade Bagage so the system have something to start with
        public void AddBagageToList()
        {
            PassengerList.Add(BagageFactory.GetBagageItem(11111));
            PassengerList.Add(BagageFactory.GetBagageItem(11112));
            PassengerList.Add(BagageFactory.GetBagageItem(11113));
            PassengerList.Add(BagageFactory.GetBagageItem(11114));
            PassengerList.Add(BagageFactory.GetBagageItem(11115));
            PassengerList.Add(BagageFactory.GetBagageItem(11116));
            PassengerList.Add(BagageFactory.GetBagageItem(11117));
            PassengerList.Add(BagageFactory.GetBagageItem(11118));
            PassengerList.Add(BagageFactory.GetBagageItem(11119));
        }

        
    }
}
