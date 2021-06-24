using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using BagageSorting_Engine.TransportersAndSorters;
using BagageSorting_Engine.Factories;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.Events;
using System.Diagnostics;

namespace BagageSorting_Engine.ViewModels
{
    public class ProgramSession : BaseNotificationClass
    {
        static IncomingPassengers incomingPassengers = new IncomingPassengers();
        PassengersToCheckIn passengersToCheckIn = new PassengersToCheckIn();
        BagageFactory bagageFactory = new BagageFactory();
        static ConveyorBelt conveyorBelt = new ConveyorBelt();
        static OutGoingPassengers outGoing = new OutGoingPassengers();

        Controller_CheckIn arrayOfCheckIns = new Controller_CheckIn();
        Controller_Gates arrayOfGates = new Controller_Gates();

        public static IncomingPassengers Current_IncomingPassengers 
        { 
            get => incomingPassengers; 
            set 
            {
                incomingPassengers = value;
            } 
        }
        public static ConveyorBelt Current_ConveyorBelt { get => conveyorBelt; set => conveyorBelt = value; }
        public static OutGoingPassengers Current_OutGoing { get => outGoing; set => outGoing = value; }
        
        ///For bagage generating
        BagageFactory BagageFactory { get => bagageFactory; set => bagageFactory = value; }
        
        ///Controllers (Generating checkins and gates)
        public Controller_CheckIn Current_Controller_CheckIn { get => arrayOfCheckIns; set => arrayOfCheckIns = value; }
        public Controller_Gates Current_Controller_Gate { get => arrayOfGates; set => arrayOfGates = value; }
        public PassengersToCheckIn Current_PassengersToCheckIn { get => passengersToCheckIn; set => passengersToCheckIn = value; }
       
       
        ///Observables
        private static TrulyObservableCollection<BagageItem> passengerList = new TrulyObservableCollection<BagageItem>(Current_IncomingPassengers.PassengerList);
        public TrulyObservableCollection<BagageItem> PassengerList 
        { 
            get => passengerList;
            set
            {
                passengerList = value;
            }
        }


        private static ObservableCollection<BagageItem> conveyor = new ObservableCollection<BagageItem>(ConveyorBelt.Conveyor);
        public static ObservableCollection<BagageItem> Conveyor { get => conveyor; set => conveyor = value; }


        private static TrulyObservableCollection<BagageItem> checkedOutList = new TrulyObservableCollection<BagageItem>(Current_OutGoing.OutGoingPassengerList);
        public static TrulyObservableCollection<BagageItem> CheckedOutList { get => checkedOutList; set => checkedOutList = value; }




        public void StartSession()
        {
            //Random sorting from Passengers to CheckIn
            Thread passengerToCheckInThread = new Thread(() => PassengerToCheckIn());

            //Sorting Conveyor to Gates
            ConveyorToGates sortConveyorToGates = new ConveyorToGates();
            Thread sortConveyorToGatesThread = new Thread(() => ConveyorToGatesMethod());

            //Thread for random generating Bagage
            Thread createRandomBagage = new Thread(() => CreateBagage());
            
            //Creating Gates and CheckIns and starts checkIn threads
            Current_Controller_CheckIn.CreateCheckIns();
            Current_Controller_Gate.CreateGates();
            
            //GateThreads created here for the view to register change. 
            for (int i = 0; i < Current_Controller_Gate.GateArray.Length; i++)
            {
                Thread gateToPlaneThread = new Thread(() => GateToCheckOut(i));
                gateToPlaneThread.Start();
            }

            //Start the threads 
            passengerToCheckInThread.Start();
            sortConveyorToGatesThread.Start();
            createRandomBagage.Start();

        }

        //Events
        public event EventHandler BagageMovedToCheckOutList;
        public event EventHandler CheckInOpenClosedEvent;
        public event EventHandler GateOpenClosedEvent;
        public event EventHandler BagageCreated;
        public event EventHandler ItemRemovedFromList;
        public event EventHandler MovedToConveyor;



        //Create passenger bagage and moves them to a random check in. 
        private void CreateBagage()
        {
            System.Random rndNmb = new System.Random();
            while (true)
            {
                lock (IncomingPassengers.PassengerLock)
                {
                    BagageItem bagageItem = BagageFactory.CreateRandomBagage();

                    Current_IncomingPassengers.AddBagageToList(bagageItem);
                    BagageCreated?.Invoke(this, new PassengerEventArgs(bagageItem));

                    
                    Monitor.PulseAll(IncomingPassengers.PassengerLock);

                }
                Thread.Sleep(rndNmb.Next(500, 9000));
            }
        }
        private void PassengerToCheckIn()
        {
            BagageItem itemToMove;
            
            while (true)
            {

                Thread.Sleep(Random.rndNum.Next(2000, 10000));
                lock (IncomingPassengers.PassengerLock)
                {
                    itemToMove = Current_PassengersToCheckIn.GrabItemFromPassengerList();


                    Monitor.PulseAll(IncomingPassengers.PassengerLock);
                }

                if (itemToMove != null)
                {
                    Thread.Sleep(Random.rndNum.Next(500, 1000));
                    
                    bool CheckIfOpen = Current_PassengersToCheckIn.MoveToCheckIn(itemToMove);
                    if (CheckIfOpen == true)
                    {
                        ItemRemovedFromList?.Invoke(this, new PassengerEventArgs(itemToMove));
                    }

                }
            }
        }


        //From CheckInToConveyor ////UNUSED
        public void ItemMovedToConveyor(BagageItem bagageItem)
        {

            MovedToConveyor?.Invoke(this, new ConveyorEventArgs(Conveyor, bagageItem));
            ConveyorBelt.ConveyorCounter++;

            Debug.WriteLine(bagageItem.Name + "Should be in conveyor");
        }
        public void ConveyorToGatesMethod()
        {
            while (true)
            {
                //Refactor to loop if item to move is null. 
                Thread.Sleep(Random.rndNum.Next(2000, 10000));

                BagageItem itemToMove = ConveyorToGates.GrapItemFromConveyor();

                if (itemToMove == null)
                {
                    Thread.Sleep(2000);
                    itemToMove = ConveyorToGates.GrapItemFromConveyor();
                }

                ConveyorToGates.MoveItemToGate(itemToMove);

                Thread.Sleep(Random.rndNum.Next(2000, 10000));

            }
        }


        //Moves Passenger to checkout list when properly checked out. 
        public void GateToCheckOut(int i)
        {
            int arrayIdentifier = i -1;
            
            Thread.Sleep(Random.rndNum.Next(2000, 10000));
            
            while (true)
            {
                lock (Gate.GateLock)
                {
                    if (Current_Controller_Gate.GateArray[arrayIdentifier].IsOpen == true)
                    {
                        Thread.Sleep(Random.rndNum.Next(2000, 10000));
                        BagageItem itemToMove = Current_Controller_Gate.GateArray[arrayIdentifier].gateToPlane.Transport();

                        Debug.WriteLine(itemToMove.Name + "Have boarded a plane \n");

                        BagageMovedToCheckOutList?.Invoke(this, new PassengerEventArgs(itemToMove));
                        Monitor.PulseAll(Gate.GateLock);
                        
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        Monitor.Wait(Gate.GateLock);
                    }
                }
            }
        }
        


        //CheckIns and Gates open and close Methods
        public void OpenCheckIn()
        {
            if (Controller_CheckIn.ArrayCounter < Controller_CheckIn.CheckInArray.Length)
            {

                Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen = true;

                CheckInOpenClosedEvent.Invoke(this, new OpenClosedEvent(Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].CheckInNumber, Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen));
                
                Controller_CheckIn.ArrayCounter++;
            }
            else
            {
                Debug.WriteLine("Max number of Check Ins reached");
            }
        }
        public void CloseCheckIn()
        {
            Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen = false;
            
            CheckInOpenClosedEvent.Invoke(this, new OpenClosedEvent(Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].CheckInNumber, Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen));

            if (Controller_CheckIn.ArrayCounter != 0)
            {
                Controller_CheckIn.ArrayCounter--;

            }
        }

        public void OpenGate()
        {

            if (Current_Controller_Gate.ArrayCounter < Current_Controller_Gate.GateArray.Length)
            {

                Current_Controller_Gate.GateArray[Current_Controller_Gate.ArrayCounter].IsOpen = true;
                
                GateOpenClosedEvent.Invoke(this, new OpenClosedEvent(Current_Controller_Gate.GateArray[Current_Controller_Gate.ArrayCounter].GateNumber, Current_Controller_Gate.GateArray[Current_Controller_Gate.ArrayCounter].IsOpen));

                Current_Controller_Gate.ArrayCounter++;
            }
            else
            {
                Debug.WriteLine("Max number of Gates reached");

            }
        }
        public void CloseGate()
        {
            Current_Controller_Gate.GateArray[Current_Controller_Gate.ArrayCounter].IsOpen = false;

            CheckInOpenClosedEvent.Invoke(this, new OpenClosedEvent(Current_Controller_Gate.GateArray[Current_Controller_Gate.ArrayCounter].GateNumber, Current_Controller_Gate.GateArray[Current_Controller_Gate.ArrayCounter].IsOpen));


            if (Current_Controller_Gate.ArrayCounter != 0)
            {
                Current_Controller_Gate.ArrayCounter--;
            }
        }




        
    }
        
}
