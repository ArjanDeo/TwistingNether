<script lang="ts">
	import { ItemQuality, type CharacterData } from '$lib/types';
    import type { PageData } from './$types';

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
</script>
<div>
    <header class="mb-8">
            <h1 class="text-2xl lg:text-4xl font-semibold">
                {character.raiderIOCharacterData.name}-{character.raiderIOCharacterData.realm} 
                {#if character.raiderIOCharacterData.guild != null}
                <a target="_blank" class="hover:text-purple-700 transition-colors ease-in-out duration-300" href="https://worldofwarcraft.blizzard.com/en-us/guild/{character.raiderIOCharacterData.region}/{character.raiderIOCharacterData.guild.realm.replace('\'', "")}/{character.raiderIOCharacterData.guild.name.replace(' ', '-')}/">&lt;{character.raiderIOCharacterData.guild.name}&gt;</a>
                {/if}
            </h1>
            <div class="mt-2 flex md:flex-row gap-x-5 gap-y-2 md:gap-y-0">
                <div style="color: {character.classColor}; border-color: {character.classColor};" class="text-center md:text-xl badge p-4">{character.raiderIOCharacterData.race}</div>
                <div style="color: {character.classColor}; border-color: {character.classColor};" class="text-center md:text-xl badge p-4">{character.raiderIOCharacterData.active_spec_name} {character.raiderIOCharacterData.char_class}</div> 
                <a aria-label="raider.io link" href="https://raider.io/characters/{character.raiderIOCharacterData.region}/{character.raiderIOCharacterData.realm}/{character.raiderIOCharacterData.name}" target="_blank">
                    <svg width="45" height="40">
                        <image xlink:href="https://cdn.raiderio.net/images/brand/Icon_2ColorWhite.svg" width="36" height="36"/>    
                   </svg>
                </a>
                <a class="ml-1" href="https://www.warcraftlogs.com/character/{character.raiderIOCharacterData.region}/{character.raiderIOCharacterData.realm}/{character.raiderIOCharacterData.name}" target="_blank">
                    <img src="https://assets.rpglogs.com/cms/WCL_Icon_57fc009f4e.png" class="w-10" alt="warcraft logs logo"/>
                </a>
                <a class="ml-1" href="https://worldofwarcraft.blizzard.com/en-gb/character/{character.raiderIOCharacterData.region}/{character.raiderIOCharacterData.realm}/{character.raiderIOCharacterData.name}" target="_blank">
                    <img src="https://logos-download.com/wp-content/uploads/2016/02/WOW_logo.png" class="w-10" alt="world of warcraft logo"/>
                </a>
            </div>
        </header>        
        <div class="md:flex mb-8 items-start md:mx-auto lg:mx-0 flex-wrap lg:flex-none">           
            <div class="flex lg:flex-col flex-row sm:mx-auto lg:mx-0 md:order-none order-1 gap-x-2">
                {#each [head, neck, shoulders, back, chest] as item}
                <div class="has-tooltip flex items-center">
                    <a target="_blank" href="https://www.wowhead.com/item={item.item_id}/">
                        <img style="border-color: {itemRarityColor(item.item_quality)};" class="max-w-11 mb-2 hover:cursor-pointer border" src="https://wow.zamimg.com/images/wow/icons/large/{item.icon}.jpg" alt="{item.name}">
                    </a>
                    {#if item.item_quality == ItemQuality.Common}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#ffffff] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Uncommon}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#1eff00] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Rare}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#0070dd] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Epic}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#a335ee] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Legendary}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#ff8000] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Artifact}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#e6cc80] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Heirloom}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#00ccff] -mt-8">{item.name}</span>
                    {:else}
                    <span style="color: {character?.classColor}" class="tooltip rounded shadow-lg p-1 bg-slate-700 text-heirloom -mt-8">{item.name}</span>
                    {/if}
                </div>
                {/each}
                <div class="has-tooltip flex flex-row items-center">
                    <img style="border-color: {itemRarityColor(1)};" class="max-w-11 mb-2 hover:cursor-pointer border" src="https://wow.zamimg.com/images/wow/icons/large/inv_shirt_guildtabard_01.jpg" alt="inv_shirt_guildtabard_01">
                     <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#ffffff] -mt-8">Personal Tabard</span>
                </div>
                <div class="has-tooltip flex flex-row items-center">
                    <img style="border-color: {itemRarityColor(1)};" class="max-w-11 mb-2 hover:cursor-pointer border" src="https://wow.zamimg.com/images/wow/icons/large/inv_shirt_white_01.jpg" alt="inv_shirt_white_01">
                     <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#ffffff] -mt-8">Common White Shirt</span>
                </div>
                <div class="has-tooltip flex flex-row items-center">
                    <a target="_blank" href="https://www.wowhead.com/item={wrists.item_id}/">
                        <img style="border-color: {itemRarityColor(wrists.item_quality)};" class="max-w-11 mb-2 hover:cursor-pointer border" src="https://wow.zamimg.com/images/wow/icons/large/{wrists.icon}.jpg" alt="{wrists.name}">
                    </a>
                    {#if wrists.item_quality == ItemQuality.Common}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#ffffff] -mt-8">{wrists.name}</span>
                    {:else if wrists.item_quality == ItemQuality.Uncommon}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#1eff00] -mt-8">{wrists.name}</span>
                    {:else if wrists.item_quality == ItemQuality.Rare}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#0070dd] -mt-8">{wrists.name}</span>
                    {:else if wrists.item_quality == ItemQuality.Epic}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#a335ee] -mt-8">{wrists.name}</span>
                    {:else if wrists.item_quality == ItemQuality.Legendary}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#ff8000] -mt-8">{wrists.name}</span>
                    {:else if wrists.item_quality == ItemQuality.Artifact}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#e6cc80] -mt-8">{wrists.name}</span>
                    {:else if wrists.item_quality == ItemQuality.Heirloom}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#00ccff] -mt-8">{wrists.name}</span>
                    {:else}
                    <span style="color: {character?.classColor}" class="tooltip rounded shadow-lg p-1 bg-slate-700 text-heirloom -mt-8">{wrists.name}</span>
                    {/if}
                </div>
            </div>
            <img 
                src={character.characterMedia ? character.characterMedia[2].link : '/default-image.png'} 
                alt="Character Render" 
                class=" sm:order-last md:order-none md:max-w-2xl lg:max-w-3xl lg:max-h-max object-cover mx-0"
            />
            <div class="flex lg:flex-col flex-row sm:mx-auto lg:mx-0 md:order-none gap-x-2">
                {#each [hands, waist, legs, feet, ring1, ring2, trinket1, trinket2] as item}
                <div class="has-tooltip flex flex-row items-center">
                    <a target="_blank" href="https://www.wowhead.com/item={item.item_id}/">
                        <img style="border-color: {itemRarityColor(item.item_quality)};" class="max-w-11 mb-2 hover:cursor-pointer border" src="https://wow.zamimg.com/images/wow/icons/large/{item.icon}.jpg" alt="{item.name}">
                    </a>
                    {#if item.item_quality == ItemQuality.Common}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#ffffff] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Uncommon}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#1eff00] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Rare}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#0070dd] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Epic}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#a335ee] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Legendary}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#ff8000] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Artifact}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#e6cc80] -mt-8">{item.name}</span>
                    {:else if item.item_quality == ItemQuality.Heirloom}
                    <span class="tooltip rounded shadow-lg p-1 bg-slate-700 text-[#00ccff] -mt-8">{item.name}</span>
                    {:else}
                    <span style="color: {character?.classColor}" class="tooltip rounded shadow-lg p-1 bg-slate-700 text-heirloom -mt-8">{item.name}</span>
                    {/if}
                </div>
            {/each}
        </div>
    </div>
</div>