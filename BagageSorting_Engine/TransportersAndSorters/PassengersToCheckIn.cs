using System.Linq;
using System.Threading;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.Controllers;

namespace BagageSorting_Engine.TransportersAndSorters
{
    public class PassengersToCheckIn
    {
        IncomingPassengers incomingPassengers = new IncomingPassengers();
        Controller_CheckIn controller_CheckIn = new Controller_CheckIn();
        IncomingPassengers IncomingPassengers { get => incomingPassengers; set => incomingPassengers = value; }
        Controller_CheckIn Controller_CheckIn { get => controller_CheckIn; set => controller_CheckIn = value; }

        public BagageItem GrabItemFromPassengerList()
        {
            //Lock one object at the time an move a component. 
            
            BagageItem itemToMove = null;
            if (IncomingPassengers.PassengerList.Count() == 0)
            {
                Monitor.PulseAll(IncomingPassengers.PassengerLock);
                Monitor.Wait(IncomingPassengers.PassengerLock);
            }
            itemToMove = IncomingPassengers.PassengerList.FirstOrDefault();
            Monitor.PulseAll(IncomingPassengers.PassengerLock);
            return itemToMove;
        }

        public bool MoveToCheckIn(BagageItem itemToMove)
        {
            //Sorting the bagage to a random CheckIn, to simulate people arriving at different gates.
            CheckIn checkIn = Controller_CheckIn.CheckInArray[Random.rndNum.Next(0, Controller_CheckIn.CheckInArray.Length)];
            
            if (checkIn.IsOpen == false)
            {
                checkIn = Controller_CheckIn.CheckInArray[0];
            }
            
            if (checkIn.IsOpen)
            {
                lock (checkIn.CheckInLock)
                {
                    while (!checkIn.AddToBagageArray(itemToMove))
                    {
                        Thread.Sleep(5000);
                        Monitor.PulseAll(checkIn.CheckInLock);
                        Monitor.Wait(checkIn.CheckInLock);
                    }
                    IncomingPassengers.RemoveBagageFromList(itemToMove);
                    Monitor.PulseAll(checkIn.CheckInLock);
                }
                return checkIn.IsOpen;
            }
            else
            {
                return checkIn.IsOpen;
            }
        }
    }
}




