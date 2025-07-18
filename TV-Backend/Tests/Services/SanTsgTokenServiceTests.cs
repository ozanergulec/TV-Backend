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
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly Mock<IDistributedCache> _distributedCacheMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly SanTsgTokenService _service;

        public SanTsgTokenServiceTests()
        {
            // Mock objects 
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _configurationMock = new Mock<IConfiguration>();
            _distributedCacheMock = new Mock<IDistributedCache>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            // HttpClient'ı mock handler ile
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

            // Redis cache mock setup 
            _distributedCacheMock
                .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((byte[]?)null);

            // SetStringAsync 
            _distributedCacheMock
                .Setup(x => x.SetAsync(
                    It.IsAny<string>(), 
                    It.IsAny<byte[]>(), 
                    It.IsAny<DistributedCacheEntryOptions>(), 
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Service instance oluştur
            _service = new SanTsgTokenService(
                _httpClientFactoryMock.Object, 
                _configurationMock.Object,
                _distributedCacheMock.Object);
        }

        [Fact]
        public void Constructor_ValidParameters_CreatesInstance()
        {
            // Arrange & Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task GetTokenAsync_NoCache_FetchesNewToken()
        {
            // Arrange
            var tokenValue = "test-token-123";
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
                    token = tokenValue,
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
            result.Should().Be($"Bearer {tokenValue}");

            // Cache'e set edildiğini doğrula
            _distributedCacheMock.Verify(
                x => x.SetAsync(
                    "san_tsg_token",
                    It.IsAny<byte[]>(),
                    It.IsAny<DistributedCacheEntryOptions>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetTokenAsync_WithCache_ReturnsCachedToken()
        {
            // Arrange
            var cachedToken = "Bearer cached-token-123";
            var tokenBytes = Encoding.UTF8.GetBytes(cachedToken);

            _distributedCacheMock
                .Setup(x => x.GetAsync("san_tsg_token", It.IsAny<CancellationToken>()))
                .ReturnsAsync(tokenBytes);

            // Act
            var result = await _service.GetTokenAsync();

            // Assert
            result.Should().Be(cachedToken);

            // HTTP call yapılmadığını doğrula
            _httpMessageHandlerMock
                .Protected()
                .Verify<Task<HttpResponseMessage>>(
                    "SendAsync",
                    Times.Never(),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>());
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
            await Assert.ThrowsAsync<JsonException>(() => _service.GetTokenAsync());
        }
    }
} 