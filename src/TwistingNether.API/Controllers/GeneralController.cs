using Microsoft.AspNetCore.Mvc;
using Pathoschild.Http.Client;
using TwistingNether.Core.Services;

namespace TwistingNether.API.Controllers
{
    [Route("api/general")]
    [ApiController]
    public class GeneralController(IGeneralService generalService) : ControllerBase
    {
        private readonly IGeneralService _generalService = generalService;

        // GET /api/general/news?limit=5
        [HttpGet("news")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> GetNews([FromQuery] int? limit)
        {
            try
            {
                var res = await _generalService.GetNews(limit);
                return res == null ? NotFound("Couldn't find any news posts.") : Ok(res);
            }
            catch (ApiException ex)
            {
                return BadRequest($"There was an issue trying to get news posts. {await ex.Response.AsString()}");
            }
            catch (Exception ex)
            {
                return BadRequest($"There was an issue processing the news posts. Error: {ex.Message}");
            }
        }

        // GET /api/general/token-price
        [HttpGet("token-price")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> GetTokenPrice()
        {
            return Ok(await _generalService.GetTokenPrice());
        }

        // GET /api/general/items/{itemId}/media
        [HttpGet("items/{itemId}/media")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> GetItemMedia(string itemId)
        {
            var media = await _generalService.GetItemMedia(itemId);
            return Ok(media.assets[0].value);
        }
    }
}
