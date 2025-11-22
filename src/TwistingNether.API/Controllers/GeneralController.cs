using Microsoft.AspNetCore.Mvc;
using TwistingNether.Core.Services.BattleNet;
using TwistingNether.DataAccess.BattleNet.WoW.News;

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
        // GET /api/general/wow-news
        [HttpGet("wow-news")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WowNewsModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> GetWowNews()
            => Ok(await _battleNetService.GetWowNews());

        // GET /api/general/overwatch-news
        [HttpGet("overwatch-news")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OverwatchNewsModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> GetOverwatchNews()
            => Ok(await _battleNetService.GetOverwatchNews());
    }
}
