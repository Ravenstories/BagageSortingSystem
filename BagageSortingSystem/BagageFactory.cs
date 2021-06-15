using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    class BagageFactory
    {
        private static List<BagageItem> _bagageItemsList = new List<BagageItem>();

        static BagageFactory()
        {
            BuildBagageItem(11111, "Jane Janson",       000001, 991234, 1, 0, 0, 0);
            BuildBagageItem(11112, "Benny Bentson",     000002, 992345, 2, 0, 0, 0);
            BuildBagageItem(11113, "Anders Anderson",   000003, 993456, 1, 0, 0, 0);
            BuildBagageItem(11114, "Drew Drewson",      000004, 994567, 2, 0, 0, 0);
            BuildBagageItem(11115, "Richy Rich",        000005, 995678, 1, 0, 0, 0);
            BuildBagageItem(11116, "Moby Dick",         000006, 996789, 2, 0, 0, 0);
            BuildBagageItem(11117, "Leonardo D. Vinci", 000007, 997891, 1, 0, 0, 0);
            BuildBagageItem(11118, "Polly Poly",        000008, 998912, 2, 0, 0, 0);
            BuildBagageItem(11119, "Gravity Falls",     000009, 999123, 1, 0, 0, 0);
        }

        private static void BuildBagageItem(int passengerNumber, string name, int bagageNumber, int flightNumber, int terminalNumber, int timeCheckIn, int timeSorted, int timeBoarded)
        {
            _bagageItemsList.Add(new BagageItem(passengerNumber, name, bagageNumber, flightNumber, terminalNumber, timeCheckIn, timeSorted, timeBoarded));
        }

        public static BagageItem GetBagageItem(int passangerNumber)
        {
            return _bagageItemsList.FirstOrDefault(item => item.PassengerNumber == passangerNumber)?.Clone();
        }
    }
}
