using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BagageSorting_Engine.Events;
using System.Threading.Tasks;

namespace BagageSorting_Engine.Models
{
    public class BagageItem : BaseNotificationClass
    {
        private int passengerNumber;
        private string name;
        private string destination;
        private int bagageNumber;
        private int flightNumber;
        private int gateNumber;
        private DateTime timeCheckIn;
        private DateTime timeSorted;
        private DateTime timeBoarded;
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
        public DateTime TimeCheckIn
        {
            get { return timeCheckIn; }
            set { timeCheckIn = value; }
        }
        public DateTime TimeSorted
        {
            get { return timeSorted; }
            set { timeSorted = value; }
        }
        public DateTime TimeBoarded
        {
            get { return timeBoarded; }
            set { timeBoarded = value; }
        }


        public BagageItem(int passengerNumber, string name, string destination, int bagageNumber, int flightNumber, int terminalNumber, DateTime timeCheckIn, DateTime timeSorted, DateTime timeBoarded)
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
