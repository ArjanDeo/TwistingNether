export type CharacterData = {
  raiderIOCharacterData: {
    name: string
    race: string
    char_class: string
    active_spec_name: string
    active_spec_role: string
    gender: string
    faction: string
    achievement_points: number
    honorable_kills: number
    thumbnail_url: string
    region: string
    realm: string
    last_crawled_at: string
    profile_url: string
    profile_banner: string
    mythic_plus_weekly_highest_level_runs: Array<Run>
    mythic_plus_best_runs: Array<{
      dungeon: string
      short_name: string
      mythic_level: number
      completed_at: string
      clear_time_ms: number
      par_time_ms: number
      num_keystone_upgrades: number
      map_challenge_mode_id: number
      zone_id: number
      zone_expansion_id: number
      icon_url: string
      background_image_url: string
      score: number
      affixes: Array<{
        id: number
        name: string
        description: string
        icon: string
        wowhead_url: string
      }>
      url: string
    }>
    mythic_plus_scores_by_season: Array<{
      season: string
      scores: {
        all: number
        dps: number
        healer: number
        tank: number
        spec_0: number
        spec_1: number
        spec_2: number
        spec_3: number
      }
      segments: {
        all: {
          score: number
          color: string
        }
        dps: {
          score: number
          color: string
        }
        healer: {
          score: number
          color: string
        }
        tank: {
          score: number
          color: string
        }
        spec_0: {
          score: number
          color: string
        }
        spec_1: {
          score: number
          color: string
        }
        spec_2: {
          score: number
          color: string
        }
        spec_3: {
          score: number
          color: string
        }
      }
    }>
    gear: {
      updated_at: string
      item_level_equipped: number
      artifact_traits: number
      corruption: {
        added: number
        resisted: number
        total: number
        cloakRank: number
      }
      items: {
        head: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        neck: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        shoulder: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          azerite_powers: Array<
            | {
                id: number
                spell: {
                  id: number
                  school: number
                  icon: string
                  name: string
                }
                tier: number
              }
            | undefined
          >
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          tier: unknown
          gems: Array<number>
          bonuses: Array<number>
        }
        back: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        chest: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          azerite_powers: Array<
              | {
                  id: Array<unknown>
                  spell: Array<Array<Array<unknown>>>
                  tier: Array<unknown>
                }
              | undefined
            >
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        waist: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        wrist: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        hands: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          tier: string
          gems: Array<number>
          bonuses: Array<number>
        }
        legs: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          tier: string
          gems: Array<number>
          bonuses: Array<number>
        }
        feet: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        finger1: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        finger2: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        trinket1: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        trinket2: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        mainhand: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
        offhand: {
          item_id: number
          item_level: number
          icon: string
          name: string
          item_quality: number
          is_legendary: boolean
          is_azerite_armor: boolean
          corruption: {
            added: number
            resisted: number
            total: number
            cloakRank: number
          }
          gems: Array<number>
          bonuses: Array<number>
        }
      }
    }
    raid_progression: {
      nerubarpalace: {
        summary: string
        total_bosses: number
        normal_bosses_killed: number
        heroic_bosses_killed: number
        mythic_bosses_killed: number
      }
      liberationofundermine: {
        summary: string
        total_bosses: number
        normal_bosses_killed: number
        heroic_bosses_killed: number
        mythic_bosses_killed: number
      }
      manaforge_omega: {
        summary: string
        total_bosses: number
        normal_bosses_killed: number
        heroic_bosses_killed: number
        mythic_bosses_killed: number
      }
    }
    raid_achievement_curve: Array<{
      raid: string
      aotc: string
    }>
    guild: {
      name: string
      realm: string
    }
  }
  raidBossesKilledThisWeek: Array<{
      boss: string
      difficulty: string
    }>
  dungeonVaultSlots: number[]
  characterMedia: Array<{
    type: string
    link: string
  }>
  characterEquipment: {
    _links: {
      self: {
        href: string
      }
    }
    character: {
      key: {
        href: string
      }
      name: string
      id: number
      realm: {
        key: {
          href: string
        }
        name: string
        id: number
        slug: string
      }
    }
    equipped_items: Array<GearPiece>
    equipped_item_sets: Array<{
      item_set: {
        key: {
          href: string
        }
        name: string
        id: number
      }
      items: Array<{
        item: {
          key: {
            href: string
          }
          name: string
          id: number
        }
        is_equipped?: boolean
      }>
      effects: Array<{
        display_string: string
        required_count: number
        is_active: boolean
      }>
      display_string: string
    }>
  }
  raidPerformance: RaidPerformance
  classColor: string
}
 export enum ItemQuality {
  Heirloom = "HEIRLOOM",
  Artifact = "ARTIFACT",
  Legendary = "LEGENDARY",
  Epic = "EPIC",
  Rare = "RARE",
  Uncommon = "UNCOMMON",
  Common = "COMMON"
}
export type GearPiece = {
    item: {
            key: {
            href: string
            }
            id: number
            name: string
            iconUrl: string
        }
    slot: {
        type: string
        name: string
    }
    quantity: number
    context: number
    bonus_list?: Array<number>
    quality: {
        type: string
        name: string
    }
    name: string
    media: {
        key: {
        href: string
        }
        id: number
    }
    item_class: {
        key: {
        href: string
        }
        name: string
        id: number
    }
    item_subclass: {
        key: {
        href: string
        }
        name: string
        id: number
    }
    inventory_type: {
        type: string
        name: string
    }
    binding: {
        type: string
        name: string
    }
    armor?: {
        value: number
        display: {
        display_string: string
        color: {
            r: number
            g: number
            b: number
            a: number
        }
        }
    }
    stats: Array<{
        type: {
        type: string
        name: string
        }
        value: number
        display: {
        display_string: string
        color: {
            r: number
            g: number
            b: number
            a: number
        }
        }
        is_negated?: boolean
        is_equip_bonus?: boolean
    }>
    sell_price?: {
        value: number
        display_strings: {
        header: string
        gold: string
        silver: string
        copper: string
        }
    }
    requirements?: {
        level: {
        value: number
        display_string: string
        }
        playable_classes?: {
        links: Array<{
            key: {
            href: string
            }
            name: string
            id: number
        }>
        display_string: string
        }
    }
    level: {
        value: number
        display_string: string
    }
    transmog?: {
        item: {
        key: {
            href: string
        }
        id: number
        name: string
        }
        display_string: string
        item_modified_appearance_id: number
    }
    durability?: {
        value: number
        display_string: string
    }
    name_description?: {
        display_string: string
        color: {
        r: number
        g: number
        b: number
        a: number
        }
    }
    sockets?: Array<{
        socket_type: {
        type: string
        name: string
        }
        item: {
        key: {
            href: string
        }
        id: number
        name: string
        }
        display_string: string
        media: {
        key: {
            href: string
        }
        id: number
        }
        context?: number
    }>
    modified_appearance_id?: number
    is_subclass_hidden?: boolean
    enchantments?: Array<{
        display_string: string
        enchantment_id: number
        enchantment_slot: {
        id: number
        type: string
        }
        source_item?: {
        key: {
            href: string
        }
        name: string
        id: number
        }
    }>
    unique_equipped?: string
    spells?: Array<{
        spell: {
        spell: {
            key: {
            href: string
            }
            name: string,
            id: number
        }
        description: string
        display_color: {
        r: number
        g: number
        b: number
        a: number
        }
        }
        description: string
        display_color?: {
        r: number
        g: number
        b: number
        a: number
        }
    }>
    set?: {
        item_set: {
        key: {
            href: string
        }
        name: string
        id: number
        }
        items: Array<{
        item: {
            key: {
            href: string
            }
            name: string
            id: number
        }
        is_equipped: boolean
        }>
        effects: Array<{
        display_string: string
        required_count: number
        is_active: boolean
        }>
        display_string: string
    }
    limit_category?: string
    modified_crafting_stat?: Array<{
        id: number
        type: string
        name: string
    }>
    weapon?: {
        damage: {
        min_value: number
        max_value: number
        display_string: string
        damage_class: {
            type: string
            name: string
        }
        }
        attack_speed: {
        value: number
        display_string: string
        }
        dps: {
        value: number
        display_string: string
        }
    }
};
  export type Affix = {
    id: number,
    icon: string
  }
  export type Run =  {
    dungeon: string
    short_name: string
    mythic_level: number
    completed_at: string
    clear_time_ms: number
    par_time_ms: number
    num_keystone_upgrades: number
    map_challenge_mode_id: number
    zone_id: number
    zone_expansion_id: number
    icon_url: string
    background_image_url: string
    score: number
    affixes: Array<{
      id: number
      name: string
      description: string
      icon: string
      icon_url: string
      wowhead_url: string
    }>
    url: string
  }
  export type Character = {
    name: string;
    realm: string;
    region: string;
    class: keyof typeof ClassColors;
  }
  export type Token = {
  _links: {
    self: {
      href: string
    }
  }
  last_updated_timestamp: number
  price: number
}

  export const ClassColors = {
    Warrior: '#C69B6D',
    Hunter: '#AAD372',
    Mage: '#3FC7EB',
    Rogue: '#FFF468',
    Priest: '#FFFFFF',
    Warlock: '#8788EE',
    Paladin: '#F48CBA',
    Druid: '#FF7C0A',
    Shaman: '#0070DD',
    Monk: '#00FF98',
    DemonHunter: '#A330C9',
    DeathKnight: '#C41E3A',
    Evoker: '#33937F',
  } as const;

  export interface Encounter {
  id: number;
  slug: string;
  name: string;
}

export const bosses: Encounter[] = [
  { id: 197124, slug: "plexus-sentinel", name: "Plexus Sentinel" },
  { id: 197125, slug: "loomithar", name: "Loom'ithar" },
  { id: 197126, slug: "soulbinder-naazindhri", name: "Soulbinder Naazindhri" },
  { id: 197127, slug: "forgeweaver-araz", name: "Forgeweaver Araz" },
  { id: 197128, slug: "the-soul-hunters", name: "The Soul Hunters" },
  { id: 197129, slug: "fractillus", name: "Fractillus" },
  { id: 197130, slug: "nexus-king-salhadaar", name: "Nexus-King Salhadaar" },
  { id: 197131, slug: "dimensius", name: "Dimensius" }
];

export type RaidPerformance = {
    bestPerformanceAverage: number
    medianPerformanceAverage: number
    difficulty: number
    metric: string
    partition: number
    zone: number
    size: number
    allStars: Array<{
      partition: number
      spec: string
      points: number
      possiblePoints: number
      rank: number
      regionRank: number
      serverRank: number
      rankPercent: number
      total: number
    }>
    rankings: Array<{
      encounter: {
        id: number
        name: string
      }
      rankPercent: number
      medianPercent: number
      lockedIn: boolean
      totalKills: number
      fastestKill: number
      allStars: {
        points: number
        possiblePoints: number
        partition: number
        rank: number
        regionRank: number
        serverRank: number
        rankPercent: number
        total: number
      }
      spec: string
      bestSpec: string
      bestAmount: number
    }>
}

export enum MainStats {
  AGILITY = 'Agility',
  STRENGTH = 'Strength',
  INTELLECT = 'Intellect'
}

export const specToMainStat: Record<string, "Strength" | "Agility" | "Intellect"> = {
  // Warrior
  "Arms": "Strength",
  "Fury": "Strength",
  "Protection": "Strength",

  // Paladin
  "Holy Paladin": "Intellect",
  "Protection Paladin": "Strength",
  "Retribution": "Strength",

  // Hunter
  "Beast Mastery": "Agility",
  "Marksmanship": "Agility",
  "Survival": "Agility",

  // Rogue
  "Assassination": "Agility",
  "Outlaw": "Agility",
  "Subtlety": "Agility",

  // Priest
  "Discipline": "Intellect",
  "Holy Priest": "Intellect",
  "Shadow": "Intellect",

  // Death Knight
  "Blood": "Strength",
  "Frost DK": "Strength",
  "Unholy": "Strength",

  // Shaman
  "Elemental": "Intellect",
  "Enhancement": "Agility",
  "Restoration Shaman": "Intellect",

  // Mage
  "Arcane": "Intellect",
  "Fire Mage": "Intellect",
  "Frost Mage": "Intellect",

  // Warlock
  "Affliction": "Intellect",
  "Demonology": "Intellect",
  "Destruction": "Intellect",

  // Monk
  "Brewmaster": "Agility",
  "Mistweaver": "Intellect",
  "Windwalker": "Agility",

  // Druid
  "Balance": "Intellect",
  "Feral": "Agility",
  "Guardian Druid": "Agility",
  "Restoration Druid": "Intellect",

  // Demon Hunter
  "Havoc": "Agility",
  "Vengeance": "Agility",

  // Evoker
  "Devastation": "Intellect",
  "Preservation": "Intellect",
  "Augmentation": "Intellect"
};
