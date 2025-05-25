<script lang="ts">
import "../app.css";
import { ModeWatcher, toggleMode } from "mode-watcher";
import SunIcon from "@lucide/svelte/icons/sun";
import MoonIcon from "@lucide/svelte/icons/moon";
	import { Button } from "$lib/components/ui/button";
	import { Toaster } from "$lib/components/ui/sonner";

let { children } = $props();
</script>
<ModeWatcher />
<Toaster />

<!-- im pretty sure I dont need this anymore but just commenting out to be safe
<div>
	<p class="invisible">the background image breaks if i dont put this text here</p>
</div>-->
<svelte:head>
	<title>The Twisting Nether</title>
</svelte:head>
<nav class="bg-primary-foreground w-auto sticky top-0 z-10 p-2">
	<div class="flex flex-row lg:gap-x-3">
		<a href="/" class="flex">
			<img class="max-w-20" src="../../../argus_500.png" alt="Argus Logo">
			<span class="place-self-center text-2xl bg-clip-text text-transparent inline-block bg-gradient-to-r from-green-600 to-green-900">
				THE TWISTING NETHER
			</span>
		</a>
		<div class="flex flex-row gap-x-4 lg:gap-x-3 mx-auto lg:m-0">
			<a class="text-center my-auto hover:text-green-700 transition-colors ease-in-out" href="/">Home</a>
			<a class="text-center my-auto hover:text-green-700 transition-colors ease-in-out" href="/about">About</a>
			<a class="text-center my-auto hover:text-green-700 transition-colors ease-in-out" href="/io-calculator">IO Calculator</a>
		</div>
		<div class="my-auto">
			<Button onclick={toggleMode} variant="outline" size="icon" class="cursor-pointer">
				<SunIcon
				class="h-[1.2rem] w-[1.2rem] rotate-0 scale-100 transition-all dark:-rotate-90 dark:scale-0"
				/>
				<MoonIcon
				class="absolute h-[1.2rem] w-[1.2rem] rotate-90 scale-0 transition-all dark:rotate-0 dark:scale-100"
				/>
				<span class="sr-only">Toggle theme</span>
			</Button>
		</div>
		
	</div>
</nav>

<main class="mt-5">
	<div>
		{@render children()}
	</div>
</main>

<style lang="postcss">
	@reference "tailwindcss";
	:global(body) {    
	  background-image: url("/WoW_7.3_Shadows_of_Argus_05.png");
	  background-repeat: no-repeat;
	  background-attachment: fixed;
	  background-position: center;
	  background-size: cover;
	  min-height: 100vh;
	}	
	  </style>
