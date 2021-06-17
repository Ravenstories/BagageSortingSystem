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
           
            //Random sorting from Passengers to CheckIn
            PassengersToCheckIn PassengerToCheckIn = new PassengersToCheckIn();
            Thread passengerToCheckInThread = new Thread(new ThreadStart(PassengerToCheckIn.StartProcess));

            //Sorting Conveyor to Gates
            ConveyorToGates sortConveyorToGates = new ConveyorToGates();
            Thread sortConveyorToGatesThread = new Thread(new ThreadStart(sortConveyorToGates.StartProcess));

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
                GateArray[i].GateNumber = i;
                GateArray[i].StartThread();

            }
            for (int i = 0; i < CheckInArray.Length; i++)
            {
                CheckInArray[i] = new CheckIn();
                CheckInArray[i].StartThread();
            }

            //Thread for random generating Bagage
            Thread generateRandomBagage = new Thread(new ThreadStart(incomingPassengers.GenerateRandomBagage));

            //Start the threads 
            passengerToCheckInThread.Start();
            sortConveyorToGatesThread.Start();
            generateRandomBagage.Start();


            Console.ReadKey();
        }



    }
        
}
