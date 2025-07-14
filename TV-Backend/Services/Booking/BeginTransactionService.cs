using System.Text;
using System.Text.Json;
using TV_Backend.Models.Booking.BeginTransaction;

namespace TV_Backend.Services.Booking
{
    public interface IBeginTransactionService
    {
        Task<BeginTransactionResponse?> BeginTransactionWithOfferAsync(BeginTransactionWithOfferRequest request);
        Task<BeginTransactionResponse?> BeginTransactionWithReservationAsync(BeginTransactionWithReservationRequest request);
    }

    public class BeginTransactionService : IBeginTransactionService
    {
        private readonly HttpClient _httpClient;
        private readonly SanTsgTokenService _tokenService;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _jsonOptions;

        public BeginTransactionService(HttpClient httpClient, SanTsgTokenService tokenService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
            _baseUrl = configuration["SanTsgApi:BaseUrl"];
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
        }

        public async Task<BeginTransactionResponse?> BeginTransactionWithOfferAsync(BeginTransactionWithOfferRequest request)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                
                var json = JsonSerializer.Serialize(request, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", token);

                // baseUrl kullanarak doğru API endpoint'ini çağır
                var response = await _httpClient.PostAsync($"{_baseUrl}bookingservice/begintransaction", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<BeginTransactionResponse>(responseContent, _jsonOptions);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"BeginTransaction with offer failed. Status: {response.StatusCode}, Content: {errorContent}");
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BeginTransaction with offer error: {ex.Message}");
                return null;
            }
        }

        public async Task<BeginTransactionResponse?> BeginTransactionWithReservationAsync(BeginTransactionWithReservationRequest request)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                
                var json = JsonSerializer.Serialize(request, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", token);

                // baseUrl kullanarak doğru API endpoint'ini çağır
                var response = await _httpClient.PostAsync($"{_baseUrl}bookingservice/begintransaction", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<BeginTransactionResponse>(responseContent, _jsonOptions);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"BeginTransaction with reservation failed. Status: {response.StatusCode}, Content: {errorContent}");
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BeginTransaction with reservation error: {ex.Message}");
                return null;
            }
        }
    }
}