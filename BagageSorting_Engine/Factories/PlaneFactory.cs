using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.Factories
{
    class PlaneFactory
    {
        private static List<PlaneItem> _planeItemsList = new List<PlaneItem>();
        internal static List<PlaneItem> PlaneItemsList { get => _planeItemsList; set => _planeItemsList = value; }

        static Controller_Gates gateController = new Controller_Gates();
        

        //Creates Planes equal to the number of gates available. 
        static PlaneFactory()
        {
            for (int i = 0; i < gateController.GateArray.Length + 10; i++)
            {
                if (DestinationNames.ArrayOfDestinations[i] != null)
                {
                    BuildPlaneItem(10000+i, i, DestinationNames.ArrayOfDestinations[i], DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                }
                else
                {
                    //Random Destination
                    BuildPlaneItem(10000 + i, i, DestinationNames.ArrayOfDestinations[Random.rndNum.Next(0, DestinationNames.ArrayOfDestinations.Length)], DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                }
            }
        }

        private static void BuildPlaneItem(int flightNumber, int gateNumber, string destination, DateTime timeEnterGate, DateTime timeSorted, DateTime timeExitGate)
        {
            PlaneItemsList.Add(new PlaneItem(flightNumber, gateNumber, destination, timeEnterGate, timeSorted, timeExitGate));
        }

        public static PlaneItem GetPlaneItem(int flightNumber)
        {
            return PlaneItemsList.FirstOrDefault(item => item.FlightNumber == flightNumber)?.Clone();
        }
    }
}
