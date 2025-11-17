<script lang="ts">
  import type { PageData } from './$types';
  import CharacterPane from './components/characterPane/CharacterPane.svelte';
  import CharacterInfoSection from './components/characterInfoSection/CharacterInfoSection.svelte';

  export let data: PageData;
</script>

<svelte:head>
    {#if data.character}
    {#await data.character}
    <title>
        Loading...
    </title>
    {:then char}
    <title>
      {char.characterData.name} - {char.characterData.realm}
    </title>
    {/await}
    {/if}
</svelte:head>

{#await data.character}
  <!-- Skeleton while loading -->
  <div class="flex flex-col md:flex-row p-8 min-h-fit gap-y-10 md:gap-x-20">
    <CharacterPane character={undefined}/>
    <CharacterInfoSection character={undefined}/>
  </div>
{:then character}
  <!-- Actual content -->
  <div class="flex flex-col md:flex-row p-8 min-h-fit gap-y-10 md:gap-x-20" style="color: {character.classColor};">
    <CharacterPane character={character}/>
    <CharacterInfoSection character={character}/>
  </div>
{:catch error}
  <div class="text-red-500 text-center mt-20">
    Failed to load character: {error.message}
  </div>
{/await}

