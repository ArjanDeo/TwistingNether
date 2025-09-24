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

        // HEAD /api/characters?name=thrall&realm=area-52&region=us
        // (using HEAD makes sense for a "ping")
        [HttpHead]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PingCharacter([FromQuery] CharacterRequestModel character)
        {
            var pingChar = await _characterService.PingCharacter(character);
            return pingChar ? Ok() : NotFound();
        }

        // GET /api/characters/quests?name=thrall&realm=area-52&region=us
        [HttpGet("quests")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCompletedQuests([FromQuery] CharacterRequestModel character)
        {
            var quests = await _characterService.GetCharacterCompletedQuests(character);
            return quests != null ? Ok(quests) : NotFound("Character or quests not found.");
        }

        // PUT /api/characters/cache
        [HttpPut("cache")]
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