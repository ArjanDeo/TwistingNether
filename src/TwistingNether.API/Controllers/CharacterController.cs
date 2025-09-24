using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Pathoschild.Http.Client;
using TwistingNether.Core.Services.Character;
using TwistingNether.DataAccess.TwistingNether.Character;

namespace TwistingNether.API.Controllers
{
    [Route("api/characters")]
    [ApiController]
    public class CharacterController(ICharacterService characterService, IAppCache appCache) : ControllerBase
    {
        private readonly ICharacterService _characterService = characterService;
        private readonly IAppCache _appCache = appCache;

        // GET /api/characters?name=thrall&realm=area-52&region=us
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CharacterModel>> GetCharacter([FromQuery] CharacterRequestModel character)
        {
            try
            {
                return Ok(await _characterService.GetCharacter(character));
            }
            catch (ApiException ex)
            {
                return BadRequest($"Failed to get character: {await ex.Response.AsString()}");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Character couldn't be found.");
            }
        }

        // DELETE /api/characters/cache
        [HttpDelete("cache")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCharacterCache([FromBody] CharacterRequestModel character)
        {
            character.Name = character.Name.ToLower();
            character.Realm = character.Realm.ToLower();
            character.Region = character.Region.ToLower();

            var characterCache = await _appCache.GetAsync<ActionResult<CharacterModel>>(
                $"GetCharacter_{character.Name}_{character.Realm}_{character.Region}");

            if (characterCache == null)
                return NotFound("Character not found.");

            _appCache.Remove($"{nameof(GetCharacter)}_{character.Name}_{character.Realm}_{character.Region}");

            return NoContent();
        }
    }
}