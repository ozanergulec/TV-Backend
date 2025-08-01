using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TV_Backend.Models.HotelProduct;
using TV_Backend.Models.HotelProduct.autoComplete;
using TV_Backend.Models.HotelProduct.checkin;
using TV_Backend.Models.HotelProduct.priceSearch;
using TV_Backend.Services;
using TV_Backend.Models.HotelProduct.getProductInfo;
using TV_Backend.Models.HotelProduct.getOffers;
using StackExchange.Redis;

namespace TV_Backend.Services.HotelProduct
{
    public class HotelProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISanTsgTokenService _tokenService;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _jsonOptions; 
        private readonly IConnectionMultiplexer _redis;

        public HotelProductService(IHttpClientFactory httpClientFactory, ISanTsgTokenService tokenService, IConfiguration configuration, IConnectionMultiplexer redis)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
            _baseUrl = configuration["SanTsgApi:BaseUrl"];
            _redis = redis;
            // case sensitivity çözümü için
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public async Task<GetArrivalAutocompleteResponse> GetArrivalAutocompleteAsync(GetArrivalAutocompleteRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var token = await _tokenService.GetTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", token);
            
            
            var json = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync($"{_baseUrl}productservice/getarrivalautocomplete", content);
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync();
            
            // deserialize için
            return JsonSerializer.Deserialize<GetArrivalAutocompleteResponse>(responseContent, _jsonOptions);
        }

        public async Task<GetCheckInDatesResponse?> GetCheckInDatesAsync(GetCheckInDatesRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var token = await _tokenService.GetTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", token);
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_baseUrl}productservice/getcheckindates", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<GetCheckInDatesResponse>(responseContent);
        }

        public async Task<PriceSearchResponse?> PriceSearchAsync(PriceSearchRequest request)
        {
            var db = _redis.GetDatabase();
            var cacheKey = $"pricesearch:{JsonSerializer.Serialize(request, _jsonOptions)}";
            var cached = await db.StringGetAsync(cacheKey);
            if (!string.IsNullOrEmpty(cached))
            {
                return JsonSerializer.Deserialize<PriceSearchResponse>(cached, _jsonOptions)!;
            }
            var client = _httpClientFactory.CreateClient();
            var token = await _tokenService.GetTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", token);
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_baseUrl}productservice/pricesearch", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            await db.StringSetAsync(cacheKey, responseContent, TimeSpan.FromMinutes(10));
            return System.Text.Json.JsonSerializer.Deserialize<PriceSearchResponse>(responseContent)!;
        }

        public async Task<GetProductInfoResponse?> GetProductInfoAsync(GetProductInfoRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var token = await _tokenService.GetTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", token);
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_baseUrl}productservice/getproductinfo", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetProductInfoResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
