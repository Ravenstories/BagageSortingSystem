using System.Collections.Generic;

namespace BagageSorting_Engine.Models
{
    public class IncomingPassengers
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

        //Methods to add or remove items
        public void AddBagageToList(BagageItem itemToMove)
        {
            PassengerList.Add(itemToMove);
        }
        public void RemoveBagageFromList(BagageItem itemToMove)
        {
            PassengerList.Remove(itemToMove);
        }
    }
}
