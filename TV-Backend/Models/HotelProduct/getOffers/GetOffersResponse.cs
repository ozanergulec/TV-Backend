using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TV_Backend.Models.HotelProduct;

namespace TV_Backend.Models.HotelProduct.getOffers
{
    public class GetOffersResponse
    {
        public Header Header { get; set; }
        public Body Body { get; set; }
    }

    public class Header
    {
        public string RequestId { get; set; }
        public bool Success { get; set; }
        public DateTime ResponseTime { get; set; }
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int MessageType { get; set; }
        public string MessageText { get; set; }
    }

    public class Body
    {
        public List<Offer> Offers { get; set; }
        public Information Information { get; set; }
        public string ProductId { get; set; }
        public List<RoomInfo> RoomInfos { get; set; }
    }

    public class Offer
    {
        public int Night { get; set; }
        public bool IsAvailable { get; set; }
        public int Availability { get; set; }
        public bool AvailabilityChecked { get; set; }
        public List<Room> Rooms { get; set; }
        public bool IsRefundable { get; set; }
        public List<CancellationPolicy> CancellationPolicies { get; set; }
        public List<PriceBreakdownGroup> PriceBreakdowns { get; set; }
        public bool ThirdPartyOwnOffer { get; set; }
        public List<object> Restrictions { get; set; }
        public List<object> Warnings { get; set; }
        public bool IsChannelManager { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string OfferId { get; set; }
        public DateTime CheckIn { get; set; }
        public Price Price { get; set; }
        public bool OwnOffer { get; set; }
        public int Provider { get; set; }
    }

    public class Room
    {
        public int PartNo { get; set; }
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public List<object> RoomGroups { get; set; }
        public string AccomId { get; set; }
        public string AccomName { get; set; }
        public string BoardId { get; set; }
        public string BoardName { get; set; }
        public List<BoardGroup> BoardGroups { get; set; }
        public int Allotment { get; set; }
        public int StopSaleGuaranteed { get; set; }
        public int StopSaleStandart { get; set; }
        public RoomPrice Price { get; set; }
        public List<SimpleTraveller> Travellers { get; set; }
        public bool VisiblePL { get; set; }
    }

    public class BoardGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Traveller
    {
        public int Type { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }

    public class CancellationPolicy
    {
        public string RoomNumber { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime DueDate { get; set; }
        public CancellationPrice Price { get; set; }
        public int Provider { get; set; }
    }

    public class PriceBreakdownGroup
    {
        public int ProductType { get; set; }
        public List<PriceBreakdown> PriceBreakdowns { get; set; }
    }

    public class PriceBreakdown
    {
        public string RoomNumber { get; set; }
        public DateTime Date { get; set; }
        public Price Price { get; set; }
    }

    public class Price
    {
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Amount { get; set; }
        public string Currency { get; set; }
    }

    public class Information
    {
        public int Total { get; set; }
    }

    public class RoomInfo
    {
        public List<object> Presentations { get; set; }
        public List<Facility> Facilities { get; set; }
        public List<MediaFile> MediaFiles { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class Facility
    {
        public string Name { get; set; }
    }

    public class MediaFile
    {
        public int FileType { get; set; }
        public string UrlFull { get; set; }
    }

    public class RoomPrice
    {
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double OldAmount { get; set; }
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Percent { get; set; }
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Amount { get; set; }
        public string Currency { get; set; }
    }

    public class SimpleTraveller
    {
        public int Type { get; set; }
    }

    public class CancellationPrice
    {
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Percent { get; set; }
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Amount { get; set; }
    }
}
