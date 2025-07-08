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
        public bool checkAllotment { get; set; }
        public bool checkStopSale { get; set; }
        [JsonPropertyName("getOnlyDiscountedPrice")]
        public bool getOnlyDiscountedPrice { get; set; }
        public bool getOnlyBestOffers { get; set; }
        public int productType { get; set; }
        // Opsiyonel yap!
        public List<ArrivalLocation>? arrivalLocations { get; set; }
        public List<RoomCriteria> roomCriteria { get; set; }
        public string nationality { get; set; }
        public string checkIn { get; set; }
        public int night { get; set; }
        public string currency { get; set; }
        public string culture { get; set; }
        
        [JsonPropertyName("Products")]
        public List<string>? products { get; set; }
        [JsonPropertyName("productPriceCategories")]
        public List<ProductPriceCategory>? productPriceCategories { get; set; }
    }

    public class PriceSearchArrivalLocation
    {
        public string id { get; set; }
        public int type { get; set; }
    }

    public class RoomCriteria
    {
        public int adult { get; set; }
        public List<int>? childAges { get; set; }
    }

    public class ProductPriceCategory
    {
        public string product { get; set; }
        public string category { get; set; }
    }
}
