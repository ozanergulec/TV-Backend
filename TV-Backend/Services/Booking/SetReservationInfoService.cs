using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TV_Backend.Models.Booking.SetReservationInfo;

namespace TV_Backend.Services.Booking
{
    public interface ISetReservationInfoService
    {
        Task<SetReservationInfoResponse?> SetReservationInfoAsync(SetReservationInfoRequest request);
    }

    public class SetReservationInfoService : ISetReservationInfoService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly SanTsgTokenService _tokenService;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        public SetReservationInfoService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            SanTsgTokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<SetReservationInfoResponse?> SetReservationInfoAsync(SetReservationInfoRequest request)
        {
            try
            {
                var baseUrl = _configuration["SanTsgApi:BaseUrl"];
                var client = _httpClientFactory.CreateClient();

                // Token al
                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Failed to get authentication token");
                }

                client.DefaultRequestHeaders.Add("Authorization", token);

                // JSON serialize et
                var json = JsonSerializer.Serialize(request, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{baseUrl}bookingservice/setreservationinfo", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        return null;
                    }

                    var result = JsonSerializer.Deserialize<SetReservationInfoResponse>(responseContent, _jsonOptions);
                    return result;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API call failed: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in SetReservationInfoAsync: {ex.Message}", ex);
            }
        }
    }
}
