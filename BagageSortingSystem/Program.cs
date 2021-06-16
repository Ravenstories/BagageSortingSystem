using System;
using System.Threading;

namespace BagageSortingSystem
{
    class Program 
    {

        static void Main(string[] args)
        {
            IncomingPassengers incomingPassengers = new IncomingPassengers();
            incomingPassengers.AddBagageToList();

            //Sorting CheckIn 1 to Conveyor 1
            SortCheckInOne sortCheckInOne = new SortCheckInOne();
            Thread checkInOneSortingThread = new Thread(new ThreadStart(sortCheckInOne.Splitter));
            
            //Sorting CheckIn 1 to Conveyor 1
            SortCheckInTwo sortCheckInTwo = new SortCheckInTwo();
            Thread checkInTwoSortingThread = new Thread(new ThreadStart(sortCheckInTwo.Splitter));

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

            //Start the thread that starts sorting items at check in. 
            sortToCheckInThread.Start();
            checkInOneSortingThread.Start();
            sortToGatesThread.Start();
            
            checkInTwoSortingThread.Start();

            Console.ReadKey();
        }



    }
        
}
