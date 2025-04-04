using LazyCache;
using Microsoft.AspNetCore.Mvc;
using TwistingNether.Core.Services;
using TwistingNether.DataAccess.TwistingNether.Character;
using TwistingNether.DataAccess.TwistingNether.Exceptions;

namespace TwistingNether.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController(ICharacterService characterService, IAppCache appCache) : ControllerBase
    {
        private readonly ICharacterService _characterService = characterService;
        private readonly IAppCache _appCache = appCache;

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CharacterModel>> Get(string name, string realm, string region)
        {
            return await _appCache.GetOrAddAsync<ActionResult<CharacterModel>>($"GetCharacter_{name}_{realm}_{region}", async () =>
            {
                try
                {
                    return Ok(await _characterService.GetCharacter(name, realm, region));
                }
                catch (TokenRetrievalException ex)
                {
                    return BadRequest($"{ex.Message}");
                }
            });
           
        }
        [HttpGet("ping")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PingCharacter(string name, string realm, string region)
        {            
            dynamic? character = await _characterService.PingCharacter(name, realm, region);
            return character != null ? Ok(character) : NotFound("Character not found.");

        }
        [HttpGet("completedQuests")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCharacterCompletedQuests(string name, string realm, string region)
        {
            var quests = await _characterService.GetCharacterCompletedQuests(name, realm, region);
            return quests != null ? Ok(quests) : NotFound("Character or quests not found.");
        }

    }
}
