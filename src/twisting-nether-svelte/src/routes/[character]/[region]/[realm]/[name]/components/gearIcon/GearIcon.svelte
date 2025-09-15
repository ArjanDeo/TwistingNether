<script lang="ts">
	import * as Tooltip from "$lib/components/ui/tooltip";
	import { ItemQuality, MainStats, specToMainStat, type GearPiece } from "$lib/types";
	import { equipmentIcons, qualityIcons } from "$lib/metadata";
    import enchant_scroll from "$lib/icons/misc/inv_misc_enchantedscroll.webp"
    import prismatic_socket from "$lib/icons/misc/ui_prismatic_socket.webp"
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

        function getQualityGlow(quality: string): string {
        const color = itemRarityColor(quality);
        return `0 0 15px ${color}40, 0 0 30px ${color}20`;
    }
</script>
<style>
    .gear-slot {
        position: relative;
        transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
        border-radius: 8px;
        overflow: visible;
        background: linear-gradient(135deg, rgba(30, 41, 59, 0.8), rgba(15, 23, 42, 0.9));
    }

    .gear-slot:hover {
        transform: translateY(-3px) scale(1.05);
    }

    .gear-icon {
        width: 48px;
        height: 48px;
        border-radius: 6px;
        transition: all 0.3s ease;
        position: relative;
        overflow: hidden;
    }



    .gear-slot:hover .gear-icon::before {
        opacity: 1;
    }

    .item-level {
        position: absolute;
        bottom: 0;
        right: 0;
        font-size: 10px;
        font-weight: 700;
        padding: 1px 3px;
        border-radius: 3px 0 0 0;
        background: rgba(0, 0, 0, 0.8);
        border: 1px solid rgba(255, 255, 255, 0.2);
        text-shadow: 0 1px 2px rgba(0, 0, 0, 0.8);
        min-width: 16px;
        text-align: center;
    }

    .empty-slot {
        opacity: 0.4;
        border: 2px dashed rgba(148, 163, 184, 0.3);
        background: rgba(30, 41, 59, 0.3);
        transition: all 0.3s ease;
    }

    .empty-slot:hover {
        opacity: 0.7;
        border-color: rgba(148, 163, 184, 0.6);
        background: rgba(30, 41, 59, 0.5);
        transform: translateY(-1px);
    }

    @keyframes enchantPulse {
        0%, 100% { opacity: 0.7; transform: scale(1); }
        50% { opacity: 1; transform: scale(1.2); }
    }

    @keyframes gemGlow {
        0%, 100% { opacity: 0.5; }
        50% { opacity: 1; }
    }

    .tooltip-content {
        background: linear-gradient(135deg, rgba(17, 24, 39, 0.95), rgba(31, 41, 55, 0.95));
        backdrop-filter: blur(20px);
        border: 1px solid rgba(75, 85, 99, 0.3);
        border-radius: 12px;
        padding: 16px;
        box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.5);
        max-width: 320px;
    }

    .stat-primary {
        color: #fbbf24;
        font-weight: 600;
    }

    .stat-secondary {
        color: #d1d5db;
    }

    .spell-effect {
        color: #10b981;
        font-style: italic;
        background: rgba(16, 185, 129, 0.1);
        padding: 4px 8px;
        border-radius: 4px;
        border-left: 2px solid #10b981;
        margin: 4px 0;
    }

    .enchant-row {
        background: rgba(16, 185, 129, 0.1);
        padding: 2px 6px;
        border-radius: 4px;
        margin: 2px 0;
        border-left: 2px solid #10b981;
    }
</style>
{#if gear}
<Tooltip.Provider>
    <Tooltip.Root>
        <Tooltip.Trigger>
                <div class="gear-slot" style="--quality-color: {itemRarityColor(gear.quality.type)};">
                    <a 
                        target="_blank" 
                        href="https://www.wowhead.com/item={gear.item.id}/"
                        class="block relative"
                    >
                        <img
                            class="gear-icon"
                            style="border-color: {itemRarityColor(gear.quality.type)}; box-shadow: {getQualityGlow(gear.quality.type)};"
                            src="https://wsrv.nl/?url={gear.item.iconUrl}&w=48&h=48&fit=cover&output=webp&q=90"
                            alt="{gear.name}"
                        />
                        
                        <!-- Item Level Badge -->
                        <span 
                            class="item-level"
                            style="color: {itemRarityColor(gear.quality.type)};"
                        >
                            {gear.level.value}
                        </span>
                        
                        <!-- Enchant Indicator -->
                        {#if gear.enchantments && gear.enchantments.length > 0}
                            <img class="w-5 absolute -top-1 -right-1" src={enchant_scroll} alt="Enchant Scroll"/>
                        {/if}
                        
                         {#if gear.sockets?.find(s => s.item != undefined)}
                            <img class="w-4 absolute -top-1 -left-1" src={prismatic_socket} alt="Prismatic Socket"/>
                            {#if gear.sockets.length > 1}
                            <img class="w-4 absolute top-4 -left-1" src={prismatic_socket} alt="Prismatic Socket"/>
                            {/if}
                        {/if} 
                    </a>
                </div>
            </Tooltip.Trigger>
        <Tooltip.Content class="bg-transparent">
                <div class="tooltip-content" style="--rarityColor: {itemRarityColor(gear.quality.type)};">
                    <!-- Item Header -->
                    <div class="mb-3">
                        <p class="text-lg font-bold mb-1" style="color: var(--rarityColor);">
                            {gear.name}
                        </p>
                        <p class="text-sm text-gray-300 mb-1">{gear.level.display_string}</p>
                        <p class="text-sm text-gray-400">{gear.inventory_type.name}</p>
                    </div>
                    
                    <div class="border-t border-gray-600 my-3 opacity-50"></div>
                    
                    <!-- Stats -->
                    <div class="space-y-1">
                        {#each gear.stats as stat}
                            {#if stat.type.name == displayStat}
                                <p class="stat-primary text-sm">
                                    +{stat.value} {stat.type.name}
                                </p>
                            {:else if stat.type.name !== "Agility" && stat.type.name !== "Strength" && stat.type.name !== "Intellect"}
                                <p class="stat-secondary text-sm">
                                    +{stat.value} {stat.type.name}
                                </p>
                            {/if}
                        {/each}
                    </div>
                    
                    <!-- Enchantments -->
                    {#if gear.enchantments && gear.enchantments.length > 0}
                        <div class="mt-3 space-y-1">
                            {#each gear.enchantments as enchant}
                                <div class="enchant-row flex items-center gap-2">
                                    <span class="text-sm text-green-300 flex-1">
                                        {trimEnchantString(enchant.display_string)}
                                    </span>
                                    <img 
                                        class=" opacity-80" 
                                        src="{getEnchantQualityIcon(enchant.display_string)}" 
                                        alt="Enchant quality"
                                    />
                                </div>
                            {/each}
                        </div>
                    {/if}
                    
                    <!-- Spell Effects -->
                    {#if gear.spells && gear.spells.length > 0}
                        <div class="mt-3 space-y-2">
                            {#each gear.spells as spell}
                                <div class="spell-effect text-sm">
                                    {spell.description}
                                </div>
                            {/each}
                        </div>
                    {/if}

                    <!-- Socket Gems -->
                    {#if gear.sockets?.find(s => s.item != undefined)}
                        <div class="mt-3 space-y-1">
                            {#each gear.sockets as socket}
                                <div class="spell-effect text-sm flex flex-row gap-x-1">
                                    <img class="w-4 h-4 mt-0.5" src={prismatic_socket} alt="Prismatic Socket"/>
                                    <p>{socket.display_string}</p>
                                </div>
                            {/each}
                        </div>
                    {/if}
                </div>
            </Tooltip.Content>
    </Tooltip.Root>
</Tooltip.Provider>
{:else}
<Tooltip.Provider>
        <Tooltip.Root>
            <Tooltip.Trigger>
                <div class="gear-slot empty-slot">
                    <img
                        class="gear-icon opacity-60"
                        src="{equipmentIcons[slot]}"
                        alt="Empty {slot}"
                    />
                </div>
            </Tooltip.Trigger>
            <Tooltip.Content class="bg-transparent">
                <span class="text-gray-300 tooltip-content">
                    Empty {slot}
                </span>
            </Tooltip.Content>
        </Tooltip.Root>
    </Tooltip.Provider>
{/if}