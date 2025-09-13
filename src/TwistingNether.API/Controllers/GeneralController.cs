
using Microsoft.AspNetCore.Mvc;
using Pathoschild.Http.Client;
using TwistingNether.Core.Services;
using TwistingNether.DataAccess.BattleNet.WoW.Media;
using TwistingNether.DataAccess.BattleNet.WoW.News;
using TwistingNether.DataAccess.BattleNet.WoW.Token;

namespace TwistingNether.API.Controllers
{
    [Route("api/general")]
    [ApiController]
    public class GeneralController(IGeneralService generalService) : ControllerBase
    {
        private readonly IGeneralService _generalService = generalService;

        [HttpGet("getNews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<List<WowNewsModel>>> GetNews(int? limit)
        {
            try
            {
                var res = await _generalService.GetNews(limit);

                return res == null ? NotFound("Couldn't find any news posts.") : Ok(res);
            } catch (ApiException ex)
            {
                return BadRequest($"There was an issue trying to get news posts. {await ex.Response.AsString()}");
            } catch(Exception ex)
            {
                return BadRequest($"There was an issue processing the news posts. Error: {ex.Message}");
            }
        }

        [HttpGet("getTokenPrice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<WowTokenModel>> GetTokenPrice()
        {
           return Ok(await _generalService.GetTokenPrice());
        }
        [HttpGet("getItemMedia/{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<string>> GetItemMedia(string itemId)
        {
            var media = await _generalService.GetItemMedia(itemId);
            return Ok(media.assets[0].value);
        }
    }
}
