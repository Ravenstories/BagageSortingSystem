using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSorting_Engine
{
    class BagageFactory
    {
        private static List<BagageItem> _bagageItemsList = new List<BagageItem>();

        static BagageFactory()
        {
            BuildBagageItem(11111, "Jane Janson",       "Copenhagen",   000001, 991234, 1, 0, 0, 0);
            BuildBagageItem(11112, "Benny Bentson",     "Helsinki",     000002, 992345, 2, 0, 0, 0);
            BuildBagageItem(11113, "Anders Anderson",   "New York",     000003, 993456, 3, 0, 0, 0);
            BuildBagageItem(11114, "Drew Drewson",      "Berlin",       000004, 994567, 4, 0, 0, 0);
            BuildBagageItem(11115, "Richy Rich",        "Dubai",        000005, 995678, 5, 0, 0, 0);
            BuildBagageItem(11116, "Moby Dick",         "Paris",        000006, 996789, 6, 0, 0, 0);
            BuildBagageItem(11117, "Leonardo D. Vinci", "Washington",   000007, 997891, 7, 0, 0, 0);
            BuildBagageItem(11118, "Polly Poly",        "Madrid",       000008, 998912, 8, 0, 0, 0);
            BuildBagageItem(11119, "Gravity Falls",     "Rome",         000009, 999123, 9, 0, 0, 0);
        }

        private static void BuildBagageItem(int passengerNumber, string name, string destination, int bagageNumber, int flightNumber, int gateNumber, int timeCheckIn, int timeSorted, int timeBoarded)
        {
            _bagageItemsList.Add(new BagageItem(passengerNumber, name, destination, bagageNumber, flightNumber, gateNumber, timeCheckIn, timeSorted, timeBoarded));
        }

        public static BagageItem GetBagageItem(int passangerNumber)
        {
            return _bagageItemsList.FirstOrDefault(item => item.PassengerNumber == passangerNumber)?.Clone();
        }
    }
}

            
             
            
            
            
            
            
             
