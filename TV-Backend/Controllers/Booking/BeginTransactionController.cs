using Microsoft.AspNetCore.Mvc;
using TV_Backend.Models.Booking.BeginTransaction;
using TV_Backend.Services.Booking;

namespace TV_Backend.Controllers.Booking
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeginTransactionController : ControllerBase
    {
        private readonly IBeginTransactionService _beginTransactionService;

        public BeginTransactionController(IBeginTransactionService beginTransactionService)
        {
            _beginTransactionService = beginTransactionService;
        }

        [HttpPost("with-offer")]
        public async Task<ActionResult<BeginTransactionResponse>> BeginTransactionWithOffer([FromBody] BeginTransactionWithOfferRequest request)
        {
            try
            {
                var result = await _beginTransactionService.BeginTransactionWithOfferAsync(request);
                
                if (result != null)
                {
                    return Ok(result);
                }
                
                return BadRequest("Failed to begin transaction with offer");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("with-reservation")]
        public async Task<ActionResult<BeginTransactionResponse>> BeginTransactionWithReservation([FromBody] BeginTransactionWithReservationRequest request)
        {
            try
            {
                var result = await _beginTransactionService.BeginTransactionWithReservationAsync(request);
                
                if (result != null)
                {
                    return Ok(result);
                }
                
                return BadRequest("Failed to begin transaction with reservation");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}