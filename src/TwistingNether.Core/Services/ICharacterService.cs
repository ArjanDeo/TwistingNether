using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistingNether.DataAccess.TwistingNether.Character;

namespace TwistingNether.Core.Services
{
    public interface ICharacterService
    {
        Task<CharacterModel> GetCharacter(string name, string realm, string region);
        Task<bool> PingCharacter (string name, string realm, string region);
    }
}
