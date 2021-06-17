using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    public class BagageItem
    {
        private int passengerNumber;
        private string name;
        private string destination;
        private int bagageNumber;
        private int flightNumber;
        private int gateNumber;
        private int timeCheckIn;
        private int timeSorted;
        private int timeBoarded;
        public int PassengerNumber
        {
            get { return passengerNumber; }
            set { passengerNumber = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Destination 
        {
            get { return destination; }
            set { destination = value; }
        }
        public int BagageNumber
        {
            get { return bagageNumber; }
            set { bagageNumber = value; }
        }
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
        public int TimeCheckIn
        {
            get { return timeCheckIn; }
            set { timeCheckIn = value; }
        }
        public int TimeSorted
        {
            get { return timeSorted; }
            set { timeSorted = value; }
        }
        public int TimeBoarded
        {
            get { return timeBoarded; }
            set { timeBoarded = value; }
        }


        public BagageItem(int passengerNumber, string name, string destination, int bagageNumber, int flightNumber, int terminalNumber, int timeCheckIn, int timeSorted, int timeBoarded)
        {
            PassengerNumber = passengerNumber;
            Name = name;
            Destination = destination;
            BagageNumber = bagageNumber;
            FlightNumber = flightNumber;
            GateNumber = terminalNumber;
            TimeCheckIn = timeCheckIn;
            TimeSorted = timeSorted;
            TimeBoarded = timeBoarded;
        }

        public BagageItem Clone()
        {
            return new BagageItem(PassengerNumber, Name, Destination, BagageNumber, FlightNumber, GateNumber, TimeCheckIn, TimeSorted, TimeBoarded);
        }
    }
}
