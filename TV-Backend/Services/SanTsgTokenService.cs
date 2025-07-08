using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TV_Backend.Models.Login;
using TV_Backend.Services;
using TV_Backend.Models.Login;

public class SanTsgTokenService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private string? _token;
    private DateTime _expiresOn;

    public SanTsgTokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<string> GetTokenAsync()
    {
        if (_token != null && DateTime.Now < _expiresOn)
            return _token;

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

        _token = "Bearer " + loginResponse.body.token;
        _expiresOn = DateTime.Parse(loginResponse.body.expiresOn).AddMinutes(-5);
        return _token;
    }
}