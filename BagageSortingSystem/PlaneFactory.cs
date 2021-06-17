using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    class PlaneFactory
    {
        private static List<PlaneItem> _planeItemsList = new List<PlaneItem>();
        internal static List<PlaneItem> PlaneItemsList { get => _planeItemsList; set => _planeItemsList = value; }

        static PlaneFactory()
        {
            for (int i = 0; i < Program.GateArray.Length; i++)
            {
                BuildPlaneItem(10000+i, 0, null, 0, 0, 0);
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
