using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TV_Backend.Models;
using Microsoft.Extensions.Configuration;

namespace TV_Backend.Controllers.Login
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _sanTsgApiBaseUrl;

        public LoginController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _sanTsgApiBaseUrl = configuration["SanTsgApi:BaseUrl"];
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            // Artık base URL'i appsettings.json'dan alıyoruz
            var apiUrl = $"{_sanTsgApiBaseUrl}authenticationservice/login";

            // Request'i JSON'a çevir
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // API'ye POST isteği gönder
            var response = await client.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Giriş başarısız.");
            }

            // Response'u oku ve modele deserialize et
            var responseContent = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent);

            return Ok(loginResponse);
        }
    }
}
