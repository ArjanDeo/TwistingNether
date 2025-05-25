<script lang="ts">
	import { goto } from "$app/navigation";
	import { API_BASE_URL } from "$lib/common";
	import { realms } from "$lib/realms";
	import type { Character } from "$lib/types";
	import { onMount } from "svelte";
	import CharacterForm from "./character-form.svelte";
	import type { PageServerData } from "./$types";
	import * as Carousel from "$lib/components/ui/carousel";

	let characterName = "";
	let characterRealm = "";
	let characterRegion = "default";

	let showError = $state(false);
	let showNewsError = $state(false);
	let showNewsErrorMessage = $state("");
	let errorMessage = $state("Character not found");
	let characterFound = $state(false);

	let recentCharacters: Array<Character> = $state([]);
	let newsPosts: Array<any> = $state([]);
	let { data }: {data: PageServerData} = $props();
	onMount(async () => {
		recentCharacters = JSON.parse(localStorage.getItem("recentCharacters") || "[]");
		try {
			const news = await fetch(`${API_BASE_URL}/General/GetNews?limit=15`);

			if (!news.ok) {
				showNewsErrorMessage = "Couldn't fetch news.";
				showNewsError = true;
				return;
			}

			newsPosts = await news.json();
		} catch (error) {
			showNewsErrorMessage = "Couldn't fetch news.";
			showNewsError = true;
		}
	});

	let invalidFields = {
		characterName: false,
		characterRealm: false,
		characterRegion: false
	};

	async function getCharacter() {
		invalidFields = {
			characterName: !characterName.trim(),
			characterRealm: !characterRealm.trim(),
			characterRegion: !characterRegion.trim() || characterRegion === "default"
		};

		if (Object.values(invalidFields).some((isInvalid) => isInvalid)) {			
			return;
		}

		try {
			const res = await fetch(
				`${API_BASE_URL}/character/pingcharacter?name=${characterName}&realm=${characterRealm}&region=${characterRegion}`
			);

			if (!res.ok) {
				let errorText = "Unknown error";
					const errorData = await res.json();
					errorText = errorData?.message || JSON.stringify(errorData);
				

				errorMessage = `API error: ${errorText}`;
				showError = true;
				return;
			}

			const resData: Character = await res.json();
			showError = false;

			const newCharacter: Character = {
				name: resData.name,
				realm: resData.realm,
				region: resData.region,
				class: resData.class
			};

			updateRecentCharacters(newCharacter);

			goto(
				`/character/${characterRegion.toLowerCase()}/${characterRealm.toLowerCase()}/${characterName.toLowerCase()}`
			);

			characterFound = true;
		} catch (error: any) {
			errorMessage = `Fetch error: ${error?.message || "An unexpected error occurred."}`;
			showError = true;
		}
	}

	function validateField(field: keyof typeof invalidFields, value: string) {
	if (field === 'characterRealm') {
		invalidFields[field] = !realms.some(realm => realm.realmName === value.trim());
	} else {
		invalidFields[field] = !value.trim();
	}
}


	function updateRecentCharacters(character: Character) {
		// Remove duplicates
		recentCharacters = recentCharacters.filter(
			(c) =>
				c.name.toLowerCase() !== character.name.toLowerCase() ||
				c.realm.toLowerCase() !== character.realm.toLowerCase() ||
				c.region.toLowerCase() !== character.region.toLowerCase() ||
				c.class.toLowerCase() !== character.class.toLowerCase()
		);

		// Add new character at the front
		recentCharacters.unshift(character);

		// Trim to max 5 characters
		recentCharacters = recentCharacters.slice(0, 5);

		// Save to localStorage
		localStorage.setItem("recentCharacters", JSON.stringify(recentCharacters));
	}

	function loadCharacter(character: { name: string; realm: string; region: string }) {
		characterName = character.name;
		characterRealm = character.realm;
		characterRegion = character.region;

		getCharacter();
	}
</script>

{#if characterFound} 
<div class="flex justify-center items-center">
    <span class="loading loading-spinner text-success text-center w-12"></span>
</div>
{/if}
{#if showError}
<div role="alert" class="alert alert-error max-w-96 mx-auto mb-2">
	<svg
		xmlns="http://www.w3.org/2000/svg"
		fill="none"
		viewBox="0 0 24 24"
		class="stroke-info h-6 w-6 shrink-0">
		<path
			stroke="#1E293B"
			stroke-linecap="round"
			stroke-linejoin="round"
			stroke-width="2"
			d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
	</svg>
	<span>{errorMessage}</span>
	<div>
		<button class="btn btn-sm" onclick={() => showError = false}>Ok</button>
	</div>
</div>
{/if}
<div class="bg-primary-foreground p-2 w-fit mx-auto rounded-md">
	<h1 class="mx-auto text-3xl text-green-500">Twisting Nether IO Calculation</h1>
</div>
<div class="w-fit mx-auto bg-primary-foreground p-4 rounded-md my-4">
	<CharacterForm {data} />
</div>
{#if showNewsError}
<div role="alert" class="alert alert-error max-w-96 mx-auto mb-2 my-10">
	<svg
		xmlns="http://www.w3.org/2000/svg"
		fill="none"
		viewBox="0 0 24 24"
		class="stroke-info h-6 w-6 shrink-0">
		<path
			stroke="#1E293B"
			stroke-linecap="round"
			stroke-linejoin="round"
			stroke-width="2"
			d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
	</svg>
	<span>{showNewsErrorMessage}</span>
	<div>
		<button class="btn btn-sm" onclick={() => showNewsError = false}>Ok</button>
	</div>
</div>
{/if}
{#if newsPosts.length > 0 && !showNewsError}
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