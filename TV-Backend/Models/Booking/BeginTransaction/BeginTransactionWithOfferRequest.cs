using System.Collections.Generic;

namespace TV_Backend.Models.Booking.BeginTransaction
{
    public class BeginTransactionWithOfferRequest
    {
        public List<string> OfferIds { get; set; } = new List<string>();
        public string Currency { get; set; } = string.Empty;
        public string Culture { get; set; } = string.Empty;
    }
}