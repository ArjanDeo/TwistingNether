using Microsoft.AspNetCore.Mvc;
using TwistingNether.Core.Services.BattleNet;

namespace TwistingNether.API.Controllers
{
    [Route("api/general")]
    [ApiController]
    public class GeneralController(IBattleNetService battleNetService) : ControllerBase
    {
        private readonly IBattleNetService _battleNetService = battleNetService;


        // GET /api/general/token-price
        [HttpGet("token-price")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> GetTokenPrice()
        {
            return Ok(await _battleNetService.GetTokenPriceAsync());
        }

    }
}
