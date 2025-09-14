<script lang="ts">
	import * as Tooltip from "$lib/components/ui/tooltip";
    import { itemQualityColor } from '$lib/utils';    
	import { ItemQuality, MainStats, specToMainStat, type GearPiece } from "$lib/types";
	import { equipmentIcons, qualityIcons } from "$lib/metadata";
    let { gear, slot, characterSpec }: { gear: GearPiece | undefined, slot: string, characterSpec: string  } = $props();
    function itemRarityColor(itemQuality: string): string {
        switch (itemQuality) {
            case ItemQuality.Common:
                return '#ffffff';
            case ItemQuality.Uncommon:
                return '#1eff00';
            case ItemQuality.Rare:
                return '#0070dd';
            case ItemQuality.Epic:
                return '#a335ee';
            case ItemQuality.Legendary:
                return '#ff8000';
            case ItemQuality.Artifact:
                return '#e6cc80';
            case ItemQuality.Heirloom:
                return '#00ccff';
            default:
                return '#ffffff'; // Default color in case no match
        }
    }
    function getEnchantQualityIcon(display_string: string): string {
        if (display_string.includes('Tier3')) {
            return qualityIcons["Tier 3"];
        }
        if (display_string.includes('Tier2')) {
            return qualityIcons["Tier 2"];
        }
        if (display_string.includes('Tier1')) {
            return qualityIcons["Tier 1"];
        }
        return "test";
    }
    function trimEnchantString(display_string: string): string {
        return display_string.split('|')[0];
    }
    let displayStat: string = $state('');
    if (gear?.stats)
        for (let i = 0; i < gear?.stats.length; i++) {
            if (gear.stats[i].type.name == MainStats.AGILITY || gear.stats[i].type.name == MainStats.STRENGTH || gear.stats[i].type.name == MainStats.INTELLECT) {
                if (gear.stats[i].type.name == specToMainStat[characterSpec]) {
                    displayStat = gear.stats[i].type.name;
                    break;
                }
            }
        }
</script>
{#if gear}
<Tooltip.Provider>
    <Tooltip.Root>
        <Tooltip.Trigger>
            <div class="relative flex items-center">
                <a target="_blank" href="https://www.wowhead.com/item={gear.item.id}/">
                    <img
                        style="border-color: {itemRarityColor(gear.quality.type)};"
                        class="max-w-12 mb-2 hover:cursor-pointer border"
                        src="https://wsrv.nl/?url={gear.item.iconUrl}"
                        alt="{gear.name}" />
                    <span style="color: {itemQualityColor(gear.quality.type)}" class="absolute bottom-2 -right-0.5 text-xs font-bold px-1 rounded-tl">
                        {gear.level.value}
                    </span>
                </a>
            </div>
        </Tooltip.Trigger>
        <Tooltip.Content class="bg-gray-800 max-w-80">
            <span style="--rarityColor: {itemRarityColor(gear.quality.type)};" class="text-[var(--rarityColor)]">
                <div>
                    <p class="text-lg">{gear.name}</p>
                    <p class="text-base">{gear.level.display_string}</p>
                    <p class="text-base">{gear.inventory_type.name}</p>
                </div>
                <div class="w-full bg-black h-0.5 my-1"></div>
                {#each gear.stats as stat}
                    {#if stat.type.name == displayStat}
                        <p class="text-sm text-gray-300">+ {stat.value} {stat.type.name}</p>
                    {:else if stat.type.name !== "Agility" && stat.type.name !== "Strength" && stat.type.name !== "Intellect"}
                        <p class="text-sm text-gray-300">+ {stat.value} {stat.type.name}</p>
                    {/if}
                {/each}
                <div class="mt-2"></div>
                {#if gear.enchantments && gear.enchantments.length != 0}
                {#each gear.enchantments as enchant}
                <div class="flex flex-row gap-x-1">
                    <p class="text-sm">{trimEnchantString(enchant.display_string)}</p>
                    <img class="max-h-4 max-w-5 mt-1" src="{getEnchantQualityIcon(enchant.display_string)}" alt="{enchant.display_string}"/>
                </div>
                    
                {/each}
                {/if}
                <div class="mt-1"></div>

                {#if gear.spells && gear.spells.length != 0}
                {#each gear.spells as spell}
                    <p class="text-[#85FF85] text-sm">
                        {spell.description}
                    </p>
                {/each}
                {/if}
            </span>
        </Tooltip.Content>
    </Tooltip.Root>
</Tooltip.Provider>
{:else}
<Tooltip.Provider>
    <Tooltip.Root>
        <Tooltip.Trigger>
            <div class="flex items-center">
                <img
                    class="w-12 mb-2"
                    src="{equipmentIcons[slot]}"
                    alt="Missing {slot}" />
            </div>
        </Tooltip.Trigger>
        <Tooltip.Content class="bg-slate-700">
            <span
                class="rounded shadow-lg text-white">Empty {slot} slot</span>
        </Tooltip.Content>
    </Tooltip.Root>
</Tooltip.Provider>
{/if}