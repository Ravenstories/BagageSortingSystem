using System.Threading;
using BagageSorting_Engine.Models;

namespace BagageSorting_Engine.Controllers 
{
    public class Controller_CheckIn
    {
        private static CheckIn[] checkInArray = new CheckIn[10];
        public CheckIn[] CheckInArray { get => checkInArray; set => checkInArray = value; }
        private static int _arrayCounter = 0;
        public static int ArrayCounter { get => _arrayCounter; set => _arrayCounter = value; }

        public void CreateCheckIns()
        {
            for (int i = 0; i < CheckInArray.Length; i++)
            {
                CheckInArray[i] = new CheckIn();
                CheckInArray[i].CheckInNumber = i;
                Thread.Sleep(20);
            }
        }
    }
}
