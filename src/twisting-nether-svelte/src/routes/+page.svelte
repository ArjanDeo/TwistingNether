<script lang="ts">
	import { goto } from "$app/navigation";
	import type { CharacterData } from "$lib/types";
    import {realms } from "$lib/realms"
	let characterName = "";
	let characterRealm = "";
	let characterRegion = "default";
	let showError = false; // Controls visibility of the error alert
	let errorMessage = "Character not found"; // Error message text
	let characterFound = false

	let invalidFields = {
		characterName: false,
		characterRealm: false,
		characterRegion: false,
	};

	async function getCharacter() {
	// Reset invalid fields
	invalidFields = {
		characterName: !characterName.trim(),
		characterRealm: !characterRealm.trim(),
		characterRegion: !characterRegion.trim(),
	};

	// Check if there are any invalid fields
	if (Object.values(invalidFields).some((isInvalid) => isInvalid)) {
        errorMessage =  "Please fill out all fields"// Read the response body for a more specific error message
		showError = true;
		return;
	}

	try {
		const res = await fetch(
			`https://localhost:7176/api/character/ping?name=${characterName}&realm=${characterRealm}&region=${characterRegion}`
		);

		if (res.ok) {
			showError = false; // Clear any previous errors
			goto(`/character/${characterRegion.toLowerCase()}/${characterRealm.toLowerCase()}/${characterName.toLowerCase()}`);
			characterFound = true;
		} else {
            var x = await res.json();
			errorMessage =  x;
			showError = true;
		}
	} catch (error: any) {
		// Network or other errors
		errorMessage = error.message || "An unexpected error occurred.";
		showError = true;
	}
}


	function validateField(field: keyof typeof invalidFields, value: string) {
		invalidFields[field] = !value.trim();
	}

	function dismissError() {
		showError = false;
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
		<button class="btn btn-sm" on:click={dismissError}>Ok</button>
	</div>
</div>
{/if}

<div class="card glass w-fit mx-auto p-10">
	<div class="card-body">
		<form on:submit|preventDefault={getCharacter} class="flex flex-col gap-4">
			<input
				bind:value={characterName}
				class="input input-primary"
				placeholder="Character Name"
				type="text"
				class:input-invalid={invalidFields.characterName}
				on:blur={() => validateField("characterName", characterName)}				
			/>
			<input
				bind:value={characterRealm}
				class="input input-primary"
				placeholder="Character Realm"
				type="text"
                list="realms"
				class:input-invalid={invalidFields.characterRealm}
				on:blur={() => validateField("characterRealm", characterRealm)}				
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
</style>
