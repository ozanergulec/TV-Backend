using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TV_Backend.Models.HotelProduct.autoComplete
{
    public class GetArrivalAutocompleteResponse
    {
        public Header header { get; set; }
        public Body body { get; set; }
    }

    public class Header
    {
        public string requestId { get; set; }
        public bool success { get; set; }
        public List<Message> messages { get; set; }
    }

    public class Message
    {
        public long id { get; set; }
        public string code { get; set; }
        public int messageType { get; set; }
        public string message { get; set; }
    }

    public class Body
    {
        public List<Item> items { get; set; }
    }

    public class Item
    {
        public int type { get; set; }
        public Geolocation geolocation { get; set; }
        public Country country { get; set; }
        public State state { get; set; }
        public City city { get; set; }
        public GiataInfo giataInfo { get; set; }
        public int provider { get; set; }
    }

    public class Geolocation
    {
        [JsonPropertyName("longitude")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double longitude { get; set; }

        [JsonPropertyName("latitude")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double latitude { get; set; }
    }

    public class Country
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class State
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class City
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class GiataInfo
    {
        public string hotelId { get; set; }
        public string destinationId { get; set; }
    }
}
