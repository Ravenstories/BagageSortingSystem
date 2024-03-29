﻿using BagageSorting_Engine.TransportersAndSorters;

namespace BagageSorting_Engine.Models
{
    public class Gate : IAddBagageToArray, IRemoveFromBagageArray
    {
        //Locks
        public object GateLock = new object();

        //Variables
        private int _bagageArrayIndex = 0;
        private int _gateNumber;
        private bool _isOpen = false;
        private BagageItem[] _bagageArray = new BagageItem[10];
        public GateToPlane gateToPlane;

        //Properties
        public BagageItem[] BagageArray { get => _bagageArray; set => _bagageArray = value; }
        public int BagageArrayIndex { get => _bagageArrayIndex; set => _bagageArrayIndex = value; }
        public int GateNumber { get => _gateNumber; set => _gateNumber = value; }
        public bool IsOpen { get => _isOpen; set => _isOpen = value; }

        //Constructor
        public Gate()
        {
            //Thread
            gateToPlane = new GateToPlane(this);
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
