<script lang="ts">
	import * as Dialog from "$lib/components/ui/dialog/index.js";
	import { Button, buttonVariants } from "$lib/components/ui/button/index.js";
	import { Label } from "$lib/components/ui/label/index.js";
	import { SettingsIcon } from "@lucide/svelte";
	import MoonIcon from "@lucide/svelte/icons/moon";
	import SunIcon from "@lucide/svelte/icons/sun";
	import { toggleMode } from "mode-watcher";
	import Checkbox from "$lib/components/ui/checkbox/checkbox.svelte";
	import type { TwistingNetherSettings } from "$lib/types";
	import { appSettings } from "$lib/settings.svelte";
	import { onMount } from "svelte";

	let open = $state(false);

	let draftSettings: TwistingNetherSettings = $state({
		...appSettings.defaults
	});

	onMount(() => {
		appSettings.load();

		draftSettings = {
			...appSettings.settings
		};
	});

	function openSettings() {
		draftSettings = {
			...appSettings.settings
		};

		open = true;
	}

	function saveSettings() {
		appSettings.commit(draftSettings);
		open = false;
        location.reload();
	}

	function cancelSettings() {
		draftSettings = {
			...appSettings.settings
		};

		open = false;
	}
</script>

<Dialog.Root bind:open>
	<Dialog.Trigger
		type="button"
		onclick={openSettings}
		class="bg-primary-foreground cursor-pointer hover:text-green-700 transition-colors ease-in-out"
	>
		<SettingsIcon />
	</Dialog.Trigger>

	<Dialog.Content class="sm:max-w-[425px]">
		<Dialog.Header>
			<Dialog.Title>Settings</Dialog.Title>
			<Dialog.Description>
				Make changes to website settings here. Click save when you&apos;re done.
			</Dialog.Description>
		</Dialog.Header>

		<div class="grid gap-4">
			<div class="grid gap-3">
				<Label for="themeToggle">Theme</Label>

				<Button
					id="themeToggle"
					type="button"
					onclick={toggleMode}
					variant="outline"
					size="icon"
				>
					<SunIcon
						class="h-[1.2rem] w-[1.2rem] scale-100 rotate-0 !transition-all dark:scale-0 dark:-rotate-90"
					/>

					<MoonIcon
						class="absolute h-[1.2rem] w-[1.2rem] scale-0 rotate-90 !transition-all dark:scale-100 dark:rotate-0"
					/>

					<span class="sr-only">Toggle theme</span>
				</Button>
			</div>

			<div class="grid gap-3">
				<Label for="useWowheadTooltips">
					Use Wowhead tooltips for items
				</Label>

				<Checkbox
					bind:checked={draftSettings.useWowheadTooltips}
					id="useWowheadTooltips"
				/>
			</div>
		</div>

		<Dialog.Footer>
			<Dialog.Close
				type="button"
				onclick={cancelSettings}
				class={buttonVariants({ variant: "outline" })}
			>
				Cancel
			</Dialog.Close>

			<Button type="button" onclick={saveSettings}>
				Save changes
			</Button>
		</Dialog.Footer>
	</Dialog.Content>
</Dialog.Root>