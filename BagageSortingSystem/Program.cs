using System;
using System.Threading;

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
           
            //Sorting Conveyor 1 to Gates
            SortToGates sortToGates = new SortToGates();
            Thread sortToGatesThread = new Thread(new ThreadStart(sortToGates.Splitter));

            //Sorting from Passengers to Gates
            SortPassengerToCheckIn sortPassengerToCheckIn = new SortPassengerToCheckIn();
            Thread sortToCheckInThread = new Thread(new ThreadStart(sortPassengerToCheckIn.Splitter));

            // Print list of items at CheckIn
            Console.WriteLine("Passengers waiting to check in: \n");
            foreach (BagageItem item in incomingPassengers.PassengersToCheckInList)
            {
                Console.WriteLine(item.Name + ", " + item.PassengerNumber);

            }

            Console.WriteLine("\nSorting Bagage.");
            
            //Creating Gates and CheckIns 
            for (int i = 0; i < GateArray.Length; i++)
            {
                GateArray[i] = new Gate();
            }
            for (int i = 0; i < CheckInArray.Length; i++)
            {
                CheckInArray[i] = new CheckIn();
                CheckInArray[i].StartThread();
            }

            //Start the thread that starts sorting items at check in. 
            sortToCheckInThread.Start();
            sortToGatesThread.Start();
            

            Console.ReadKey();
        }



    }
        
}
