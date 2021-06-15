using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    class IncomingPassengers
    {
        //List to contain all passengers 1
        public List<BagageItem> PassengersToCheckInList = new List<BagageItem>();

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
