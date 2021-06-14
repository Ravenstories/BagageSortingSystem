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
    public interface IItemsOnConveyor
    {
        public void ItemsOnConveyor(BagageItem[] conveyorArray);
    }
    public interface ISortCheckIn
    {
        void SortCheckIn(List<BagageItem> bagageList, BagageItem[] conveyor);
    }
    public interface IMoveArray
    {
        BagageItem[] MoveArray(BagageItem[] conveyorArray);
    }
}
