
namespace BagageSorting_Engine.Factories
{
    class DestinationNames
    {
        //Destinations for random generation of passenger bagage and planes

        private static string[] arrayOfDestinations = 
        { 
            "Copenhagen", 
            "Helsinki", 
            "New York", 
            "Berlin", 
            "Dubai", 
            "Paris", 
            "Madrid", 
            "Rome", 
            "Washington", 
            "Warsaw", 
            "Stockholm", 
            "Oslo", 
            "Prague", 
            "Hongkong", 
            "London", 
            "Tokyo"
        };

        public static string[] ArrayOfDestinations { get => arrayOfDestinations; set => arrayOfDestinations = value; }
    }
}
