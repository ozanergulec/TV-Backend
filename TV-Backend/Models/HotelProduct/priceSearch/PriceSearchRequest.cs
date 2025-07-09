using System.Collections.Generic;
using TV_Backend.Models.HotelProduct.autoComplete;
using TV_Backend.Models.HotelProduct.checkin;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;

namespace TV_Backend.Models.HotelProduct.priceSearch
{
    public class PriceSearchRequest
    {
        public bool CheckAllotment { get; set; }
        public bool CheckStopSale { get; set; }
        public bool GetOnlyDiscountedPrice { get; set; }
        public bool GetOnlyBestOffers { get; set; }
        public int ProductType { get; set; }

        // Lokasyon için
        public List<PriceSearchArrivalLocation>? ArrivalLocations { get; set; }

        // Otel(ler) için
        public List<string>? Products { get; set; }
        public List<ProductPriceCategory>? ProductPriceCategories { get; set; }

        public List<RoomCriterion> RoomCriteria { get; set; }
        public string Nationality { get; set; }
        public string CheckIn { get; set; }
        public int Night { get; set; }
        public string Currency { get; set; }
        public string Culture { get; set; }
    }

    public class PriceSearchArrivalLocation
    {
        public string Id { get; set; }
        public int Type { get; set; }
    }

    public class ProductPriceCategory
    {
        public string Product { get; set; }
        public string Category { get; set; }
    }

    public class RoomCriterion
    {
        public int Adult { get; set; }
        public List<int>? ChildAges { get; set; }
    }
}
