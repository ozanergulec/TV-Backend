using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TV_Backend.Models.HotelProduct.getOffers;
using TV_Backend.Services.HotelProduct;
using System.Text.Json.Serialization;

namespace TV_Backend.Controllers.HotelProduct
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetOffersController : ControllerBase
    {
        private readonly GetOffersService _getOffersService;

        public GetOffersController(GetOffersService getOffersService)
        {
            _getOffersService = getOffersService;
        }

        [HttpPost]
        public async Task<IActionResult> GetOffers([FromBody] GetOffersRequest request)
        {
            try
            {
                var result = await _getOffersService.GetOffersAsync(request);
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
