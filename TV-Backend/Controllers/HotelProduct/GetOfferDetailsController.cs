using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TV_Backend.Models.HotelProduct.getOfferDetails;
using TV_Backend.Services.HotelProduct;
using System.Text.Json.Serialization;

namespace TV_Backend.Controllers.HotelProduct
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GetOfferDetailsController : ControllerBase
    {
        private readonly GetOfferDetailsService _getOfferDetailsService;

        public GetOfferDetailsController(GetOfferDetailsService getOfferDetailsService)
        {
            _getOfferDetailsService = getOfferDetailsService;
        }

        [HttpPost]
        public async Task<IActionResult> GetOfferDetails([FromBody] GetOfferDetailsRequest request)
        {
            if (request == null || request.OfferIds == null || request.OfferIds.Count == 0)
                return BadRequest("OfferIds is required.");

            var result = await _getOfferDetailsService.GetOfferDetailsAsync(request);
            return Ok(result);
        }
    }
}
