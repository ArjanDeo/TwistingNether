<script lang="ts">
import "../app.css";
import { Toaster } from "$lib/components/ui/sonner";
	import SettingsModal from "./components/settings-modal.svelte";
let { children } = $props();
import { ModeWatcher } from "mode-watcher";
</script>

<ModeWatcher />
<Toaster />
<svelte:head>
	<title>The Twisting Nether</title>
</svelte:head>


<main class="">
<nav class="bg-primary-foreground w-auto sticky top-0 z-20 p-2 ">
	<div class="flex flex-row lg:gap-x-3">
		<a href="/" class="flex">
			<enhanced:img class="max-w-20" src="../lib/argus_500.webp" alt="Argus Logo" />
			<span class="place-self-center text-2xl bg-clip-text text-transparent inline-block bg-gradient-to-r from-green-600 to-green-900">
				THE TWISTING NETHER
			</span>
		</a>
		<div class="flex flex-row gap-x-4 lg:gap-x-3 mx-auto lg:m-0">
			<a class="text-center my-auto hover:text-green-700 transition-colors ease-in-out" href="/">Home</a>
			<a class="text-center my-auto hover:text-green-700 transition-colors ease-in-out" href="/about">About</a>
		</div>
		<div class="my-auto">
			<SettingsModal />
		</div>
	</div>
</nav>
	<div>
		{@render children()}
	</div>
</main>

<style lang="postcss">
	@reference "tailwindcss";
	:global(body) {    
	  background-image: url("/WoW_7.3_Shadows_of_Argus_05.webp");
	  background-repeat: no-repeat;
	  background-attachment: fixed;
	  background-position: center;
	  background-size: cover;
	  min-height: 100vh;
	}	
	  </style>
