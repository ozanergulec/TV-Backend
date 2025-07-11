using System;
using System.Collections.Generic;

namespace TV_Backend.Models.HotelProduct.getOfferDetails
{
    public class GetOfferDetailsResponse
    {
        public Body body { get; set; }
        public Header header { get; set; }
    }

    public class Body
    {
        public List<OfferDetail> offerDetails { get; set; }
    }

    public class OfferDetail
    {
        public string expiresOn { get; set; }
        public string offerId { get; set; }
        public string checkIn { get; set; }
        public string checkOut { get; set; }
        public bool isSpecial { get; set; }
        public bool isAvailable { get; set; }
        public int availability { get; set; }
        public bool isRefundable { get; set; }
        public Price price { get; set; }
        public List<Hotel> hotels { get; set; }
        public List<CancellationPolicy> cancellationPolicies { get; set; }
        public List<PriceBreakdown> priceBreakdowns { get; set; }
        public int provider { get; set; }
        public ReservableInfo reservableInfo { get; set; }
    }

    public class Price
    {
        public double oldAmount { get; set; }
        public double percent { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class Hotel
    {
        public string faxNumber { get; set; }
        public string phoneNumber { get; set; }
        public string homePage { get; set; }
        public List<Room> rooms { get; set; }
        public int stopSaleGuaranteed { get; set; }
        public int stopSaleStandart { get; set; }
        public List<PaymentPlanInfo> paymentPlanInfo { get; set; }
        public List<object> handicaps { get; set; }
        public int stars { get; set; }
        public Location location { get; set; }
        public Country country { get; set; }
        public City city { get; set; }
        public List<Offer> offers { get; set; }
        public HotelCategory hotelCategory { get; set; }
        public string originalName { get; set; }
        public bool hasChannelManagerOffer { get; set; }
        public string code { get; set; }
        public int provider { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Room
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    public class PaymentPlanInfo
    {
        public int id { get; set; }
        public int phase { get; set; }
        public int day { get; set; }
        public int paymentTimeStatus { get; set; }
        public Price price { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int provider { get; set; }
        public bool isTopRegion { get; set; }
        public bool ownLocation { get; set; }
        public string id { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int provider { get; set; }
        public bool isTopRegion { get; set; }
        public bool ownLocation { get; set; }
        public string id { get; set; }
    }

    public class City
    {
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int provider { get; set; }
        public bool isTopRegion { get; set; }
        public bool ownLocation { get; set; }
        public string id { get; set; }
    }

    public class Offer
    {
        public int night { get; set; }
        public bool isAvailable { get; set; }
        public int availability { get; set; }
        public List<OfferRoom> rooms { get; set; }
        public bool isRefundable { get; set; }
        public List<CancellationPolicy> cancellationPolicies { get; set; }
        public bool isChannelManager { get; set; }
        public string expiresOn { get; set; }
        public string offerId { get; set; }
        public string checkIn { get; set; }
        public Price price { get; set; }
        public bool ownOffer { get; set; }
        public int provider { get; set; }
    }

    public class OfferRoom
    {
        public int partNo { get; set; }
        public string roomId { get; set; }
        public string roomName { get; set; }
        public List<RoomGroup> roomGroups { get; set; }
        public string accomId { get; set; }
        public string accomName { get; set; }
        public string boardId { get; set; }
        public string boardName { get; set; }
        public List<BoardGroup> boardGroups { get; set; }
        public int allotment { get; set; }
        public int stopSaleGuaranteed { get; set; }
        public int stopSaleStandart { get; set; }
        public Price price { get; set; }
        public List<Traveller> travellers { get; set; }
        public bool visiblePL { get; set; }
    }

    public class RoomGroup
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class BoardGroup
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Traveller
    {
        public int type { get; set; }
    }

    public class CancellationPolicy
    {
        public string roomNumber { get; set; }
        public string beginDate { get; set; }
        public string dueDate { get; set; }
        public Price price { get; set; }
        public int provider { get; set; }
    }

    public class PriceBreakdown
    {
        public string roomNumber { get; set; }
        public string date { get; set; }
        public Price price { get; set; }
    }

    public class ReservableInfo
    {
        public bool reservable { get; set; }
    }

    public class Header
    {
        public string requestId { get; set; }
        public bool success { get; set; }
        public string responseTime { get; set; }
        public List<Message> messages { get; set; }
    }

    public class Message
    {
        public int id { get; set; }
        public string code { get; set; }
        public int messageType { get; set; }
        public string message { get; set; }
    }

    public class HotelCategory
    {
        public string name { get; set; }
        public string id { get; set; }
        public string code { get; set; }
    }
}
