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
      mythic_plus_best_runs: Array<Run>
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
          spells: Array<unknown>
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
              spells: unknown
            }
            domination_shards: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
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
                    rank: unknown
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
              spells: unknown
            }
            domination_shards: Array<unknown>
            tier: unknown
            gems: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
            gems: Array<unknown>
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
                  id: number
                  spell: {
                    id: number
                    school: number
                    icon: string
                    name: string
                    rank: unknown
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
              spells: unknown
            }
            domination_shards: Array<unknown>
            tier: string
            gems: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
            tier: string
            gems: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
            tier: string
            gems: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
            gems: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
            gems: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
            gems: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
            gems: Array<unknown>
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
            azerite_powers: Array<unknown>
            corruption: {
              added: number
              resisted: number
              total: number
              cloakRank: number
              spells: unknown
            }
            domination_shards: Array<unknown>
            gems: Array<unknown>
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
        },
        liberationofundermine: {
          summary: string
          total_bosses: number
          normal_bosses_killed: number
          heroic_bosses_killed: number
          mythic_bosses_killed: number
        },
        manaforge_omega: {
          summary: string
          total_bosses: number
          normal_bosses_killed: number
          heroic_bosses_killed: number
          mythic_bosses_killed: number
        }
       // amirdrassilthedreamshope: unknown
       // vaultoftheincarnates: unknown
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
    classColor: string
    characterMedia: Array<{
      type: string
      link: string
    }>
  }

  export enum ItemQuality {
    Heirloom = 7,
    Artifact = 6,
    Legendary = 5,
    Epic = 4,
    Rare = 3,
    Uncommon = 2,
    Common = 1
  }
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
