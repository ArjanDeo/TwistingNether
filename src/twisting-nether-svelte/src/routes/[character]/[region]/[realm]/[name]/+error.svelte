<script lang="ts">
	import { goto } from '$app/navigation';
    import { page } from '$app/state';
	import Button from '$lib/components/ui/button/button.svelte';
</script>
<svelte:head>
	<title>{page.error?.message}</title>
</svelte:head>
<main class="grid min-h-full place-items-center bg-white/10 backdrop-blur-lg border border-white/30 shadow-lg px-6 py-24 sm:py-32 lg:px-8">
  <div class="text-center">
    <p class="text-lg font-semibold text-green-500">{page.status}</p>
    <h1 class="mt-4 text-5xl font-semibold tracking-tight text-balance text-[#008A38] sm:text-7xl">{page.status == 404 ? "Character not found" : "Error processing character"}</h1>
    <p class="mt-6 text-lg font-medium text-pretty text-gray-400 sm:text-xl/8">{page.status == 404 ? "Sorry, we couldn’t find that character. Make sure you entered the correct name, realm & region" : "Something wrent wrong!"}!</p>
    <div class="mt-10 flex items-center justify-center gap-x-6">
      <Button onclick={() => goto('/')} class="cursor-pointer rounded-md bg-green-700 px-3.5 py-2.5 text-sm font-semibold text-white shadow-xs hover:bg-green-600 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-500">Go back home</Button>
    </div>
  </div>
</main>
