<script lang="ts">
    import type { PageData } from './$types';
    
    let { data }: { data: PageData } = $props();
	import { onMount } from 'svelte';
	import CharacterPane from './components/characterPane/CharacterPane.svelte';
	import CharacterInfo from './components/characterInfo/CharacterInfo.svelte';
    import * as Table from "$lib/components/ui/table";
	import { browser } from '$app/environment';

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
{#if !browser} 
    <span class="loading loading-spinner text-success mx-auto block justify-center scale-150 self-center my-auto"></span>
{/if}
{#if data.character && !loading}
    <div class="flex flex-row p-8 min-h-screen" style="color: {data.character.classColor};">
        <CharacterPane character={data.character}/>
        <CharacterInfo character={data.character}/>
    </div>


    <h1 class="text-4xl mb-4">Top Mythic+ Runs:</h1>

<div
  class="rounded-2xl overflow-hidden border-4 shadow-md w-fit"
  style="border-color: {data.character.classColor};"
>
  <Table.Root class="w-full table-auto">
    <Table.Head>
      <Table.Row class="bg-muted/30 text-base">
        <Table.Cell colspan={3} class="font-semibold">Dungeon</Table.Cell>
        <Table.Cell colspan={1} class="font-semibold">Time Taken</Table.Cell>
        <Table.Cell colspan={1} class="font-semibold">Date</Table.Cell>
      </Table.Row>
    </Table.Head>

    <Table.Body>
      {#if data.character.raiderIOCharacterData.mythic_plus_best_runs.length === 0}
        <Table.Row>
          <Table.Cell colspan={3}  class="text-center py-4">No Mythic+ runs found.</Table.Cell>
        </Table.Row>
      {:else}
        {#each data.character.raiderIOCharacterData.mythic_plus_best_runs as run}
          <Table.Row
            class="text-center text-lg"
            style="background-image: url({run.background_image_url}); background-size: cover;"
          >
            <Table.Cell class="backdrop-blur-sm flex items-center gap-2 py-2 px-3">
              <img
                src="{run.icon_url}"
                alt="{run.dungeon}"
                class="w-10 h-10 rounded-sm"
              />
              <span class={run.num_keystone_upgrades === 0 ? "text-gray-500" : ""}>
                {'+'.repeat(run.num_keystone_upgrades)}{run.mythic_level} {run.dungeon}
              </span>
            </Table.Cell>
            <Table.Cell class="backdrop-blur-sm py-2 px-3">{msToTime(run.clear_time_ms)}</Table.Cell>
            <Table.Cell class="backdrop-blur-sm py-2 px-3">{new Date(run.completed_at).toLocaleDateString()}</Table.Cell>
          </Table.Row>
        {/each}
      {/if}
    </Table.Body>
  </Table.Root>
</div>
{:else if loading}
    <span class="loading loading-spinner text-success mx-auto block justify-center scale-150 self-center my-auto"></span>
{/if}