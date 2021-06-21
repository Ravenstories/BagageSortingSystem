using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using BagageSorting_Engine.TransportersAndSorters;
using BagageSorting_Engine.Factories;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.Events;

namespace BagageSorting_Engine.ViewModels
{
    public class ProgramSession : BaseNotificationClass
    {
        static IncomingPassengers incomingPassengers = new IncomingPassengers();
        BagageFactory bagageFactory = new BagageFactory();
        static ConveyorBelt conveyorBelt = new ConveyorBelt();
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
        BagageFactory BagageFactory { get => bagageFactory; set => bagageFactory = value; }
        public static ConveyorBelt Current_ConveyorBelt { get => conveyorBelt; set => conveyorBelt = value; }
        public Controller_CheckIn Current_Controller_CheckIn { get => arrayOfCheckIns; set => arrayOfCheckIns = value; }
        public Controller_Gates Current_Controller_Gate { get => arrayOfGates; set => arrayOfGates = value; }

        
        public static TrulyObservableCollection<BagageItem> PassengerList = new TrulyObservableCollection<BagageItem>(Current_IncomingPassengers.PassengerList);
        public static ObservableCollection<BagageItem> Conveyor = new ObservableCollection<BagageItem>(Current_ConveyorBelt.Conveyor);



        public void StartSession()
        {
            
            //Current_IncomingPassengers.AddBagageToList();
           
            //Random sorting from Passengers to CheckIn
            PassengersToCheckIn passengerToCheckIn = new PassengersToCheckIn();
            Thread passengerToCheckInThread = new Thread(new ThreadStart(passengerToCheckIn.StartProcess));

            //Sorting Conveyor to Gates
            ConveyorToGates sortConveyorToGates = new ConveyorToGates();
            Thread sortConveyorToGatesThread = new Thread(new ThreadStart(sortConveyorToGates.StartProcess));

            //Creating Gates and CheckIns and starts checkIn threads
            Current_Controller_CheckIn.CreateCheckIns();

            Current_Controller_Gate.CreateGates();
            

            //Thread for random generating Bagage
            Thread createRandomBagage = new Thread(() => CreateBagage());

            //Start the threads 
            passengerToCheckInThread.Start();
            sortConveyorToGatesThread.Start();
            createRandomBagage.Start();

        }


        public event EventHandler BagageCreated;
        private void CreateBagage()
        {
            System.Random rndNmb = new System.Random();
            while (true)
            {
                Thread.Sleep(rndNmb.Next(500, 5000));
                lock (IncomingPassengers.PassengerLock)
                {
                    BagageItem bagageItem = BagageFactory.CreateRandomBagage();
                    BagageCreated?.Invoke(this, new PassengerEventArgs(PassengerList, bagageItem));

                    
                    Monitor.PulseAll(IncomingPassengers.PassengerLock);

                }
            }
        }


        public void OpenCheckIn()
        {
            Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen = true;
            if (Controller_CheckIn.ArrayCounter < Controller_CheckIn.CheckInArray.Length)
            {
                Controller_CheckIn.ArrayCounter++;
            }
            else
            {
                //Throw max reached message
            }
        }
        public void CloseCheckIn()
        {
            Controller_CheckIn.CheckInArray[Controller_CheckIn.ArrayCounter].IsOpen = false;
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
