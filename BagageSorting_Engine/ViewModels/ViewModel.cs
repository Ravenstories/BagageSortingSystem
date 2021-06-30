using System;
using System.Collections.ObjectModel;
using System.Threading;
using BagageSorting_Engine.TransportersAndSorters;
using BagageSorting_Engine.Factories;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.Events;
using System.Diagnostics;
using BagageSorting_Engine.Controllers;


namespace BagageSorting_Engine.ViewModels
{
    public class ViewModel
    {
        #region Variables and Properties
        
        #region Incoming and outgoing bagage
        //For controlling peoples bagage intering and exiting the airport
        static IncomingPassengers incomingPassengers = new IncomingPassengers();
        static OutGoingPassengers outGoing = new OutGoingPassengers();
        static IncomingPassengers Current_IncomingPassengers { get => incomingPassengers; set { incomingPassengers = value;}}
        static OutGoingPassengers Current_OutGoing { get => outGoing; set => outGoing = value; }
        #endregion

        #region Bagage Creation and CheckIn
        //Needed objects for model connection 
        PassengersToCheckIn passengersToCheckIn = new PassengersToCheckIn();
        BagageFactory bagageFactory = new BagageFactory();
        PassengersToCheckIn Current_PassengersToCheckIn { get => passengersToCheckIn; set => passengersToCheckIn = value; }
        BagageFactory BagageFactory { get => bagageFactory; set => bagageFactory = value; }
        #endregion

        #region Controllers
        //Controllers of the CheckIn and Gate arrays. Used with accomponied threads to produce and consume. 
        Controller_CheckIn arrayOfCheckIns = new Controller_CheckIn();
        Controller_Gates arrayOfGates = new Controller_Gates();
        Controller_Planes planeController = new Controller_Planes();
        Controller_CheckIn Current_Controller_CheckIn { get => arrayOfCheckIns; set => arrayOfCheckIns = value; }
        Controller_Gates Current_Controller_Gate { get => arrayOfGates; set => arrayOfGates = value; }
        Controller_Planes PlaneController { get => planeController; set => planeController = value; }
        #endregion

        #region Planes and plane creation
        PlaneFactory planeFactory = new PlaneFactory();
        PlaneFactory PlaneFactory { get => planeFactory; set => planeFactory = value; }
        #endregion

        #region Observable Lists
        private static ObservableCollection<BagageItem> passengerList = new ObservableCollection<BagageItem>(Current_IncomingPassengers.PassengerList);
        public ObservableCollection<BagageItem> PassengerList { get => passengerList; set{passengerList = value;}}
        
        private static ObservableCollection<BagageItem> conveyor = new ObservableCollection<BagageItem>(ConveyorBelt.Conveyor);
        public static ObservableCollection<BagageItem> Conveyor { get => conveyor; set => conveyor = value; }
        
        private static ObservableCollection<BagageItem> checkedOutList = new ObservableCollection<BagageItem>(Current_OutGoing.OutGoingPassengerList);
        public static ObservableCollection<BagageItem> CheckedOutList { get => checkedOutList; set => checkedOutList = value; }

        #endregion

        #endregion

        #region Events
        //Events - Chosen instead of Inotfify for learning purposes.
        public event EventHandler BagageCreatedAndMovedToList;
        public event EventHandler BagageRemovedFromPassengerList;
        public event EventHandler MovedToConveyor;
        public event EventHandler MovedFromConveyor;
        public event EventHandler BagageMovedToCheckOutList;
        public event EventHandler CheckInOpenClosedEvent;
        public event EventHandler GateOpenClosedEvent;
        public event EventHandler PlaneEvent;
        #endregion

        public void StartViewModel()
        {
            //Building planes and add them to list, ready for use
            PlaneFactory.AddPlanesToList();

            //Random sorting from Passengers to CheckIn
            Thread passengerToCheckInThread = new Thread(() => PassengerToCheckIn());

            //Sorting Conveyor to Gates
            ConveyorToGates sortConveyorToGates = new ConveyorToGates();
            Thread sortConveyorToGatesThread = new Thread(() => ConveyorToGates());

            //Thread for random generating Bagage
            Thread createRandomBagage = new Thread(() => CreateBagage());
            
            //Creating Gates and CheckIns and starts checkIn threads
            Current_Controller_CheckIn.CreateCheckIns();
            Current_Controller_Gate.CreateGates();

            //GateThreads created here for the view to register change. 
            for (int i = 0; i < Current_Controller_CheckIn.CheckInArray.Length; i++)
            {
                Thread checkInToConveyorThread = new Thread(() => CheckInToConveyor(i));
                checkInToConveyorThread.Start();
                Thread.Sleep(20);
            }
            for (int i = 0; i < Current_Controller_Gate.GateArray.Length; i++)
            {
                Thread gateToPlaneThread = new Thread(() => GateToCheckOut(i));
                gateToPlaneThread.Start();
                Thread.Sleep(20);
            }

            //Start the threads 
            passengerToCheckInThread.Start();
            sortConveyorToGatesThread.Start();
            createRandomBagage.Start();
        }

        #region Methods - Check how much code can be moved to models
        
        //Create passenger bagage and moves them to a random check in. 
        private void CreateBagage()
        {
            while (true)
            {
                lock (IncomingPassengers.PassengerLock)
                {
                    BagageItem bagageItem = BagageFactory.CreateRandomBagage();
                    Current_IncomingPassengers.AddBagageToList(bagageItem);
                    BagageCreatedAndMovedToList?.Invoke(this, new BagageEventArgs(bagageItem));
                    Monitor.PulseAll(IncomingPassengers.PassengerLock);
                }
                Thread.Sleep(Random.rndNum.Next(500, 9000));
            }
        }
        
        //Moving Passengers from the passenger list to a random checkin
        private void PassengerToCheckIn()
        {
            BagageItem itemToMove;

            while (true)
            {
                Thread.Sleep(Random.rndNum.Next(500, 4000));
                lock (IncomingPassengers.PassengerLock)
                {
                    itemToMove = Current_PassengersToCheckIn.GrabItemFromPassengerList();
                    Monitor.PulseAll(IncomingPassengers.PassengerLock);
                }

                if (itemToMove != null)
                {
                    Thread.Sleep(Random.rndNum.Next(500, 1000));
                    
                    bool CheckIfOpen = Current_PassengersToCheckIn.MoveToCheckIn(itemToMove);
                    if (CheckIfOpen)
                    {
                        BagageRemovedFromPassengerList?.Invoke(this, new BagageEventArgs(itemToMove));
                    }
                }
                Thread.Sleep(Random.rndNum.Next(500, 1000));
            }
        }
        
        //From CheckInToConveyor 
        public void CheckInToConveyor(int i) 
        {
            int arrayCounter = i;

            while (true)
            {
                BagageItem itemToMove = null;

                Thread.Sleep(Random.rndNum.Next(1000, 2000));
                
                lock (Current_Controller_CheckIn.CheckInArray[arrayCounter].CheckInLock)
                {
                    while (!Current_Controller_CheckIn.CheckInArray[arrayCounter].IsOpen)
                    {
                        Thread.Sleep(Random.rndNum.Next(2000,16000));
                        Monitor.PulseAll(Current_Controller_CheckIn.CheckInArray[arrayCounter].CheckInLock);
                        Monitor.Wait(Current_Controller_CheckIn.CheckInArray[arrayCounter].CheckInLock); //Maybe deadlock CHANGED
                    }
                    if (Current_Controller_CheckIn.CheckInArray[arrayCounter].IsOpen)
                    {
                        itemToMove = Current_Controller_CheckIn.CheckInArray[arrayCounter].checkInToConveyor.GrabItemFromCheckIn();
                        Monitor.PulseAll(Current_Controller_CheckIn.CheckInArray[arrayCounter].CheckInLock);
                    }
                }
                
                Thread.Sleep(Random.rndNum.Next(200, 500));
                
                if (itemToMove != null)
                {
                    lock (ConveyorBelt.ConveyorLock)
                    {
                        Current_Controller_CheckIn.CheckInArray[arrayCounter].checkInToConveyor.MoveItemToConveyor(itemToMove);
                        MovedToConveyor?.Invoke(this, new BagageEventArgs(itemToMove));
                        Monitor.PulseAll(ConveyorBelt.ConveyorLock);
                    }
                }
                
                Thread.Sleep(Random.rndNum.Next(1000, 3000));
            }
        }
        
        //From ConveyorToGate 
        public void ConveyorToGates()
        {
            while (true)
            {
                BagageItem itemToMove = null;
                Thread.Sleep(Random.rndNum.Next(200, 2000));

                lock (ConveyorBelt.ConveyorLock)
                {
                    itemToMove = TransportersAndSorters.ConveyorToGates.GrabItemFromConveyor(); //Posible deadlock
                    Thread.Sleep(Random.rndNum.Next(200, 1000));
                    Monitor.PulseAll(ConveyorBelt.ConveyorLock);
                }
                
                
                lock (Current_Controller_Gate.GateArray[itemToMove.GateNumber].GateLock)
                {
                    while (!Current_Controller_Gate.GateArray[itemToMove.GateNumber].IsOpen)
                    {
                        Thread.Sleep(200);
                        Monitor.PulseAll(Current_Controller_Gate.GateArray[itemToMove.GateNumber].GateLock);
                        Monitor.Wait(Current_Controller_Gate.GateArray[itemToMove.GateNumber].GateLock);
                    }
                    if (Current_Controller_Gate.GateArray[itemToMove.GateNumber].IsOpen)
                    {
                        TransportersAndSorters.ConveyorToGates.MoveItemToGate(itemToMove);
                        MovedFromConveyor?.Invoke(this, new BagageEventArgs(itemToMove));
                    }
                    
                }
                Thread.Sleep(Random.rndNum.Next(200, 2000));
            }
        }
        
        //Moves Passenger to checkout list when properly checked out. 
        public void GateToCheckOut(int i)
        {
            int arrayCounter = i;
            Thread.Sleep(Random.rndNum.Next(20, 2000));

            PlaneItem plane = planeController.PlaneToGate();
            //Debug.WriteLine(plane.FlightNumber + " has entered the airport");
            PlaneEvent.Invoke(this, new PlaneEventArgs(plane));

            while (true)
            {
                //Evaluates if there is a plane and if not a new will appear
                if (!PlaneController.CheckPlanePresence(plane) && Current_Controller_Gate.GateArray[arrayCounter].IsOpen)
                {
                    PlaneEvent.Invoke(this, new PlaneEventArgs(plane));
                    Debug.WriteLine(plane.FlightNumber + " has left the airport");
                    
                    Current_Controller_Gate.GateArray[arrayCounter].IsOpen = false;
                    
                    Thread.Sleep(Random.rndNum.Next(2000, 8000));
                    
                    plane = planeController.PlaneToGate();
                    plane.GateNumber = Current_Controller_Gate.GateArray[arrayCounter].GateNumber;
                    
                    Current_Controller_Gate.GateArray[arrayCounter].IsOpen = true;
                    
                    //Debug.WriteLine(plane.FlightNumber + " has entered the airport");
                    PlaneEvent.Invoke(this, new PlaneEventArgs(plane));
                }

                Thread.Sleep(Random.rndNum.Next(500, 2000));
                BagageItem itemToMove = null;

                lock (Current_Controller_Gate.GateArray[arrayCounter].GateLock)
                {
                    if (! Current_Controller_Gate.GateArray[arrayCounter].IsOpen || !PlaneController.CheckPlanePresence(plane))
                    {
                        Thread.Sleep(1000);
                        Monitor.PulseAll(Current_Controller_Gate.GateArray[arrayCounter].GateLock);
                        Monitor.Wait(Current_Controller_Gate.GateArray[arrayCounter].GateLock);
                    }
                    else
                    {
                        Thread.Sleep(Random.rndNum.Next(500, 2000));
                        itemToMove = Current_Controller_Gate.GateArray[arrayCounter].gateToPlane.MoveFromGateToPlane();
                        BagageMovedToCheckOutList?.Invoke(this, new BagageEventArgs(itemToMove));
                        Monitor.PulseAll(Current_Controller_Gate.GateArray[arrayCounter].GateLock);
                    }
                }
                Thread.Sleep(Random.rndNum.Next(1000, 5000));
            }
        }

        # region CheckIns and Gates open and close Methods
        //Methods for oppening and closing of gates and checkins
        public void OpenCheckIn()
        {
            if (Controller_CheckIn.ArrayCounter < Current_Controller_CheckIn.CheckInArray.Length)
            {
                Current_Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen = true;
                CheckInOpenClosedEvent.Invoke(this, new OpenClosedEvent(Current_Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].CheckInNumber, Current_Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen));
                
                lock (Current_Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].CheckInLock)
                {
                    Monitor.PulseAll(Current_Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].CheckInLock);
                }

                if (Controller_CheckIn.ArrayCounter < Current_Controller_CheckIn.CheckInArray.Length -1)
                {
                    Controller_CheckIn.ArrayCounter++;
                }
            }
        }
        public void CloseCheckIn()
        {
            Current_Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen = false;
            
            CheckInOpenClosedEvent.Invoke(this, new OpenClosedEvent(Current_Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].CheckInNumber, Current_Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen));

            if (Controller_CheckIn.ArrayCounter != 0)
            {
                Controller_CheckIn.ArrayCounter--;
            }
        }
        public void OpenGate()
        {

            if (Controller_Gates.ArrayCounter < Current_Controller_Gate.GateArray.Length)
            {

                Current_Controller_Gate.GateArray[Controller_Gates.ArrayCounter].IsOpen = true;
                
                GateOpenClosedEvent.Invoke(this, new OpenClosedEvent(Current_Controller_Gate.GateArray[Controller_Gates.ArrayCounter].GateNumber, Current_Controller_Gate.GateArray[Controller_Gates.ArrayCounter].IsOpen));

                lock (Current_Controller_Gate.GateArray[Controller_Gates.ArrayCounter].GateLock)
                {
                    Monitor.PulseAll(Current_Controller_Gate.GateArray[Controller_Gates.ArrayCounter].GateLock);
                }


                if (Controller_Gates.ArrayCounter < Current_Controller_Gate.GateArray.Length -1)
                {
                    Controller_Gates.ArrayCounter++;
                }
            }
        }
        public void CloseGate()
        {
            Current_Controller_Gate.GateArray[Controller_Gates.ArrayCounter].IsOpen = false;

            GateOpenClosedEvent.Invoke(this, new OpenClosedEvent(Current_Controller_Gate.GateArray[Controller_Gates.ArrayCounter].GateNumber, Current_Controller_Gate.GateArray[Controller_Gates.ArrayCounter].IsOpen));


            if (Controller_Gates.ArrayCounter != 0)
            {
                Controller_Gates.ArrayCounter--;
            }
        }
        #endregion Open and Close Gates and CheckIns

        #endregion
    }
}
