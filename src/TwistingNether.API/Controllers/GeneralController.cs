
using Microsoft.AspNetCore.Mvc;
using Pathoschild.Http.Client;
using TwistingNether.Core.Services;
using TwistingNether.DataAccess.BattleNet.WoW.News;

namespace TwistingNether.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController(IGeneralService generalService) : ControllerBase
    {
        private readonly IGeneralService _generalService = generalService;

        [HttpGet("GetNews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<List<WowNewsModel>>> GetNews (int? limit)
        {
            try
            {
                var res = await _generalService.GetNews(limit);

                return res == null ? NotFound("Couldn't find any news posts.") : Ok(res);
            } catch (ApiException)
            {
                return BadRequest("There was an issue trying to get news posts.");
            } catch
            {
                return BadRequest("There was an issue processing the news posts.");
            }
        }
    }
}
