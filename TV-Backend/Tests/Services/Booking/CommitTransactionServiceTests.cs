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
    public class CommitTransactionServiceTests
    {
        private readonly Mock<IHttpClientFactody> _httpClientFactoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<ISanTsgTokenService> _tokenServiceMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly CommitTransactionService _service;

        public CommitTransactionServiceTests()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _configurationMock = new Mock<IConfiguration>();
            _tokenServiceMock = new Mock<ISanTsgTokenService>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            // HttpClient mock setup
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://test-api.example.com/")
            };
            
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient);
            
            // Configuration mock
            _configurationMock.Setup(x => x["SanTsgApi:BaseUrl"]).Returns("https://test-api.example.com/");

            // Service instance
            _service = new CommitTransactionService(_httpClientFactoryMock.Object, _configurationMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public void Constructor_ValidParameters_CreatesInstance()
        {
            // Arrange & Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task CommitTransactionAsync_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var request = new CommitTransactionRequest
            {
                TransactionId = "tx-123456"
            };

            var expectedResponse = new CommitTransactionResponse
            {
                Header = new CommitTransactionHeader
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Success = true,
                    ResponseTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"),
                    Messages = new List<CommitTransactionMessage>()
                },
                Body = new CommitTransactionBody
                {
                    ReservationNumber = "BOOK123456",
                    EncryptedReservationNumber = "encrypted-book-123456",
                    TransactionId = "tx-123456"
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
            var result = await _service.CommitTransactionAsync(request);

            // Assert
            result.Should().NotBeNull();
            result!.Header.Success.Should().BeTrue();
            result.Body.ReservationNumber.Should().Be("BOOK123456");
            result.Body.TransactionId.Should().Be("tx-123456");
        }

        [Fact]
        public async Task CommitTransactionAsync_TokenServiceFails_ThrowsException()
        {
            // Arrange
            var request = new CommitTransactionRequest
            {
                TransactionId = "tx-123456"
            };

            _tokenServiceMock.Setup(x => x.GetTokenAsync()).ThrowsAsync(new Exception("Token service failed"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CommitTransactionAsync(request));
            exception.Message.Should().Contain("Error in CommitTransactionAsync");
            exception.InnerException?.Message.Should().Contain("Token service failed");
        }

        [Fact]
        public async Task CommitTransactionAsync_EmptyToken_ThrowsException()
        {
            // Arrange
            var request = new CommitTransactionRequest
            {
                TransactionId = "tx-123456"
            };

            _tokenServiceMock.Setup(x => x.GetTokenAsync()).ReturnsAsync(string.Empty);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CommitTransactionAsync(request));
            exception.Message.Should().Contain("Failed to get authentication token");
        }

        [Fact]
        public async Task CommitTransactionAsync_HttpRequestFails_ThrowsException()
        {
            // Arrange
            var request = new CommitTransactionRequest
            {
                TransactionId = "tx-123456"
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

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CommitTransactionAsync(request));
            exception.Message.Should().Contain("API call failed: BadRequest");
        }

        [Fact]
        public async Task CommitTransactionAsync_EmptyResponse_ReturnsNull()
        {
            // Arrange
            var request = new CommitTransactionRequest
            {
                TransactionId = "tx-123456"
            };

            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
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
            var result = await _service.CommitTransactionAsync(request);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CommitTransactionAsync_InvalidJson_ThrowsException()
        {
            // Arrange
            var request = new CommitTransactionRequest
            {
                TransactionId = "tx-123456"
            };

            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("invalid json", Encoding.UTF8, "application/json")
            };

            _tokenServiceMock.Setup(x => x.GetTokenAsync()).ReturnsAsync("Bearer valid-token");

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CommitTransactionAsync(request));
            exception.Message.Should().Contain("Error in CommitTransactionAsync");
            exception.InnerException.Should().BeOfType<JsonException>();
        }
    }
} 