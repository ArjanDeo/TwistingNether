// Classes
import warrior from '$lib/classes/warrior.webp';
import paladin from '$lib/classes/paladin.webp';
import monk from '$lib/classes/monk.webp';
import shaman from '$lib/classes/shaman.webp';
import demonhunter from '$lib/classes/demonhunter.webp';
import priest from '$lib/classes/priest.webp';
import deathknight from '$lib/classes/deathknight.webp';
import evoker from '$lib/classes/evoker.webp';
import warlock from '$lib/classes/warlock.webp';
import mage from '$lib/classes/mage.webp';
import rogue from '$lib/classes/rogue.webp';
import hunter from '$lib/classes/hunter.webp';
import druid from '$lib/classes/druid.webp';

// Races
import bloodelfMale from '$lib/races/bloodelf-male.webp';
import bloodelfFemale from '$lib/races/bloodelf-female.webp';
import draeneiMale from '$lib/races/draenei-male.webp';
import draeneiFemale from '$lib/races/draenei-female.webp';
import dwarfMale from '$lib/races/dwarf-male.webp';
import dwarfFemale from '$lib/races/dwarf-female.webp';
import gnomeMale from '$lib/races/gnome-male.webp';
import gnomeFemale from '$lib/races/gnome-female.webp';
import goblinMale from '$lib/races/goblin-male.webp';
import goblinFemale from '$lib/races/goblin-female.webp';
import humanMale from '$lib/races/human-male.webp';
import humanFemale from '$lib/races/human-female.webp';
import nightelfMale from '$lib/races/nightelf-male.webp';
import nightelfFemale from '$lib/races/nightelf-female.webp';
import orcMale from '$lib/races/orc-male.webp';
import orcFemale from '$lib/races/orc-female.webp';
import pandarenMale from '$lib/races/panda-male.webp';
import pandarenFemale from '$lib/races/panda-female.webp';
import taurenMale from '$lib/races/tauren-male.webp';
import taurenFemale from '$lib/races/tauren-female.webp';
import trollMale from '$lib/races/troll-male.webp';
import trollFemale from '$lib/races/troll-female.webp';
import undeadMale from '$lib/races/undead-male.webp';
import undeadFemale from '$lib/races/undead-female.webp';
import voidelfMale from '$lib/races/voidelf-male.webp';
import voidelfFemale from '$lib/races/voidelf-female.webp';
import worgenMale from '$lib/races/worgen-male.webp';
import worgenFemale from '$lib/races/worgen-female.webp';
import mechagnomeMale from '$lib/races/mechagnome-male.webp';
import mechagnomeFemale from '$lib/races/mechagnome-female.webp';
import vulperaMale from '$lib/races/vulpera-male.webp';
import vulperaFemale from '$lib/races/vulpera-female.webp';
import nightborneMale from '$lib/races/nightborne-male.webp';
import nightborneFemale from '$lib/races/nightborne-female.webp';
import highmountaintaurenMale from '$lib/races/highmountain-male.webp';
import highmountaintaurenFemale from '$lib/races/highmountain-female.webp';
import magharorcMale from '$lib/races/maghar-male.webp';
import magharorcFemale from '$lib/races/maghar-female.webp';
import zandalaritrollMale from '$lib/races/zandalaritroll-male.webp';
import zandalaritrollFemale from '$lib/races/zandalaritroll-female.webp';
import dracthyrMale from '$lib/races/dracthyr-male.webp';
import dracthyrFemale from '$lib/races/dracthyr-female.webp';

// Equipment
import chestIcon from '$lib/icons/chest.webp';
import feetIcon from '$lib/icons/feet.webp';
import finger1Icon from '$lib/icons/finger 1.webp';
import finger2Icon from '$lib/icons/finger 2.webp';
import handsIcon from '$lib/icons/hands.webp';
import headIcon from '$lib/icons/head.webp';
import legsIcon from '$lib/icons/legs.webp';
import mainhandIcon from '$lib/icons/mainhand.webp';
import neckIcon from '$lib/icons/neck.webp';
import offhandIcon from '$lib/icons/offhand.webp';
import shirtIcon from '$lib/icons/shirt.webp';
import shoulderIcon from '$lib/icons/shoulder.webp';
import tabardIcon from '$lib/icons/tabard.webp';
import trinket1Icon from '$lib/icons/trinket 1.webp';
import trinket2Icon from '$lib/icons/trinket 2.webp';
import waistIcon from '$lib/icons/waist.webp';
import wristsIcon from '$lib/icons/wrists.webp';

// Crafting Tiers
import tier1Icon from '$lib/icons/quality/tier1.webp';
import tier2Icon from '$lib/icons/quality/tier2.webp';
import tier3Icon from '$lib/icons/quality/tier3.webp';

import plexusSentinel from '$lib/icons/bosses/3129.webp'
import forgeweaverAraz from '$lib/icons/bosses/3132.webp'
import soulbinderNaazdhrini from '$lib/icons/bosses/3130.webp'
import loomithar from '$lib/icons/bosses/3131.webp'
import soulHunters from '$lib/icons/bosses/3122.webp'
import fractillus from '$lib/icons/bosses/3133.webp'
import nexusKingSalhadaar from '$lib/icons/bosses/3134.webp'
import dimensius from '$lib/icons/bosses/3135.webp'

export const raceIcons: Record<string, string> = {
  'bloodelf-male': bloodelfMale,
  'bloodelf-female': bloodelfFemale,
  'draenei-male': draeneiMale,
  'draenei-female': draeneiFemale,
  'dwarf-male': dwarfMale,
  'dwarf-female': dwarfFemale,
  'gnome-male': gnomeMale,
  'gnome-female': gnomeFemale,
  'goblin-male': goblinMale,
  'goblin-female': goblinFemale,
  'human-male': humanMale,
  'human-female': humanFemale,
  'nightelf-male': nightelfMale,
  'nightelf-female': nightelfFemale,
  'orc-male': orcMale,
  'orc-female': orcFemale,
  'pandaren-male': pandarenMale,
  'pandaren-female': pandarenFemale,
  'tauren-male': taurenMale,
  'tauren-female': taurenFemale,
  'troll-male': trollMale,
  'troll-female': trollFemale,
  'undead-male': undeadMale,
  'undead-female': undeadFemale,
  'voidelf-male': voidelfMale,
  'voidelf-female': voidelfFemale,
  'worgen-male': worgenMale,
  'worgen-female': worgenFemale,
  'mechagnome-male': mechagnomeMale,
  'mechagnome-female': mechagnomeFemale,
  'vulpera-male': vulperaMale,
  'vulpera-female': vulperaFemale,
  'nightborne-male': nightborneMale,
  'nightborne-female': nightborneFemale,
  'highmountaintauren-male': highmountaintaurenMale,
  'highmountaintauren-female': highmountaintaurenFemale,
  'magharorc-male': magharorcMale,
  'magharorc-female': magharorcFemale,
  'zandalaritroll-male': zandalaritrollMale,
  'zandalaritroll-female': zandalaritrollFemale,
  'dracthyr-male': dracthyrMale,
  'dracthyr-female': dracthyrFemale
};

export const classIcons: Record<string, string> = {
  'Warrior': warrior,
  'Paladin': paladin,
  'Monk': monk,
  'Shaman': shaman,
  'Demon Hunter': demonhunter,
  'Priest': priest,
  'Death Knight': deathknight,
  'Evoker': evoker,
  'Warlock': warlock,
  'Mage': mage,
  'Rogue': rogue,
  'Hunter': hunter,
  'Druid': druid
};
export const equipmentIcons: Record<string, string> = {
  'Chest': chestIcon,
  'Feet': feetIcon,
  'Finger 1': finger1Icon,
  'Finger 2': finger2Icon,
  'Hands': handsIcon,
  'Head': headIcon,
  'Legs': legsIcon,
  'Main Hand': mainhandIcon,
  'Neck': neckIcon,
  'Off Hand': offhandIcon,
  'Shirt': shirtIcon,
  'Shoulder': shoulderIcon,
  'Tabard': tabardIcon,
  'Trinket 1': trinket1Icon,
  'Trinket 2': trinket2Icon,
  'Waist': waistIcon,
  'Wrists': wristsIcon,
}

export const qualityIcons: Record<string, string> = {
  'Tier 1': tier1Icon,
  'Tier 2': tier2Icon,
  'Tier 3': tier3Icon
}
export const bossIcons: Record<number, string> = {
  3129: plexusSentinel,
  3132: forgeweaverAraz,
  3130: soulbinderNaazdhrini,
  3131: loomithar,
  3122: soulHunters,
  3133: fractillus,
  3134: nexusKingSalhadaar,
  3135: dimensius,
}