<script lang="ts">
/* eslint-disable @typescript-eslint/no-unused-vars */
  import type { PageData } from './$types';
  import CharacterPane from './components/characterPane/CharacterPane.svelte';
  import CharacterInfoSection from './components/characterInfoSection/CharacterInfoSection.svelte';
  import CharacterError from './components/CharacterError.svelte';
  import CharacterNotFound from './components/CharacterNotFound.svelte';
	import { browser } from '$app/environment';
import type { Character } from '$lib/types/character'
import type { CharacterCache, ClassColors } from '$lib/types';
  export let data: PageData;

function saveCharacter(character: Character) {
  if (!browser) return;

  const char: CharacterCache = {
    name: character.characterData.name,
    realm: character.characterData.realm,
    region: character.characterData.region,
    class: character.characterData.char_class.replace(' ', '') as keyof typeof ClassColors
  };

  const stored = localStorage.getItem('recentCharacters');
  const characters: CharacterCache[] = stored ? JSON.parse(stored) : [];

  const exists = characters.some(c =>
    c.name === char.name &&
    c.realm === char.realm &&
    c.region === char.region &&
    c.class === char.class
  );

  if (!exists) {
    characters.push(char);
    localStorage.setItem('recentCharacters', JSON.stringify(characters));
  }
}

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
    {:catch error}
    <title>
        {error?.status === 404 ? "Character not found" : "Something went wrong"}
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
  {@const _ = saveCharacter(character)}

  <div
    class="flex flex-col md:flex-row p-8 min-h-fit gap-y-10 md:gap-x-20"
    style="color: {character.classColor};"
  >
    <CharacterPane character={character}/>
    <CharacterInfoSection character={character}/>
  </div>
{:catch error}
  <div class="p-8">
    {#if error?.status === 404}
      <CharacterNotFound />
    {:else}
      <CharacterError status={error?.status} />
    {/if}
  </div>
{/await}

