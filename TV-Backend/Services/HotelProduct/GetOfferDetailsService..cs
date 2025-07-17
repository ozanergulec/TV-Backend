using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TV_Backend.Models.HotelProduct.getOfferDetails;
using Microsoft.Extensions.Configuration;

namespace TV_Backend.Services.HotelProduct
{
    public class GetOfferDetailsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ISanTsgTokenService _tokenService;
        private readonly string _baseUrl;

        public GetOfferDetailsService(HttpClient httpClient, IConfiguration configuration, ISanTsgTokenService tokenService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _tokenService = tokenService;
            _baseUrl = _configuration["SanTsgApi:BaseUrl"];
        }

        public async Task<GetOfferDetailsResponse> GetOfferDetailsAsync(GetOfferDetailsRequest request)
        {
            var token = await _tokenService.GetTokenAsync();
            var url = _baseUrl + "productservice/getofferdetails";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequest.Headers.Add("Authorization", token);
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GetOfferDetailsResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
    }
}
