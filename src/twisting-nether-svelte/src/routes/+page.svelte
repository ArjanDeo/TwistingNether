<script lang="ts">
	import type { Character, CharacterCache } from "$lib/types";
	import { onMount } from "svelte";
	import CharacterForm from "./components/character-form.svelte";
	import type { PageServerData } from "./$types";
	import { toast } from "svelte-sonner";
    import { ClassColors } from "$lib/types";

	let recentCharacters: Array<CharacterCache> = $state([]);
	let { data }: {data: PageServerData} = $props();

	onMount(async () => {
		recentCharacters = JSON.parse(localStorage.getItem("recentCharacters") || "[]");
		if (data.token === null) {
			toast.error("Couldn't fetch WoW Token info.")
		}
	});
</script>
<svelte:head>
    <title>
        Home | Twisting Nether
    </title>
</svelte:head>
<section class="bg-primary-foreground p-6 rounded-xl text-center border border-yellow-500 shadow-lg max-w-md mx-auto">
	<div class="mx-auto">
<h2 class="text-2xl font-bold text-yellow-400">WoW Token Price</h2>
  <p class="text-lg text-secondary-foreground mt-3 flex flex-row ">
	<img src="/money-gold.webp" alt="gold icon" class="h-fit my-auto mr-1 "/>
	{#if data.token != null}
    {(data.token.price / 10000).toLocaleString()} Gold
	{:else}
	WoW token info not available.
	{/if}
  </p>
	</div>
</section>
<div class="w-fit mx-auto my-4 flex flex-row gap-x-2">
	<div class="bg-primary-foreground p-4 rounded-md max-h-44">
		<CharacterForm {data} />
	</div>
    {#if recentCharacters.length > 0}
	<div class="w-xs mx-auto">
        <div class="bg-gradient-to-br from-slate-900/90 to-slate-800/90 p-6 rounded-xl backdrop-blur-sm border border-purple-500/30 shadow-2xl">
            <div class="flex items-center justify-between mb-6">
                <h1 class="text-2xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-purple-400 to-pink-400">
                    Recent Searches
                </h1>
            </div>
            <ul class="flex flex-col gap-y-3">
                <!-- svelte-ignore a11y_no_noninteractive_element_interactions -->
                {#each recentCharacters as char (char.name + char.realm)}
                <!-- svelte-ignore a11y_click_events_have_key_events -->
                <li
                    class="character-item border-2 p-3 rounded-lg cursor-pointer group"
                    style="border-color: {ClassColors[char.class]}80; background-color: #02061766"
                    onclick={() =>
                    window.location.href =
                        `/character/${char.region}/${char.realm}/${char.name}`
                    }
                >
                    <div class="flex items-center justify-between">
                    <div class="flex items-center gap-3">
                        <div
                        class="w-2 h-2 rounded-full"
                        style="background-color: {ClassColors[char.class]}"
                        ></div>
                        <span
                        class="font-semibold group-hover:opacity-90"
                        style="color: {ClassColors[char.class]}"
                        >
                        {char.name}
                        </span>
                    </div>
                    <span class="text-slate-400 text-sm">
                        {char.region.toUpperCase()} – {char.realm}
                    </span>
                    </div>
                </li>
                {/each}
            </ul>
            <div class="mt-6 pt-4 border-t border-slate-700/50">
                <button
                    class="w-full py-2 px-4 bg-gradient-to-r from-purple-600 to-pink-600 hover:from-purple-500 hover:to-pink-500 text-white font-semibold rounded-lg transition-all"
                    onclick={() => {
                        recentCharacters = [];
                        localStorage.removeItem('recentCharacters');
                    }}
                    >
                    Clear History
                </button>
            </div>
        </div>
    </div>
    {/if}
</div>