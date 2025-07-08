using Microsoft.AspNetCore.Mvc;
using TV_Backend.Models.HotelProduct;
using TV_Backend.Models.HotelProduct.autoComplete;
using TV_Backend.Models.HotelProduct.checkin;
using TV_Backend.Models.HotelProduct.priceSearch;
using TV_Backend.Services.HotelProduct; 

namespace TV_Backend.Controllers.HotelProduct
{
    // Hotel Product Controller
    [ApiController]
    [Route("api/[controller]")]
    public class HotelProductController : ControllerBase
    {
        private readonly HotelProductService _hotelProductService;

        public HotelProductController(HotelProductService hotelProductService)
        {
            _hotelProductService = hotelProductService;
        }

        // Get Arrival Autocomplete
        [HttpPost("get-arrival-autocomplete")]
        public async Task<IActionResult> GetArrivalAutocomplete(
            [FromBody] GetArrivalAutocompleteRequest request)
        {
            var result = await _hotelProductService.GetArrivalAutocompleteAsync(request);
            return Ok(result);
        }

        // Get Check In Dates
        [HttpPost("get-checkin-dates")]
        public async Task<IActionResult> GetCheckInDates(
            [FromBody] GetCheckInDatesRequest request)
        {
            var result = await _hotelProductService.GetCheckInDatesAsync(request);
            return Ok(result);
        }

        [HttpPost("price-search")]
        public async Task<IActionResult> PriceSearch([FromBody] PriceSearchRequest request)
        {
            var result = await _hotelProductService.PriceSearchAsync(request);
            return Ok(result);
        }
    }
}
