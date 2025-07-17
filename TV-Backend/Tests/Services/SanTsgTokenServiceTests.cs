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
using TV_Backend.Models.Login;
using TV_Backend.Services;
using Xunit;

namespace TV_Backend.Tests.Services
{
    public class SanTsgTokenServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly SanTsgTokenService _service;

        public SanTsgTokenServiceTests()
        {
            // Mock objects oluştur
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _configurationMock = new Mock<IConfiguration>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            // HttpClient'ı mock handler ile oluştur
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://test-api.example.com/")
            };

            // Mock setup'ları - SanTsgApi configuration'ları
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient);
            _configurationMock.Setup(x => x["SanTsgApi:BaseUrl"]).Returns("https://test-api.example.com/");
            _configurationMock.Setup(x => x["SanTsgApi:Agency"]).Returns("testagency");
            _configurationMock.Setup(x => x["SanTsgApi:User"]).Returns("testuser");
            _configurationMock.Setup(x => x["SanTsgApi:Password"]).Returns("testpass");

            // Service instance oluştur
            _service = new SanTsgTokenService(_httpClientFactoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public void Constructor_ValidParameters_CreatesInstance()
        {
            // Arrange & Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task GetTokenAsync_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var tokenValue = "test-token-123"; // ✅ "Bearer" olmadan sadece token
            var loginResponse = new LoginResponse
            {
                header = new TV_Backend.Models.Login.Header
                {
                    requestId = Guid.NewGuid().ToString(),
                    success = true,
                    messages = new List<TV_Backend.Models.Login.Message>()
                },
                body = new TV_Backend.Models.Login.Body
                {
                    token = tokenValue, // ✅ Sadece token, "Bearer" yok
                    expiresOn = DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss"),
                    tokenId = 12345
                }
            };

            var responseJson = JsonSerializer.Serialize(loginResponse);
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _service.GetTokenAsync();

            // Assert
            result.Should().Be($"Bearer {tokenValue}"); // ✅ Service "Bearer " ekliyor
        }

        [Fact]
        public async Task GetTokenAsync_HttpRequestFails_ThrowsHttpRequestException()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act & Assert
            // ✅ Service HttpRequestException fırlatıyor
            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => _service.GetTokenAsync());
            exception.Message.Should().Contain("401");
        }

        [Fact]
        public async Task GetTokenAsync_InvalidJson_ThrowsJsonException()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("invalid json", Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act & Assert
            // ✅ Service JsonException fırlatıyor
            await Assert.ThrowsAsync<JsonException>(() => _service.GetTokenAsync());
        }
    }
} 