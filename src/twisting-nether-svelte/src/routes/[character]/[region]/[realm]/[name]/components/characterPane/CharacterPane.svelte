<script lang="ts">
	import { ItemQuality, type CharacterData } from '$lib/types';
    import * as Tooltip from '$lib/components/ui/tooltip';
    import { itemQualityColor } from '$lib/utils';
	import { Badge } from '$lib/components/ui/badge';
    import { classIcons, raceIcons } from '$lib/metadata';
    let { character }: { character: CharacterData } = $props();

    let head = character.raiderIOCharacterData.gear.items.head;
    let neck = character.raiderIOCharacterData.gear.items.neck;
    let shoulders = character.raiderIOCharacterData.gear.items.shoulder;
    let back = character.raiderIOCharacterData.gear.items.back;
    let chest = character.raiderIOCharacterData.gear.items.chest;
    let wrists = character.raiderIOCharacterData.gear.items.wrist;
    let hands = character.raiderIOCharacterData.gear.items.hands;
    let waist = character.raiderIOCharacterData.gear.items.waist;
    let legs = character.raiderIOCharacterData.gear.items.legs;
    let feet = character.raiderIOCharacterData.gear.items.feet;
    let ring1 = character.raiderIOCharacterData.gear.items.finger1;
    let ring2 = character.raiderIOCharacterData.gear.items.finger2;
    let trinket1 = character.raiderIOCharacterData.gear.items.trinket1;
    let trinket2 = character.raiderIOCharacterData.gear.items.trinket2;
    let mainhand = character.raiderIOCharacterData.gear.items.mainhand;
    let offhand = character.raiderIOCharacterData.gear.items.offhand;

const hasEpicMilestone = [
head, neck, shoulders, back, chest, wrists, hands, waist, legs, feet, 
ring1, ring2, trinket1, trinket2, mainhand, offhand
].every(item => item && item.item_quality >= ItemQuality.Epic);

function itemRarityColor(itemQuality: number): string {
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
<div class="">
    <header class="mb-8">
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
                    <img src="/raiderioicon.png" class="w-10 h-10" alt="raider io logo">
                </a>
                <a class="ml-1" href="https://www.warcraftlogs.com/character/{character.raiderIOCharacterData.region}/{character.raiderIOCharacterData.realm}/{character.raiderIOCharacterData.name}" target="_blank">
                    <img src="/warcraftlogsicon.png" class="w-10 h-10" alt="warcraft logs logo"/>
                </a>
                <a class="ml-1" href="https://worldofwarcraft.blizzard.com/en-gb/character/{character.raiderIOCharacterData.region}/{character.raiderIOCharacterData.realm}/{character.raiderIOCharacterData.name}" target="_blank">
                    <img src="/wowicon.png" class="w-10 h-10" alt="world of warcraft logo"/>
                </a>
            </div>
        </header>        
        <div class="md:flex mb-8 items-start md:mx-auto lg:mx-0 flex-wrap lg:flex-none">           
            <div class="flex lg:flex-col flex-row sm:mx-auto lg:mx-0 md:order-none order-1 gap-x-2">
                {#each [head, neck, shoulders, back, chest] as item}
                <Tooltip.Provider>
                    <Tooltip.Root>
                        <Tooltip.Trigger>
                            <div class="flex items-center">
                                <a target="_blank" href="https://www.wowhead.com/item={item.item_id}/">
                                    <img style="border-color: {itemRarityColor(item.item_quality)};" class="max-w-11 mb-2 hover:cursor-pointer border" src="https://wow.zamimg.com/images/wow/icons/large/{item.icon}.jpg" alt="{item.name}">
                                </a>                                
                            </div>
                        </Tooltip.Trigger>                          
                            <Tooltip.Content class="bg-slate-700 text-[{itemQualityColor(item.item_quality)}]">
                                <span style="color: {itemQualityColor(item.item_quality)}" class="rounded shadow-lg">{item.name}</span>
                            </Tooltip.Content>
                    </Tooltip.Root>
                </Tooltip.Provider>
                {/each}
                <Tooltip.Provider>
                    <Tooltip.Root>
                        <Tooltip.Trigger>
                            <div class="flex items-center">
                                <img style="border-color: {itemRarityColor(1)};" class="max-w-11 mb-2 hover:cursor-pointer border" src="https://wow.zamimg.com/images/wow/icons/large/inv_shirt_guildtabard_01.jpg" alt="inv_shirt_guildtabard_01">
                            </div>
                        </Tooltip.Trigger>                          
                            <Tooltip.Content class="bg-slate-700 text-[{itemQualityColor(1)}]">
                                <span class="rounded shadow-lg p-1 bg-slate-700 text-[#ffffff] -mt-8 mx-auto">Personal Tabard</span>
                            </Tooltip.Content>
                    </Tooltip.Root>
                </Tooltip.Provider>
                <Tooltip.Provider>
                    <Tooltip.Root>
                        <Tooltip.Trigger>
                            <div class="flex items-center">
                                <img style="border-color: {itemRarityColor(1)};" class="max-w-11 mb-2 hover:cursor-pointer border" src="https://wow.zamimg.com/images/wow/icons/large/inv_shirt_white_01.jpg" alt="inv_shirt_white_01">
                            </div>
                        </Tooltip.Trigger>                          
                            <Tooltip.Content class="bg-slate-700 text-[{itemQualityColor(1)}]">
                                <span class="rounded shadow-lg p-1 bg-slate-700 text-[#ffffff] -mt-8 mx-auto">Common White Shirt</span>
                            </Tooltip.Content>
                    </Tooltip.Root>
                </Tooltip.Provider>        
                <Tooltip.Provider>
                    <Tooltip.Root>
                        <Tooltip.Trigger>
                            <div class="flex items-center">
                                <a target="_blank" href="https://www.wowhead.com/item={wrists.item_id}/">
                                    <img style="border-color: {itemRarityColor(wrists.item_quality)};" class="max-w-11 mb-2 hover:cursor-pointer border" src="https://wow.zamimg.com/images/wow/icons/large/{wrists.icon}.jpg" alt="{wrists.name}">
                                </a>                                
                            </div>
                        </Tooltip.Trigger>                          
                            <Tooltip.Content class="bg-slate-700 text-[{itemQualityColor(wrists.item_quality)}]">
                                <span class="rounded shadow-lg">{wrists.name}</span>
                            </Tooltip.Content>
                    </Tooltip.Root>
                </Tooltip.Provider>
            </div>
            <img 
                src={character.characterMedia ? character.characterMedia[2].link : '/default-image.png'} 
                alt="Character Render" 
                class=" sm:order-last md:order-none md:max-w-2xl lg:max-w-3xl lg:max-h-max fit mx-0"
            />
            <div class="flex lg:flex-col flex-row sm:mx-auto lg:mx-0 md:order-none gap-x-2">
                {#each [hands, waist, legs, feet, ring1, ring2, trinket1, trinket2] as item}
                <Tooltip.Provider>
                    <Tooltip.Root>
                        <Tooltip.Trigger>
                            <div class="flex items-center">
                                <a target="_blank" href="https://www.wowhead.com/item={item.item_id}/">
                                    <img style="border-color: {itemRarityColor(item.item_quality)};" class="max-w-11 mb-2 hover:cursor-pointer border" src="https://wow.zamimg.com/images/wow/icons/large/{item.icon}.jpg" alt="{item.name}">
                                </a>                                
                            </div>
                        </Tooltip.Trigger>                          
                            <Tooltip.Content class="bg-slate-700 text-[{itemQualityColor(item.item_quality)}]">
                                <span style="color: {itemQualityColor(item.item_quality)}" class="rounded shadow-lg">{item.name}</span>
                            </Tooltip.Content>
                    </Tooltip.Root>
                </Tooltip.Provider>
            {/each}
        </div>
    </div>
</div>