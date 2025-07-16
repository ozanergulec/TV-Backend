namespace TV_Backend.Models.Booking.BeginTransaction
{
    public class CommitTransactionResponse
    {
        public CommitTransactionHeader Header { get; set; } = new CommitTransactionHeader();
        public CommitTransactionBody Body { get; set; } = new CommitTransactionBody();
    }

    public class CommitTransactionHeader
    {
        public string RequestId { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string ResponseTime { get; set; } = string.Empty;
        public List<CommitTransactionMessage> Messages { get; set; } = new List<CommitTransactionMessage>();
    }

    public class CommitTransactionMessage
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public int MessageType { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class CommitTransactionBody
    {
        public string ReservationNumber { get; set; } = string.Empty;
        public string EncryptedReservationNumber { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
    }
} 