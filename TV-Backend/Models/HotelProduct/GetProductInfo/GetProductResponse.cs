using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TV_Backend.Models.HotelProduct.getProductInfo
{
    public class GetProductInfoResponse
    {
        [JsonPropertyName("body")]
        public Body Body { get; set; }
        [JsonPropertyName("header")]
        public Header Header { get; set; }
    }

    public class Body
    {
        [JsonPropertyName("hotel")]
        public Hotel Hotel { get; set; }
    }

    public class Hotel
    {
        [JsonPropertyName("seasons")]
        public List<Season> Seasons { get; set; }
        [JsonPropertyName("address")]
        public Address Address { get; set; }
        [JsonPropertyName("faxNumber")]
        public string FaxNumber { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("homePage")]
        public string HomePage { get; set; }
        [JsonPropertyName("stopSaleGuaranteed")]
        public int StopSaleGuaranteed { get; set; }
        [JsonPropertyName("stopSaleStandart")]
        public int StopSaleStandart { get; set; }
        [JsonPropertyName("handicaps")]
        public List<object> Handicaps { get; set; }
        [JsonPropertyName("geolocation")]
        public Geolocation Geolocation { get; set; }
        [JsonPropertyName("themes")]
        public List<Theme> Themes { get; set; }
        [JsonPropertyName("location")]
        public Location Location { get; set; }
        [JsonPropertyName("country")]
        public Country Country { get; set; }
        [JsonPropertyName("city")]
        public City City { get; set; }
        [JsonPropertyName("giataInfo")]
        public GiataInfo GiataInfo { get; set; }
        [JsonPropertyName("hotelCategory")]
        public HotelCategory HotelCategory { get; set; }
        [JsonPropertyName("hasChannelManagerOffer")]
        public bool? HasChannelManagerOffer { get; set; }
        [JsonPropertyName("provider")]
        public int Provider { get; set; }
        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }
        [JsonPropertyName("thumbnailFull")]
        public string ThumbnailFull { get; set; }
        [JsonPropertyName("description")]
        public Description Description { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Season
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("textCategories")]
        public List<TextCategory> TextCategories { get; set; }
        [JsonPropertyName("facilityCategories")]
        public List<FacilityCategory> FacilityCategories { get; set; }
        [JsonPropertyName("mediaFiles")]
        public List<MediaFile> MediaFiles { get; set; }
    }

    public class TextCategory
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("presentations")]
        public List<Presentation> Presentations { get; set; }
    }

    public class Presentation
    {
        [JsonPropertyName("textType")]
        public int TextType { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class FacilityCategory
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("facilities")]
        public List<Facility> Facilities { get; set; }
    }

    public class Facility
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isPriced")]
        public bool IsPriced { get; set; }
    }

    public class MediaFile
    {
        [JsonPropertyName("fileType")]
        public int FileType { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("urlFull")]
        public string UrlFull { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("city")]
        public CityShort City { get; set; }
        [JsonPropertyName("addressLines")]
        public List<string> AddressLines { get; set; }
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("streetNumber")]
        public string StreetNumber { get; set; }
        [JsonPropertyName("geolocation")]
        public Geolocation Geolocation { get; set; }
    }

    public class CityShort
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("provider")]
        public int Provider { get; set; }
        [JsonPropertyName("isTopRegion")]
        public bool IsTopRegion { get; set; }
        [JsonPropertyName("ownLocation")]
        public bool OwnLocation { get; set; }
    }

    public class Geolocation
    {
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
    }

    public class Theme
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
        [JsonPropertyName("provider")]
        public int Provider { get; set; }
        [JsonPropertyName("isTopRegion")]
        public bool IsTopRegion { get; set; }
        [JsonPropertyName("ownLocation")]
        public bool? OwnLocation { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class Country
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("provider")]
        public int Provider { get; set; }
        [JsonPropertyName("isTopRegion")]
        public bool IsTopRegion { get; set; }
        [JsonPropertyName("ownLocation")]
        public bool? OwnLocation { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class City
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
        [JsonPropertyName("provider")]
        public int Provider { get; set; }
        [JsonPropertyName("isTopRegion")]
        public bool IsTopRegion { get; set; }
        [JsonPropertyName("ownLocation")]
        public bool? OwnLocation { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class GiataInfo
    {
        [JsonPropertyName("hotelId")]
        public string HotelId { get; set; }
        [JsonPropertyName("destinationId")]
        public string DestinationId { get; set; }
    }

    public class HotelCategory
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }

    public class Description
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Header
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("responseTime")]
        public string ResponseTime { get; set; }
        [JsonPropertyName("messages")]
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("messageType")]
        public int MessageType { get; set; }
        [JsonPropertyName("message")]
        public string MessageText { get; set; }
    }
}