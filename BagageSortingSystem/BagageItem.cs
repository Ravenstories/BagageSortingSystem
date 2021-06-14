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
        private int bagageNumber;
        private int flightNumber;
        private int terminalNumber;
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
        public int TerminalNumber
        {
            get { return terminalNumber; }
            set { terminalNumber = value; }
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

        public BagageItem(int passengerNumber, string name, int bagageNumber, int flightNumber, int terminalNumber, int timeCheckIn, int timeSorted, int timeBoarded)
        {
            PassengerNumber = passengerNumber;
            Name = name;
            BagageNumber = bagageNumber;
            FlightNumber = flightNumber;
            TerminalNumber = terminalNumber;
            TimeCheckIn = timeCheckIn;
            TimeSorted = timeSorted;
            TimeBoarded = timeBoarded;
        }

        public BagageItem Clone()
        {
            return new BagageItem(PassengerNumber, Name, BagageNumber, FlightNumber, TerminalNumber, TimeCheckIn, TimeSorted, TimeBoarded);
        }
    }
}
