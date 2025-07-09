using System;
using System.Collections.Generic;

namespace TV_Backend.Models.HotelProduct.getOffers
{
    public class GetOffersRequest
    {
        public string SearchId { get; set; }
        public string OfferId { get; set; }
        public int ProductType { get; set; }
        public string ProductId { get; set; }
        public string Currency { get; set; }
        public string Culture { get; set; }
        public bool GetRoomInfo { get; set; }
    }
}
