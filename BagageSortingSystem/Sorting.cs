using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace BagageSortingSystem
{
    #region Sort CheckIn
    class SortCheckInOne : ISplitter, IItemsAtLocation, IMoveArray
    {
        public void Splitter()
        {
            while (true)
            {
                SortCheckIn(ConveyorBelts.ConveyorOne);
                Thread.Sleep(4500);
                ItemsAtLocation(ConveyorBelts.ConveyorOne);
                Console.WriteLine("Number of bagage on conveyor 1: " + ConveyorBelts.ConveyorOneCounter + "\n");
            }
        }
                
        public void SortCheckIn(BagageItem[] conveyor)
        {
            Console.WriteLine("Check In sorter, trying to sort");
            CheckIn checkIn = Program.CheckInArray[0];
           
            BagageItem itemToMove = null;
            
            lock (checkIn.CheckInLock)
            {
                if (checkIn.BagageArray[0] == null)
                {
                    Monitor.Wait(checkIn.CheckInLock);
                }

                itemToMove = checkIn.RemoveFromBagageArray();
                Monitor.PulseAll(checkIn.CheckInLock);
            }

            lock (ConveyorBelts.ConveyorLockOne)
            {
                if (itemToMove == null)
                {
                    Monitor.Wait(ConveyorBelts.ConveyorLockOne);
                }
                
                conveyor[ConveyorBelts.ConveyorOneCounter] = itemToMove;
                ConveyorBelts.ConveyorOneCounter++;

                Monitor.PulseAll(ConveyorBelts.ConveyorLockOne);
                Thread.Sleep(100);
                
            }

            Console.WriteLine("\nThis is the Bagage at CheckIn " + Program.CheckInArray[0] + ": ");
            ItemsAtLocation(checkIn.BagageArray);
            Console.WriteLine("Number of bagage at Gate: " + checkIn.BagageArrayIndex + "\n");
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
    class SortCheckInTwo : ISplitter, ISortCheckIn, IItemsAtLocation, IMoveArray
    {
        public void Splitter()
        {
            while (true)
            {
                SortCheckIn(CheckIns.CheckInTwo, ConveyorBelts.ConveyorOne);
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

            lock (ConveyorBelts.ConveyorLockOne)
            {
                if (ConveyorBelts.ConveyorOneCounter > 50 && conveyor[ConveyorBelts.ConveyorOneCounter] != null)
                {
                    Monitor.Wait(ConveyorBelts.ConveyorLockOne);
                }
                
                conveyor[ConveyorBelts.ConveyorOneCounter] = itemToMove;
                ConveyorBelts.ConveyorOneCounter++;

                Monitor.PulseAll(ConveyorBelts.ConveyorLockOne);
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
    #endregion

    #region Sort from conveyor to Gates
    class SortToGates : ISplitter, IMoveArray, IItemsAtLocation, ISortArray
    {
        public void Splitter()
        {
            while (true)
            {
                Thread.Sleep(2200);
                SortArray(ConveyorBelts.ConveyorOne);
                
            }
        }

        public void SortArray(BagageItem[] conveyor)
        {
            //Lock one object at the time an move a component. 
            BagageItem itemToMove = null;
            
            lock (ConveyorBelts.ConveyorLockOne)
            {
                if (conveyor[0] == null)
                {
                    Monitor.Wait(ConveyorBelts.ConveyorLockOne);
                }
                
                itemToMove = conveyor[0];
                MoveArray(conveyor);
                ConveyorBelts.ConveyorOneCounter--;

                Monitor.PulseAll(ConveyorBelts.ConveyorLockOne);
            }

            Gate gate = Program.GateArray[itemToMove.TerminalNumber];
            
            lock (gate.GateLock)
            {
                while (!gate.AddToBagageArray(itemToMove))
                {
                    Monitor.Wait(gate.GateLock);
                }
                
                Monitor.PulseAll(gate.GateLock);
            }

            Thread.Sleep(100);

            Console.WriteLine("\nThis is the Bagage at Gate " + itemToMove.TerminalNumber + ": ");
            ItemsAtLocation(gate.BagageArray);
            Console.WriteLine("Number of bagage at Gate: " + Gates.GateOneCounter + "\n");

        }
        public void ItemsAtLocation(BagageItem[] locationArray)
        {
            for (int i = 0; i < locationArray.Length; i++)
            {
                if (locationArray[i] != null)
                {
                    Console.WriteLine(locationArray[i].Name + ", " + locationArray[i].PassengerNumber);
                }
            }
        }
        public BagageItem[] MoveArray(BagageItem[] locationArray)
        {
            for (int i = 1; i < locationArray.Length; i++)
            {
                locationArray[i - 1] = locationArray[i];
            }
            locationArray[locationArray.Length - 1] = null;
            return locationArray;
        }
    }
    #endregion

    #region Passengerbagage to CheckIn Array
    class SortPassengerToCheckIn : ISplitter, IItemsAtLocation
    {
        public void Splitter()
        {
            while (true)
            {
                IncomingPassengers incomingPassengers = new IncomingPassengers();

                Thread.Sleep(2200);
                SortToCheckIn(incomingPassengers.PassengersToCheckInList);

                Console.WriteLine("\nThis is the Bagage at CheckIn 1: \n");
                ItemsAtLocation(CheckIns.CheckInOne);
                Console.WriteLine("Number of bagage at CheckIn 1: " + CheckIns.CheckInOneCounter + "\n");

                Console.WriteLine("This is the Bagage at CheckIn 2: \n");
                ItemsAtLocation(CheckIns.CheckInTwo);
                Console.WriteLine("Number of bagage at CheckIn 2: " + CheckIns.CheckInTwoCounter + "\n");
            }
        }

        public void SortToCheckIn(List<BagageItem> bagage)
        {
            Console.WriteLine("Passenger Sorter is trying to sort");
            //Lock one object at the time an move a component. 
            BagageItem itemToMove = null;

            lock (IncomingPassengers.PassengerLockOne) //Need bagage lock
            {
                if (bagage.FirstOrDefault() == null)
                {
                    Console.WriteLine("Passenger couldnt find any bagage to sort");
                    Monitor.Wait(IncomingPassengers.PassengerLockOne);
                }

                itemToMove = bagage.FirstOrDefault();
                bagage.Remove(itemToMove);

                Monitor.PulseAll(IncomingPassengers.PassengerLockOne);
            }

            object lockObj = CheckIns.GetLocationLock(itemToMove.TerminalNumber);
            int counter = CheckIns.GetLocationCounter(itemToMove.TerminalNumber);
            BagageItem[] checkIn = CheckIns.GetLocation(itemToMove.TerminalNumber);

            lock (lockObj)
            {
                if (counter > 50 && checkIn[counter] != null)
                {
                    Monitor.Wait(lockObj);
                }
                checkIn[counter] = itemToMove;
                CheckIns.AddToLocationCounter(itemToMove.TerminalNumber);
                Monitor.PulseAll(lockObj);
            }

            Thread.Sleep(100);
        }

        public void ItemsAtLocation(BagageItem[] conveyorArray)
        {
            for (int i = 0; i < conveyorArray.Length; i++)
            {
                if (conveyorArray[i] != null)
                {
                    Console.WriteLine(conveyorArray[i].Name + ", " + conveyorArray[i].PassengerNumber);
                }
            }
        }
        
    }
    #endregion
}




