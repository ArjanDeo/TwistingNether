<script lang="ts">
    import type { PageData } from './$types';

    let { data }: { data: PageData } = $props();
</script>
<div>
    <p class="text-4xl">The Twisting Nether</p>
    <p class="mt-2">A work in progress website to provide quick, desired information about WoW characters.</p>
    <p>You can follow development and look at the source code <a class="hover:text-green-700 transition-colors ease-in-out underline" href="https://github.com/ArjanDeo/TwistingNether" target="_blank">here.</a></p>
    <p class="mt-4">p.s. I'm not an avid frontend developer, apologies if the UI isn't great.</p>
</div>
