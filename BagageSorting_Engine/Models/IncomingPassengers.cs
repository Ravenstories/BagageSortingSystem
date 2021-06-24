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
        //Events
        public event EventHandler ItemAddedToList;


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
                OnPropertyChanged();
            }
        }


        //Methods to add or remove items
        public void AddBagageToList(BagageItem itemToMove)
        {
            PassengerList.Add(itemToMove);

            ItemAddedToList?.Invoke(this, new PassengerEventArgs(itemToMove));

        }

        public void RemoveBagageFromList(BagageItem itemToMove)
        {
            PassengerList.Remove(itemToMove);


        }


    }
}
