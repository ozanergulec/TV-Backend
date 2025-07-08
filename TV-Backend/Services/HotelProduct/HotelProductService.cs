using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TV_Backend.Models.HotelProduct;
using TV_Backend.Models.HotelProduct.autoComplete;
using TV_Backend.Models.HotelProduct.checkin;
using TV_Backend.Models.HotelProduct.priceSearch;
using TV_Backend.Services;

namespace TV_Backend.Services.HotelProduct
{
    public class HotelProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SanTsgTokenService _tokenService;
        private readonly string _baseUrl;

        public HotelProductService(IHttpClientFactory httpClientFactory, SanTsgTokenService tokenService, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
            _baseUrl = configuration["SanTsgApi:BaseUrl"];
        }

        public async Task<GetArrivalAutocompleteResponse> GetArrivalAutocompleteAsync(GetArrivalAutocompleteRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var token = await _tokenService.GetTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", token);
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_baseUrl}productservice/getarrivalautocomplete", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<GetArrivalAutocompleteResponse>(responseContent);
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
            var client = _httpClientFactory.CreateClient();
            var token = await _tokenService.GetTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", token);
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_baseUrl}productservice/pricesearch", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<PriceSearchResponse>(responseContent);
        }
    }
}
