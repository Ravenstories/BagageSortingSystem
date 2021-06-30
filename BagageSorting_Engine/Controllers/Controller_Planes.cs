using System;
using BagageSorting_Engine.Factories;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.Controllers
{
    class Controller_Planes
    {
        int i = 1;

        //PlaneToGate can be called to get a new plane, it resets when it's through the list
        public PlaneItem PlaneToGate()
        {
            PlaneItem planeItem = PlaneFactory.GetPlaneItem(10000+i);
            if (planeItem != null)
            {
                planeItem.TimeEnterGate = DateTime.Now;
                planeItem.TimeExitGate = DateTime.Now.AddSeconds(Random.rndNum.Next(15, 60));
                    
                i++;
                return planeItem;
            }
            else
            {
                i = 1;
                planeItem = PlaneFactory.GetPlaneItem(10000 + i);
                planeItem.TimeEnterGate = DateTime.Now;
                planeItem.TimeExitGate = DateTime.Now.AddSeconds(Random.rndNum.Next(15, 60));

                i++;
                return planeItem;
            }
        }

        //This checks if the plane have left the airport. 
        public bool CheckPlanePresence(PlaneItem plane)
        {
            if(DateTime.Now < plane.TimeExitGate)
            {
                plane.IsPlaneAtGate = true;
            }
            else { plane.IsPlaneAtGate = false; }
            
            return plane.IsPlaneAtGate;
        }

    }
}
