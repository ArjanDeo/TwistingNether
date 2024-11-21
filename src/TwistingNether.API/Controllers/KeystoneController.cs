using Microsoft.AspNetCore.Mvc;
using Pathoschild.Http.Client;
using System.Net;
using TwistingNether.Core.Services;
using TwistingNether.DataAccess.BattleNet.WoW.Keystone.Affixes.Media;

namespace TwistingNether.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeystoneController(IKeystoneService keystoneService) : ControllerBase
    {
        private readonly IKeystoneService _keystoneService = keystoneService;

        [HttpGet("get-affix-media")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WoWAffixMediaDto>> GetAffixMedia(int id)
        {
            try
            {
                return await _keystoneService.GetAffixMedia(id);
            } catch (ApiException ex)
            {
                if (ex.Status == HttpStatusCode.NotFound)
                {
                    return NotFound();
                } else
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
