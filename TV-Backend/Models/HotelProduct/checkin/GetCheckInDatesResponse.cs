using System.Collections.Generic;

namespace TV_Backend.Models.HotelProduct.checkin
{
    public class GetCheckInDatesResponse
    {
        public GetCheckInDatesBody body { get; set; }
    }

    public class GetCheckInDatesBody
    {
        public List<string> dates { get; set; }
    }
}
