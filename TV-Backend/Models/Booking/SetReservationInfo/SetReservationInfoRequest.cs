using System;
using System.Collections.Generic;

namespace TV_Backend.Models.Booking.SetReservationInfo
{
    public class SetReservationInfoRequest
    {
        public string TransactionId { get; set; } = string.Empty;
        public List<SetReservationTraveller> Travellers { get; set; } = new List<SetReservationTraveller>();
        public SetReservationCustomerInfo? CustomerInfo { get; set; }
        public string ReservationNote { get; set; } = string.Empty;
        public string AgencyReservationNumber { get; set; } = string.Empty;
    }

    public class SetReservationTraveller
    {
        public string TravellerId { get; set; } = string.Empty;
        public int Type { get; set; }
        public int Title { get; set; }
        public SetReservationAcademicTitle? AcademicTitle { get; set; }
        public int PassengerType { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public bool IsLeader { get; set; }
        public DateTime BirthDate { get; set; }
        public SetReservationNationality Nationality { get; set; } = new SetReservationNationality();
        public string IdentityNumber { get; set; } = string.Empty;
        public SetReservationPassportInfo PassportInfo { get; set; } = new SetReservationPassportInfo();
        public SetReservationAddress Address { get; set; } = new SetReservationAddress();
        public object DestinationAddress { get; set; } = new object();
        public int OrderNumber { get; set; }
        public List<object> Documents { get; set; } = new List<object>();
        public List<object> InsertFields { get; set; } = new List<object>();
        public int Status { get; set; }
        public int Gender { get; set; }
    }

    public class SetReservationAcademicTitle
    {
        public int Id { get; set; }
    }

    public class SetReservationNationality
    {
        public string TwoLetterCode { get; set; } = string.Empty;
    }

    public class SetReservationPassportInfo
    {
        public string Serial { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public DateTime ExpireDate { get; set; }
        public DateTime IssueDate { get; set; }
        public string CitizenshipCountryCode { get; set; } = string.Empty;
    }

    public class SetReservationAddress
    {
        public SetReservationContactPhone? ContactPhone { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public SetReservationCity City { get; set; } = new SetReservationCity();
        public SetReservationCountry Country { get; set; } = new SetReservationCountry();
    }

    public class SetReservationContactPhone
    {
        public string CountryCode { get; set; } = string.Empty;
        public string AreaCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class SetReservationCity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class SetReservationCountry
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class SetReservationCustomerInfo
    {
        public bool IsCompany { get; set; }
        public object PassportInfo { get; set; } = new object();
        public SetReservationCustomerAddress Address { get; set; } = new SetReservationCustomerAddress();
        public SetReservationTaxInfo TaxInfo { get; set; } = new SetReservationTaxInfo();
        public int Title { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
    }

    public class SetReservationCustomerAddress
    {
        public SetReservationCity City { get; set; } = new SetReservationCity();
        public SetReservationCountry Country { get; set; } = new SetReservationCountry();
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }

    public class SetReservationTaxInfo
    {
        public string TaxOffice { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
    }
}
