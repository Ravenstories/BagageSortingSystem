using System;
using System.Threading;
using BagageSorting_Engine.TransportersAndSorters;

namespace BagageSorting_Engine.ViewModels
{
    public class ProgramSession 
    {
        public void StartSession()
        {
            
            IncomingPassengers incomingPassengers = new IncomingPassengers();
            incomingPassengers.AddBagageToList();
           
            //Random sorting from Passengers to CheckIn
            PassengersToCheckIn passengerToCheckIn = new PassengersToCheckIn();
            Thread passengerToCheckInThread = new Thread(new ThreadStart(passengerToCheckIn.StartProcess));

            //Sorting Conveyor to Gates
            ConveyorToGates sortConveyorToGates = new ConveyorToGates();
            Thread sortConveyorToGatesThread = new Thread(new ThreadStart(sortConveyorToGates.StartProcess));

            // Print list of passengers at the beginning
            foreach (BagageItem item in incomingPassengers.PassengersToCheckInList)
            {
                Console.WriteLine(item.Name + ", " + item.PassengerNumber);
            }

            //Creating Gates and CheckIns and starts checkIn threads
            ArrayOfCheckIns arrayOfCheckIns = new ArrayOfCheckIns();
            arrayOfCheckIns.CreateCheckIns();

            ArrayOfGates arrayOfGates = new ArrayOfGates();
            arrayOfGates.CreateGates();
            

            //Thread for random generating Bagage
            Thread generateRandomBagage = new Thread(new ThreadStart(incomingPassengers.GenerateRandomBagage));

            //Start the threads 
            passengerToCheckInThread.Start();
            sortConveyorToGatesThread.Start();
            generateRandomBagage.Start();
        }

        public void OpenCheckIn()
        {
            ArrayOfCheckIns.CheckInArray[ArrayOfCheckIns.ArrayCounter].IsOpen = true;
            if (ArrayOfCheckIns.ArrayCounter < ArrayOfCheckIns.CheckInArray.Length)
            {
                ArrayOfCheckIns.ArrayCounter++;
            }
            else
            {
                //Throw max reached message
            }
        }
        public void CloseCheckIn()
        {
            ArrayOfCheckIns.CheckInArray[ArrayOfCheckIns.ArrayCounter].IsOpen = false;
            if (ArrayOfCheckIns.ArrayCounter != 0)
            {
                ArrayOfCheckIns.ArrayCounter--;

            }
        }

        public void OpenGate()
        {
            ArrayOfGates.GateArray[ArrayOfGates.ArrayCounter].IsOpen = true;
            if (ArrayOfGates.ArrayCounter < ArrayOfGates.GateArray.Length)
            {
                ArrayOfGates.ArrayCounter++;
            }
            else
            {
                //Throw max reached message
            }
        }
        public void CloseGate()
        {
            ArrayOfGates.GateArray[ArrayOfGates.ArrayCounter].IsOpen = false;
            if (ArrayOfGates.ArrayCounter != 0)
            {
                ArrayOfGates.ArrayCounter--;
            }
        }


        ///UI Create a method for gates and check ins that can open and close them with user input. Make a boolean check on them to stop their while loop. 

    }
        
}
