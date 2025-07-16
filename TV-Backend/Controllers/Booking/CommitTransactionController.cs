using Microsoft.AspNetCore.Mvc;
using TV_Backend.Models.Booking.BeginTransaction;
using TV_Backend.Services.Booking;

namespace TV_Backend.Controllers.Booking
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommitTransactionController : ControllerBase
    {
        private readonly ICommitTransactionService _commitTransactionService;

        public CommitTransactionController(ICommitTransactionService commitTransactionService)
        {
            _commitTransactionService = commitTransactionService;
        }

        [HttpPost]
        public async Task<ActionResult<CommitTransactionResponse>> CommitTransaction([FromBody] CommitTransactionRequest request)
        {
            // Ödeme kontrolü burada yapılmalı (dokümantasyon uyarısı)
            // ...
            try
            {
                var result = await _commitTransactionService.CommitTransactionAsync(request);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Failed to commit transaction");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 