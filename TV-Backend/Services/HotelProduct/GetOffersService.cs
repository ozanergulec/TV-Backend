using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TV_Backend.Models.HotelProduct.getOffers;
using TV_Backend.Services;
using StackExchange.Redis;

namespace TV_Backend.Services.HotelProduct
{
    public class GetOffersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISanTsgTokenService _tokenService;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IConnectionMultiplexer _redis;

        public GetOffersService(IHttpClientFactory httpClientFactory, ISanTsgTokenService tokenService, IConfiguration configuration, IConnectionMultiplexer redis)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
            _baseUrl = configuration["SanTsgApi:BaseUrl"];
            _redis = redis;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<GetOffersResponse> GetOffersAsync(GetOffersRequest request)
        {
            var db = _redis.GetDatabase();
            var cacheKey = $"offers:{JsonSerializer.Serialize(request, _jsonOptions)}";
            var cached = await db.StringGetAsync(cacheKey);
            if (!string.IsNullOrEmpty(cached))
            {
                return JsonSerializer.Deserialize<GetOffersResponse>(cached, _jsonOptions)!;
            }
            var client = _httpClientFactory.CreateClient();
            var token = await _tokenService.GetTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", token);
            var json = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_baseUrl}productservice/getoffers", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            await db.StringSetAsync(cacheKey, responseContent, TimeSpan.FromMinutes(10));
            return JsonSerializer.Deserialize<GetOffersResponse>(responseContent, _jsonOptions)!;
        }
    }
}
