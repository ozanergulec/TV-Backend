namespace TV_Backend.Models.Booking.BeginTransaction
{
    public class BeginTransactionWithReservationRequest
    {
        public string Currency { get; set; } = string.Empty;
        public string Culture { get; set; } = string.Empty;
        public string ReservationNumber { get; set; } = string.Empty;
    }
}