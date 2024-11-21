<script lang="ts">
	import { goto } from '$app/navigation';
    import { page } from '$app/stores';
</script>
<svelte:head>
	<title>{$page.error?.message}</title>
</svelte:head>
<div class="mx-5">
    <h1 class="text-3xl text-green-600">{$page.error?.message}</h1>
    <button on:click={() => goto('/')} class="text-2xl text-green-600 bg-gray-800 hover:bg-gray-700 transition-colors ease-in-out duration-150 rounded-md p-2 mt-5">Go Back</button>
</div>
