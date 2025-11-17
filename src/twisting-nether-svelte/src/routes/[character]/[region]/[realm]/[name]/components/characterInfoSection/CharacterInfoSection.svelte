<script lang="ts">
    import { bosses, getScoreColor, type CharacterData } from "$lib/types";
    import * as Tooltip from '$lib/components/ui/tooltip';
    import { CircleQuestionMark } from '@lucide/svelte'
    import * as Tabs from '$lib/components/ui/tabs';
    import { bossIcons } from "$lib/metadata";
    import { API_BASE_URL, getParseColor, getRaidDifficultyId, getRaidDifficultyString } from "$lib/common";
	import { onMount } from "svelte";
	import type { Character, WeeklyBosses } from "$lib/types/character";
	import { toast } from "svelte-sonner";
	import type { RaidPerformance } from "$lib/types/warcraftLogs";
    import * as DropdownMenu from "$lib/components/ui/dropdown-menu"

    let activeTab: string = $state('stats');
    let sortedEncounters: any = $state([]);
    let raidBossesKilledThisWeek: WeeklyBosses[] | undefined = $state();
    let raidPerformance: RaidPerformance | undefined = $state();
    let raidPerformanceDifficulty: "LFR" | "Flex" | "Normal" | "Heroic" | "Mythic" | "M+" | "Unknown" | undefined = $state();
    let { character }: { character: Character | undefined } = $props();
    
    function msToTime(ms: number): string {
        const totalSeconds = Math.floor(ms / 1000);
        const minutes = Math.floor(totalSeconds / 60);
        const seconds = totalSeconds % 60;

        const formattedMinutes = String(minutes).padStart(2, '0');
        const formattedSeconds = String(seconds).padStart(2, '0');

        return `${formattedMinutes}:${formattedSeconds}`;
    }

    function getProgressPercent(current: number, total: number): number {
        return (current / total) * 100;
    }

    // Animation helper for staggered entrance
    function stagger(index: number): string {
        return `animation-delay: ${index * 100}ms;`;
    }

    const bossIndex: Record<string, number> = bosses.reduce(
    (acc, b) => ({ ...acc, [b.name]: b.journalIndex }),
    {}
  );

  async function setWeeklyBosses(name: string, realm: string, region: string) {
        const res = await fetch(`${API_BASE_URL}/characters/weekly-bosses?name=${name}&realm=${realm}&region=${region}`);

        if (!res.ok) {
            toast.error('Couldn\'t get character weekly lockout.');
            return;
        }
        raidBossesKilledThisWeek = await res.json();
        return;
    }

      async function setRaidPerformance(name: string, realm: string, region: string, difficulty: number) {
        raidPerformance = undefined;
        const res = await fetch(`${API_BASE_URL}/characters/zone-rankings?name=${name}&realm=${realm}&region=${region}&difficulty=${difficulty}`);

        if (!res.ok) {
            toast.error('Couldn\'t get character raid performance.');
            return;
        }
        raidPerformance = await res.json();
        if (raidPerformance && raidPerformance.difficulty)  
            raidPerformanceDifficulty = getRaidDifficultyString(raidPerformance.difficulty);
        return;
    }
onMount(async () => {
    if (character) {
        await setWeeklyBosses(character.characterData.name, character.characterData.realm, character.characterData.region);
        await setRaidPerformance(character.characterData.name, character.characterData.realm, character.characterData.region, 0);
        if (raidBossesKilledThisWeek)
            sortedEncounters = [...raidBossesKilledThisWeek].sort(
                (a, b) => (bossIndex[a.boss] ?? 999) - (bossIndex[b.boss] ?? 999)
            );
    }
  });
$effect(() => {
  if (!character || !raidPerformanceDifficulty) return;

  setRaidPerformance(
    character.characterData.name,
    character.characterData.realm,
    character.characterData.region,
    getRaidDifficultyId(raidPerformanceDifficulty)
  );
});
</script>

<style>
    .staggered-item {
        opacity: 0;
        animation: fadeInUp 0.6s ease-out forwards;
    }
    
    .progress-bar {
        transition: width 0.8s ease-out;
    }
    
    .glow {
        box-shadow: 0 0 20px rgba(59, 130, 246, 0.3);
    }
    
    .hover-lift {
        transition: transform 0.3s ease, box-shadow 0.3s ease;
    }
    
    .hover-lift:hover {
        transform: translateY(-2px);
        box-shadow: 0 8px 25px rgba(0, 0, 0, 0.3);
    }
    
    @keyframes slideInFromBottom {
        from {
            opacity: 0;
            transform: translateY(20px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
    
    @keyframes fadeInUp {
        from {
            opacity: 0;
            transform: translateY(10px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
    
    .mythic-card {
        position: relative;
        overflow: hidden;
        background-size: cover;
        background-position: center;
        transition: all 0.3s ease;
    }
    
    .mythic-card:hover {
        transform: scale(1.02);
        box-shadow: 0 10px 30px rgba(0, 0, 0, 0.5);
    }
    
    .mythic-card::before {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background: linear-gradient(135deg, rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0.9));
        z-index: 1;
    }
    
    .mythic-card > * {
        position: relative;
        z-index: 2;
    }
</style>
{#if character}
<div class="w-4xl h-fit mx-auto p-6 bg-gradient-to-br from-gray-900 via-gray-800 to-gray-900 text-white rounded-2xl shadow-2xl border border-gray-700">
    <!-- Enhanced Header -->
    <div class="text-center mb-8 relative">
        <div class="absolute inset-0 bg-gradient-to-r from-blue-500/10 via-purple-500/10 to-blue-500/10 rounded-xl blur-xl"></div>
        <div class="relative">
            <h1 class="text-4xl font-bold mb-2" style="color: {character.classColor}">
                {character.characterData.name || 'Character'}
            </h1>
            <p class="text-xl font-semibold" style="color: {character.classColor};">
                {character.characterData.active_spec_name} {character.characterData.char_class}
            </p>
        </div>
    </div>

    <Tabs.Root bind:value={activeTab} class="w-full ">
        <!-- Enhanced Tab Buttons -->
        <Tabs.List class="flex border-b-2 border-gray-700 mb-6 bg-gray-800/50 rounded-t-lg p-1">
            <Tabs.Trigger 
                value="stats" 
                class="flex-1 px-6 py-3 text-sm font-semibold rounded-md transition-all duration-300 relative overflow-hidden group"
            >
                <span class="flex items-center justify-center gap-2 relative z-10">
                    📊 Stats
                </span>
                {#if activeTab === 'stats'}
                    <div class="absolute inset-0 bg-gradient-to-r from-blue-500/20 to-purple-500/20 rounded-md"></div>
                {/if}
            </Tabs.Trigger>
            
            <Tabs.Trigger 
                value="raid" 
                class="flex-1 px-6 py-3 text-sm font-semibold rounded-md transition-all duration-300 relative overflow-hidden group"
            >
                <span class="flex items-center justify-center gap-2 relative z-10">
                    ⚔️ Raid
                </span>
                {#if activeTab === 'raid'}
                    <div class="absolute inset-0 bg-gradient-to-r from-red-500/20 to-orange-500/20 rounded-md"></div>
                {/if}
            </Tabs.Trigger>
            
            <Tabs.Trigger 
                value="m+" 
                class="flex-1 px-6 py-3 text-sm font-semibold rounded-md transition-all duration-300 relative overflow-hidden group"
            >
                <span class="flex items-center justify-center gap-2 relative z-10">
                    ⚡ M+
                </span>
                {#if activeTab === 'm+'}
                    <div class="absolute inset-0 bg-gradient-to-r from-yellow-500/20 to-amber-500/20 rounded-md"></div>
                {/if}
            </Tabs.Trigger>
        </Tabs.List>

        <!-- Enhanced Stats Tab -->
        <Tabs.Content value="stats" class="tab-enter">
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                <div class="staggered-item bg-gradient-to-br from-gray-800 to-gray-700 p-6 rounded-xl border border-gray-600 hover-lift glow" style="{stagger(0)}">
                    <div class="flex items-center gap-3 mb-3">
                        <span class="text-2xl">🛡️</span>
                        <h3 class="text-lg font-semibold">Item Level</h3>
                    </div>
                    <p class="text-3xl font-bold text-yellow-400">
                        {character.characterData.gear.item_level_equipped}
                    </p>
                    <div class="mt-2 h-1 bg-gray-600 rounded-full overflow-hidden">
                        <div class="h-full bg-gradient-to-r from-yellow-400 to-orange-500 progress-bar" 
                             style="width: {Math.min((character.characterData.gear.item_level_equipped / 500) * 100, 100)}%"></div>
                    </div>
                </div>

                <div class="staggered-item bg-gradient-to-br from-gray-800 to-gray-700 p-6 rounded-xl border border-gray-600 hover-lift" style="{stagger(1)}">
                    <div class="flex items-center gap-3 mb-3">
                        <span class="text-2xl">🏆</span>
                        <h3 class="text-lg font-semibold">Average Parse</h3>
                        {#if raidPerformance && raidPerformance.bestPerformanceAverage == null}
                        <div class="ml-auto">
                            <Tooltip.Provider>
                                    <Tooltip.Root>
                                    <Tooltip.Trigger>
                                        <div class="ml-auto cursor-pointer transform transition duration-300 ease-in-out hover:scale-110 hover:text-blue-400">
                                        <CircleQuestionMark style="color: {character.classColor}" />
                                        </div>
                                    </Tooltip.Trigger>
                                    <Tooltip.Content side="top" align="center" class="bg-gray-700 p-2 rounded-md shadow-lg text-sm">
                                        <span style="color: {character.classColor}">No parses? This person may have their logs hidden.<br/>(Or they just don't like raiding)</span>
                                    </Tooltip.Content>
                                </Tooltip.Root>
                            </Tooltip.Provider>
                        </div>
                        {/if}
                    </div>
                    <p class="text-3xl font-bold text-center" style="color: {raidPerformance?.bestPerformanceAverage ? getParseColor(raidPerformance.bestPerformanceAverage) : '#9d9d9d'}">
                        {#if raidPerformance && raidPerformance.bestPerformanceAverage != null}
                        {Math.ceil(raidPerformance.bestPerformanceAverage)} ({getRaidDifficultyString(raidPerformance.difficulty)})
                        {:else}
                        N/A
                        {/if}
                    </p>
                </div>

                <div class="staggered-item bg-gradient-to-br from-gray-800 to-gray-700 p-6 rounded-xl border border-gray-600 hover-lift" style="{stagger(2)}">
                    <div class="flex items-center gap-3 mb-3">
                        <span class="text-2xl">⭐</span>
                        <h3 class="text-lg font-semibold">M+ Rating</h3>
                    </div>
                    <p class="text-3xl font-bold text-center" style="color: {getScoreColor(character.characterData.mythic_plus_scores_by_season?.[0]?.scores?.all)}">
                        {character.characterData.mythic_plus_scores_by_season?.[0]?.scores?.all || '0'}
                    </p>
                </div>
            </div>
        </Tabs.Content>

        <!-- Enhanced Raid Tab -->
        <Tabs.Content value="raid" class="tab-enter space-y-8">
            <!-- Raid Progress Section -->
            <div class="bg-[url('/raids-manaforge-omega.webp')] bg-right  rounded-xl border border-gray-600">
                <div class="backdrop-blur-sm w-full rounded-xl p-8">
                    <h2 class="text-3xl font-bold mb-6 flex items-center gap-3">
                    <span class="text-3xl">🏰</span>
                    Manaforge: Omega Progress
                    </h2>
                    
                    <div class="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-6 xl:grid-cols-12 gap-8">
                        <!-- Normal Progress -->
                        <div class="space-y-4 md:col-span-1 lg:col-span-2 xl:col-span-4">
                            <div class="flex justify-between items-center">
                                <span class="text-green-400 font-bold text-lg">Normal</span>
                                <span class="text-xl font-semibold">
                                    {character.characterData.raid_progression.manaforge_omega.normal_bosses_killed}/{character.characterData.raid_progression.manaforge_omega.total_bosses}
                                </span>
                            </div>
                            <div class="h-3 bg-gray-700 rounded-full overflow-hidden">
                                <div class="h-full bg-gradient-to-r from-green-500 to-green-400 progress-bar shadow-lg" 
                                    style="width: {getProgressPercent(character.characterData.raid_progression.manaforge_omega.normal_bosses_killed, character.characterData.raid_progression.manaforge_omega.total_bosses)}%; box-shadow: 0 0 10px #10b981;"></div>
                            </div>
                        </div>

                        <!-- Heroic Progress -->
                        <div class="space-y-4 md:col-span-1 lg:col-span-2 xl:col-span-4">
                            <div class="flex justify-between items-center">
                                <span class="text-blue-400 font-bold text-lg">Heroic</span>
                                <span class="text-xl font-semibold">
                                    {character.characterData.raid_progression.manaforge_omega.heroic_bosses_killed}/{character.characterData.raid_progression.manaforge_omega.total_bosses}
                                </span>
                            </div>
                            <div class="h-3 bg-gray-700 rounded-full overflow-hidden">
                                <div class="h-full bg-gradient-to-r from-blue-500 to-blue-400 progress-bar shadow-lg" 
                                    style="width: {getProgressPercent(character.characterData.raid_progression.manaforge_omega.heroic_bosses_killed, character.characterData.raid_progression.manaforge_omega.total_bosses)}%; box-shadow: 0 0 10px #3b82f6;"></div>
                            </div>
                        </div>

                        <!-- Mythic Progress -->
                        <div class="space-y-4 md:col-span-1 lg:col-span-2 xl:col-span-4">
                            <div class="flex justify-between items-center">
                                <span class="text-purple-400 font-bold text-lg">Mythic</span>
                                <span class="text-xl font-semibold">
                                    {character.characterData.raid_progression.manaforge_omega.mythic_bosses_killed}/{character.characterData.raid_progression.manaforge_omega.total_bosses}
                                </span>
                            </div>
                            <div class="h-3 bg-gray-700 rounded-full overflow-hidden">
                                <div class="h-full bg-gradient-to-r from-purple-500 to-purple-400 progress-bar shadow-lg" 
                                    style="width: {getProgressPercent(character.characterData.raid_progression.manaforge_omega.mythic_bosses_killed, character.characterData.raid_progression.manaforge_omega.total_bosses)}%; box-shadow: 0 0 10px #a855f7;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                
            </div>
            
            <!-- This Week's Kills -->
{#if raidBossesKilledThisWeek && raidBossesKilledThisWeek.length !== 0}
    <div class="bg-gradient-to-br from-gray-800 to-gray-700 p-6 rounded-xl border border-gray-600">
        <h3 class="text-2xl font-bold mb-4 flex items-center gap-3">
            <span class="text-2xl">🗡️</span>
            Bosses Killed This Week
        </h3>
        <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
           {#each sortedEncounters as encounter, i}
                <div class="staggered-item flex items-center gap-4 p-4 bg-gray-700/50 rounded-lg hover:bg-gray-600/50 transition-all duration-300" style="{stagger(i)}">
                    <img 
                        src={bossIcons[bosses.find(e => e.name === encounter.boss)?.id ?? 0]} 
                        alt="{encounter.boss} Image"
                        class="w-12 h-12 rounded-lg object-cover border-2 border-gray-500"
                    >
                    <div>
                        <p class="font-semibold text-lg">{encounter.boss}</p>
                        <p class="text-sm opacity-75" class:text-[#38f531]={encounter.difficulty === "Normal"}
                                                        class:text-[#4369e0]={encounter.difficulty === "Heroic"}
                                                        class:text-[#c342c7]={encounter.difficulty === "Mythic"}>
                            {encounter.difficulty}
                        </p>
                    </div>
                </div>
            {/each}
        </div>
    </div>
{:else}
    <div class="text-center py-12 bg-gray-800/50 rounded-xl border border-gray-700">
        <span class="text-6xl mb-4 block opacity-50">😴</span>
        <p class="text-xl text-gray-400">No boss kills this week!</p>
    </div>
{/if}

<!-- Performance Rankings -->
    <div class="bg-gradient-to-br from-gray-800 to-gray-700 p-6 rounded-xl border border-gray-600">
        <h3 class="text-2xl font-bold mb-4 flex items-center gap-3">
            <span class="text-2xl">📊</span>
            Top Parses
            <DropdownMenu.Root>
                <DropdownMenu.Trigger class="bg-slate-900 hover:bg-slate-800 p-2 rounded-md">{raidPerformanceDifficulty}</DropdownMenu.Trigger>
                <DropdownMenu.Content>
                    <DropdownMenu.Group>
                        <DropdownMenu.Item onclick={() => raidPerformanceDifficulty = "LFR"}>LFR</DropdownMenu.Item>
                        <DropdownMenu.Item onclick={() => raidPerformanceDifficulty = "Normal"}>Normal</DropdownMenu.Item>
                        <DropdownMenu.Item onclick={() => raidPerformanceDifficulty = "Heroic"}>Heroic</DropdownMenu.Item>
                        <DropdownMenu.Item onclick={() => raidPerformanceDifficulty = "Mythic"}>Mythic</DropdownMenu.Item>
                    </DropdownMenu.Group>
                </DropdownMenu.Content>
            </DropdownMenu.Root>
        </h3>
        {#if raidPerformance?.rankings && raidPerformance.rankings.length > 0}
        <div class="space-y-3 grid grid-cols-2 gap-2">
            {#if !raidPerformance.rankings.find(r => r.totalKills > 0)}
                <div class="text-center py-12 bg-gray-800/50 rounded-xl border border-gray-700 col-span-2">
                    <span class="text-6xl mb-4 block opacity-50">📊</span>
                    <p class="text-xl text-gray-400">No parses for {raidPerformanceDifficulty}</p>
                </div>
            {/if}
            {#each raidPerformance.rankings as ranking, i}
                {#if ranking.totalKills != 0}
                    <div class="staggered-item flex items-center  gap-4 p-4 bg-gray-700/50 rounded-lg hover:bg-gray-600/50 transition-all duration-300 h-20" style="{stagger(i)}">
                        <img src={bossIcons[ranking.encounter.id]} class="w-12 h-12 rounded-lg border-2 border-gray-500" alt={ranking.encounter.name} />
                        <div class="flex-1">
                            <p class="font-semibold text-lg">{ranking.encounter.name}</p>
                            <p class="text-sm opacity-75">{ranking.spec} {character.characterData.char_class}</p>
                        </div>
                        <div class="text-right">
                            <span class="text-2xl font-bold px-3 py-1 rounded-lg bg-black/30" style="color: {getParseColor(ranking.medianPercent)};">
                                {Math.ceil(ranking.medianPercent)}
                            </span>
                            <p class="text-xs opacity-75 mt-1">{ranking.totalKills} {ranking.totalKills > 1 ? "kills" : "kill"}</p>
                        </div>
                    </div>
                {/if}
            {/each}
        </div>
        {/if}
    </div>
        </Tabs.Content>

        <!-- Enhanced M+ Tab -->
        <Tabs.Content value="m+" class="tab-enter">
            <div class="space-y-6">
                <h1 
                class="text-3xl font-bold flex items-center gap-3"
                style="color: {character.classColor};"
                >
                    <span class="text-3xl">⚡</span>
                    <span>S3 Top Mythic+ Runs</span>
                    <span 
                        class="text-3xl font-bold ml-auto"
                        style="color: {getScoreColor(character.characterData.mythic_plus_scores_by_season?.[0]?.scores?.all)}"
                    >
                    {#if character.characterData.mythic_plus_scores_by_season?.[0]?.scores?.all}
                        {character.characterData.mythic_plus_scores_by_season?.[0]?.scores?.all} IO
                    {/if}
                    </span>
                </h1>

                
                <div class="grid grid-cols-1 lg:grid-cols-2 gap-4">
                    {#if character.characterData.mythic_plus_best_runs.length === 0}
                        <div class="lg:col-span-2 text-center py-16 bg-gray-800/50 rounded-xl border border-gray-700">
                            <span class="text-6xl mb-4 block opacity-50">⚡</span>
                            <p class="text-xl text-gray-400">No Mythic+ runs found.</p>
                        </div>
                    {:else}
                        {#each character.characterData.mythic_plus_best_runs as run, i}
                        <a href={run.url} target="_blank">
                            <div 
                                class="staggered-item mythic-card rounded-lg border border-gray-600 overflow-hidden"
                                style="background-image: url({run.background_image_url}); {stagger(i)}"
                            >
                                <div class="p-4 flex items-center gap-4">
                                    <img 
                                        src="{run.icon_url}" 
                                        alt="{run.dungeon}" 
                                        class="w-12 h-12 rounded-lg shadow-lg border border-gray-500 hover:scale-105 transition-transform duration-300" 
                                        loading="lazy" 
                                    />
                                    
                                    <div class="flex-1 min-w-0">
                                        <div class="flex items-center gap-2 mb-1">
                                            <span class="text-lg font-bold {run.num_keystone_upgrades === 0 ? 'text-gray-500' : 'text-yellow-400'}">
                                                {'+'.repeat(run.num_keystone_upgrades)}{run.mythic_level}
                                            </span>
                                            <h3 class="text-base font-semibold truncate">{run.dungeon}</h3>
                                        </div>
                                        
                                        <div class="flex items-center gap-4 text-sm text-gray-300">
                                            <div class="flex items-center gap-1">
                                                <span>⏱️</span>
                                                <span class="font-medium">{msToTime(run.clear_time_ms)}</span>
                                            </div>
                                            <div class="flex items-center gap-1">
                                                <span>📅</span>
                                                <span>{new Date(run.completed_at).toLocaleDateString()}</span>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    {#if run.num_keystone_upgrades > 0}
                                        <div class="flex gap-0.5">
                                            {#each Array.from({length: run.num_keystone_upgrades}, (_, i) => i) as star}
                                                <span class="text-yellow-400 text-lg">⭐</span>
                                            {/each}
                                        </div>
                                    {/if}
                                </div>
                            </div>
                        </a>
                        {/each}
                    {/if}
                </div>
            </div>
        </Tabs.Content>
    </Tabs.Root>
</div>
{:else}
<div class="max-w-6xl w-3xl mx-auto p-6 bg-gray-900 rounded-2xl shadow-2xl border border-gray-700 animate-pulse">
    <!-- Header Skeleton -->
    <div class="text-center mb-8 relative">
        <div class="h-10 w-48 mx-auto bg-gray-700 rounded-lg mb-2"></div>
        <div class="h-6 w-32 mx-auto bg-gray-700 rounded-lg"></div>
    </div>

    <!-- Tabs Skeleton -->
    <div class="flex border-b-2 border-gray-700 mb-6 bg-gray-800/50 rounded-t-lg p-1 gap-2">
        <div class="flex-1 h-10 bg-gray-700 rounded-md"></div>
        <div class="flex-1 h-10 bg-gray-700 rounded-md"></div>
        <div class="flex-1 h-10 bg-gray-700 rounded-md"></div>
    </div>

    <!-- Stats Tab Skeleton -->
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <div class="h-40 bg-gray-800 rounded-xl border border-gray-600"></div>
        <div class="h-40 bg-gray-800 rounded-xl border border-gray-600"></div>
        <div class="h-40 bg-gray-800 rounded-xl border border-gray-600"></div>
    </div>

    <!-- Raid Tab Skeleton -->
    <div class="mt-8 space-y-6">
        <div class="bg-gray-800 rounded-xl border border-gray-600 p-6 space-y-4">
            <div class="h-8 w-64 bg-gray-700 rounded-lg mb-4"></div>
            <div class="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-6 xl:grid-cols-12 gap-4">
                {#each Array(3) as _, i}
                    <div class="space-y-2 md:col-span-1 lg:col-span-2 xl:col-span-4">
                        <div class="h-6 bg-gray-700 rounded-lg"></div>
                        <div class="h-3 bg-gray-700 rounded-full"></div>
                    </div>
                {/each}
            </div>
        </div>

        <div class="bg-gray-800 rounded-xl border border-gray-600 p-6 space-y-2">
            <div class="h-6 w-48 bg-gray-700 rounded-lg mb-4"></div>
            {#each Array(3) as _, i}
                <div class="flex items-center gap-4 p-4 bg-gray-700/50 rounded-lg">
                    <div class="w-12 h-12 bg-gray-600 rounded-lg"></div>
                    <div class="flex-1 space-y-1">
                        <div class="h-4 bg-gray-600 rounded w-32"></div>
                        <div class="h-3 bg-gray-600 rounded w-20"></div>
                    </div>
                    <div class="w-10 h-6 bg-gray-600 rounded-lg"></div>
                </div>
            {/each}
        </div>
    </div>

    <!-- M+ Tab Skeleton -->
    <div class="mt-8 space-y-6">
        <div class="h-8 w-64 bg-gray-700 rounded-lg mb-4"></div>
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-4">
            {#each Array(4) as _, i}
                <div class="h-24 bg-gray-800 rounded-xl border border-gray-600"></div>
            {/each}
        </div>
    </div>
</div>

{/if}