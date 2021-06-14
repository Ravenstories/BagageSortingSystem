using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    class CheckIn
    {
        //CheckIn 1
        public static List<BagageItem> CheckInBagageListOne = new List<BagageItem>();


        //CheckIn 2
        public static List<BagageItem> CheckInBagageListTwo = new List<BagageItem>();
        
        //public static List<BagageItem> CheckInBagageListThree = new List<BagageItem>();
        //public List<BagageItem> CheckInBagageListFour = new List<BagageItem>();


        public void AddBagageToList()
        {
            CheckInBagageListOne.Add(BagageFactory.GetBagageItem(11111));
            CheckInBagageListOne.Add(BagageFactory.GetBagageItem(11112));
            CheckInBagageListOne.Add(BagageFactory.GetBagageItem(11113));
            CheckInBagageListOne.Add(BagageFactory.GetBagageItem(11114));
            CheckInBagageListOne.Add(BagageFactory.GetBagageItem(11115));
            CheckInBagageListTwo.Add(BagageFactory.GetBagageItem(11116));
            CheckInBagageListTwo.Add(BagageFactory.GetBagageItem(11117));
            CheckInBagageListTwo.Add(BagageFactory.GetBagageItem(11118));
            CheckInBagageListTwo.Add(BagageFactory.GetBagageItem(11119));
        }

    }
}
