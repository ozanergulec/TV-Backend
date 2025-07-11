using System.Text.Json.Serialization;

namespace TV_Backend.Models.HotelProduct.getOfferDetails
{
    public class GetOfferDetailsRequest
    {
        [JsonPropertyName("offerIds")]
        public List<string> OfferIds { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("getProductInfo")]
        public bool GetProductInfo { get; set; }
    }
} 