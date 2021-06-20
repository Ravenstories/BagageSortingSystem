using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.Factories
{
    class GenerateNewPassengerBagage : IncomingPassengers
    {
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
                        0, 0, 0, 0, 0
                        ));
                    Console.WriteLine(passengerName + " have checkedIn");
                    passengerCounter++;
                    bagageNumber++;
                    Monitor.PulseAll(PassengerLock);
                }
            }
        }
    }
}
