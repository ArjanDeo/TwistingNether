<script lang="ts">
	import '../app.css';
	let { children } = $props();
</script>
<div>
	<p class="invisible">the background image breaks if i dont put this text here</p>
</div>
<svelte:head>
	<title>The Twisting Nether</title>
</svelte:head>
<nav class="bg-slate-800 w-auto lg:w-1/3 mx-auto rounded-full p-3">
	<div class="flex flex-col lg:flex-row lg:gap-x-3">
		<a href="/" class="flex">
			<img class="max-w-20" src="../../../argus_500.png" alt="Argus Logo">
			<span class="place-self-center text-2xl bg-clip-text text-transparent inline-block bg-gradient-to-r from-green-600 to-green-900">
				THE TWISTING NETHER
			</span>
		</a>
		<div class="flex flex-row gap-x-4 lg:gap-x-3 mx-auto lg:m-0">
			<a class="text-center my-auto hover:text-green-700 transition-colors ease-in-out" href="/">Home</a>
			<a class="text-center my-auto hover:text-green-700 transition-colors ease-in-out" href="/about">About</a>
		</div>
		
	</div>
</nav>

<main class="mt-5">
	<div>
		{@render children()}
	</div>
</main>

<style>
	:global(body) {    
	  background-image: url("/WoW_7.3_Shadows_of_Argus_05.png");
	  background-repeat: no-repeat;
	  background-attachment: fixed;
	  background-position: center;
	  background-size: cover;
	  min-height: 100vh;
	}	
	  </style>
