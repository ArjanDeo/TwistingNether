<script lang="ts">
	import { browser } from "$app/environment";
	import { goto } from "$app/navigation";
	import { API_BASE_URL } from "$lib/common";
	import { realms } from "$lib/realms";
	import type { Character } from "$lib/types";
	import { ClassColors } from "$lib/types";
	import { onMount } from "svelte";

	let characterName = "";
	let characterRealm = "";
	let characterRegion = "default";

	let showError = false;
	let showNewsError = false;
	let showNewsErrorMessage = "";
	let errorMessage = "Character not found";
	let characterFound = false;

	let recentCharacters: Array<Character> = [];
	let newsPosts: Array<any> = [];

	onMount(async () => {
		recentCharacters = JSON.parse(localStorage.getItem("recentCharacters") || "[]");
		try {
			const news = await fetch(`${API_BASE_URL}/General/GetNews?limit=6`);

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
				`${API_BASE_URL}/character/ping?name=${characterName}&realm=${characterRealm}&region=${characterRegion}`
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
		<button class="btn btn-sm" on:click={() => showError = false}>Ok</button>
	</div>
</div>
{/if}
<div class="flex flex-col md:flex-row justify-center items-start gap-4 mt-10">
	<div class="card glass w-fit p-10 mx-auto md:mx-0">
		<div class="card-body">
			<form on:submit|preventDefault={getCharacter} class="flex flex-col gap-4">
				<input
					bind:value={characterName}
					class="input input-primary"
					placeholder="Character Name"
					type="text"
					class:input-invalid={invalidFields.characterName}
					on:submit={() => validateField("characterName", characterName)}				
				/>
				<input
					bind:value={characterRealm}
					class="input input-primary"
					placeholder="Character Realm"
					type="text"
					list="realms"
					class:input-invalid={invalidFields.characterRealm}
					on:submit={() => validateField("characterRealm", characterRealm)}				
				/>
				<datalist id="realms">
					{#each realms
						.sort((a, b) => a.realmName.localeCompare(b.realmName))
						as realm (realm.realmName)}
						<option value="{realm.realmName}">{realm.realmName}</option>
					{/each}
				</datalist>
				<select
					bind:value={characterRegion}
					class="input input-primary {characterRegion === "default" ? "text-gray-500" : ""}"
					placeholder="Character Region"
					class:input-invalid={invalidFields.characterRegion}
					on:blur={() => validateField("characterRegion", characterRegion)}>
					<option value="default" disabled selected>Character Region</option>
					<option value="US">US</option>
					<option value="EU">EU</option>
					<option value="KR">KR</option>
				</select>
				<div class="flex justify-center mt-4">
					<input class="btn btn-primary" type="submit" value="Submit" />
				</div>
			</form>
		</div>
	</div>

	{#if recentCharacters.length > 0}
	<div class="recent-characters card glass w-fit p-4 mx-auto md:mx-0">
		<h3 class="font-bold text-lg mb-2">Recently Searched</h3>
		<ul class="list-none">
			{#each recentCharacters as character}
			<li class="mb-2">
				<button
				    style="color: {ClassColors[character.class]};"
					class="btn btn-outline btn-sm w-full hover:scale-110 transition-transform ease-in-out duration-500"
					on:click={() => loadCharacter(character)}
				>
					{character.name} - {character.realm} ({character.region.toUpperCase()})
				</button>
			</li>
			{/each}
		</ul>
	</div>
	{/if}
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
		<button class="btn btn-sm" on:click={() => showNewsError = false}>Ok</button>
	</div>
</div>
{/if}
{#if newsPosts.length > 0 && !showNewsError}
<div>
	<h1 class="text-center my-10 text-2xl bg-slate-800 w-fit p-2 rounded-xl mx-auto">RECENT NEWS</h1>
	<div class="grid md:grid-cols-2 lg:grid-cols-3 md:grid-flow-rows gap-y-4 lg:gap-x-3 ml-2">		
		{#each newsPosts as post}
			<div class="glass md:w-52 2xl:w-5/6 h-full mx-auto lg:mx-0 rounded-box">
				<a href="{post.link}" target="_blank">
				<img loading="lazy" src="{post.image}" class="w-fit" alt="{post.title}">
				<h1 class="text-xl p-2 truncate">{post.title}</h1>
				<p class="p-2 line-clamp-2 leading-7 overflow-hidden">{post.description}</p>
				</a>
			</div>
		{/each}
	</div>
</div>
{/if}
<style>
	.input-invalid {
		border-color: red;
		animation: shake 0.5s ease;
	}

	@keyframes shake {
		0%, 100% {
			transform: translateX(0);
		}
		25% {
			transform: translateX(-5px);
		}
		50% {
			transform: translateX(5px);
		}
		75% {
			transform: translateX(-5px);
		}
	}
	.recent-characters {
		padding-top: 1rem;
	}
	.recent-characters button {
		text-align: left;
	}
</style>
