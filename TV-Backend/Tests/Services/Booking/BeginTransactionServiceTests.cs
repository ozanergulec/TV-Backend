using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using TV_Backend.Models.Booking.BeginTransaction;
using TV_Backend.Services.Booking;
using TV_Backend.Services;
using Xunit;

namespace TV_Backend.Tests.Services.Booking
{
    public class BeginTransactionServiceTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly Mock<ISanTsgTokenService> _tokenServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly HttpClient _httpClient;
        private readonly BeginTransactionService _service;

        public BeginTransactionServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _tokenServiceMock = new Mock<ISanTsgTokenService>();
            _configurationMock = new Mock<IConfiguration>();

            // HttpClient mock setup
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://test-api.example.com/")
            };
            
            // Configuration mock
            _configurationMock.Setup(x => x["SanTsgApi:BaseUrl"]).Returns("https://test-api.example.com/");

            // Service instance
            _service = new BeginTransactionService(_httpClient, _tokenServiceMock.Object, _configurationMock.Object);
        }

        [Fact]
        public void Constructor_ValidParameters_CreatesInstance()
        {
            // Arrange & Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task BeginTransactionWithOfferAsync_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var request = new BeginTransactionWithOfferRequest
            {
                OfferIds = new List<string> { "offer1", "offer2" },
                Currency = "USD",
                Culture = "en-US"
            };

            // Gerçek response yapısına uygun mock data
            var expectedResponse = new BeginTransactionResponse
            {
                Header = new BeginTransactionHeader
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Success = true,
                    ResponseTime = DateTime.UtcNow,
                    Messages = new List<BeginTransactionMessage>()
                },
                Body = new BeginTransactionBody
                {
                    TransactionId = "tx-123456",
                    ExpiresOn = DateTime.UtcNow.AddHours(1),
                    Status = 1,
                    TransactionType = 1,
                    ReservationData = new ReservationData()
                }
            };

            var responseJson = JsonSerializer.Serialize(expectedResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });

            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
            };

            _tokenServiceMock.Setup(x => x.GetTokenAsync()).ReturnsAsync("Bearer valid-token");

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _service.BeginTransactionWithOfferAsync(request);

            // Assert
            result.Should().NotBeNull();
            result!.Header.Success.Should().BeTrue();
            result.Body.TransactionId.Should().Be("tx-123456");
        }

        [Fact]
        public async Task BeginTransactionWithOfferAsync_TokenServiceFails_ReturnsNull()
        {
            // Arrange
            var request = new BeginTransactionWithOfferRequest
            {
                OfferIds = new List<string> { "offer1", "offer2" }
            };

            _tokenServiceMock.Setup(x => x.GetTokenAsync()).ThrowsAsync(new Exception("Token service failed"));

            // Act
            var result = await _service.BeginTransactionWithOfferAsync(request);

            // Assert
            // ✅ Service exception'ı yakalıyor ve null döndürüyor (console log yazıyor)
            result.Should().BeNull();
        }

        [Fact]
        public async Task BeginTransactionWithOfferAsync_HttpRequestFails_ReturnsNull()
        {
            // Arrange
            var request = new BeginTransactionWithOfferRequest
            {
                OfferIds = new List<string> { "offer1", "offer2" }
            };

            var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Bad Request", Encoding.UTF8, "text/plain")
            };

            _tokenServiceMock.Setup(x => x.GetTokenAsync()).ReturnsAsync("Bearer valid-token");

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _service.BeginTransactionWithOfferAsync(request);

            // Assert
            // Servis hata durumunda null döndürüyor (console.writeline ile log yazıyor)
            result.Should().BeNull();
        }
    }
} 