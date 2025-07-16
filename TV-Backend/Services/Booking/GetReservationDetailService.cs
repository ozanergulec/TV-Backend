using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TV_Backend.Models.Booking.GetReservationDetail;

namespace TV_Backend.Services.Booking
{
    public class GetReservationDetailService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly SanTsgTokenService _tokenService;

        public GetReservationDetailService(IHttpClientFactory httpClientFactory, IConfiguration configuration, SanTsgTokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<GetReservationDetailResponse> GetReservationDetailAsync(GetReservationDetailRequest request)
        {
            try
            {
                var token = await _tokenService.GetTokenAsync();
                var baseUrl = _configuration["SanTsgApi:BaseUrl"];
                
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("Authorization", token);
                
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await client.PostAsync($"{baseUrl}bookingservice/getreservationdetail", content);
                response.EnsureSuccessStatusCode();
                
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetReservationDetailResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                return result ?? new GetReservationDetailResponse();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"Error calling GetReservationDetail API: {ex.Message}", ex);
            }
        }
    }
}
