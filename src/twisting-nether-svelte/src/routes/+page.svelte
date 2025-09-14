<script lang="ts">
	import { goto } from "$app/navigation";
	import { API_BASE_URL } from "$lib/common";
	import { realms } from "$lib/realms";
	import type { Character } from "$lib/types";
	import { onMount } from "svelte";
	import CharacterForm from "./components/character-form.svelte";
	import type { PageServerData } from "./$types";
	import * as Carousel from "$lib/components/ui/carousel";
	import { toast } from "svelte-sonner";
	import { dev } from "$app/environment";

	let recentCharacters: Array<Character> = $state([]);
	let newsPosts: Array<any> = $state([]);
	let { data }: {data: PageServerData} = $props();

	onMount(async () => {
		recentCharacters = JSON.parse(localStorage.getItem("recentCharacters") || "[]");
		// Updating the news endpoint is requiring constant change, i'm considering leaving this functionality out as it doesnt really fit with the app.
		
		// try {
		// 	const news = await fetch(`${API_BASE_URL}/General/GetNews?limit=15`);

		// 	if (!news.ok) {
		// 		toast.error("Couldn't fetch news.");				
		// 		return;
		// 	}

		// 	newsPosts = await news.json();
		// } catch (error) {
		// 	toast.error("Couldn't fetch news.");
		// }
		if (data.token === null) {
			toast.error("Couldn't fetch WoW Token info.")
		}
	});
</script>

<section class="bg-gray-800 p-6 rounded-xl text-center border border-yellow-500 shadow-lg max-w-md mx-auto">
	<div class="mx-auto">
<h2 class="text-2xl font-bold text-yellow-400">WoW Token Price</h2>
  <p class="text-lg text-white mt-3 flex flex-row ">
	<img src="/money-gold.webp" alt="gold icon" class="h-fit my-auto mr-1 "/>
	{#if data.token != null}
    {(data.token.price / 10000).toLocaleString()} Gold
	{:else}
	WoW token info not available.
	{/if}
  </p>
	</div>
</section>
<div class="w-fit mx-auto bg-primary-foreground p-4 rounded-md my-4">
	<CharacterForm {data} />
</div>

{#if newsPosts.length > 0}
<div class="w-full">
	<h1 class="text-center my-10 text-2xl bg-primary-foreground w-fit p-2 rounded-xl mx-auto">RECENT NEWS</h1>
	<Carousel.Root class="w-full mx-auto">
		<Carousel.Content class="flex gap-4 -ml-0">
			{#each newsPosts as post}
			<Carousel.Item class="basis-[300px] p-0 m-0">
				<div class="w-full bg-primary-foreground h-full rounded-md">
					<a href="{post.link}" target="_blank">
						<img loading="lazy" src="{post.image}" class="w-full" alt="{post.title}">
						<h1 class="text-xl p-2 truncate">{post.title}</h1>
						<p class="p-2 line-clamp-2 leading-7 overflow-hidden">{post.description}</p>
					</a>
				</div>
			</Carousel.Item>
			{/each}
		</Carousel.Content>
		<Carousel.Previous />
		<Carousel.Next />
	</Carousel.Root>
</div>
{/if}