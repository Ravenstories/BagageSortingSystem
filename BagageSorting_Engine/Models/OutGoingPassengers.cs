using System.Collections.Generic;

namespace BagageSorting_Engine.Models
{
    public class OutGoingPassengers
    {
        public static object OutGoingLock = new object();

        private static List<BagageItem> outGoingPassengerList = new List<BagageItem>();
        public List<BagageItem> OutGoingPassengerList
        {
            get => outGoingPassengerList;
            set
            {
                outGoingPassengerList = value;
            }
        }
    }
}
