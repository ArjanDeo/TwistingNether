using Microsoft.AspNetCore.Mvc;
using TwistingNether.Core.Services;
using TwistingNether.DataAccess.TwistingNether.Character;
using TwistingNether.DataAccess.TwistingNether.Exceptions;

namespace TwistingNether.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController(ICharacterService characterService) : ControllerBase
    {
        private readonly ICharacterService _characterService = characterService;

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CharacterModel>> Get(string name, string realm, string region)
        {
            try
            {
                return Ok (await _characterService.GetCharacter(name, realm, region));
            }
            catch (TokenRetrievalException ex)
            {
                return BadRequest($"{ex.Message}");
            }
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

    }
}
