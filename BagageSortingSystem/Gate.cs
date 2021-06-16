using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    class Gate : IAddBagageToArray, IRemoveFromBagageArray
    {
        //Locks
        public object GateLock = new object();

        //Array
        private BagageItem[] _bagageArray = new BagageItem[50];
        private int _bagageArrayIndex = 0;
        
        //Properties
        public BagageItem[] BagageArray { get => _bagageArray; set => _bagageArray = value; }
        public int BagageArrayIndex { get => _bagageArrayIndex; set => _bagageArrayIndex = value; }

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
