using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistingNether.DataAccess.BattleNet.OAuth;
using TwistingNether.DataAccess.TwistingNether.Character;

namespace TwistingNether.DataAccess.Configuration
{
    public static class AppConstants
    {
        public static BattleNetAccessTokenModel BattleNetAccessToken { get; set; }
        public static readonly List<CharacterClass> CharacterClasses =
        [
            new CharacterClass { Id = 1, ClassName = "Warrior", ColorCode = "#C69B6D" },
            new CharacterClass { Id = 2, ClassName = "Hunter", ColorCode = "#AAD372" },
            new CharacterClass { Id = 3, ClassName = "Mage", ColorCode = "#3FC7EB" },
            new CharacterClass { Id = 4, ClassName = "Rogue", ColorCode = "#FFF468" },
            new CharacterClass { Id = 5, ClassName = "Priest", ColorCode = "#FFFFFF" },
            new CharacterClass { Id = 6, ClassName = "Warlock", ColorCode = "#8788EE" },
            new CharacterClass { Id = 7, ClassName = "Paladin", ColorCode = "#F48CBA" },
            new CharacterClass { Id = 8, ClassName = "Druid", ColorCode = "#FF7C0A" },
            new CharacterClass { Id = 9, ClassName = "Shaman", ColorCode = "#0070DD" },
            new CharacterClass { Id = 10, ClassName = "Monk", ColorCode = "#00FF98" },
            new CharacterClass { Id = 11, ClassName = "Demon Hunter", ColorCode = "#A330C9" },
            new CharacterClass { Id = 12, ClassName = "Death Knight", ColorCode = "#C41E3A" },
            new CharacterClass { Id = 13, ClassName = "Evoker", ColorCode = "#33937F" }
        ];
    }
}
