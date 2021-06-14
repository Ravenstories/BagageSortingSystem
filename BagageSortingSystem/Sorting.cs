using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace BagageSortingSystem
{
    class CheckInSorting : ISplitter, IMoveArray, ISortCheckIn, IItemsOnConveyor
    {
        
        public void Splitter()
        {
            while (true)
            {
                SortCheckIn(CheckIn.CheckInBagageListOne, ConveyorBelts.ConveyorOne);
                //MoveArray(ConveyorBelts.ConveyorOne);
                ItemsOnConveyor(ConveyorBelts.ConveyorOne);
                Thread.Sleep(4000);

            }
        }

        public void ItemsOnConveyor(BagageItem[] conveyorArray)
        {
            Console.WriteLine("\nThis is the Bagage on Conveyor One: \n");
            
            for (int i = 0; i < conveyorArray.Length; i++)
            {
                if (conveyorArray[i] != null)
                {
                    Console.WriteLine(conveyorArray[i].Name + ", " + conveyorArray[i].PassengerNumber);
                }
            }
        }
        public void SortCheckIn(List<BagageItem> bagageList, BagageItem[] conveyor)
        {
            lock (ConveyorBelts.ConveyorLockOne)
            {
                for (int i = 0; i < conveyor.Length;)
                {
                    if (conveyor[i] == null)
                    {
                        conveyor[i] = bagageList.FirstOrDefault();
                        BagageItem toRemove = bagageList.FirstOrDefault();
                        bagageList.Remove(toRemove);
                    }
                    i++;
                }

                Thread.Sleep(100);
            }
        }
        public BagageItem[] MoveArray(BagageItem[] conveyorArray)
        {
            for (int i = 1; i < conveyorArray.Length; i++)
            {
                conveyorArray[i - 1] = conveyorArray[i];
            }
            conveyorArray[conveyorArray.Length - 1] = null;
            return conveyorArray;
        }

    }

    class GateSorting : ISplitter, IMoveArray, ISortCheckIn, IItemsOnConveyor
    {
        public void Splitter()
        {
            while (true)
            {
                SortCheckIn(CheckIn.CheckInBagageListOne, ConveyorBelts.ConveyorOne);
                //MoveArray(ConveyorBelts.ConveyorOne);
                ItemsOnConveyor(ConveyorBelts.ConveyorOne);
                Thread.Sleep(4000);

            }
        }

        public void ItemsOnConveyor(BagageItem[] conveyorArray)
        {
            Console.WriteLine("\nThis is the Bagage on Conveyor One: \n");

            for (int i = 0; i < conveyorArray.Length; i++)
            {
                if (conveyorArray[i] != null)
                {
                    Console.WriteLine(conveyorArray[i].Name + ", " + conveyorArray[i].PassengerNumber);
                }
            }
        }

        public void SortCheckIn(List<BagageItem> bagageList, BagageItem[] conveyor)
        {
            lock (ConveyorBelts.ConveyorLockOne)
            {
                for (int i = 0; i < conveyor.Length;)
                {
                    if (conveyor[i] == null)
                    {
                        conveyor[i] = bagageList.FirstOrDefault();
                        BagageItem toRemove = bagageList.FirstOrDefault();
                        bagageList.Remove(toRemove);
                    }
                    i++;
                }

                Thread.Sleep(100);
            }
        }

        public BagageItem[] MoveArray(BagageItem[] conveyorArray)
        {
            for (int i = 1; i < conveyorArray.Length; i++)
            {
                conveyorArray[i - 1] = conveyorArray[i];
            }
            conveyorArray[conveyorArray.Length - 1] = null;
            return conveyorArray;
        }
    }

}
    



