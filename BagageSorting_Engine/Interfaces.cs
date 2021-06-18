using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSorting_Engine
{
    public interface IStartProcess
    {
        void StartProcess();
    }
    public interface IItemsAtLocation
    {
        public void ItemsAtLocation(BagageItem[] conveyorArray);
    }
    public interface ISortCheckIn
    {
        void SortCheckIn(BagageItem[] bagageList, BagageItem[] conveyor);
    }
    public interface ISortArray
    {
        void Sorting(BagageItem[] conveyor);
    }
    public interface IMoveArray
    {
        BagageItem[] MoveArray(BagageItem[] conveyorArray);
    }

    public interface IAddBagageToArray
    {
        public bool AddToBagageArray(BagageItem bagageItem);
    }

    public interface IRemoveFromBagageArray
    {
        public BagageItem RemoveFromBagageArray();
    }

}
