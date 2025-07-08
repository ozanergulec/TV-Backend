using System.Collections.Generic;
using TV_Backend.Models.HotelProduct.autoComplete;
using System.Text.Json.Serialization;

namespace TV_Backend.Models.HotelProduct.priceSearch
{
    public class PriceSearchResponse
    {
        public PriceSearchHeader header { get; set; }
        public PriceSearchBody body { get; set; }
    }

    public class PriceSearchHeader
    {
        public string requestId { get; set; }
        public bool success { get; set; }
        public List<PriceSearchMessage> messages { get; set; }
    }

    public class PriceSearchMessage
    {
        public long id { get; set; }
        public string code { get; set; }
        public int messageType { get; set; }
        public string message { get; set; }
        public string provider { get; set; }
        public List<PriceSearchInnerMessage> innerMessages { get; set; }
    }

    public class PriceSearchInnerMessage
    {
        public long id { get; set; }
        public string code { get; set; }
        public int messageType { get; set; }
        public string message { get; set; }
        public string provider { get; set; }
    }

    public class PriceSearchBody
    {
        public string searchId { get; set; }
        public string expiresOn { get; set; }
        public List<Hotel> hotels { get; set; }
        public List<Tour> tours { get; set; }
        public PriceSearchDetails details { get; set; }
    }

    public class Hotel
    {
        public Geolocation geolocation { get; set; }
        [JsonPropertyName("stars")]
        [JsonConverter(typeof(StringToIntConverter))]
        public int? stars { get; set; }
        public double? rating { get; set; }
        public List<Theme> themes { get; set; }
        public List<Facility> facilities { get; set; }
        public Location location { get; set; }
        public List<Board> boards { get; set; }
        public List<BoardGroup> boardGroups { get; set; }
        public Country country { get; set; }
        public City city { get; set; }
        public GiataInfo giataInfo { get; set; }
        public Town town { get; set; }
        public Village village { get; set; }
        public List<Offer> offers { get; set; }
        public string address { get; set; }
        public int provider { get; set; }
        public string thumbnail { get; set; }
        public string thumbnailFull { get; set; }
        public Description description { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public List<Badge> badges { get; set; }
        public HotelCategory hotelCategory { get; set; }
        public bool hasThirdPartyOwnOffer { get; set; }
        public ThirdPartyInformation thirdPartyInformation { get; set; }
        public bool hasChannelManagerOffer { get; set; }
    }

    public class Geolocation
    {
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class Theme
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Board
    {
        public string id { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int provider { get; set; }
        public string id { get; set; }
    }

    public class City
    {
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int provider { get; set; }
        public string id { get; set; }
    }

    public class PriceSearchGiataInfo
    {
        public string hotelId { get; set; }
        public string destinationId { get; set; }
    }

    public class Town
    {
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int provider { get; set; }
        public string id { get; set; }
    }

    public class Village
    {
        public string name { get; set; }
        public int provider { get; set; }
        public string id { get; set; }
    }

    public class Offer
    {
        public int night { get; set; }
        public bool isAvailable { get; set; }
        public List<Room> rooms { get; set; }
        public bool isRefundable { get; set; }
        public string offerId { get; set; }
        public string checkIn { get; set; }
        public Price price { get; set; }
        public bool ownOffer { get; set; }
    }

    public class Room
    {
        public string roomId { get; set; }
        public string roomName { get; set; }
        public List<object> roomGroups { get; set; }
        public string accomId { get; set; }
        public string accomName { get; set; }
        public string boardId { get; set; }
        public string boardName { get; set; }
        public List<object> boardGroups { get; set; }
        public int allotment { get; set; }
        public int stopSaleGuaranteed { get; set; }
        public int stopSaleStandart { get; set; }
        public Price price { get; set; }
        public List<Traveller> travellers { get; set; }
    }

    public class Price
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class Traveller
    {
        public int type { get; set; }
    }

    public class Description
    {
        public string text { get; set; }
    }

    public class PriceSearchDetails
    {
        public bool enablePaging { get; set; }
        public bool getOnlyBestOffers { get; set; }
    }

    public class Facility { public bool isPriced { get; set; } public string id { get; set; } public string name { get; set; } }
    public class Location { public string name { get; set; } public string countryId { get; set; } public int provider { get; set; } public bool isTopRegion { get; set; } public bool ownLocation { get; set; } public string id { get; set; } }
    public class Badge { public string id { get; set; } public string name { get; set; } }
    public class HotelCategory { public string name { get; set; } public string id { get; set; } public string code { get; set; } }
    public class ThirdPartyInformation { public List<object> infos { get; set; } }
    public class BoardGroup { public string id { get; set; } public string name { get; set; } }
    public class Tour { }
}
