using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagageSortingSystem
{
    public interface ISplitter
    {
        void Splitter();
    }
    public interface IProducer
    {
        void Producer();
    }
    public interface IItemsAtLocation
    {
        public void ItemsAtLocation(BagageItem[] conveyorArray);
    }
    public interface ISortCheckIn
    {
        void SortCheckIn(List<BagageItem> bagageList, BagageItem[] conveyor);
    }
    public interface ISortArray
    {
        void SortArray(BagageItem[] conveyor);
    }
    public interface IMoveArray
    {
        BagageItem[] MoveArray(BagageItem[] conveyorArray);
    }
}
