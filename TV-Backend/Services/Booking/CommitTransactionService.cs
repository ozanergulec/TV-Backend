using System.Text;
using System.Text.Json;
using TV_Backend.Models.Booking.BeginTransaction;

namespace TV_Backend.Services.Booking
{
    public interface ICommitTransactionService
    {
        Task<CommitTransactionResponse?> CommitTransactionAsync(CommitTransactionRequest request);
    }

    public class CommitTransactionService : ICommitTransactionService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly SanTsgTokenService _tokenService;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        public CommitTransactionService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            SanTsgTokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<CommitTransactionResponse?> CommitTransactionAsync(CommitTransactionRequest request)
        {
            // Ödeme kontrolü burada yapılmalı (dokümantasyon uyarısı)
            // ...
            try
            {
                var baseUrl = _configuration["SanTsgApi:BaseUrl"];
                var client = _httpClientFactory.CreateClient();
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Failed to get authentication token");
                }
                client.DefaultRequestHeaders.Add("Authorization", token);
                var json = JsonSerializer.Serialize(request, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{baseUrl}bookingservice/committransaction", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        return null;
                    }
                    var result = JsonSerializer.Deserialize<CommitTransactionResponse>(responseContent, _jsonOptions);
                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API call failed: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CommitTransactionAsync: {ex.Message}", ex);
            }
        }
    }
} 