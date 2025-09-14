<script lang="ts">
	import { ItemQuality, type CharacterData } from '$lib/types';
    import * as Tooltip from '$lib/components/ui/tooltip';
    import { itemQualityColor } from '$lib/utils';
    import { classIcons, raceIcons, equipmentIcons } from '$lib/metadata';
    import GearIcon from '../gearIcon/GearIcon.svelte';
        let { character }: { character: CharacterData } = $props();
    const equipped = character.characterEquipment.equipped_items;

    const equippedGear = {
    head: equipped.find(i => i.inventory_type.name === "Head"),
    neck: equipped.find(i => i.inventory_type.name === "Neck"),
    shoulders: equipped.find(i => i.inventory_type.name === "Shoulder"),
    back: equipped.find(i => i.inventory_type.name === "Back"),
    chest: equipped.find(i => i.inventory_type.name === "Chest"),
    tabard: equipped.find(i => i.inventory_type.name === "Tabard"),
    shirt: equipped.find(i => i.inventory_type.name === "Shirt"),
    wrists: equipped.find(i => i.inventory_type.name === "Wrist"),
    hands: equipped.find(i => i.inventory_type.name === "Hands"),
    waist: equipped.find(i => i.inventory_type.name === "Waist"),
    legs: equipped.find(i => i.inventory_type.name === "Legs"),
    feet: equipped.find(i => i.inventory_type.name === "Feet"),
    ring1: equipped.find(i => i.slot.name === "Ring 1"),
    ring2: equipped.find(i => i.slot.name === "Ring 2"),
    trinket1: equipped.find(i => i.slot.name === "Trinket 1"),
    trinket2: equipped.find(i => i.slot.name === "Trinket 2"),
    mainhand: equipped.find(i => i.slot.name === "Main Hand"),
    offhand: equipped.find(i => i.slot.name === "Off Hand"),
    };
function itemRarityColor(itemQuality: string): string {
    switch (itemQuality) {
        case ItemQuality.Common:
            return '#ffffff';
        case ItemQuality.Uncommon:
            return '#1eff00';
        case ItemQuality.Rare:
            return '#0070dd';
        case ItemQuality.Epic:
            return '#a335ee';
        case ItemQuality.Legendary:
            return '#ff8000';
        case ItemQuality.Artifact:
            return '#e6cc80';
        case ItemQuality.Heirloom:
            return '#00ccff';
        default:
            return '#ffffff'; // Default color in case no match
    }
}



const raceGenderString = `${character.raiderIOCharacterData.race.replace(' ', '').toLowerCase()}-${character.raiderIOCharacterData.gender}`
</script>
<div>
    <header class="mb-4">
            <h1 class="text-2xl lg:text-4xl font-semibold">
                {character.raiderIOCharacterData.name}-{character.raiderIOCharacterData.realm} 
            </h1>
            {#if character.raiderIOCharacterData.guild != null}
            <h2 class="text-xl lg:text-3x; font-semibold">
                <a target="_blank" class="hover:text-purple-700 transition-colors ease-in-out duration-300" href="https://worldofwarcraft.blizzard.com/en-us/guild/{character.raiderIOCharacterData.region}/{character.raiderIOCharacterData.guild.realm.replace('\'', "")}/{character.raiderIOCharacterData.guild.name.replace(' ', '-')}/">&lt;{character.raiderIOCharacterData.guild.name}&gt;</a>
            </h2>
            {/if}

            <div class="mt-2 flex md:flex-row gap-x-5 gap-y-2 md:gap-y-0">
                <div class="flex flex-row gap-x-1 max-w-full">
                    <img src={raceIcons[raceGenderString]} alt={character.raiderIOCharacterData.race} class="max-w-8 max-h-8 mt-1 rounded-full ml"/>
                    <p class="text-center md:text-xl h-fit my-auto">{character.raiderIOCharacterData.race}</p>
                </div>
                <div class="flex flex-row gap-x-1">
                    <img src={classIcons[character.raiderIOCharacterData.char_class]} alt={character.raiderIOCharacterData.char_class} class="max-w-8 max-h-8 mt-1 rounded-full"/>
                    <p class="text-center md:text-xl h-fit my-auto">{character.raiderIOCharacterData.char_class}</p>
                </div>
            
                <a aria-label="raider.io link" href="https://raider.io/characters/{character.raiderIOCharacterData.region}/{character.raiderIOCharacterData.realm}/{character.raiderIOCharacterData.name}" target="_blank">
                    <img src="/raiderioicon.webp" class="w-10 h-10" alt="raider io logo">
                </a>
                <a class="ml-1" href="https://www.warcraftlogs.com/character/{character.raiderIOCharacterData.region}/{character.raiderIOCharacterData.realm}/{character.raiderIOCharacterData.name}" target="_blank">
                    <img src="/warcraftlogsicon.webp" class="w-10 h-10" alt="warcraft logs logo"/>
                </a>
                <a class="ml-1" href="https://worldofwarcraft.blizzard.com/en-gb/character/{character.raiderIOCharacterData.region}/{character.raiderIOCharacterData.realm}/{character.raiderIOCharacterData.name}" target="_blank">
                    <img src="/wowicon.webp" class="w-10 h-10" alt="world of warcraft logo"/>
                </a>
            </div>
    </header>        
    <div class="md:flex items-start md:mx-auto lg:mx-0 flex-wrap lg:flex-none">
        <!-- First Gear Column-->
        <div class="flex lg:flex-col flex-row sm:mx-auto lg:mx-0 md:order-none order-1 gap-x-2">
            {#each [
            { slot: 'Head', gear: equippedGear.head },
            { slot: 'Neck', gear: equippedGear.neck },
            { slot: 'Shoulders', gear: equippedGear.shoulders },
            { slot: 'Back', gear: equippedGear.back },
            { slot: 'Chest', gear: equippedGear.chest },
            { slot: 'Tabard', gear: equippedGear.tabard },
            { slot: 'Shirt', gear: equippedGear.shirt },
            { slot: 'Wrists', gear: equippedGear.wrists }
            ] as { slot, gear }}
                <GearIcon gear={gear} slot={slot} characterSpec={character.raiderIOCharacterData.active_spec_name} />
            {/each}
        </div>
        <!-- Character Render with Centered Weapons -->
    <div class="flex flex-col items-center sm:order-last md:order-none mx-auto">
        <img 
            src={character.characterMedia ? `https://wsrv.nl/?url=${character.characterMedia[2].link}&w=800&output=webp&q=80` : '/default-image.webp'} 
            alt="Character Render" 
            class="md:max-w-2xl lg:max-w-3xl lg:max-h-max"
            fetchpriority=high
        />

        <!-- Main & Off-Hand Weapons -->
        <div class="flex gap-4">
            {#each [
                { slot: 'Main Hand', gear: equippedGear.mainhand },
                { slot: 'Off Hand', gear: equippedGear.offhand }
            ] as { slot, gear }}
                    <GearIcon gear={gear} slot={slot} characterSpec={character.raiderIOCharacterData.active_spec_name}/>
            {/each}
        </div>
    </div>
        <!-- Second Gear Column-->
        <div class="flex lg:flex-col flex-row sm:mx-auto lg:mx-0 md:order-none gap-x-2">
            {#each [
                { slot: 'Hands', gear: equippedGear.hands },
                { slot: 'Waist', gear: equippedGear.waist },
                { slot: 'Legs', gear: equippedGear.legs },
                { slot: 'Feet', gear: equippedGear.feet },
                { slot: 'Ring 1', gear: equippedGear.ring1 },
                { slot: 'Ring 2', gear: equippedGear.ring2 },
                { slot: 'Trinket 1', gear: equippedGear.trinket1 },
                { slot: 'Trinket 2', gear: equippedGear.trinket2 }
            ] as { slot, gear }}
                <GearIcon gear={gear} slot={slot} characterSpec={character.raiderIOCharacterData.active_spec_name}/>
            {/each}
        </div>
    </div>
</div>