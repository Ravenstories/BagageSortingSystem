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
    class SortCheckInOne : ISplitter, ISortCheckIn, IItemsAtLocation
    {
        
        public void Splitter()
        {
            while (true)
            {
                SortCheckIn(CheckIn.CheckInBagageListOne, ConveyorBelts.ConveyorOne);
                ItemsAtLocation(ConveyorBelts.ConveyorOne);
                Thread.Sleep(4500);
                Console.WriteLine("Number of bagage on conveyor 1: " + ConveyorBelts.ConveyorOneCounter + "\n");
            }
        }

        public void SortCheckIn(List<BagageItem> bagageList, BagageItem[] conveyor)
        {
            BagageItem itemToMove = null;

            if (bagageList.FirstOrDefault() != null)
            {
                lock (CheckIn.CheckInLockOne)
                {
                    itemToMove = bagageList.FirstOrDefault();
                    BagageItem toRemove = bagageList.FirstOrDefault();
                    bagageList.Remove(toRemove);
                }
            }

            if (itemToMove != null)
            {
                if (ConveyorBelts.ConveyorOneCounter < 50)
                {
                    if (conveyor[ConveyorBelts.ConveyorOneCounter] == null)
                    {
                        lock (ConveyorBelts.ConveyorLockOne)
                        {
                            conveyor[ConveyorBelts.ConveyorOneCounter] = itemToMove;
                            ConveyorBelts.ConveyorOneCounter++;
                            
                        }
                    }
                }
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

    }
    class SortCheckInTwo : ISplitter, ISortCheckIn, IItemsAtLocation
    {
        public void Splitter()
        {
            while (true)
            {
                Thread.Sleep(50);
                SortCheckIn(CheckIn.CheckInBagageListTwo, ConveyorBelts.ConveyorOne);

                //Console.WriteLine("\nThis is the Bagage on Conveyor One: \n");
                //ItemsAtLocation(ConveyorBelts.ConveyorOne);
                //Console.WriteLine("Number of bagage on conveyor 1: " + ConveyorBelts.ConveyorOneCounter);
                Thread.Sleep(5200);
            }
        }

        public void SortCheckIn(List<BagageItem> bagageList, BagageItem[] conveyor)
        {
            BagageItem itemToMove = null;

            if (bagageList.FirstOrDefault() != null)
            {
                lock (CheckIn.CheckInLockTwo)
                {
                    itemToMove = bagageList.FirstOrDefault();
                    BagageItem toRemove = bagageList.FirstOrDefault();
                    bagageList.Remove(toRemove);
                }
            }

            if (itemToMove != null)
            {
                if (ConveyorBelts.ConveyorOneCounter < 50)
                {
                    if (conveyor[ConveyorBelts.ConveyorOneCounter] == null)
                    {
                        lock (ConveyorBelts.ConveyorLockOne)
                        {
                            conveyor[ConveyorBelts.ConveyorOneCounter] = itemToMove;
                            ConveyorBelts.ConveyorOneCounter++;
                            
                        }
                    }
                }
                
                Thread.Sleep(100);
            }

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
}




