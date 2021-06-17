using System;
using System.Threading;
using BagageSortingSystem.TransportersAndSorters;

namespace BagageSortingSystem
{
    class Program 
    {
        private static Gate[] gateArray = new Gate[10];
        public static Gate[] GateArray { get => gateArray; set => gateArray = value; }

        private static CheckIn[] checkInArray = new CheckIn[10];
        public static CheckIn[] CheckInArray { get => checkInArray; set => checkInArray = value; }


        static void Main(string[] args)
        {
            
            IncomingPassengers incomingPassengers = new IncomingPassengers();
            incomingPassengers.AddBagageToList();
           
            //Sorting from Passengers to Gates
            PassengersToCheckIn sortPassengerToCheckIn = new PassengersToCheckIn();
            Thread sortToCheckInThread = new Thread(new ThreadStart(sortPassengerToCheckIn.StartProcess));

            //Sorting Conveyor 1 to Gates
            ConveyorToGates sortToGates = new ConveyorToGates();
            Thread sortToGatesThread = new Thread(new ThreadStart(sortToGates.StartProcess));

            //Transporting from Gate to Plane
            
            
            // Print list of passengers at the beginning
            Console.WriteLine("Passengers waiting to check in: \n");
            foreach (BagageItem item in incomingPassengers.PassengersToCheckInList)
            {
                Console.WriteLine(item.Name + ", " + item.PassengerNumber);

            }

            
            //Creating Gates and CheckIns and starts checkIn threads
            for (int i = 0; i < GateArray.Length; i++)
            {
                GateArray[i] = new Gate();
            }
            for (int i = 0; i < CheckInArray.Length; i++)
            {
                CheckInArray[i] = new CheckIn();
                CheckInArray[i].StartThread();
            }

            //Start the threads 
            sortToCheckInThread.Start();
            sortToGatesThread.Start();


            Console.ReadKey();
        }



    }
        
}
