using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagageSorting_Engine.Factories;

namespace BagageSorting_Engine.Models
{
    class PlaneControl
    {
        Controller_Gates gateController = new Controller_Gates();
        public void PlaneToGate()
        {
            int i = 1;
            foreach (Gate gate in gateController.GateArray)
            {
                PlaneItem planeItem = PlaneFactory.GetPlaneItem(10000+i);
                i++;
            }
            PlaneItem planeAtGateZero = PlaneFactory.GetPlaneItem(10001);

            planeAtGateZero.TimeEnterGate = DateTime.Now;
            planeAtGateZero.TimeExitGate = DateTime.Now.AddSeconds(Random.rndNum.Next(15, 30));
        }

        public void PlaneLeaving()
        {

        }
    }
}
