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
        public static List<string> Realms = new()
        {
            "Alexstrasza", "Alleria", "Blackhand", "Dalaran", "Dentarg", "Galakrond", "Garona", "Ghostlands", "Goldrinn",
            "Hellscream", "Illidan", "Kael'thas", "Khadgar", "Kirin Tor", "Nemesis", "Ravencrest", "Sentinels",
            "Steamwheedle Cartel", "Stormreaver", "Terokkar", "Thrall", "Uldaman", "Whisperwind", "Zangarmarsh",
            "Agamaggan", "Andorhal", "Archimonde", "Azralon", "Azuremyst", "Baelgun", "Blackwing Lair", "Burning Legion",
            "Coilfang", "Dalvengyr", "Dark Iron", "Deathwing", "Demon Soul", "Dethecus", "Detheroc", "Doomhammer",
            "Emerald Dream", "Eredar", "Executus", "Gnomeregan", "Gorefiend", "Greymane", "Haomarush", "Jaedenar",
            "Kalecgos", "Lethon", "Lightninghoof", "Maelstrom", "Moonrunner", "Ravenholdt", "Sargeras", "Scilla",
            "Shadowmoon", "Shattered Halls", "Shattered Hand", "Spinebreaker", "Staghelm", "Tanaris", "The Underbog",
            "The Venture Co", "Tol Barad", "Twisting Nether", "Ursin", "Wildhammer", "Zuluhed", "Altar of Storms",
            "Alterac Mountains", "Anetheron", "Anvilmar", "Argent Dawn", "Arthas", "Azgalor", "Azshara", "Balnazzar",
            "Black Dragonflight", "Bleeding Hollow", "Blood Furnace", "Bloodhoof", "Dawnbringer", "Destromath", "Durotan",
            "Duskwood", "Elune", "Exodar", "Gallywix", "Gilneas", "Gorgonnash", "Grizzly Hills", "Gul'dan", "Lothar",
            "Madoran", "Magtheridon", "Malfurion", "Mannoroth", "Medivh", "Nazjatar", "Skullcrusher", "Stormrage",
            "The Forgotten Coast", "The Scryers", "Thunderlord", "Trollbane", "Undermine", "Warsong", "Ysera", "Ysondre",
            "Zul'jin", "Aerie Peak", "Aggramar", "Area 52", "Arygos", "Auchindoun", "Blade's Edge", "Burning Blade",
            "Cho'gall", "Drakkari", "Earthen Ring", "Eonar", "Fizzcrank", "Icecrown", "Kargath", "Kel'Thuzad",
            "Laughing Skull", "Lightning's Blade", "Llane", "Mal'Ganis", "Malygos", "Norgannon", "Onyxia", "Quel'Thalas",
            "Ragnaros", "Thunderhorn", "Turalyon", "Velen", "Blackrock", "Farstriders", "Frostwolf", "Kil'jaeden",
            "Kilrogg", "Nazgrel", "Nesingwary", "Proudmoore", "Quel'dorei", "Sen'jin", "Silver Hand", "Thorium Brotherhood",
            "Tichondrius", "Vashj", "Vek'nilash", "Winterhoof", "Aegwynn", "Anub'arak", "Bladefist", "Bonechewer",
            "Chromaggus", "Crushridge", "Daggerspine", "Eitrigg", "Garithos", "Garrosh", "Gurubashi", "Hakkar", "Hyjal",
            "Korgath", "Kul Tiras", "Misha", "Muradin", "Nathrezim", "Nordrassil", "Rexxar", "Runetotem", "Sisters of Elune",
            "Spirestone", "Stormscale", "Suramar", "Terenas", "Tortheldrin", "Uther", "Venture Co", "Warsong Gulch",
            "Windrunner", "World Tree", "Zandalar Tribe"
        };

    }
}
