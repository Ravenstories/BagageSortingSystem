using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagageSorting_Engine.Events;
using BagageSorting_Engine.ViewModels;

namespace BagageSorting_Engine.Models
{ 
    
    public class PlaneItem
    {
        public static List<BagageItem> CheckedOutList = new List<BagageItem>();

        private int flightNumber;
        private int gateNumber;
        private string destination;
        private bool isPlaneAtGate;
        private DateTime timeEnterGate;
        private DateTime timeSorted;
        private DateTime timeExitGate;

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
        public bool IsPlaneAtGate { get => isPlaneAtGate; set => isPlaneAtGate = value; }
        public DateTime TimeEnterGate
        {
            get { return timeEnterGate; }
            set { timeEnterGate = value; }
        }
        public DateTime TimeSorted
        {
            get { return timeSorted; }
            set { timeSorted = value; }
        }
        public DateTime TimeExitGate
        {
            get { return timeExitGate; }
            set { timeExitGate = value; }
        }


        public PlaneItem(int flightNumber, int gateNumber, string destination, bool isPlaneAtGate, DateTime timeEnterGate, DateTime timeSorted, DateTime timeExitGate)
        {
            FlightNumber = flightNumber;
            GateNumber = gateNumber;
            Destination = destination;
            IsPlaneAtGate = isPlaneAtGate;
            TimeEnterGate = timeEnterGate;
            TimeSorted = timeSorted;
            TimeExitGate = timeExitGate;
        }

        public PlaneItem Clone()
        {
            return new PlaneItem(FlightNumber, GateNumber, Destination, IsPlaneAtGate, TimeEnterGate, TimeSorted, TimeExitGate);
        }

        
    }
}
