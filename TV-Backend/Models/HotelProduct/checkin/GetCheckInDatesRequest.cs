using System.Collections.Generic;

namespace TV_Backend.Models.HotelProduct.checkin
{
    public class GetCheckInDatesRequest
    {
        public int ProductType { get; set; }
        public bool IncludeSubLocations { get; set; }
        public object? Product { get; set; } // null gönderilebiliyor
        public List<ArrivalLocation> ArrivalLocations { get; set; }
    }

    public class ArrivalLocation
    {
        public string Id { get; set; }
        public int Type { get; set; }
    }
}