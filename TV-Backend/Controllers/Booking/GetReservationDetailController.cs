using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TV_Backend.Models.Booking.GetReservationDetail;
using TV_Backend.Services.Booking;

namespace TV_Backend.Controllers.Booking
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetReservationDetailController : ControllerBase
    {
        private readonly GetReservationDetailService _getReservationDetailService;

        public GetReservationDetailController(GetReservationDetailService getReservationDetailService)
        {
            _getReservationDetailService = getReservationDetailService;
        }

        [HttpPost]
        public async Task<IActionResult> GetReservationDetail([FromBody] GetReservationDetailRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ReservationNumber))
                {
                    return BadRequest("ReservationNumber is required");
                }

                var result = await _getReservationDetailService.GetReservationDetailAsync(request);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
