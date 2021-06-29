using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagageSorting_Engine.Factories;

namespace BagageSorting_Engine.Models
{
    class Controller_Planes
    {
        int i = 1;
        public PlaneItem PlaneToGate()
        {
            try
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
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

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
