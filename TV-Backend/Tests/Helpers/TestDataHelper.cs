using System;
using System.Collections.Generic;
using TV_Backend.Models.Login;
using TV_Backend.Models.Booking.BeginTransaction;
using TV_Backend.Models.Booking.SetReservationInfo;

namespace TV_Backend.Tests.Helpers
{
    public static class TestDataHelper
    {
        public static LoginResponse CreateValidLoginResponse()
        {
            return new LoginResponse
            {
                header = new TV_Backend.Models.Login.Header
                {
                    requestId = Guid.NewGuid().ToString(),
                    success = true,
                    messages = new List<TV_Backend.Models.Login.Message>()
                },
                body = new TV_Backend.Models.Login.Body
                {
                    token = "Bearer test-token-123",
                    expiresOn = DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss"),
                    tokenId = 12345
                }
            };
        }

        public static BeginTransactionResponse CreateValidBeginTransactionResponse()
        {
            return new BeginTransactionResponse
            {
                Header = new BeginTransactionHeader
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Success = true,
                    ResponseTime = DateTime.UtcNow
                },
                Body = new BeginTransactionBody
                {
                    TransactionId = "test-transaction-123",
                    ExpiresOn = DateTime.UtcNow.AddHours(1),
                    ReservationData = new ReservationData()
                }
            };
        }

        public static SetReservationInfoRequest CreateValidSetReservationInfoRequest()
        {
            return new SetReservationInfoRequest
            {
                TransactionId = "test-transaction-123",
                Travellers = new List<SetReservationTraveller>
                {
                    new SetReservationTraveller
                    {
                        TravellerId = "traveller-1",
                        Name = "John",
                        Surname = "Doe",
                        IsLeader = true
                    }
                }
            };
        }
    }
} 