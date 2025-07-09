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
        public DateTime responseTime { get; set; }
        public List<PriceSearchMessage> messages { get; set; }
    }

    public class PriceSearchMessage
    {
        public long id { get; set; }
        public string code { get; set; }
        public int messageType { get; set; }
        public string message { get; set; }
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
        public double? stars { get; set; }
        public double? rating { get; set; }
        public List<Theme> themes { get; set; }
        public List<Facility> facilities { get; set; }
        public Location location { get; set; }
        public List<Board> boards { get; set; }
        public List<BoardGroup> boardGroups { get; set; }
        public Country country { get; set; }
        public City city { get; set; }
        public GiataInfo giataInfo { get; set; }
        public List<Offer> offers { get; set; }
        public string address { get; set; }
        public int provider { get; set; }
        public string thumbnail { get; set; }
        public string thumbnailFull { get; set; }
        public Description description { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public List<Badge>? badges { get; set; }
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

    public class Facility
    {
        public bool isPriced { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Board
    {
        public string id { get; set; }
    }

    public class Country
    {
        [JsonPropertyName("internationalCode")]
        public string? InternationalCode { get; set; }
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        
        [JsonPropertyName("provider")]
        public int? Provider { get; set; }
        
        [JsonPropertyName("isTopRegion")]
        public bool? IsTopRegion { get; set; }
        
        [JsonPropertyName("ownLocation")]
        public bool? OwnLocation { get; set; }
    }

    public class City
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        
        [JsonPropertyName("countryId")]
        public string? CountryId { get; set; }
        
        [JsonPropertyName("provider")]
        public int? Provider { get; set; }
        
        [JsonPropertyName("isTopRegion")]
        public bool? IsTopRegion { get; set; }
        
        [JsonPropertyName("ownLocation")]
        public bool? OwnLocation { get; set; }
        
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }

    public class GiataInfo
    {
        public string hotelId { get; set; }
        public string destinationId { get; set; }
    }



    public class Offer
    {
        [JsonPropertyName("night")]
        public int? Night { get; set; }
        
        [JsonPropertyName("isAvailable")]
        public bool? IsAvailable { get; set; }
        
        [JsonPropertyName("availability")]
        public int? Availability { get; set; }
        
        [JsonPropertyName("availabilityChecked")]
        public bool? AvailabilityChecked { get; set; }
        
        [JsonPropertyName("rooms")]
        public List<Room>? Rooms { get; set; }
        
        [JsonPropertyName("isRefundable")]
        public bool? IsRefundable { get; set; }
        
        [JsonPropertyName("cancellationPolicies")]
        public List<CancellationPolicy>? CancellationPolicies { get; set; }
        
        [JsonPropertyName("thirdPartyOwnOffer")]
        public bool? ThirdPartyOwnOffer { get; set; }
        
        [JsonPropertyName("restrictions")]
        public List<object>? Restrictions { get; set; }
        
        [JsonPropertyName("warnings")]
        public List<object>? Warnings { get; set; }
        
        [JsonPropertyName("isChannelManager")]
        public bool? IsChannelManager { get; set; }
        
        [JsonPropertyName("expiresOn")]
        public DateTime? ExpiresOn { get; set; }
        
        [JsonPropertyName("offerId")]
        public string? OfferId { get; set; }
        
        [JsonPropertyName("checkIn")]
        public DateTime? CheckIn { get; set; }
        
        [JsonPropertyName("price")]
        public Price? Price { get; set; }
        
        [JsonPropertyName("ownOffer")]
        public bool? OwnOffer { get; set; }
    }

    public class Room
    {
        [JsonPropertyName("roomId")]
        public string? RoomId { get; set; }
        
        [JsonPropertyName("roomName")]
        public string? RoomName { get; set; }
        
        [JsonPropertyName("boardId")]
        public string? BoardId { get; set; }
        
        [JsonPropertyName("boardName")]
        public string? BoardName { get; set; }
        
        [JsonPropertyName("boardGroups")]
        public List<BoardGroup>? BoardGroups { get; set; }
        
        [JsonPropertyName("stopSaleGuaranteed")]
        public int StopSaleGuaranteed { get; set; }
        
        [JsonPropertyName("stopSaleStandart")]
        public int StopSaleStandart { get; set; }
        
        [JsonPropertyName("travellers")]
        public List<Traveller>? Travellers { get; set; }
        
        [JsonPropertyName("thirdPartyInformation")]
        public object? ThirdPartyInformation { get; set; }
        
        [JsonPropertyName("visiblePL")]
        public bool? VisiblePL { get; set; }
    }

    public class Price
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }
        
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class Traveller
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }
        
        [JsonPropertyName("age")]
        public int? Age { get; set; }
        
        [JsonPropertyName("nationality")]
        public string? Nationality { get; set; }
    }

    public class Description
    {
        public string text { get; set; }
    }

    public class PriceSearchDetails
    {
        public bool EnablePaging { get; set; }
        public bool? GetOnlyBestOffers { get; set; }
    }


    public class Location { public string name { get; set; } public string countryId { get; set; } public int provider { get; set; } public bool isTopRegion { get; set; } public bool ownLocation { get; set; } public string id { get; set; } }
    public class Badge { public string id { get; set; } public string name { get; set; } }
    public class HotelCategory { public string name { get; set; } public string id { get; set; } public string code { get; set; } }
    public class ThirdPartyInformation { public List<object> infos { get; set; } }
    public class BoardGroup 
    { 
        [JsonPropertyName("id")]
        public string Id { get; set; } 
        
        [JsonPropertyName("name")]
        public string Name { get; set; } 
    }
    public class Tour { }
    
    public class CancellationPolicy
    {
        [JsonPropertyName("roomNumber")]
        public string? RoomNumber { get; set; }
        
        [JsonPropertyName("dueDate")]
        public DateTime? DueDate { get; set; }
        
        [JsonPropertyName("price")]
        public Price? Price { get; set; }
        
        [JsonPropertyName("provider")]
        public int? Provider { get; set; }
    }
}
