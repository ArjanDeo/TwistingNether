<script lang="ts">
	import { dev } from "$app/environment";
	import { API_BASE_URL } from "$lib/common";
	import { ItemQuality, type Affix, type CharacterData } from "$lib/types";
	import { onMount } from "svelte";

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

    const topRuns = () => {
        return character.raiderIOCharacterData.mythic_plus_weekly_highest_level_runs
            .slice(0, mythicRunAmount);
    };
async function getAffixMedia() {
    for (let i: number = 0; i < uniqueSortedAffixIds.length; i++) {
        
       
    let response = await fetch(`${API_BASE_URL}/keystone/getAffixMedia?id=${uniqueSortedAffixIds[i]}`)
    affixList.push(await response.json());
  }
}
const uniqueSortedAffixIds = [
    ...new Set(
      character.raiderIOCharacterData.mythic_plus_weekly_highest_level_runs
        .flatMap(run => run.affixes.map(affix => affix.id))
    )
  ].sort((a, b) => a - b);

onMount(async () => {
    await getAffixMedia();
    for (let i = 0; i < mythicRunAmount; i++) {
        // Do something with the mythic runs
    }
});
const hasEpicMilestone = [
head, neck, shoulders, back, chest, wrists, hands, waist, legs, feet, 
ring1, ring2, trinket1, trinket2, mainhand, offhand
].every(item => item && item.item_quality >= ItemQuality.Epic);
    let affixList: Affix[] = $state([]);
    let mythicRunAmount = $state(3);    
    async function updateCharacter() {

       let updatedCharacter = await fetch(`${API_BASE_URL}/character/updatecharacter`, {
            method: 'PUT',
            body: JSON.stringify({
                region: character.raiderIOCharacterData.region,
                realm: character.raiderIOCharacterData.realm,
                name: character.raiderIOCharacterData.name
            }),
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!updatedCharacter.ok) {
           alert('Failed to update character: ' + updatedCharacter.statusText + '\n' + await updatedCharacter.text());
        } else {
            alert('Character updated successfully! Please refresh the page to see the changes.\nNote: If there are no changes, try updating the character on the Raider.IO website first.');
            if (dev) {
                console.log('Character updated successfully! (dev log):', await updatedCharacter.json());
            }
        }
    }
</script>

<div class="lg:ml-24 lg:order-1 order-last lg:mt-28">                
                <button onclick={() => updateCharacter()} style="color:{character.classColor}" class="btn btn-outline mb-2">Update Character</button>
                <p class="text-2xl">Item Level: <span style="color:{hasEpicMilestone === true ? '#a335ee' : '#0070dd'}">{Math.round(character.raiderIOCharacterData.gear.item_level_equipped)}</span></p>
                <p class="text-2xl">M+ Score: <span style="color:{character.raiderIOCharacterData.mythic_plus_scores_by_season[0].segments.all.color}">{character.raiderIOCharacterData.mythic_plus_scores_by_season[0].scores.all}</span></p>
                <p class="text-2xl">Liberation of Undermine Progress:</p>
                <ul>
                    {#if character.raiderIOCharacterData.raid_progression.liberationofundermine.normal_bosses_killed > 0}
                    <li class="text-xl text-[#1eff00]">
                        {character.raiderIOCharacterData.raid_progression.liberationofundermine.normal_bosses_killed}/{character.raiderIOCharacterData.raid_progression.liberationofundermine.total_bosses}N
                    </li>
                    {:else}
                    <li class="text-xl text-[#1eff00]">
                        0/{character.raiderIOCharacterData.raid_progression.liberationofundermine.total_bosses}N    
                    </li>
                    {/if}
                    {#if character.raiderIOCharacterData.raid_progression.liberationofundermine.heroic_bosses_killed > 0}
                    <li class="text-xl text-[#1873da]">
                        {character.raiderIOCharacterData.raid_progression.liberationofundermine.heroic_bosses_killed}/{character.raiderIOCharacterData.raid_progression.liberationofundermine.total_bosses}H
                    </li>
                    {:else}
                    <li class="text-xl text-[#1873da] flex">
                        <span>0/{character.raiderIOCharacterData.raid_progression.liberationofundermine.total_bosses}H</span>
                        {#if character.raiderIOCharacterData.raid_achievement_curve[0] != undefined && character.raiderIOCharacterData.raid_achievement_curve[0].aotc != undefined}
                        {character.raiderIOCharacterData.raid_achievement_curve[0].aotc}
                        <span class="badge">AOTC</span>
                        {/if}
                    </li>
                    {/if}
                    {#if character.raiderIOCharacterData.raid_progression.liberationofundermine.mythic_bosses_killed > 0}
                    <li class="text-xl text-[#a837e8]">
                        {character.raiderIOCharacterData.raid_progression.liberationofundermine.mythic_bosses_killed}/{character.raiderIOCharacterData.raid_progression.liberationofundermine.total_bosses}M
                    </li>
                    {:else}
                    <li class="text-xl text-[#a837e8]">
                        0/{character.raiderIOCharacterData.raid_progression.liberationofundermine.total_bosses}M
                    </li>
                    {/if}
                </ul>
                <div class="flex">
                    <h1 class="text-3xl mt-10">Most recent M+ runs:</h1>
                    <select style="border-color:{character.classColor}; outline-color:{character.classColor}; color: {character.classColor}" class="bg-slate-800 h-fit mt-10 ml-1 input input-primary" bind:value={mythicRunAmount}>                        
                        <option value="3" selected>Top 3</option>
                        <option value="5">Top 5</option>
                        <option value="10">Top 10</option>
                    </select>
                </div>
                {#each topRuns() as run}
                    <a href="{run.url}" target="_blank">
                        <div style="--classColor: {character.classColor}; border-color: {character.classColor};" class="border my-1 rounded-lg px-1 hover:bg-[var(--classColor)] hover:text-black transition-colors ease-in-out">
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