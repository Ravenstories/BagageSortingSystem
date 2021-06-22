using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.ViewModels;

namespace BagageSorting_Engine.TransportersAndSorters
{
    public class CheckInToConveyor : IStartProcess
    {

        ConveyorBelt conveyorBelt = new ConveyorBelt();
        private CheckIn checkIn; 
        public CheckInToConveyor(CheckIn checkIn)
        {
            this.checkIn = checkIn;
        }

        ProgramSession session = new ProgramSession();

        public void StartProcess()
        {
            /*while (checkIn.IsOpen == true)
            {
            }*/
            Thread.Sleep(Random.rndNum.Next(2000, 10000));
            Transport(conveyorBelt.Conveyor);
            Thread.Sleep(Random.rndNum.Next(2000, 10000));

        }

        public void Transport(BagageItem[] conveyor)
        {
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

            lock (ConveyorBelt.ConveyorLock)
            {
                // This might be very redundant
                if (itemToMove == null)
                {
                    Monitor.Wait(ConveyorBelt.ConveyorLock);
                }

                Debug.WriteLine(itemToMove.Name + " Is trying to be moved to Conveyor");

                //Event
                conveyorBelt.Conveyor[ConveyorBelt.ConveyorCounter] = itemToMove;
                ConveyorBelt.ConveyorCounter++;

                //session.ItemMovedToConveyor(itemToMove);
               

                ItemsAtLocation(conveyor);

                //ItemsAtLocation(ProgramSession.Conveyor);
                
                Monitor.PulseAll(ConveyorBelt.ConveyorLock);
                Thread.Sleep(100);


            }
        }
        public void ItemsAtLocation(BagageItem[] conveyorArray)
        {
            Debug.WriteLine("Items at static conveyor: ");
            for (int i = 0; i < conveyorArray.Length; i++)
            {
                if (conveyorArray[i] != null)
                {
                    Debug.WriteLine(conveyorArray[i].Name + ", " + conveyorArray[i].PassengerNumber);
                }
            }
            Debug.WriteLine("\n");

        }

        public void ItemsAtLocation(ObservableCollection<BagageItem> conveyorArray)
        {
            Debug.WriteLine("Items at observable conveyor: ");
            for (int i = 0; i < conveyorArray.Count; i++)
            {
                if (conveyorArray[i] != null)
                {
                    Debug.WriteLine(conveyorArray[i].Name + ", " + conveyorArray[i].PassengerNumber);
                }
            }
            Debug.WriteLine("\n");
        }
    }
}




