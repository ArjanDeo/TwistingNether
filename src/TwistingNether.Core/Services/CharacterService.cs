using Pathoschild.Http.Client;
using TwistingNether.BattleNet.WoW.Character;
using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.Configuration;
using TwistingNether.DataAccess.RaiderIO;
using TwistingNether.DataAccess.TwistingNether.Character;
using TwistingNether.DataAccess.TwistingNether.Exceptions;

namespace TwistingNether.Core.Services
{
    public class CharacterService(Common common, FluentClient client) : ICharacterService
    {
        private readonly Common _common = common;
        private readonly FluentClient _client = client;
        public async Task<CharacterModel> GetCharacter(string name, string realm, string region)
        {
            name = name.ToLower();
            realm = realm.ToLower();
            region = region.ToLower();
            realm = realm.Replace(" ", "-").Replace("\'", "");
            RaiderIOCharacterDataModel raiderIOCharacterData = await _client
                             .GetAsync("https://raider.io/api/v1/characters/profile")
                             .WithArgument("region", region)
                             .WithArgument("name", name)
                             .WithArgument("realm", realm)
                             .WithArgument("fields", "raid_progression,mythic_plus_weekly_highest_level_runs,mythic_plus_scores_by_season:current,guild,gear,mythic_plus_highest_level_runs,mythic_plus_best_runs,raid_achievement_curve:nerubar-palace,raid_achievement_curve:liberation-of-undermine")
                             .As<RaiderIOCharacterDataModel>();

            bool tokenSuccessful = await _common.GetNewBattleNetAccessToken();

            if (!tokenSuccessful)
            {
                throw new TokenRetrievalException("Could not get a token from BattleNet.");
            }

            WoWCharacterMediaModel data = await _client
             .GetAsync($"https://{region}.api.blizzard.com/profile/wow/character/{realm}/{name}/character-media?namespace=profile-us&locale=en_US&:region=us")
             .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
             .As<WoWCharacterMediaModel>();

            List<CharacterMediaModel> characterMediaList = [];
            foreach (var image in data.assets)
            {
                CharacterMediaModel characterMediaModel = new()
                {
                    Link = image.value,
                    Type = image.key
                };

                characterMediaList.Add(characterMediaModel);
            }
            return new CharacterModel
            {
                RaiderIOCharacterData = raiderIOCharacterData,
                classColor = await _common.GetClassColor(raiderIOCharacterData.char_class),
                CharacterMedia = characterMediaList
            };
        }

        public async Task<List<Quest>> GetCharacterCompletedQuests(string name, string realm, string region)
        {
            await _common.GetNewBattleNetAccessToken();
            WoWCharacterCompletedQuestsModel res = await _client.GetAsync($"https://{region}.api.blizzard.com/profile/wow/character/{realm}/{name}/quests/completed")
                .WithArguments(new Dictionary<string, string>()
                    {
                        { "namespace", "profile-us" },
                        {"locale", "en_US" }
                    })
                .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
                .As<WoWCharacterCompletedQuestsModel>();
            int[] undermineQuestIds = [86775, 83151, 83096, 83176,];

            List<Quest> undermineWeeklyQuests = res.quests.Where(q => undermineQuestIds.Contains(q.id)).ToList();

            return undermineWeeklyQuests;
        }

        public async Task<object?> PingCharacter(string name, string realm, string region)
        {
            try
            {
                RaiderIOCharacterDataModel data = await _client
                      .GetAsync("https://raider.io/api/v1/characters/profile")
                      .WithArgument("region", "us")
                      .WithArgument("name", name)
                      .WithArgument("realm", realm.Replace(" ", "-"))
                      .As<RaiderIOCharacterDataModel>();


                return new
                {
                    Name = data.name,
                    Realm = data.realm,
                    Region = data.region,
                    Class = data.char_class.Replace(" ", "")
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
