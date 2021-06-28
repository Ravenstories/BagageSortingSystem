using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.Events;
using System.Diagnostics;

namespace BagageSorting_Engine.Factories
{
    class BagageFactory
    {
        private static List<BagageItem> _bagageItemsList = new List<BagageItem>();

        static BagageFactory()
        {
            BuildBagageItem(11111, "Jane Janson",       "Copenhagen",   000001, 991234, 1, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            BuildBagageItem(11112, "Benny Bentson",     "Helsinki",     000002, 992345, 2, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            BuildBagageItem(11113, "Anders Anderson",   "New York",     000003, 993456, 3, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            BuildBagageItem(11114, "Drew Drewson",      "Berlin",       000004, 994567, 4, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            BuildBagageItem(11115, "Richy Rich",        "Dubai",        000005, 995678, 5, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            BuildBagageItem(11116, "Moby Dick",         "Paris",        000006, 996789, 6, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            BuildBagageItem(11117, "Leonardo D. Vinci", "Washington",   000007, 997891, 7, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            BuildBagageItem(11118, "Polly Poly",        "Madrid",       000008, 998912, 8, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            BuildBagageItem(11119, "Gravity Falls",     "Rome",         000009, 999123, 9, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }
        private static void BuildBagageItem(int passengerNumber, string name, string destination, int bagageNumber, int flightNumber, int gateNumber, DateTime timeCheckIn, DateTime timeSorted, DateTime timeBoarded)
        {
            _bagageItemsList.Add(new BagageItem(passengerNumber, name, destination, bagageNumber, flightNumber, gateNumber, timeCheckIn, timeSorted, timeBoarded));
        }
        public static BagageItem GetBagageItem(int passangerNumber)
        {
            return _bagageItemsList.FirstOrDefault(item => item.PassengerNumber == passangerNumber)?.Clone();
        }

        //Function that keeps generating random Bagage

        static int passengerCounter = 11120;
        static int bagageNumber = 000010;
        System.Random rndNmb = new System.Random();

        public BagageItem CreateRandomBagage()
        {
            string passengerName = PassengerNames.ArrayOfNames[rndNmb.Next(0, PassengerNames.ArrayOfNames.Length)];

            Debug.WriteLine(passengerName + " have checkedIn");

            passengerCounter++;
            bagageNumber++;
                    
            return new BagageItem(
                passengerCounter,
                passengerName,
                DestinationNames.ArrayOfDestinations[rndNmb.Next(0, DestinationNames.ArrayOfDestinations.Length)],
                bagageNumber,

                //Flight Number
                0, 
                //Gate Number
                Random.rndNum.Next(0, 10), 

                DateTime.MinValue, DateTime.MinValue, DateTime.MinValue
                );
        }
    }
}
