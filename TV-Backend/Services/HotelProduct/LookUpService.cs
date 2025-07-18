using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TV_Backend.Models.HotelProduct.Lookup;
using TV_Backend.Services;
using System;
using System.Collections.Generic;
//test
namespace TV_Backend.Services.HotelProduct
{
    public class LookupService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISanTsgTokenService _tokenService;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _jsonOptions;

        public LookupService(IHttpClientFactory httpClientFactory, ISanTsgTokenService tokenService, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
            _baseUrl = configuration["SanTsgApi:BaseUrl"];
            
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<GetNationalitiesResponse> GetNationalitiesAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = await _tokenService.GetTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", token);
            
            // Boş JSON object 
            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_baseUrl}lookupservice/getnationalities", content);
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<GetNationalitiesResponse>(responseContent, _jsonOptions);
        }

        public async Task<GetCurrenciesResponse> GetCurrenciesAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var token = await _tokenService.GetTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", token);
            
            // Boş JSON object 
            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_baseUrl}lookupservice/getcurrencies", content);
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<GetCurrenciesResponse>(responseContent, _jsonOptions);
        }
    }
}
