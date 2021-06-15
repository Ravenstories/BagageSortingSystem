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
                Console.WriteLine("Number of bagage on conveyor 1: " + ConveyorBelts.ConveyorOneCounter);
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
            for (int i = 0; i < conveyorArray.Length; i++)
            {
                if (conveyorArray[i] != null)
                {
                    Console.WriteLine(conveyorArray[i].Name + ", " + conveyorArray[i].PassengerNumber);
                }
            }
        }

    }

    class SortGateOne : ISplitter, IMoveArray, IItemsAtLocation, ISortArray
    {
        public void Splitter()
        {
            while (true)
            {
                Thread.Sleep(3200);
                SortArray(ConveyorBelts.ConveyorOne, Gates.GateOne, 1);
                
                Console.WriteLine("\nThis is the Bagage at Gate 1: \n");    
                ItemsAtLocation(Gates.GateOne);
                Console.WriteLine("Number of bagage at gate 1: " + Gates.GateOneCounter);
            }
        }

        public void SortArray(BagageItem[] conveyor, BagageItem[] gate, int gateDestination)
        {
            //Lock one object at the time an move a component. 
            BagageItem itemToMove = null;

            if (conveyor[0] != null)
            {
                if (conveyor[0].TerminalNumber == gateDestination) //Bagage Terminal number == Gate Number
                {
                    lock (ConveyorBelts.ConveyorLockOne)
                    {
                        itemToMove = conveyor[0];
                        MoveArray(conveyor);
                        ConveyorBelts.ConveyorOneCounter--;
                        
                    }
                }
            }

            if (itemToMove != null)
            {
                lock (Gates.GateLockOne)
                {
                    if (Gates.GateOneCounter < 50)
                    {
                        if (gate[Gates.GateOneCounter] == null)
                        {
                            gate[Gates.GateOneCounter] = itemToMove;
                            Gates.GateOneCounter++;
                        }
                    }
                }
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
    class SortGateTwo : ISplitter, IMoveArray, IItemsAtLocation, ISortArray
    {
        public void Splitter()
        {
            while (true)
            {
                Thread.Sleep(3700);
                SortArray(ConveyorBelts.ConveyorOne, Gates.GateTwo, 2);

                Console.WriteLine("\nThis is the Bagage at Gate 2: \n");
                ItemsAtLocation(Gates.GateTwo);
                Console.WriteLine("Number of bagage at gate 2: " + Gates.GateTwoCounter);
            }
        }

        public void SortArray(BagageItem[] conveyor, BagageItem[] gate, int gateDestination)
        {
            //Lock one object at the time an move a component. 
            BagageItem itemToMove = null;

            if (conveyor[0] != null)
            {
                if (conveyor[0].TerminalNumber == gateDestination) //Bagage Terminal number == Gate Number
                {
                    lock (ConveyorBelts.ConveyorLockOne)
                    {
                        itemToMove = conveyor[0];
                        MoveArray(conveyor);
                        ConveyorBelts.ConveyorOneCounter--;

                    }
                }
            }

            if (itemToMove != null)
            {
                lock (Gates.GateLockTwo)
                {
                    if (Gates.GateTwoCounter < 50)
                    {
                        if (gate[Gates.GateTwoCounter] == null)
                        {
                            gate[Gates.GateTwoCounter] = itemToMove;
                            Gates.GateTwoCounter++;
                        }
                    }
                }
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




    /*class FlightSorting : IMoveArray, 
    {

    }
    */
}




