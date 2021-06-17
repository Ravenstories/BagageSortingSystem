using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BagageSortingSystem.OldCode
{
    class SortCheckInTwo : IStartProcess, ISortCheckIn, IItemsAtLocation, IMoveArray
    {
        public void StartProcess()
        {
            while (true)
            {
                SortCheckIn(CheckIns.CheckInTwo, ConveyorBelt.Conveyor);
                Thread.Sleep(4500);
            }
        }

        //
        public void SortCheckIn(BagageItem[] checkIn, BagageItem[] conveyor)
        {
            BagageItem itemToMove = null;

            lock (CheckIns.CheckInLockTwo)
            {
                if (checkIn[0] == null)
                {
                    Monitor.Wait(CheckIns.CheckInLockTwo);
                }

                itemToMove = checkIn[0];
                MoveArray(checkIn);
                CheckIns.CheckInTwoCounter--;

                Monitor.PulseAll(CheckIns.CheckInLockTwo);
            }

            lock (ConveyorBelt.ConveyorLock)
            {
                if (ConveyorBelt.ConveyorCounter > 50 && conveyor[ConveyorBelt.ConveyorCounter] != null)
                {
                    Monitor.Wait(ConveyorBelt.ConveyorLock);
                }

                conveyor[ConveyorBelt.ConveyorCounter] = itemToMove;
                ConveyorBelt.ConveyorCounter++;

                Monitor.PulseAll(ConveyorBelt.ConveyorLock);
                Thread.Sleep(100);

            }
        }
        public void ItemsAtLocation(BagageItem[] conveyorArray)
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

