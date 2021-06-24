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
        private static Gate[] gateArray = new Gate[10];
        public static Gate[] GateArray { get => gateArray; set => gateArray = value; }


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
                OnPropertyChanged();
            }
        }


        public static ObservableCollection<BagageItem> Conveyor = new ObservableCollection<BagageItem>(Current_ConveyorBelt.Conveyor);
        
        public static TrulyObservableCollection<BagageItem> CheckedOutList = new TrulyObservableCollection<BagageItem>(Current_OutGoing.OutGoingPassengerList);



        public void StartSession()
        {
            
            //Current_IncomingPassengers.AddBagageToList();
           
            //Random sorting from Passengers to CheckIn
            Thread passengerToCheckInThread = new Thread(() => PassengerToCheckIn());

            //Sorting Conveyor to Gates
            ConveyorToGates sortConveyorToGates = new ConveyorToGates();
            Thread sortConveyorToGatesThread = new Thread(new ThreadStart(sortConveyorToGates.StartProcess));

           

            //Creating Gates and CheckIns and starts checkIn threads
            Current_Controller_CheckIn.CreateCheckIns();
            
            Current_Controller_Gate.CreateGates();
            for (int i = 0; i < Controller_Gates.GateArray.Length; i++)
            {
                Thread gateToPlaneThread = new Thread(() => GateToCheckOut(i));
                gateToPlaneThread.Start();
            }

            //Thread for random generating Bagage
            Thread createRandomBagage = new Thread(() => CreateBagage());

            //Start the threads 
            passengerToCheckInThread.Start();
            sortConveyorToGatesThread.Start();
            createRandomBagage.Start();

        }

        //Events
        public event EventHandler IsOpenEvent;
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


        //From CheckInToConveyor
        public void ItemMovedToConveyor(BagageItem bagageItem)
        {
            MovedToConveyor?.Invoke(this, new ConveyorEventArgs(Conveyor, bagageItem));
            ConveyorBelt.ConveyorCounter++;

            Debug.WriteLine(bagageItem.Name + "Should be in conveyor");
        }
        
        //Moves Passenger to checkout list when properly checked out. 
        public void GateToCheckOut(int i)
        {
           gateArray[i].GateNumber
            while (gate.IsOpen == true)
            {
            Thread.Sleep(Random.rndNum.Next(2000, 10000));
            Transport();
            Thread.Sleep(Random.rndNum.Next(2000, 10000));
            }

        }

        
        //CheckIns and Gates open and close Methods
        public void OpenCheckIn()
        {
            if (Controller_CheckIn.ArrayCounter < Controller_CheckIn.CheckInArray.Length)
            {

                Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen = true;

                IsOpenEvent.Invoke(this, new CheckInOpenEvent(Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].CheckInNumber, Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen));
                
                Debug.WriteLine(Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen);
                
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
            
            IsOpenEvent.Invoke(this, new CheckInOpenEvent(Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].CheckInNumber, Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen));

            if (Controller_CheckIn.ArrayCounter != 0)
            {
                Controller_CheckIn.ArrayCounter--;

            }
        }

        public void OpenGate()
        {
            Controller_Gates.GateArray[Controller_Gates.ArrayCounter].IsOpen = true;
            if (Controller_Gates.ArrayCounter < Controller_Gates.GateArray.Length)
            {
                Controller_Gates.ArrayCounter++;
            }
            else
            {
                //Throw max reached message
            }
        }
        public void CloseGate()
        {
            Controller_Gates.GateArray[Controller_Gates.ArrayCounter].IsOpen = false;
            if (Controller_Gates.ArrayCounter != 0)
            {
                Controller_Gates.ArrayCounter--;
            }
        }




        
    }
        
}
