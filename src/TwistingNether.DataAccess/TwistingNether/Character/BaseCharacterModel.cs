using TwistingNether.DataAccess.BattleNet.WoW.Character;
using TwistingNether.DataAccess.RaiderIO;

namespace TwistingNether.DataAccess.TwistingNether.Character
{
    public class BaseCharacterModel
    {
        public RaiderIOCharacterDataModel CharacterData { get; set; }
        public WoWCharacterEquipmentModel CharacterEquipment { get; set; }
        public string ClassColor { get; set; }
    }
   
}
