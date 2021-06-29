using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BagageSorting_Engine.TransportersAndSorters;
using BagageSorting_Engine.Events;


namespace BagageSorting_Engine.Models
{
    public class CheckIn : IAddBagageToArray, IRemoveFromBagageArray
    {
        //Locks
        public object CheckInLock = new object();

        //Variables 
        private BagageItem[] _bagageArray = new BagageItem[1];
        private int _checkInNumber;
        private int _bagageArrayIndex = 0;
        private bool _isOpen = false;
        public CheckInToConveyor checkInToConveyor;

        //Properties
        public BagageItem[] BagageArray 
        { 
            get => _bagageArray; 
            set 
            {
                _bagageArray = value;
            }
        }
        public int BagageArrayIndex { get => _bagageArrayIndex; set => _bagageArrayIndex = value; }
        public bool IsOpen 
        { 
            get => _isOpen;
            set 
            { 
                _isOpen = value;
            } 
        }

        public int CheckInNumber { get => _checkInNumber; set => _checkInNumber = value; }

        //Constructor
        public CheckIn()
        {
            checkInToConveyor = new CheckInToConveyor(this);
        }

        //Methods
        public bool AddToBagageArray(BagageItem bagageItem)
        {
            if (BagageArrayIndex < BagageArray.Length)
            {
                BagageArray[BagageArrayIndex] = bagageItem;
                BagageArrayIndex++;
                return true;
            }

            return false;
        }
        public BagageItem RemoveFromBagageArray()
        {
            BagageItem bagageItem = BagageArray[0];
            for (int i = 1; i < BagageArray.Length; i++)
            {
                BagageArray[i - 1] = BagageArray[i];
            }
            BagageArray[BagageArray.Length - 1] = null;
            BagageArrayIndex--;
            return bagageItem;

        }

    }
}
