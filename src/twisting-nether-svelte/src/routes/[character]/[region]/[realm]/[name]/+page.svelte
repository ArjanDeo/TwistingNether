<script lang="ts">
    import type { PageData } from './$types';
    
    let { data }: { data: PageData } = $props();
	import { ItemQuality, type Affix } from '$lib/types';
	import { onMount } from 'svelte';
	import { run } from 'svelte/legacy';

    let head = data.character.raiderIOCharacterData.gear.items.head;
    let neck = data.character.raiderIOCharacterData.gear.items.neck;
    let shoulders = data.character.raiderIOCharacterData.gear.items.shoulder;
    let back = data.character.raiderIOCharacterData.gear.items.back;
    let chest = data.character.raiderIOCharacterData.gear.items.chest;
    let wrists = data.character.raiderIOCharacterData.gear.items.wrist;
    let hands = data.character.raiderIOCharacterData.gear.items.hands;
    let waist = data.character.raiderIOCharacterData.gear.items.waist;
    let legs = data.character.raiderIOCharacterData.gear.items.legs;
    let feet = data.character.raiderIOCharacterData.gear.items.feet;
    let ring1 = data.character.raiderIOCharacterData.gear.items.finger1;
    let ring2 = data.character.raiderIOCharacterData.gear.items.finger2;
    let trinket1 = data.character.raiderIOCharacterData.gear.items.trinket1;
    let trinket2 = data.character.raiderIOCharacterData.gear.items.trinket2;
    let mainhand = data.character.raiderIOCharacterData.gear.items.mainhand;
    let offhand = data.character.raiderIOCharacterData.gear.items.offhand;
    let affixList: Affix[] = $state([]);
    let loading: boolean = $state(true);
    let mythicRunAmount = $state(3);
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
const hasEpicMilestone = [
head, neck, shoulders, back, chest, wrists, hands, waist, legs, feet, 
ring1, ring2, trinket1, trinket2, mainhand, offhand
].every(item => item && item.item_quality >= ItemQuality.Epic);

const uniqueSortedAffixIds = [
    ...new Set(
      data.character.raiderIOCharacterData.mythic_plus_weekly_highest_level_runs
        .flatMap(run => run.affixes.map(affix => affix.id))
    )
  ].sort((a, b) => a - b);
async function getAffixMedia() {
    for (let i: number = 0; i < uniqueSortedAffixIds.length; i++) {
    let response = await fetch(`https://localhost:7176/api/keystone/get-affix-media?id=${uniqueSortedAffixIds[i]}`)
    affixList.push(await response.json());
    console.log(affixList)
  }
}
const topRuns = () => {
        return data.character.raiderIOCharacterData.mythic_plus_weekly_highest_level_runs
            .slice(0, mythicRunAmount);
    };
    function updateCharacter() {
        alert('This function has not been implemented yet.')
    }
onMount(async () => {
    await getAffixMedia();
    for (let i = 0; i < mythicRunAmount; i++)
    loading = false;
	});
</script>
<svelte:head>
	<title>{data.character.raiderIOCharacterData.name} - {data.character.raiderIOCharacterData.realm}</title>
</svelte:head>
{#if data.character && !loading}
    <div class="flex flex-col p-8 min-h-screen" style="color: {data.character.classColor};">
        <!-- Character Header -->
        <header class=" mb-8">
            <h1 class="text-2xl lg:text-4xl font-semibold">
                {data.character.raiderIOCharacterData.name}-{data.character.raiderIOCharacterData.realm} <a target="_blank" class="hover:text-purple-700 transition-colors ease-in-out duration-300" href="https://worldofwarcraft.blizzard.com/en-us/guild/{data.character.raiderIOCharacterData.region}/{data.character.raiderIOCharacterData.guild.realm.replace('\'', "")}/{data.character.raiderIOCharacterData.guild.name.replace(' ', '-')}/">&lt;{data.character.raiderIOCharacterData.guild.name}&gt;</a>
            </h1>
            <div class="mt-2">
                <div style="color: {data.character.classColor}; border-color: {data.character.classColor};" class="text-center text-xl badge p-3">{data.character.raiderIOCharacterData.race}</div>
                <div style="color: {data.character.classColor}; border-color: {data.character.classColor};" class="text-center text-xl badge p-3">{data.character.raiderIOCharacterData.active_spec_name} {data.character.raiderIOCharacterData.char_class}</div> 
            </div>
        </header>
        
        <div class="lg:flex mb-8 items-start md:mx-auto lg:mx-0">           
            <div class="flex lg:flex-col flex-row sm:mx-auto lg:mx-0 lg:order-none order-1 md:gap-x-2">
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
                    <span style="color: {data.character?.classColor}" class="tooltip rounded shadow-lg p-1 bg-slate-700 text-heirloom -mt-8">{item.name}</span>
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
                    <span style="color: {data.character?.classColor}" class="tooltip rounded shadow-lg p-1 bg-slate-700 text-heirloom -mt-8">{wrists.name}</span>
                    {/if}
                </div>
            </div>
            <img 
                src={data.character.characterMedia ? data.character.characterMedia[2].link : '/default-image.png'} 
                alt="Character Render" 
                class=" sm:order-last lg:order-none md:max-w-2xl lg:max-w-3xl lg:max-h-max object-cover mx-0"
            />
            <div class="flex lg:flex-col flex-row sm:mx-auto lg:mx-0 lg:order-none order-2 md:gap-x-2">
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
                    <span style="color: {data.character?.classColor}" class="tooltip rounded shadow-lg p-1 bg-slate-700 text-heirloom -mt-8">{item.name}</span>
                    {/if}
                </div>
            {/each}
            </div>
            <div class="lg:ml-24 lg:order-none order-last">
                <button onclick={() => updateCharacter()} style="color:{data.character.classColor}" class="btn btn-outline mb-2">Update Character</button>
                <p class="text-2xl">Item Level: <span style="color:{hasEpicMilestone === true ? '#a335ee' : '#0070dd'}">{Math.round(data.character.raiderIOCharacterData.gear.item_level_equipped)}</span></p>
                <p class="text-2xl">M+ Score: <span style="color:{data.character.raiderIOCharacterData.mythic_plus_scores_by_season[0].segments.all.color}">{data.character.raiderIOCharacterData.mythic_plus_scores_by_season[0].scores.all}</span></p>
                <p class="text-2xl">Nerub-ar Palace Progress:</p>
                <ul>
                    {#if data.character.raiderIOCharacterData.raid_progression.nerubarpalace.normal_bosses_killed > 0}
                    <li class="text-xl text-[#1eff00]">
                        {data.character.raiderIOCharacterData.raid_progression.nerubarpalace.normal_bosses_killed}/{data.character.raiderIOCharacterData.raid_progression.nerubarpalace.total_bosses}N
                    </li>
                    {:else}
                    <li class="text-xl text-[#1eff00]">
                        0/{data.character.raiderIOCharacterData.raid_progression.nerubarpalace.total_bosses}N    
                    </li>
                    {/if}
                    {#if data.character.raiderIOCharacterData.raid_progression.nerubarpalace.heroic_bosses_killed > 0}
                    <li class="text-xl text-[#1873da]">
                        {data.character.raiderIOCharacterData.raid_progression.nerubarpalace.heroic_bosses_killed}/{data.character.raiderIOCharacterData.raid_progression.nerubarpalace.total_bosses}H
                    </li>
                    {:else}
                    <li class="text-xl text-[#1873da]">
                        0/{data.character.raiderIOCharacterData.raid_progression.nerubarpalace.total_bosses}H
                    </li>
                    {/if}
                    {#if data.character.raiderIOCharacterData.raid_progression.nerubarpalace.mythic_bosses_killed > 0}
                    <li class="text-xl text-[#a837e8]">
                        {data.character.raiderIOCharacterData.raid_progression.nerubarpalace.mythic_bosses_killed}/{data.character.raiderIOCharacterData.raid_progression.nerubarpalace.total_bosses}M
                    </li>
                    {:else}
                    <li class="text-xl text-[#a837e8]">
                        0/{data.character.raiderIOCharacterData.raid_progression.nerubarpalace.total_bosses}M
                    </li>
                    {/if}
                </ul>
                <div class="flex">
                    <h1 class="text-3xl mt-10">Most recent M+ runs:</h1>
                    <select style="border-color:{data.character.classColor}; outline-color:{data.character.classColor}; color: {data.character.classColor}" class="bg-slate-800 h-fit mt-10 ml-1 input input-primary" bind:value={mythicRunAmount}>
                        <option value="3" selected>Top 3</option>
                        <option value="5">Top 5</option>
                        <option value="10">Top 10</option>
                    </select>
                </div>
                {#each topRuns() as run}
                    <a href="{run.url}" target="_blank">
                        <div style="--classColor: {data.character.classColor}; border-color: {data.character.classColor};" class="border my-1 rounded-lg px-1 hover:bg-[var(--classColor)] hover:text-black transition-colors ease-in-out">
                            <h2 class="text-2xl">{'+'.repeat(run.num_keystone_upgrades)}{run.mythic_level} {run.dungeon}</h2>
                            <div class="flex gap-x-3 justify-center">
                                {#each run.affixes as affix}                            
                                <img class="max-w-10 my-1" alt="{affix.name}" src="{affixList.find((item) => item.id === affix.id)?.icon || "Unknown"}"/>
                                
                                {/each}
                            </div>
                        </div>
                    </a>
                {/each}
            </div>
        </div>
    </div>
    {:else if loading}
        <span class="loading loading-spinner text-success mx-auto block justify-center scale-150 self-center my-auto"></span>
{/if}