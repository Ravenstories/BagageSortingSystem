using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSorting_Engine
{
    class PlaneItem
    {
        private int flightNumber;
        private int gateNumber;
        private string destination;
        private int timeEnterGate;
        private int timeSorted;
        private int timeExitGate;

        public int FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }
        public int GateNumber
        {
            get { return gateNumber; }
            set { gateNumber = value; }
        }
        public string Destination 
        {
            get { return destination; }
            set { destination = value; } 
        }
        public int TimeEnterGate
        {
            get { return timeEnterGate; }
            set { timeEnterGate = value; }
        }
        public int TimeSorted
        {
            get { return timeSorted; }
            set { timeSorted = value; }
        }
        public int TimeExitGate
        {
            get { return timeExitGate; }
            set { timeExitGate = value; }
        }


        public PlaneItem(int flightNumber, int gateNumber, string destination, int timeEnterGate, int timeSorted, int timeExitGate)
        {
            FlightNumber = flightNumber;
            GateNumber = gateNumber;
            Destination = destination;
            TimeEnterGate = timeEnterGate;
            TimeSorted = timeSorted;
            TimeExitGate = timeExitGate;
        }

        public PlaneItem Clone()
        {
            return new PlaneItem(FlightNumber, GateNumber, Destination, TimeEnterGate, TimeSorted, TimeExitGate);
        }
    }
}
