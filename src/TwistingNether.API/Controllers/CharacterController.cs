using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Pathoschild.Http.Client;
using TwistingNether.Core.Services.Character;
using TwistingNether.Core.Services.WarcraftLogs;
using TwistingNether.DataAccess.TwistingNether.Character;

namespace TwistingNether.API.Controllers
{
    [Route("api/characters")]
    [ApiController]
    public class CharacterController(ICharacterService characterService, IAppCache appCache, IWarcraftLogsService warcraftLogsService) : ControllerBase
    {
        private readonly ICharacterService _characterService = characterService;
        private readonly IAppCache _appCache = appCache;
        private readonly IWarcraftLogsService _warcraftLogsService = warcraftLogsService;

        // GET /api/characters?name=thrall&realm=area-52&region=us
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCharacter([FromQuery] CharacterRequestModel character)
        {
            try
            {
                return Ok(await _characterService.GetBaseCharacterAsync(character));
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
        // GET /api/characters/media?name=thrall&realm=area-52&region=us
        [HttpGet("media")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCharacterMedia([FromQuery] CharacterRequestModel character)
        {
            try
            {
                return Ok(await _characterService.GetCharacterMediaAsync(character));
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

        // GET /api/characters/media?name=thrall&realm=area-52&region=us
        [HttpGet("weekly-bosses")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCharacterWeeklyBosses([FromQuery] CharacterRequestModel character)
        {
            try
            {
                return Ok(await _characterService.GetCharacterWeeklyBossesKilledAsync(character));
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
        // GET /api/characters/zone-rankings?difficulty=4&name=thrall&realm=area-52&region=us
        [HttpGet("zone-rankings")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <IActionResult> GetCharacterLogs([FromQuery] CharacterRequestModel character, [FromQuery] int? difficulty)
        {
            try
            {
                return Ok(await _warcraftLogsService.GetLatestRaidPerformanceAsync(character, difficulty));
            }
            catch (ApiException ex)
            {
                return BadRequest($"Failed to get character logs: {await ex.Response.AsString()}");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Character couldn't be found.");
            }
        }
    }
}