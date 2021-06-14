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

            CheckInSorting checkInSorting = new CheckInSorting();
            Thread checkInSortingThread = new Thread(new ThreadStart(checkInSorting.Splitter));


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
            checkInSortingThread.Start();

            Console.ReadKey();
        }



    }
        
}
