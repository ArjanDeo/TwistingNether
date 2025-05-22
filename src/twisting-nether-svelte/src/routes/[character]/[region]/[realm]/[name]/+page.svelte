<script lang="ts">
    import type { PageData } from './$types';
    
    let { data }: { data: PageData } = $props();
	import { onMount } from 'svelte';
	import CharacterPane from './components/characterPane/CharacterPane.svelte';
	import CharacterInfo from './components/characterInfo/CharacterInfo.svelte';

    let loading: boolean = $state(true);

function msToTime(ms: number): string {
        const totalSeconds = Math.floor(ms / 1000); // Convert milliseconds to seconds
        const minutes = Math.floor(totalSeconds / 60); // Get the whole minutes
        const seconds = totalSeconds % 60; // Get the remaining seconds

        // Format with leading zeroes if needed
        const formattedMinutes = String(minutes).padStart(2, '0');
        const formattedSeconds = String(seconds).padStart(2, '0');

        return `${formattedMinutes}:${formattedSeconds}`;
    }

onMount(() => {
    loading = false;
});

</script>
<svelte:head>
	<title>{data.character.raiderIOCharacterData.name} - {data.character.raiderIOCharacterData.realm}</title>
</svelte:head>
{#if data.character && !loading}
    <div class="flex flex-row p-8 min-h-screen" style="color: {data.character.classColor};">
        <CharacterPane character={data.character}/>
        <CharacterInfo character={data.character}/>
    </div>
    <h1 class="text-4xl mb-4">Top Mythic+ Runs:</h1>
    <table style="border-color: {data.character.classColor};" class="border-4 rounded-md glass table w-fit">
        <tbody>
            <tr>
                <th>Dungeon</th>
                <th>Time Taken</th>
                <th>Date</th>
            </tr>     
            {#if data.character.raiderIOCharacterData.mythic_plus_best_runs.length === 0}
                <tr>
                    <td colspan="3" class="text-center">No Mythic+ runs found.</td>
                </tr>
            {/if}                   
            {#each data.character.raiderIOCharacterData.mythic_plus_best_runs as run}
            <tr style="background-image: url({run.background_image_url});"  class="mx-auto text-center mt-2 text-xl">
                <td class="flex backdrop-blur-sm"><img class="max-w-10 my-1 mr-2 rounded-sm" alt="{run.dungeon}" src="{run.icon_url}"/><span class="{run.num_keystone_upgrades === 0 ? 'text-gray-600' : ''}"> {'+'.repeat(run.num_keystone_upgrades)}{run.mythic_level} {run.dungeon}</span></td>
                <td class="backdrop-blur-sm">{msToTime(run.clear_time_ms)}</td>
                <td class="backdrop-blur-sm">{new Date(run.completed_at).toLocaleDateString()}</td>
            </tr>
            {/each}
        </tbody>
    </table>
{:else if loading}
    <span class="loading loading-spinner text-success mx-auto block justify-center scale-150 self-center my-auto"></span>
{/if}