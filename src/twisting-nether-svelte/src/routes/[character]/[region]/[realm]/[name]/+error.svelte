<script lang="ts">
    import { page } from '$app/state';
	import CharacterError from './components/CharacterError.svelte';
	import CharacterNotFound from './components/CharacterNotFound.svelte';
</script>
<svelte:head>
	<title>{page.status == 404 ? "Character not found" : "Something went wrong"}</title>
</svelte:head>
{#if page.status == 404}
	<CharacterNotFound />
{:else}
	<CharacterError status={page.status} />
{/if}
