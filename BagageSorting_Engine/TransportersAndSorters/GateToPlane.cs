﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.TransportersAndSorters

{
    class GateToPlane
    {
        private Gate gate;
        public GateToPlane(Gate gate)
        {
            this.gate = gate;
        }

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
                Monitor.PulseAll(gate.GateLock);
            }
            
        }
    }
}
