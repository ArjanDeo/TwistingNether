<script lang="ts">
	import type { Character } from "$lib/types";
	import { onMount } from "svelte";
	import CharacterForm from "./components/character-form.svelte";
	import type { PageServerData } from "./$types";
	import { toast } from "svelte-sonner";

	let recentCharacters: Array<Character> = $state([]);
	let { data }: {data: PageServerData} = $props();

	onMount(async () => {
		recentCharacters = JSON.parse(localStorage.getItem("recentCharacters") || "[]");
		if (data.token === null) {
			toast.error("Couldn't fetch WoW Token info.")
		}
	});
</script>

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
	<!-- <div class="w-xs mx-auto">
        <div class="bg-gradient-to-br from-slate-900/90 to-slate-800/90 p-6 rounded-xl backdrop-blur-sm border border-purple-500/30 shadow-2xl">
            <div class="flex items-center justify-between mb-6">
                <h1 class="text-3xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-purple-400 to-pink-400">
                    Recent Searches
                </h1>
            </div>
            
            <ul class="flex flex-col gap-y-3">
                <li class="character-item border-purple-500/50 bg-slate-900/40 border-2 p-4 rounded-lg hover:border-purple-400 cursor-pointer glow-border group">
                    <div class="flex items-center justify-between">
                        <div class="flex items-center gap-3">
                            <div class="w-2 h-2 rounded-full bg-purple-500"></div>
                            <span class="text-purple-200 font-semibold group-hover:text-purple-100">
                                Furyshiftz
                            </span>
                        </div>
                        <span class="text-slate-400 text-sm">
                            US - Tichondrius
                        </span>
                    </div>
                </li>
                
                <li class="character-item border-cyan-500/50 bg-slate-900/40 border-2 p-4 rounded-lg hover:border-cyan-400 cursor-pointer glow-border-cyan group">
                    <div class="flex items-center justify-between">
                        <div class="flex items-center gap-3">
                            <div class="w-2 h-2 rounded-full bg-cyan-500"></div>
                            <span class="text-cyan-200 font-semibold group-hover:text-cyan-100">
                                Imonthegcd
                            </span>
                        </div>
                        <span class="text-slate-400 text-sm">
                            US - Illidan
                        </span>
                    </div>
                </li>
                
                <li class="character-item border-pink-500/50 bg-slate-900/40 border-2 p-4 rounded-lg hover:border-pink-400 cursor-pointer glow-border-pink group">
                    <div class="flex items-center justify-between">
                        <div class="flex items-center gap-3">
                            <div class="w-2 h-2 rounded-full bg-pink-500"></div>
                            <span class="text-pink-200 font-semibold group-hover:text-pink-100">
                                Ellesmere
                            </span>
                        </div>
                        <span class="text-slate-400 text-sm">
                            EU - Mal'Ganis
                        </span>
                    </div>
                </li>
            </ul>
            
            <div class="mt-6 pt-4 border-t border-slate-700/50">
                <button class="w-full py-2 px-4 bg-gradient-to-r from-purple-600 to-pink-600 hover:from-purple-500 hover:to-pink-500 text-white font-semibold rounded-lg transition-all duration-300 shadow-lg hover:shadow-purple-500/50">
                    Clear History
                </button>
            </div>
        </div>
    </div> -->
</div>