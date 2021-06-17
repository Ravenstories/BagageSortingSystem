using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BagageSortingSystem
{
    public class IncomingPassengers
    {
        //Lock
        public static object PassengerLock = new object();

        //List to contain all passengers
        private static List<BagageItem> passengersToCheckInList = new List<BagageItem>();
        public List<BagageItem> PassengersToCheckInList { get => passengersToCheckInList; set => passengersToCheckInList = value; }

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

        //Function that keeps generating random Bagage
        public void GenerateRandomBagage()
        {
            System.Random rndNmb = new System.Random();
            while (true)
            {
                Thread.Sleep(rndNmb.Next(500, 12000));
                lock (PassengerLock)
                {
                    int passengerCounter = 11120;
                    int bagageNumber = 000010;
                    string passengerName = PassengerNames.ArrayOfNames[rndNmb.Next(0, PassengerNames.ArrayOfNames.Length)];

                    PassengersToCheckInList.Add(new BagageItem(
                        passengerCounter,
                        passengerName,
                        DestinationNames.ArrayOfDestinations[rndNmb.Next(0, DestinationNames.ArrayOfDestinations.Length)],
                        bagageNumber,
                        0,0,0,0,0
                        ));
                    Console.WriteLine(passengerName + " want to check in.");
                    passengerCounter++;
                    bagageNumber++;
                    Monitor.PulseAll(PassengerLock);
                }
            }
        }
    }
}
