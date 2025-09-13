using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Pathoschild.Http.Client;
using TwistingNether.Core.Services;
using TwistingNether.DataAccess.TwistingNether.Character;

namespace TwistingNether.API.Controllers
{
    [Route("api/character")]
    [ApiController]
    public class CharacterController(ICharacterService characterService, IAppCache appCache) : ControllerBase
    {
        private readonly ICharacterService _characterService = characterService;
        private readonly IAppCache _appCache = appCache;

        [HttpGet("getCharacter")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CharacterModel>> GetCharacter([FromQuery] CharacterRequestModel character)
        {
            character.Name = character.Name.ToLower();
            character.Realm = character.Realm.ToLower();
            character.Region = character.Region.ToLower();

            return await _appCache.GetOrAddAsync<ActionResult<CharacterModel>>($"GetCharacter_{character.Name}_{character.Realm}_{character.Region}", async () =>
            {
                try
                {
                    return Ok(await _characterService.GetCharacter(character));
                }
                catch (ApiException ex)
                {
                    return BadRequest($"Failed to get character: {await ex.Response.AsString()}");
                }
            });
           
        }

        [HttpGet("pingCharacter")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PingCharacter([FromQuery] CharacterRequestModel character)
        {
            dynamic? pingChar = await _characterService.PingCharacter(character);
            return pingChar != null ? Ok(pingChar) : NotFound("Character not found.");

        }

        [HttpGet("getCompletedQuests")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCharacterCompletedQuests([FromQuery] CharacterRequestModel character)
        {
            var quests = await _characterService.GetCharacterCompletedQuests(character);
            return quests != null ? Ok(quests) : NotFound("Character or quests not found.");
        }

        [HttpPut("updateCharacter")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCharacter([FromBody] CharacterRequestModel character)
        {
            character.Name = character.Name.ToLower();
            character.Realm = character.Realm.ToLower();
            character.Region = character.Region.ToLower();

            var characterCache = await _appCache.GetAsync<ActionResult<CharacterModel>>($"GetCharacter_{character.Name}_{character.Realm}_{character.Region}");

            if (characterCache == null) return NotFound("Character not found."); else
            _appCache.Remove($"{nameof(GetCharacter)}_{character.Name}_{character.Realm}_{character.Region}");

            return NoContent();
        }

    }
}
