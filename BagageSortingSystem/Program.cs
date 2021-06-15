using System;
using System.Threading;

namespace BagageSortingSystem
{
    class Program 
    {

        static void Main(string[] args)
        {
            CheckIn checkIn = new CheckIn();
            checkIn.AddBagageToList();

            //Sorting CheckIn 1 to Conveyor 1
            SortCheckInOne sortCheckInOne = new SortCheckInOne();
            Thread checkInOneSortingThread = new Thread(new ThreadStart(sortCheckInOne.Splitter));
            
            //Sorting CheckIn 1 to Conveyor 1
            SortCheckInTwo sortCheckInTwo = new SortCheckInTwo();
            Thread checkInTwoSortingThread = new Thread(new ThreadStart(sortCheckInTwo.Splitter));

            //Sorting Conveyor 1 to Gate 1
            SortGateOne sortGateOne = new SortGateOne();
            Thread gateOneSortingThread = new Thread(new ThreadStart(sortGateOne.Splitter));

            //Sorting Conveyor 1 to Gate 2
            SortGateTwo sortGateTwo = new SortGateTwo();
            Thread gateTwoSortingThread = new Thread(new ThreadStart(sortGateTwo.Splitter));



            // Print list of items at CheckIn
            Console.WriteLine("This is in Bagage List One: \n");
            foreach (BagageItem item in CheckIn.CheckInBagageListOne)
            {
                Console.WriteLine(item.Name + ", " + item.PassengerNumber);

            }

            Console.WriteLine("\nThis is in Bagage List Two: \n");
            foreach (BagageItem item in CheckIn.CheckInBagageListTwo)
            {
                Console.WriteLine(item.Name + ", " + item.PassengerNumber);

            }

            Console.WriteLine("\nSorting Bagage.");

            //Start the thread that starts sorting items at check in. 
            checkInOneSortingThread.Start();
            checkInTwoSortingThread.Start();
            gateOneSortingThread.Start();
            gateTwoSortingThread.Start();

            Console.ReadKey();
        }



    }
        
}
