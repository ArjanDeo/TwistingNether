using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistingNether.DataAccess.BattleNet.WoW.Character
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Agility
    {
        public int @base { get; set; }
        public int effective { get; set; }
    }

    public class ArmorStat
    {
        public int @base { get; set; }
        public int effective { get; set; }
    }

    public class Avoidance
    {
        public double rating_bonus { get; set; }
        public int rating_normalized { get; set; }
    }

    public class Block
    {
        public double rating_bonus { get; set; }
        public double value { get; set; }
        public int rating_normalized { get; set; }
    }

    public class Dodge
    {
        public double rating_bonus { get; set; }
        public double value { get; set; }
        public int rating_normalized { get; set; }
    }

    public class Intellect
    {
        public int @base { get; set; }
        public int effective { get; set; }
    }



    public class Lifesteal
    {
        public double rating_bonus { get; set; }
        public double value { get; set; }
        public int rating_normalized { get; set; }
    }


    public class Mastery
    {
        public double rating_bonus { get; set; }
        public double value { get; set; }
        public int rating_normalized { get; set; }
    }

    public class MeleeCrit
    {
        public double rating_bonus { get; set; }
        public double value { get; set; }
        public int rating_normalized { get; set; }
    }

    public class MeleeHaste
    {
        public double rating_bonus { get; set; }
        public double value { get; set; }
        public int rating_normalized { get; set; }
    }

    public class Parry
    {
        public double rating_bonus { get; set; }
        public double value { get; set; }
        public int rating_normalized { get; set; }
    }

    public class PowerType
    {
        public Key key { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class RangedCrit
    {
        public double rating_bonus { get; set; }
        public double value { get; set; }
        public int rating_normalized { get; set; }
    }

    public class RangedHaste
    {
        public double rating_bonus { get; set; }
        public double value { get; set; }
        public int rating_normalized { get; set; }
    }

    public class WoWCharacterStatisticsModel
    {
        public Links _links { get; set; }
        public int health { get; set; }
        public int power { get; set; }
        public PowerType power_type { get; set; }
        public Speed speed { get; set; }
        public Strength strength { get; set; }
        public Agility agility { get; set; }
        public Intellect intellect { get; set; }
        public Stamina stamina { get; set; }
        public MeleeCrit melee_crit { get; set; }
        public MeleeHaste melee_haste { get; set; }
        public Mastery mastery { get; set; }
        public int bonus_armor { get; set; }
        public Lifesteal lifesteal { get; set; }
        public double versatility { get; set; }
        public double versatility_damage_done_bonus { get; set; }
        public double versatility_healing_done_bonus { get; set; }
        public double versatility_damage_taken_bonus { get; set; }
        public Avoidance avoidance { get; set; }
        public int attack_power { get; set; }
        public double main_hand_damage_min { get; set; }
        public double main_hand_damage_max { get; set; }
        public double main_hand_speed { get; set; }
        public double main_hand_dps { get; set; }
        public double off_hand_damage_min { get; set; }
        public double off_hand_damage_max { get; set; }
        public double off_hand_speed { get; set; }
        public double off_hand_dps { get; set; }
        public int spell_power { get; set; }
        public int spell_penetration { get; set; }
        public SpellCrit spell_crit { get; set; }
        public double mana_regen { get; set; }
        public double mana_regen_combat { get; set; }
        public ArmorStat armor { get; set; }
        public Dodge dodge { get; set; }
        public Parry parry { get; set; }
        public Block block { get; set; }
        public RangedCrit ranged_crit { get; set; }
        public RangedHaste ranged_haste { get; set; }
        public SpellHaste spell_haste { get; set; }
        public Character character { get; set; }
    }


    public class Speed
    {
        public double rating_bonus { get; set; }
        public int rating_normalized { get; set; }
    }

    public class SpellCrit
    {
        public double rating_bonus { get; set; }
        public double value { get; set; }
        public int rating_normalized { get; set; }
    }

    public class SpellHaste
    {
        public double rating_bonus { get; set; }
        public double value { get; set; }
        public int rating_normalized { get; set; }
    }

    public class Stamina
    {
        public int @base { get; set; }
        public int effective { get; set; }
    }

    public class Strength
    {
        public int @base { get; set; }
        public int effective { get; set; }
    }


}
