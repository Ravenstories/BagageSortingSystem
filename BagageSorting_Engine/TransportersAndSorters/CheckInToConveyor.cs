using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.TransportersAndSorters
{
    
    class CheckInToConveyor : IStartProcess
    {
        ConveyorBelt conveyorBelt = new ConveyorBelt();
        private CheckIn checkIn; 
        public CheckInToConveyor(CheckIn checkIn)
        {
            this.checkIn = checkIn;
        }

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
                
                conveyor[ConveyorBelt.ConveyorCounter] = itemToMove;
                ConveyorBelt.ConveyorCounter++;

                Monitor.PulseAll(ConveyorBelt.ConveyorLock);
                Thread.Sleep(100);
                
            }
        }
    }
}




