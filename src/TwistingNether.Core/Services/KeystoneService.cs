using Pathoschild.Http.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistingNether.DataAccess.BattleNet.WoW.Keystone.Affixes.Media;
using TwistingNether.DataAccess.Configuration;
using TwistingNether.DataAccess.TwistingNether.Exceptions;

namespace TwistingNether.Core.Services
{
    public class KeystoneService(Common common, FluentClient client) : IKeystoneService
    {
        private readonly Common _common = common;
        private readonly FluentClient _client = client;
        public async Task<WoWAffixMediaDto> GetAffixMedia(int id)
        {
                bool tokenSuccessful = await _common.GetNewBattleNetAccessToken();
                if (!tokenSuccessful)
                {
                    throw new TokenRetrievalException("Could not get a token from BattleNet.");
                }
                WoWAffixMediaModel res = await _client
                    .GetAsync($"https://us.api.blizzard.com/data/wow/media/keystone-affix/{id}")
                    .WithArgument(":region", "us")
                    .WithArgument("namespace", "static-us")
                    .WithArgument("locale", "us")
                    .WithBearerAuthentication(AppConstants.BattleNetAccessToken.access_token)
                    .As<WoWAffixMediaModel>();

                return new WoWAffixMediaDto
                {
                    id = res.id,
                    icon = res.assets[0].value
                };
            }
        }

    }
