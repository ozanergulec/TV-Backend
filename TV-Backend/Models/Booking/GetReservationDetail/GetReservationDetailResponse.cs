using System;
using System.Collections.Generic;
using TV_Backend.Models.Booking.BeginTransaction;

namespace TV_Backend.Models.Booking.GetReservationDetail
{
    public class GetReservationDetailResponse
    {
        public GetReservationDetailHeader Header { get; set; } = new GetReservationDetailHeader();
        public GetReservationDetailBody Body { get; set; } = new GetReservationDetailBody();
    }

    public class GetReservationDetailHeader
    {
        public string RequestId { get; set; } = string.Empty;
        public bool Success { get; set; }
        public DateTime ResponseTime { get; set; }
        public List<GetReservationDetailMessage> Messages { get; set; } = new List<GetReservationDetailMessage>();
    }

    public class GetReservationDetailMessage
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public int MessageType { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class GetReservationDetailBody
    {
        public string ReservationNumber { get; set; } = string.Empty;
        public string EncryptedReservationNumber { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public ReservationData ReservationData { get; set; } = new ReservationData();
        public int Status { get; set; }
    }

    // ReservationData ve diğer sınıflar BeginTransaction'dan inherit edilecek
    // Sadece eksik alanları ekleyeceğiz
}
