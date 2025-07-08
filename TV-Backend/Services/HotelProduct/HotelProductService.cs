using TV_Backend.Models.HotelProduct;

namespace TV_Backend.Services.HotelProduct
{
    public class HotelProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public HotelProductService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = configuration["SanTsgApi:BaseUrl"];
        }

        public async Task<GetArrivalAutocompleteResponse> GetArrivalAutocompleteAsync(GetArrivalAutocompleteRequest request, string token)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", token);
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_baseUrl}productservice/getarrivalautocomplete", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<GetArrivalAutocompleteResponse>(responseContent);
        }
    }
}
