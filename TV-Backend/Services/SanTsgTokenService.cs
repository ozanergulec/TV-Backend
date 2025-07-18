using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using TV_Backend.Models.Login;

namespace TV_Backend.Services
{
    public class SanTsgTokenService : ISanTsgTokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _distributedCache;
        
        private const string CACHE_KEY = "san_tsg_token";

        public SanTsgTokenService(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration,
            IDistributedCache distributedCache)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _distributedCache = distributedCache;
        }

        public async Task<string> GetTokenAsync()
        {
            // Redis'ten token kontrol et
            var cachedToken = await _distributedCache.GetStringAsync(CACHE_KEY);
            if (!string.IsNullOrEmpty(cachedToken))
                return cachedToken;

            // Yoksa yeni token al ve cache'le
            return await RefreshTokenAsync();
        }

        private async Task<string> RefreshTokenAsync()
        {
            var loginRequest = new
            {
                Agency = _configuration["SanTsgApi:Agency"],
                User = _configuration["SanTsgApi:User"],
                Password = _configuration["SanTsgApi:Password"]
            };

            var client = _httpClientFactory.CreateClient();
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_configuration["SanTsgApi:BaseUrl"] + "authenticationservice/login", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent);
            var token = "Bearer " + loginResponse.body.token;

            // Redis'e cache'le (55 dakika)
            await _distributedCache.SetStringAsync(CACHE_KEY, token, 
                new DistributedCacheEntryOptions 
                { 
                    SlidingExpiration = TimeSpan.FromMinutes(55) 
                });

            return token;
        }
    }
}