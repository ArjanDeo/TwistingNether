<script lang="ts">
  import type { PageData } from './$types';
  let { data }: { data: PageData } = $props();
  import { onMount } from 'svelte';
  import CharacterPane from './components/characterPane/CharacterPane.svelte';
  import * as Table from "$lib/components/ui/table";
  import { browser } from '$app/environment';
	import CharacterInfoSection from './components/characterInfoSection/CharacterInfoSection.svelte';

  let loading: boolean = $state(true);



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
<div class="flex flex-col md:flex-row p-8 min-h-screen gap-y-10 md:gap-x-20" style="color: {data.character.classColor};">
  <div class="">
    <CharacterPane character={data.character}/>
  </div>
  <div class="">
    <CharacterInfoSection character={data.character}/>
  </div>
</div>




{:else if loading}
    <span class="loading loading-spinner text-success mx-auto block justify-center scale-150 self-center my-auto"></span>
{/if}