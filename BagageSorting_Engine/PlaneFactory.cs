using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSorting_Engine
{
    class PlaneFactory
    {
        private static List<PlaneItem> _planeItemsList = new List<PlaneItem>();
        internal static List<PlaneItem> PlaneItemsList { get => _planeItemsList; set => _planeItemsList = value; }

        

        //Creates Planes equal to the number of gates available. 
        static PlaneFactory()
        {
            for (int i = 0; i < ArrayOfGates.GateArray.Length; i++)
            {
                if (DestinationNames.ArrayOfDestinations[i] != null)
                {
                    BuildPlaneItem(10000+i, i, DestinationNames.ArrayOfDestinations[i], 0, 0, 0);
                }
                else
                {
                    //Random Destination
                    System.Random rndPlane = new System.Random();
                    BuildPlaneItem(10000 + i, i, DestinationNames.ArrayOfDestinations[rndPlane.Next(0, DestinationNames.ArrayOfDestinations.Length)], 0, 0, 0);
                }
            }

        }

        private static void BuildPlaneItem(int flightNumber, int gateNumber, string destination, int timeEnterGate, int timeSorted, int timeExitGate)
        {
            PlaneItemsList.Add(new PlaneItem(flightNumber, gateNumber, destination, timeEnterGate, timeSorted, timeExitGate));
        }

        public static PlaneItem GetPlaneItem(int flightNumber)
        {
            return PlaneItemsList.FirstOrDefault(item => item.FlightNumber == flightNumber)?.Clone();
        }
    }
}
