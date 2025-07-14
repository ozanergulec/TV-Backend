using System;
using System.Collections.Generic;
using TV_Backend.Models.Login;

namespace TV_Backend.Models.Booking.BeginTransaction
{
    public class BeginTransactionResponse
    {
        public BeginTransactionHeader Header { get; set; } = new BeginTransactionHeader();
        public BeginTransactionBody Body { get; set; } = new BeginTransactionBody();
    }

    public class BeginTransactionHeader
    {
        public string RequestId { get; set; } = string.Empty;
        public bool Success { get; set; }
        public DateTime ResponseTime { get; set; }
        public List<BeginTransactionMessage> Messages { get; set; } = new List<BeginTransactionMessage>();
    }

    public class BeginTransactionMessage
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public int MessageType { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class BeginTransactionBody
    {
        public string TransactionId { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }
        public ReservationData ReservationData { get; set; } = new ReservationData();
        public int Status { get; set; }
        public int TransactionType { get; set; }
    }

    public class ReservationData
    {
        public List<Traveller> Travellers { get; set; } = new List<Traveller>();
        public ReservationInfo ReservationInfo { get; set; } = new ReservationInfo();
        public List<BookingService> Services { get; set; } = new List<BookingService>();
        public PaymentDetail PaymentDetail { get; set; } = new PaymentDetail();
        public List<object> Invoices { get; set; } = new List<object>();
    }

    public class Traveller
    {
        public string TravellerId { get; set; } = string.Empty;
        public int Type { get; set; }
        public int Title { get; set; }
        public List<TitleOption> AvailableTitles { get; set; } = new List<TitleOption>();
        public AcademicTitle? AcademicTitle { get; set; }
        public List<AcademicTitle> AvailableAcademicTitles { get; set; } = new List<AcademicTitle>();
        public bool IsLeader { get; set; }
        public DateTime BirthDate { get; set; }
        public Nationality Nationality { get; set; } = new Nationality();
        public string IdentityNumber { get; set; } = string.Empty;
        public PassportInfo PassportInfo { get; set; } = new PassportInfo();
        public TravellerAddress Address { get; set; } = new TravellerAddress();
        public object DestinationAddress { get; set; } = new object();
        public List<TravellerService> Services { get; set; } = new List<TravellerService>();
        public int OrderNumber { get; set; }
        public DateTime BirthDateFrom { get; set; }
        public DateTime BirthDateTo { get; set; }
        public List<string> RequiredFields { get; set; } = new List<string>();
        public List<object> Documents { get; set; } = new List<object>();
        public int PassengerType { get; set; }
        public Dictionary<string, string> AdditionalFields { get; set; } = new Dictionary<string, string>();
        public List<object> InsertFields { get; set; } = new List<object>();
        public int Status { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? LeaderEmail { get; set; }
    }

    public class TitleOption
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class AcademicTitle
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class Nationality
    {
        public string TwoLetterCode { get; set; } = string.Empty;
    }

    public class PassportInfo
    {
        public DateTime ExpireDate { get; set; }
        public DateTime IssueDate { get; set; }
        public string CitizenshipCountryCode { get; set; } = string.Empty;
    }

    public class TravellerAddress
    {
        public ContactPhone? ContactPhone { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public AddressCity City { get; set; } = new AddressCity();
        public AddressCountry Country { get; set; } = new AddressCountry();
    }

    public class ContactPhone
    {
        // Boş object olarak bırakıldı
    }

    public class AddressCity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class AddressCountry
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class TravellerService
    {
        public string Id { get; set; } = string.Empty;
        public int Type { get; set; }
        public ServicePrice Price { get; set; } = new ServicePrice();
        public int PassengerType { get; set; }
    }

    public class ServicePrice
    {
        public double Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
    }

    public class ReservationInfo
    {
        public string BookingNumber { get; set; } = string.Empty;
        public Agency Agency { get; set; } = new Agency();
        public AgencyUser AgencyUser { get; set; } = new AgencyUser();
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Note { get; set; } = string.Empty;
        public ServicePrice SalePrice { get; set; } = new ServicePrice();
        public ServicePrice SupplementDiscount { get; set; } = new ServicePrice();
        public ServicePrice PassengerEB { get; set; } = new ServicePrice();
        public ServicePrice AgencyEB { get; set; } = new ServicePrice();
        public ServicePrice PassengerAmountToPay { get; set; } = new ServicePrice();
        public ServicePrice AgencyAmountToPay { get; set; } = new ServicePrice();
        public ServicePrice Discount { get; set; } = new ServicePrice();
        public ServicePrice AgencyBalance { get; set; } = new ServicePrice();
        public ServicePrice PassengerBalance { get; set; } = new ServicePrice();
        public Commission AgencyCommission { get; set; } = new Commission();
        public Commission BrokerCommission { get; set; } = new Commission();
        public Commission AgencySupplementCommission { get; set; } = new Commission();
        public ServicePrice PromotionAmount { get; set; } = new ServicePrice();
        public ServicePrice PriceToPay { get; set; } = new ServicePrice();
        public ServicePrice AgencyPriceToPay { get; set; } = new ServicePrice();
        public ServicePrice PassengerPriceToPay { get; set; } = new ServicePrice();
        public ServicePrice TotalPrice { get; set; } = new ServicePrice();
        public int ReservationStatus { get; set; }
        public int ConfirmationStatus { get; set; }
        public int PaymentStatus { get; set; }
        public List<object> Documents { get; set; } = new List<object>();
        public List<object> OtherDocuments { get; set; } = new List<object>();
        public ReservableInfo ReservableInfo { get; set; } = new ReservableInfo();
        public int PaymentFrom { get; set; }
        public LocationInfo DepartureCountry { get; set; } = new LocationInfo();
        public LocationInfo DepartureCity { get; set; } = new LocationInfo();
        public LocationInfo ArrivalCountry { get; set; } = new LocationInfo();
        public LocationInfo ArrivalCity { get; set; } = new LocationInfo();
        public DateTime CreateDate { get; set; }
        public Dictionary<string, string> AdditionalFields { get; set; } = new Dictionary<string, string>();
        public string AdditionalCode1 { get; set; } = string.Empty;
        public string AdditionalCode2 { get; set; } = string.Empty;
        public string AdditionalCode3 { get; set; } = string.Empty;
        public string AdditionalCode4 { get; set; } = string.Empty;
        public double AgencyDiscount { get; set; }
        public bool HasAvailablePromotionCode { get; set; }
    }

    public class Agency
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public LocationInfo Country { get; set; } = new LocationInfo();
        public AgencyAddress Address { get; set; } = new AgencyAddress();
        public bool OwnAgency { get; set; }
        public bool AceExport { get; set; }
    }

    public class AgencyAddress
    {
        public LocationInfo Country { get; set; } = new LocationInfo();
        public LocationInfo City { get; set; } = new LocationInfo();
        public List<string> AddressLines { get; set; } = new List<string>();
        public string ZipCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class AgencyUser
    {
        public Office Office { get; set; } = new Office();
        public Operator Operator { get; set; } = new Operator();
        public Market Market { get; set; } = new Market();
        public AgencyInfo Agency { get; set; } = new AgencyInfo();
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }

    public class Office
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class Operator
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool AgencyCanDiscountCommission { get; set; }
    }

    public class Market
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class AgencyInfo
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool OwnAgency { get; set; }
        public bool AceExport { get; set; }
    }

    public class Commission
    {
        public double Percent { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
    }

    public class ReservableInfo
    {
        public bool Reservable { get; set; }
    }

    public class LocationInfo
    {
        public string Code { get; set; } = string.Empty;
        public string InternationalCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Type { get; set; }
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string ParentId { get; set; } = string.Empty;
        public string CountryId { get; set; } = string.Empty;
        public int Provider { get; set; }
        public bool IsTopRegion { get; set; }
        public string Id { get; set; } = string.Empty;
    }

    public class BookingService
    {
        public int OrderNumber { get; set; }
        public string Note { get; set; } = string.Empty;
        public LocationInfo DepartureCountry { get; set; } = new LocationInfo();
        public LocationInfo DepartureCity { get; set; } = new LocationInfo();
        public LocationInfo ArrivalCountry { get; set; } = new LocationInfo();
        public LocationInfo ArrivalCity { get; set; } = new LocationInfo();
        public ServiceDetails ServiceDetails { get; set; } = new ServiceDetails();
        public string PartnerServiceId { get; set; } = string.Empty;
        public bool IsMainService { get; set; }
        public bool IsRefundable { get; set; }
        public bool Bundle { get; set; }
        public List<CancellationPolicy> CancellationPolicies { get; set; } = new List<CancellationPolicy>();
        public List<object> Documents { get; set; } = new List<object>();
        public string EncryptedServiceNumber { get; set; } = string.Empty;
        public List<object> PriceBreakDowns { get; set; } = new List<object>();
        public double Commission { get; set; }
        public ReservableInfo ReservableInfo { get; set; } = new ReservableInfo();
        public int Unit { get; set; }
        public List<object> ConditionalSpos { get; set; } = new List<object>();
        public int ConfirmationStatus { get; set; }
        public int ServiceStatus { get; set; }
        public int ProductType { get; set; }
        public bool CreateServiceTypeIfNotExists { get; set; }
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
        public ServicePrice Price { get; set; } = new ServicePrice();
        public bool IncludePackage { get; set; }
        public bool Compulsory { get; set; }
        public bool IsExtraService { get; set; }
        public int Provider { get; set; }
        public List<string> Travellers { get; set; } = new List<string>();
        public bool ThirdPartyRecord { get; set; }
        public int RecordId { get; set; }
        public Dictionary<string, string> AdditionalFields { get; set; } = new Dictionary<string, string>();
    }

    public class ServiceDetails
    {
        public string ServiceId { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public HotelDetail HotelDetail { get; set; } = new HotelDetail();
        public int Night { get; set; }
        public string Room { get; set; } = string.Empty;
        public string Board { get; set; } = string.Empty;
        public string Accom { get; set; } = string.Empty;
        public GeoLocation GeoLocation { get; set; } = new GeoLocation();
    }

    public class HotelDetail
    {
        public HotelAddress Address { get; set; } = new HotelAddress();
        public LocationInfo TransferLocation { get; set; } = new LocationInfo();
        public int StopSaleGuaranteed { get; set; }
        public int StopSaleStandart { get; set; }
        public GeoLocation Geolocation { get; set; } = new GeoLocation();
        public LocationInfo Location { get; set; } = new LocationInfo();
        public LocationInfo Country { get; set; } = new LocationInfo();
        public LocationInfo City { get; set; } = new LocationInfo();
        public string Thumbnail { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class HotelAddress
    {
        public List<string?> AddressLines { get; set; } = new List<string?>();
    }

    public class GeoLocation
    {
        public string Longitude { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
    }

    public class CancellationPolicy
    {
        public DateTime BeginDate { get; set; }
        public DateTime DueDate { get; set; }
        public ServicePrice Price { get; set; } = new ServicePrice();
        public int Provider { get; set; }
    }

    public class PaymentDetail
    {
        public List<PaymentPlan> PaymentPlan { get; set; } = new List<PaymentPlan>();
        public List<object> PaymentInfo { get; set; } = new List<object>();
    }

    public class PaymentPlan
    {
        public int PaymentNo { get; set; }
        public DateTime DueDate { get; set; }
        public PaymentPrice Price { get; set; } = new PaymentPrice();
        public bool PaymentStatus { get; set; }
    }

    public class PaymentPrice
    {
        public double Percent { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}