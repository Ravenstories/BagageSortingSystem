using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.ViewModels;


namespace BagageSorting_Engine.TransportersAndSorters
{
    public class GateToPlane
    {
        private Gate gate;
        public GateToPlane(Gate gate)
        {
            this.gate = gate;
        }

        ProgramSession session = new ProgramSession();


        public void StartProcess()
        {
            /*while (gate.IsOpen == true)
            {}*/
            Thread.Sleep(Random.rndNum.Next(2000, 10000));
            Transport();
            Thread.Sleep(Random.rndNum.Next(2000, 10000));

        }

        public void Transport()
        {
            BagageItem itemToMove = null;

            
            lock (gate.GateLock)
            {
                if (gate.BagageArray[0] == null)
                {
                    Monitor.Wait(gate.GateLock);
                }

                itemToMove = gate.RemoveFromBagageArray();

                //Global CheckOut List Here

                //Event Here
                session.ItemMovedToCheckOutList(itemToMove);

                Monitor.PulseAll(gate.GateLock);
            }
            
        }
    }
}
