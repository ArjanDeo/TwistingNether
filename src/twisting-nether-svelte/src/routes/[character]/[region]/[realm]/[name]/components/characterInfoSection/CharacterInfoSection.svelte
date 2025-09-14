<script lang="ts">
	import { bosses, type CharacterData } from "$lib/types";
    import * as Tabs from '$lib/components/ui/tabs';
	import * as Table from "$lib/components/ui/table";
	import { bossIcons } from "$lib/metadata";
	import { getParseColor, getRaidDifficulty } from "$lib/common";

    let activeTab = $state('stats');
    
    let { character }: { character: CharacterData } = $props();
    function msToTime(ms: number): string {
        const totalSeconds = Math.floor(ms / 1000);
        const minutes = Math.floor(totalSeconds / 60);
        const seconds = totalSeconds % 60;

        const formattedMinutes = String(minutes).padStart(2, '0');
        const formattedSeconds = String(seconds).padStart(2, '0');

        return `${formattedMinutes}:${formattedSeconds}`;
    }
</script>
<div>
    <Tabs.Root bind:value={activeTab} class="w-full">
        <!-- Tab Buttons -->
        <Tabs.List class="flex border-b border-gray-300 dark:border-gray-700 mb-4">
            <Tabs.Trigger value="stats" class="px-4 py-2 text-sm font-medium">
            Stats
            </Tabs.Trigger>
            <Tabs.Trigger value="raid" class="px-4 py-2 text-sm font-medium">
            Raid
            </Tabs.Trigger>
            <Tabs.Trigger value="m+" class="px-4 py-2 text-sm font-medium">
            M+
            </Tabs.Trigger>
        </Tabs.List>

        <Tabs.Content value="raid" class="p-4 bg-gray-50 dark:bg-gray-800 rounded-md">
            <p class="text-2xl">Manaforge: Omega Progress</p>
            <ul class="text-lg">
                <li class="text-[#1eff00]">
                    {character.raiderIOCharacterData.raid_progression.manaforge_omega.normal_bosses_killed}/{character.raiderIOCharacterData.raid_progression.manaforge_omega.total_bosses} N
                </li>
                <li class="text-[#0070ff]">
                    {character.raiderIOCharacterData.raid_progression.manaforge_omega.heroic_bosses_killed}/{character.raiderIOCharacterData.raid_progression.manaforge_omega.total_bosses} H
                </li>
                <li class="text-[#a335ee]">
                    {character.raiderIOCharacterData.raid_progression.manaforge_omega.mythic_bosses_killed}/{character.raiderIOCharacterData.raid_progression.manaforge_omega.total_bosses} M
                </li>
            </ul>
            
            {#if character.raidBossesKilledThisWeek.length !== 0}
            <p>Bosses killed this week:</p>
                <ul class="mt-2">
                {#each character.raidBossesKilledThisWeek as encounter}
                <img src="https://wow.zamimg.com/images/wow/journal/ui-ej-{bosses.find(e => e.name == encounter.boss)?.slug}.jpg" alt="{encounter.boss} Image">
                    <li>{encounter.difficulty} {encounter.boss}</li>
                {/each}
                </ul>
            {:else}
            <p class="text-xl">No raid progress this week!</p>
            {/if}
            {#if character.raidPerformance && character.raidPerformance.bestPerformanceAverage != null} 
            <div class="mt-5">
                <h2 class="text-lg mb-2">Rankings for {getRaidDifficulty(character.raidPerformance.difficulty)}</h2>
                {#each character.raidPerformance.rankings as ranking }
                    {#if ranking.totalKills != 0}
                    <div class="flex flex-row gap-x-1">
                        <img src={bossIcons[ranking.encounter.id]} class="max-w-8" alt={ranking.encounter.name} />
                        <p>
                            <span style="color: {getParseColor(ranking.medianPercent)}" class="text-lg ml-2">{Math.ceil(ranking.medianPercent)}</span> 
                            - {ranking.spec} {character.raiderIOCharacterData.char_class}
                        </p>
                    </div>
                    {/if}
                {/each}
            </div>
            {/if}
        </Tabs.Content>

        <Tabs.Content value="m+" class="p-4 bg-gray-50 dark:bg-gray-800 rounded-md max-w-xl">
            <h1 class="text-3xl" style="color: {character.classColor};">S3 Top Mythic+ Runs:</h1>
            <div class="rounded-2xl overflow-hidden shadow-md w-full text-white">
                <Table.Root class="w-full table-auto">
                    <Table.Header>
                    <Table.Row class="bg-muted/30 text-base">
                        <Table.Head class="font-semibold">Dungeon</Table.Head>
                        <Table.Head class="font-semibold">Time Taken</Table.Head>
                        <Table.Head class="font-semibold">Date</Table.Head>
                    </Table.Row>
                    </Table.Header>
                    <Table.Body>
                    {#if character.raiderIOCharacterData.mythic_plus_best_runs.length === 0}
                        <Table.Row>
                        <Table.Cell colspan={3}  class="text-center py-4">No Mythic+ runs found.</Table.Cell>
                        </Table.Row>
                    {:else}
                        {#each character.raiderIOCharacterData.mythic_plus_best_runs as run}
                        <Table.Row
                            class="relative text-center text-lg bg-cover bg-center"
                            style="background-image: url({run.background_image_url});"
                        >
                        <Table.Cell class="bg-black/40 py-2 px-3 flex items-center gap-2">
                            <img src="{run.icon_url}" alt="{run.dungeon}" class="w-10 h-10 rounded-sm" loading="lazy" />
                            <span class="w-12 text-right {run.num_keystone_upgrades === 0 ? 'text-gray-500' : ''}">
                                {'+'.repeat(run.num_keystone_upgrades)}{run.mythic_level}
                            </span>
                            <p class="text-left">{run.dungeon}</p>
                            </Table.Cell>
                            <Table.Cell class="bg-black/40 py-2 px-3">
                            {msToTime(run.clear_time_ms)}
                            </Table.Cell>
                            <Table.Cell class="bg-black/40 py-2 px-3">
                            {new Date(run.completed_at).toLocaleDateString()}
                            </Table.Cell>
                        </Table.Row>
                        {/each}
                    {/if}
                    </Table.Body>
                </Table.Root>
            </div>
        </Tabs.Content>

        <Tabs.Content value="stats" class="p-4 bg-gray-50 dark:bg-gray-800 rounded-md">
            <h2 class="text-xl font-semibold">Stats</h2>
            <p class="text-lg">Item Level: {character.raiderIOCharacterData.gear.item_level_equipped}</p>
        </Tabs.Content>
        </Tabs.Root>
</div>