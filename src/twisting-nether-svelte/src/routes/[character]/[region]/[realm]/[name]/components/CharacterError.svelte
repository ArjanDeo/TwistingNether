<script lang="ts">
	import { goto } from '$app/navigation';
	import Button from '$lib/components/ui/button/button.svelte';

	let { status }: { status: number | undefined } = $props();

	const details = $derived.by(() => {
		if (status === 429) {
			return { title: 'Too many requests', message: 'We’re being rate limited right now. Wait a moment and try again.' };
		}
		if (status === 502 || status === 503 || status === 504) {
			return { title: 'Server unavailable', message: 'We couldn’t reach the server. It may be down or restarting, try again shortly.' };
		}
		if (status === 400) {
			return { title: 'Invalid request', message: 'That character lookup couldn’t be processed. Check the name, realm & region and try again.' };
		}
		return { title: 'Something went wrong', message: 'There was a problem loading this character. Please try again later.' };
	});
</script>

<main class="grid min-h-full place-items-center bg-white/10 backdrop-blur-lg border border-white/30 shadow-lg px-6 py-24 sm:py-32 lg:px-8">
  <div class="text-center">
    <p class="text-lg font-semibold text-green-500">{status ?? 500}</p>
    <h1 class="mt-4 text-5xl font-semibold tracking-tight text-balance text-[#008A38] sm:text-7xl">{details.title}</h1>
    <p class="mt-6 text-lg font-medium text-pretty text-gray-400 sm:text-xl/8">{details.message}</p>
    <div class="mt-10 flex items-center justify-center gap-x-6">
      <Button onclick={() => location.reload()} class="cursor-pointer rounded-md bg-green-700 px-3.5 py-2.5 text-sm font-semibold text-white shadow-xs hover:bg-green-600 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-500">Try again</Button>
      <Button onclick={() => goto('/')} variant="ghost" class="cursor-pointer text-sm font-semibold">Go back home</Button>
    </div>
  </div>
</main>
