using Microsoft.AspNetCore.Mvc;
using TV_Backend.Models.Booking.SetReservationInfo;
using TV_Backend.Services.Booking;

namespace TV_Backend.Controllers.Booking
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetReservationInfoController : ControllerBase
    {
        private readonly ISetReservationInfoService _setReservationInfoService;

        public SetReservationInfoController(ISetReservationInfoService setReservationInfoService)
        {
            _setReservationInfoService = setReservationInfoService;
        }

        [HttpPost]
        public async Task<ActionResult<SetReservationInfoResponse>> SetReservationInfo([FromBody] SetReservationInfoRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.TransactionId))
                {
                    return BadRequest("TransactionId is required");
                }

                if (request.Travellers == null || !request.Travellers.Any())
                {
                    return BadRequest("At least one traveller is required");
                }

                var result = await _setReservationInfoService.SetReservationInfoAsync(request);
                
                if (result == null)
                {
                    return StatusCode(500, "Failed to set reservation info");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
