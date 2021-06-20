using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine
{
    public interface IStartProcess
    {
        void StartProcess();
    }
    public interface IItemsAtLocation
    {
        void ItemsAtLocation(BagageItem[] conveyorArray);
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
        bool AddToBagageArray(BagageItem bagageItem);
    }

    public interface IRemoveFromBagageArray
    {
        BagageItem RemoveFromBagageArray();
    }

}
