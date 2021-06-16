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
    class SortCheckInOne : ISplitter, ISortCheckIn, IItemsAtLocation, IMoveArray
    {
        public void Splitter()
        {
            while (true)
            {
                SortCheckIn(CheckIn.CheckInOne, ConveyorBelts.ConveyorOne);
                Thread.Sleep(4500);
                ItemsAtLocation(ConveyorBelts.ConveyorOne);
                Console.WriteLine("Number of bagage on conveyor 1: " + ConveyorBelts.ConveyorOneCounter + "\n");
            }
        }

        //
        public void SortCheckIn(BagageItem[] checkIn, BagageItem[] conveyor)
        {
            BagageItem itemToMove = null;
            
            lock (CheckIn.CheckInLockOne)
            {
                if (checkIn[0] == null)
                {
                    Monitor.Wait(CheckIn.CheckInLockOne);
                }

                itemToMove = checkIn[0];
                MoveArray(checkIn);
                CheckIn.CheckInOneCounter--;

                Monitor.PulseAll(CheckIn.CheckInLockOne);
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

    #region Sort CheckIn 2
    class SortCheckInTwo : ISplitter, ISortCheckIn, IItemsAtLocation, IMoveArray
    {
        public void Splitter()
        {
            while (true)
            {
                SortCheckIn(CheckIn.CheckInTwo, ConveyorBelts.ConveyorOne);
                Thread.Sleep(4500);
            }
        }

        //
        public void SortCheckIn(BagageItem[] checkIn, BagageItem[] conveyor)
        {
            BagageItem itemToMove = null;
            
            lock (CheckIn.CheckInLockTwo)
            {
                if (checkIn[0] == null)
                {
                    Monitor.Wait(CheckIn.CheckInLockTwo);
                }

                itemToMove = checkIn[0];
                MoveArray(checkIn);
                CheckIn.CheckInTwoCounter--;

                Monitor.PulseAll(CheckIn.CheckInLockTwo);
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
    
    #endregion // 

    class SortToGates : ISplitter, IMoveArray, IItemsAtLocation, ISortArray
    {
        public void Splitter()
        {
            while (true)
            {
                Thread.Sleep(2200);
                SortArray(ConveyorBelts.ConveyorOne);
                
                Console.WriteLine("\nThis is the Bagage at Gate 1: \n");    
                ItemsAtLocation(Gates.GateOne);
                Console.WriteLine("Number of bagage at gate 1: " + Gates.GateOneCounter + "\n");

                Console.WriteLine("This is the Bagage at Gate 2: \n");
                ItemsAtLocation(Gates.GateTwo);
                Console.WriteLine("Number of bagage at gate 2: " + Gates.GateTwoCounter + "\n");
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

            object lockObj = Gates.GetGateLock(itemToMove.TerminalNumber);
            int counter = Gates.GetGateCounter(itemToMove.TerminalNumber);
            BagageItem[] gate = Gates.GetGate(itemToMove.TerminalNumber);

            lock (lockObj)
            {
                if (counter > 50 && gate[counter] != null)
                {
                    Monitor.Wait(lockObj);
                }
                gate[counter] = itemToMove;
                Gates.AddToGateCounter(itemToMove.TerminalNumber);
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
                ItemsAtLocation(CheckIn.CheckInOne);
                Console.WriteLine("Number of bagage at CheckIn 1: " + CheckIn.CheckInOneCounter + "\n");

                Console.WriteLine("This is the Bagage at CheckIn 2: \n");
                ItemsAtLocation(CheckIn.CheckInTwo);
                Console.WriteLine("Number of bagage at CheckIn 2: " + CheckIn.CheckInTwoCounter + "\n");
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

            object lockObj = CheckIn.GetLocationLock(itemToMove.TerminalNumber);
            int counter = CheckIn.GetLocationCounter(itemToMove.TerminalNumber);
            BagageItem[] checkIn = CheckIn.GetLocation(itemToMove.TerminalNumber);

            lock (lockObj)
            {
                if (counter > 50 && checkIn[counter] != null)
                {
                    Monitor.Wait(lockObj);
                }
                checkIn[counter] = itemToMove;
                CheckIn.AddToLocationCounter(itemToMove.TerminalNumber);
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
}




