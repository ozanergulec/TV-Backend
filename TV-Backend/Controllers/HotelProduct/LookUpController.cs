using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TV_Backend.Services.HotelProduct;

namespace TV_Backend.Controllers.HotelProduct
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupController : ControllerBase
    {
        private readonly LookupService _lookupService;

        public LookupController(LookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("nationalities")]
        public async Task<IActionResult> GetNationalities()
        {
            try
            {
                var result = await _lookupService.GetNationalitiesAsync();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"API Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            try
            {
                var result = await _lookupService.GetCurrenciesAsync();
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"API Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}