/* eslint-disable @typescript-eslint/no-explicit-any */

// Explanation for eslint disable: 
// Some of the (usually Raider.IO) API types are not easily accessible without manually looking at the entire schema and so are given an "any" type. 
// As most of these are legacy properties (e.g. BFA corruptions) I've decided not to explicitly add any unless there is an appropriate need.
export interface Character {
  characterData: CharacterData
  characterEquipment: CharacterEquipment
  classColor: string
}

export interface CharacterData {
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
  mythic_plus_weekly_highest_level_runs: any[]
  mythic_plus_best_runs: MythicPlusBestRun[]
  mythic_plus_scores_by_season: MythicPlusScoresBySeason[]
  gear: Gear
  raid_progression: RaidProgression
  raid_achievement_curve: any[]
  guild: Guild
}

export interface MythicPlusBestRun {
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
  affixes: Affix[]
  url: string
}

export interface Affix {
  id: number
  name: string
  description: string
  icon: string
  wowhead_url: string
}

export interface MythicPlusScoresBySeason {
  season: string
  scores: Scores
  segments: Segments
}

export interface Scores {
  all: number
  dps: number
  healer: number
  tank: number
  spec_0: number
  spec_1: number
  spec_2: number
  spec_3: number
}

export interface Segments {
  all: All
  dps: Dps
  healer: Healer
  tank: Tank
  spec_0: Spec0
  spec_1: Spec1
  spec_2: Spec2
  spec_3: Spec3
}

export interface All {
  score: number
  color: string
}

export interface Dps {
  score: number
  color: string
}

export interface Healer {
  score: number
  color: string
}

export interface Tank {
  score: number
  color: string
}

export interface Spec0 {
  score: number
  color: string
}

export interface Spec1 {
  score: number
  color: string
}

export interface Spec2 {
  score: number
  color: string
}

export interface Spec3 {
  score: number
  color: string
}

export interface Gear {
  updated_at: string
  item_level_equipped: number
  artifact_traits: number
  corruption: Corruption
  items: Items
}

export interface Corruption {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Items {
  head: Head
  neck: Neck
  shoulder: Shoulder
  back: Back
  chest: Chest
  waist: Waist
  wrist: Wrist
  hands: Hands
  legs: Legs
  feet: Feet
  finger1: Finger1
  finger2: Finger2
  trinket1: Trinket1
  trinket2: Trinket2
  mainhand: Mainhand
  offhand: Offhand
}

export interface Head {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption2
  gems: any[]
  bonuses: number[]
}

export interface Corruption2 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Neck {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption3
  gems: number[]
  bonuses: number[]
}

export interface Corruption3 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Shoulder {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  azerite_powers: AzeritePower | undefined[]
  corruption: Corruption4
  tier: any
  gems: any[]
  bonuses: number[]
}

export interface AzeritePower {
  id: number
  spell: Spell
  tier: number
}

export interface Spell {
  id: number
  school: number
  icon: string
  name: string
}

export interface Corruption4 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Back {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption5
  gems: number[]
  bonuses: number[]
}

export interface Corruption5 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Chest {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  azerite_powers: any[]
  corruption: Corruption6
  tier: any
  gems: any[]
  bonuses: number[]
}

export interface Corruption6 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Waist {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption7
  gems: number[]
  bonuses: number[]
}

export interface Corruption7 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Wrist {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption8
  gems: any[]
  bonuses: number[]
}

export interface Corruption8 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Hands {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption9
  tier: string
  gems: any[]
  bonuses: number[]
}

export interface Corruption9 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Legs {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption10
  tier: string
  gems: any[]
  bonuses: number[]
}

export interface Corruption10 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Feet {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption11
  gems: any[]
  bonuses: number[]
}

export interface Corruption11 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Finger1 {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption12
  gems: number[]
  bonuses: number[]
}

export interface Corruption12 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Finger2 {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption13
  gems: number[]
  bonuses: number[]
}

export interface Corruption13 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Trinket1 {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption14
  gems: any[]
  bonuses: number[]
}

export interface Corruption14 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Trinket2 {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption15
  gems: any[]
  bonuses: number[]
}

export interface Corruption15 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Mainhand {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption16
  gems: any[]
  bonuses: number[]
}

export interface Corruption16 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface Offhand {
  item_id: number
  item_level: number
  icon: string
  name: string
  item_quality: number
  is_legendary: boolean
  is_azerite_armor: boolean
  corruption: Corruption17
  gems: any[]
  bonuses: number[]
}

export interface Corruption17 {
  added: number
  resisted: number
  total: number
  cloakRank: number
}

export interface RaidProgression {
  nerubarpalace: Nerubarpalace
  liberationofundermine: Liberationofundermine
  manaforge_omega: ManaforgeOmega
}

export interface Nerubarpalace {
  summary: string
  total_bosses: number
  normal_bosses_killed: number
  heroic_bosses_killed: number
  mythic_bosses_killed: number
}

export interface Liberationofundermine {
  summary: string
  total_bosses: number
  normal_bosses_killed: number
  heroic_bosses_killed: number
  mythic_bosses_killed: number
}

export interface ManaforgeOmega {
  summary: string
  total_bosses: number
  normal_bosses_killed: number
  heroic_bosses_killed: number
  mythic_bosses_killed: number
}

export interface Guild {
  name: string
  realm: string
}

export interface CharacterEquipment {
  _links: Links
  character: Character
  equipped_items: EquippedItem[]
  equipped_item_sets: EquippedItemSet[]
}

export interface Links {
  self: Self
}

export interface Self {
  href: string
}

export interface Character {
  key: Key
  name: string
  id: number
  realm: Realm
}

export interface Key {
  href: string
}

export interface Realm {
  key: Key2
  name: string
  id: number
  slug: string
}

export interface Key2 {
  href: string
}

export interface EquippedItem {
  item: Item
  slot: Slot
  quantity: number
  context: number
  bonus_list?: number[]
  quality: Quality
  name: string
  media: Media
  item_class: ItemClass
  item_subclass: ItemSubclass
  inventory_type: InventoryType
  binding: Binding
  armor?: Armor
  stats: Stat[]
  sell_price?: SellPrice
  requirements?: Requirements
  level: Level2
  transmog?: Transmog
  durability?: Durability
  name_description?: NameDescription
  sockets: Socket[]
  modified_appearance_id?: number
  is_subclass_hidden?: boolean
  enchantments: Enchantment[]
  unique_equipped?: string
  spells?: Spell2[]
  set?: Set
  limit_category?: string
  modified_crafting_stat?: ModifiedCraftingStat[]
  weapon?: Weapon
}

export interface Item {
  key: Key3
  id: number
  name: any
  iconUrl: string
}

export interface Key3 {
  href: string
}

export interface Slot {
  type: string
  name: string
}

export interface Quality {
  type: string
  name: string
}

export interface Media {
  key: Key4
  id: number
}

export interface Key4 {
  href: string
}

export interface ItemClass {
  key: Key5
  name: string
  id: number
}

export interface Key5 {
  href: string
}

export interface ItemSubclass {
  key: Key6
  name: string
  id: number
}

export interface Key6 {
  href: string
}

export interface InventoryType {
  type: string
  name: string
}

export interface Binding {
  type: string
  name: string
}

export interface Armor {
  value: number
  display: Display
}

export interface Display {
  display_string: string
  color: Color
}

export interface Color {
  r: number
  g: number
  b: number
  a: number
}

export interface Stat {
  type: Type
  value: number
  display: Display2
  is_negated?: boolean
  is_equip_bonus?: boolean
}

export interface Type {
  type: string
  name: string
}

export interface Display2 {
  display_string: string
  color: Color2
}

export interface Color2 {
  r: number
  g: number
  b: number
  a: number
}

export interface SellPrice {
  value: number
  display_strings: DisplayStrings
}

export interface DisplayStrings {
  header: string
  gold: string
  silver: string
  copper: string
}

export interface Requirements {
  level: Level
  playable_classes?: PlayableClasses
}

export interface Level {
  value: number
  display_string: string
}

export interface PlayableClasses {
  links: Link[]
  display_string: string
}

export interface Link {
  key: Key7
  name: string
  id: number
}

export interface Key7 {
  href: string
}

export interface Level2 {
  value: number
  display_string: string
}

export interface Transmog {
  item: Item2
  display_string: string
  item_modified_appearance_id: number
}

export interface Item2 {
  key: Key8
  id: number
  name: string
  iconUrl: any
}

export interface Key8 {
  href: string
}

export interface Durability {
  value: number
  display_string: string
}

export interface NameDescription {
  display_string: string
  color: Color3
}

export interface Color3 {
  r: number
  g: number
  b: number
  a: number
}

export interface Socket {
  socket_type: SocketType
  item: Item3
  display_string: string
  media: Media2
  context?: number
}

export interface SocketType {
  type: string
  name: string
}

export interface Item3 {
  key: Key9
  id: number
  name: string
  iconUrl: any
}

export interface Key9 {
  href: string
}

export interface Media2 {
  key: Key10
  id: number
}

export interface Key10 {
  href: string
}

export interface Enchantment {
  display_string: string
  enchantment_id: number
  enchantment_slot: EnchantmentSlot
  source_item?: SourceItem
}

export interface EnchantmentSlot {
  id: number
  type: string
}

export interface SourceItem {
  key: Key11
  name: string
  id: number
}

export interface Key11 {
  href: string
}

export interface Spell2 {
  spell: Spell3
  description: string
  display_color?: DisplayColor
}

export interface Spell3 {
  spell: any
  description: any
  display_color: any
}

export interface DisplayColor {
  r: number
  g: number
  b: number
  a: number
}

export interface Set {
  item_set: ItemSet
  items: Item4[]
  effects: Effect[]
  display_string: string
}

export interface ItemSet {
  key: Key12
  name: string
  id: number
}

export interface Key12 {
  href: string
}

export interface Item4 {
  key: any
  id: number
  name: any
  iconUrl: any
}

export interface Effect {
  display_string: string
  required_count: number
  is_active: boolean
}

export interface ModifiedCraftingStat {
  id: number
  type: string
  name: string
}

export interface Weapon {
  damage: Damage
  attack_speed: AttackSpeed
  dps: Dps2
}

export interface Damage {
  min_value: number
  max_value: number
  display_string: string
  damage_class: DamageClass
}

export interface DamageClass {
  type: string
  name: string
}

export interface AttackSpeed {
  value: number
  display_string: string
}

export interface Dps2 {
  value: number
  display_string: string
}

export interface EquippedItemSet {
  item_set: ItemSet2
  items: Item5[]
  effects: Effect2[]
  display_string: string
}

export interface ItemSet2 {
  key: Key13
  name: string
  id: number
}

export interface Key13 {
  href: string
}

export interface Item5 {
  key: any
  id: number
  name: any
  iconUrl: any
}

export interface Effect2 {
  display_string: string
  required_count: number
  is_active: boolean
}

export interface CharacterMedia {
  type: string,
  link: string
}

export interface WeeklyBosses {
  boss: string
  difficulty: string
}

