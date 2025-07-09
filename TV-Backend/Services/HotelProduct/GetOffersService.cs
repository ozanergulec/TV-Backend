using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TV_Backend.Models.HotelProduct.getOffers;
using TV_Backend.Services;

namespace TV_Backend.Services.HotelProduct
{
    public class GetOffersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SanTsgTokenService _tokenService;
        private readonly string _baseUrl;

        public GetOffersService(IHttpClientFactory httpClientFactory, SanTsgTokenService tokenService, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
            _baseUrl = configuration["SanTsgApi:BaseUrl"];
        }

        public async Task<GetOffersResponse> GetOffersAsync(GetOffersRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var token = await _tokenService.GetTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", token);
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync($"{_baseUrl}productservice/getoffers", content);
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetOffersResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
