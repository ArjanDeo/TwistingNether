<script lang="ts">
    import { raceColors, type EquippedGear, type GearPiece } from '$lib/types';
    import { type Character, type CharacterMedia, type EquippedItem } from '$lib/types/character'
    import { classIcons, raceIcons } from '$lib/metadata';
    import GearIcon from '../gearIcon/GearIcon.svelte';
	import { onMount } from 'svelte';
	import { API_BASE_URL } from '$lib/common';
	import { toast } from 'svelte-sonner';
    
    let { character }: { character: Character | undefined } = $props();
    let equipped: EquippedItem[] | undefined = $state()
    let equippedGear: EquippedGear | undefined = $state();
    let characterMedia: CharacterMedia[] | undefined = $state();
       
    let raceGenderString: string = $state("");

    // Animation delay helper
    function staggerDelay(index: number): string {
        return `animation-delay: ${index * 50}ms;`;
    }

    async function setCharacterMedia(name: string, realm: string, region: string) {
        const res = await fetch(`${API_BASE_URL}/characters/media?name=${name}&realm=${realm}&region=${region}`);

        if (!res.ok) {
            toast.error('Couldn\'t get character media.');
            return;
        }
        characterMedia = await res.json();
        return;
    }

    onMount(async () => {
        if (character) {
            equipped = character.characterEquipment.equipped_items;
            equippedGear = {
                head: equipped.find(i => i.inventory_type.name === "Head"),
                neck: equipped.find(i => i.inventory_type.name === "Neck"),
                shoulders: equipped.find(i => i.inventory_type.name === "Shoulder"),
                back: equipped.find(i => i.inventory_type.name === "Back"),
                chest: equipped.find(i => i.inventory_type.name === "Chest"),
                tabard: equipped.find(i => i.inventory_type.name === "Tabard"),
                shirt: equipped.find(i => i.inventory_type.name === "Shirt"),
                wrists: equipped.find(i => i.inventory_type.name === "Wrist"),
                hands: equipped.find(i => i.inventory_type.name === "Hands"),
                waist: equipped.find(i => i.inventory_type.name === "Waist"),
                legs: equipped.find(i => i.inventory_type.name === "Legs"),
                feet: equipped.find(i => i.inventory_type.name === "Feet"),
                ring1: equipped.find(i => i.slot.name === "Ring 1"),
                ring2: equipped.find(i => i.slot.name === "Ring 2"),
                trinket1: equipped.find(i => i.slot.name === "Trinket 1"),
                trinket2: equipped.find(i => i.slot.name === "Trinket 2"),
                mainhand: equipped.find(i => i.slot.name === "Main Hand"),
                offhand: equipped.find(i => i.slot.name === "Off Hand"),
            }
            raceGenderString = `${character.characterData.race.replace(' ', '').toLowerCase()}-${character.characterData.gender}`;
            await setCharacterMedia(character.characterData.name, character.characterData.realm, character.characterData.region);
        }
    })
</script>
<style>
    .character-container {
        background: linear-gradient(135deg, #1e293b 0%, #0f172a 50%, #1e293b 100%);
        backdrop-filter: blur(10px);
        border: 1px solid rgba(148, 163, 184, 0.2);
    }

    .header-glow {
        background: linear-gradient(135deg, rgba(59, 130, 246, 0.1), rgba(147, 51, 234, 0.1));
        backdrop-filter: blur(20px);
        border: 1px solid rgba(59, 130, 246, 0.2);
    }

    .gear-item {
        opacity: 0;
        animation: slideInFade 0.6s ease-out forwards;
        transition: transform 0.3s ease, filter 0.3s ease;
    }

    .gear-item:hover {
        transform: translateY(-2px) scale(1.05);
        filter: drop-shadow(0 8px 16px rgba(0, 0, 0, 0.4));
    }

    .character-render {
        transition: transform 0.5s ease, filter 0.3s ease;
        filter: drop-shadow(0 10px 30px rgba(0, 0, 0, 0.5));
    }

    .character-render:hover {
        transform: scale(1.02);
        filter: drop-shadow(0 15px 40px rgba(0, 0, 0, 0.6));
    }

    .external-link {
        transition: all 0.3s ease;
        border-radius: 50%;
        border: 2px solid transparent;
        background: rgba(30, 41, 59, 0.8);
    }

    .external-link:hover {
        transform: translateY(-2px) scale(1.1);
        border-color: rgba(59, 130, 246, 0.5);
        background: rgba(59, 130, 246, 0.1);
        box-shadow: 0 8px 25px rgba(59, 130, 246, 0.3);
    }

    .stat-card {
        background: linear-gradient(135deg, rgba(30, 41, 59, 0.8), rgba(15, 23, 42, 0.9));
        backdrop-filter: blur(10px);
        border: 1px solid rgba(148, 163, 184, 0.2);
        transition: all 0.3s ease;
    }

    .stat-card:hover {
        transform: translateY(-2px);
        border-color: rgba(59, 130, 246, 0.4);
        box-shadow: 0 8px 25px rgba(0, 0, 0, 0.3);
    }

    .guild-link {
        background: linear-gradient(90deg, rgba(137, 22, 244, 0.521), rgba(14, 92, 218, 0.51));
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
        background-clip: text;
        transition: all 0.3s ease;
    }

    .guild-link:hover {
        background: linear-gradient(90deg, rgba(147, 51, 234, 0.8), rgba(59, 130, 246, 0.8));
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
        background-clip: text;
        text-shadow: 0 0 20px rgba(147, 51, 234, 0.5);
    }

    @keyframes slideInFade {
        from {
            opacity: 0;
            transform: translateY(10px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }

    @keyframes float {
        0%, 100% { transform: translateY(0px); }
        50% { transform: translateY(-5px); }
    }

    .floating {
        animation: float 3s ease-in-out infinite;
    }
</style>
{#if character}
<div class="character-container rounded-2xl p-8 shadow-2xl">
    <!-- Enhanced Header -->
    <header class="header-glow rounded-xl p-6 mb-8">
        <div class="text-center mb-6">
            <h1 class="text-3xl lg:text-5xl font-bold mb-2 ">
                {character.characterData.name}
                <span class="text-2xl lg:text-3xl">-{character.characterData.realm}</span>
            </h1>
            
            {#if character.characterData.guild != null}
                <h2 class="text-xl lg:text-2xl font-semibold mb-4">
                    <a 
                        target="_blank" 
                        class="guild-link hover:scale-105 transition-transform duration-300 inline-block" 
                        href="https://worldofwarcraft.blizzard.com/en-us/guild/{character.characterData.region}/{character.characterData.guild.realm.replace('\'', "")}/{character.characterData.guild.name.replace(' ', '-')}/"
                    >
                        &lt;{character.characterData.guild.name}&gt;
                    </a>
                </h2>
            {/if}

            <!-- Character Info Cards -->
            <div class="grid grid-cols-1 md:grid-cols-3 gap-4 mb-6">
                <!-- Race Card -->
                <div class="stat-card rounded-lg p-4 flex items-center gap-3">
                    <img 
                        src={raceIcons[raceGenderString]} 
                        alt={character.characterData.race} 
                        class="w-10 h-10 rounded-full border-2 border-gray-600"
                    />
                    <div>
                        <p class="text-xs text-gray-400 uppercase tracking-wide">Race</p>
                        <p class="text-lg font-semibold " style="color: {raceColors[character.characterData.race]}">{character.characterData.race}</p>
                    </div>
                </div>

                <!-- Class Card -->
                <div class="stat-card rounded-lg p-4 flex items-center gap-3">
                    <img 
                        src={classIcons[character.characterData.char_class]} 
                        alt={character.characterData.char_class} 
                        class="w-10 h-10 rounded-full border-2 border-gray-600"
                    />
                    <div>
                        <p class="text-xs text-gray-400 uppercase tracking-wide">Class</p>
                        <p class="text-lg font-semibold" style="color: {character.classColor};">
                            {character.characterData.char_class}
                        </p>
                        <p class="-mt-2 text-sm text-center">
                            {character.characterData.active_spec_name}
                        </p>
                    </div>
                </div>

                <!-- Item Level Card -->
                <div class="stat-card rounded-lg p-4 flex items-center gap-3">
                    <div class="w-10 h-10 bg-gradient-to-br from-yellow-400 to-orange-500 rounded-full flex items-center justify-center">
                        <span class="text-xl">⚔️</span>
                    </div>
                    <div>
                        <p class="text-xs text-gray-400 uppercase tracking-wide">Item Level</p>
                        <p class="text-lg font-semibold text-yellow-400">{character.characterData.gear.item_level_equipped}</p>
                    </div>
                </div>
            </div>

            <!-- External Links -->
            <div class="flex justify-center gap-4">
                <a 
                    aria-label="raider.io link" 
                    href="https://raider.io/characters/{character.characterData.region}/{character.characterData.realm}/{character.characterData.name}" 
                    target="_blank"
                    class="external-link p-3"
                >
                    <img src="/raiderioicon.webp" class="w-8 h-8" alt="raider io logo">
                </a>
                <a 
                    aria-label="warcraft logs link"
                    href="https://www.warcraftlogs.com/character/{character.characterData.region}/{character.characterData.realm}/{character.characterData.name}" 
                    target="_blank"
                    class="external-link p-3"
                >
                    <img src="/warcraftlogsicon.webp" class="w-8 h-8" alt="warcraft logs logo"/>
                </a>
                <a 
                    aria-label="world of warcraft armory link"
                    href="https://worldofwarcraft.blizzard.com/en-gb/character/{character.characterData.region}/{character.characterData.realm}/{character.characterData.name}" 
                    target="_blank"
                    class="external-link p-3"
                >
                    <img src="/wowicon.webp" class="w-8 h-8" alt="world of warcraft logo"/>
                </a>
            </div>
        </div>
    </header>

    <!-- Enhanced Gear Layout -->
    <div class="flex items-start justify-center flex-wrap lg:flex-nowrap gap-8">
        <!-- Left Gear Column -->
        <div class="flex flex-col gap-3 order-2 lg:order-1">
            {#if equippedGear}
                {#each [
                    { slot: 'Head', gear: equippedGear.head },
                    { slot: 'Neck', gear: equippedGear.neck },
                    { slot: 'Shoulders', gear: equippedGear.shoulders },
                    { slot: 'Back', gear: equippedGear.back },
                    { slot: 'Chest', gear: equippedGear.chest },
                    { slot: 'Tabard', gear: equippedGear.tabard },
                    { slot: 'Shirt', gear: equippedGear.shirt },
                    { slot: 'Wrists', gear: equippedGear.wrists }
                ] as { slot, gear }, index}
                    <div class="gear-item" style="{staggerDelay(index)}">
                        <GearIcon 
                            gear={gear} 
                            slot={slot} 
                            characterSpec={character.characterData.active_spec_name} 
                        />
                    </div>
                {/each}
            {/if}
        </div>

        <!-- Character Render -->
        <div class="flex flex-col items-center order-1 lg:order-2">
            <div class="relative">
                <!-- Floating animation wrapper -->
                <div class="floating overflow-hidden">
                    {#if characterMedia}
                    <img 
                        src={characterMedia[2].link ? `https://wsrv.nl/?url=${characterMedia[2].link}&w800&output=webp&q=80` : '/default-image.webp'} 
                        alt="Character Render" 
                        class="character-render max-w-sm md:max-w-md lg:max-w-lg xl:max-w-xl rounded-2xl object-cover"
                        fetchpriority="high"
                    />
                    {:else}
                    <div class="w-[800px] character-render max-w-sm md:max-w-md lg:max-w-lg xl:max-w-xl rounded-2xl object-cover animate-pulse"></div>
                    {/if}
                    
                </div>
                
                <!-- Glow effect -->
                <div class="absolute inset-0 bg-gradient-to-t from-blue-500/10 via-transparent to-purple-500/10 rounded-2xl pointer-events-none"></div>
            </div>

            <!-- Weapons -->
            <div class="flex gap-6 mt-6">
                {#if equippedGear}
                    {#each [
                        { slot: 'Main Hand', gear: equippedGear.mainhand },
                        { slot: 'Off Hand', gear: equippedGear.offhand }
                    ] as { slot, gear }, index}
                        <div class="gear-item" style="{staggerDelay(index + 16)}">
                            <GearIcon 
                                gear={gear} 
                                slot={slot} 
                                characterSpec={character.characterData.active_spec_name}
                            />
                        </div>
                    {/each}
                {/if}
            </div>
        </div>

        <!-- Right Gear Column -->
        <div class="flex flex-col gap-3 order-3">
            {#if equippedGear}
                {#each [
                    { slot: 'Hands', gear: equippedGear.hands },
                    { slot: 'Waist', gear: equippedGear.waist },
                    { slot: 'Legs', gear: equippedGear.legs },
                    { slot: 'Feet', gear: equippedGear.feet },
                    { slot: 'Ring 1', gear: equippedGear.ring1 },
                    { slot: 'Ring 2', gear: equippedGear.ring2 },
                    { slot: 'Trinket 1', gear: equippedGear.trinket1 },
                    { slot: 'Trinket 2', gear: equippedGear.trinket2 }
                ] as { slot, gear }, index}
                    <div class="gear-item" style="{staggerDelay(index + 8)}">
                        <GearIcon 
                            gear={gear} 
                            slot={slot} 
                            characterSpec={character.characterData.active_spec_name}
                        />
                    </div>
                {/each}
            {/if}
        </div>
    </div>
</div>
{:else} 
<div class="character-container rounded-2xl p-8 shadow-2xl">
    <!-- Enhanced Header -->
    <header class="header-glow rounded-xl p-6 mb-8">
        <div class="text-center mb-6">
            <div class="text-3xl lg:text-5xl font-bold mb-2 w-10 h-10">
                <span class="text-2xl lg:text-3xl w-10 animate-pulse h-10 bg-gray-700 "></span>
            </div>
            <!-- Character Info Cards -->
            <div class="grid grid-cols-1 md:grid-cols-3 gap-4 mb-6">
                <!-- Race Card -->
                <div class="stat-card rounded-lg p-4 flex items-center gap-3">
                    <div 
                        class="w-10 h-10 rounded-full border-2 border-gray-600 animate-pulse bg-gray-700"
                    ></div>
                    <div>
                        <p class="text-xs text-gray-400 uppercase tracking-wide">Race</p>
                        <p class="text-lg font-semibold  animate-pulse w-12"></p>
                    </div>
                </div>

                <!-- Class Card -->
                <div class="stat-card rounded-lg p-4 flex items-center gap-3">
                    <div 
                        class="w-10 h-10 rounded-full border-2 border-gray-600 animate-pulse bg-gray-700"
                    ></div>
                    <div>
                        <p class="text-xs text-gray-400 uppercase tracking-wide">Class</p>
                        <p class="text-lg font-semibold animate-pulse w-12" >
                        </p>
                        <p class="-mt-2 text-sm text-center animate-pulse w-12">
                        </p>
                    </div>
                </div>

                <!-- Item Level Card -->
                <div class="stat-card rounded-lg p-4 flex items-center gap-3">
                    <div class="w-10 h-10 animate-pulse bg-gray-700 border-gray-600 border-2 rounded-full flex items-center justify-center">
                    </div>
                    <div>
                        <p class="text-xs text-gray-400 uppercase tracking-wide">Item Level</p>
                        <p class="text-lg font-semibold text-yellow-400 w-10 animate-pulse"></p>
                    </div>
                </div>
            </div>

            <!-- External Links -->
            <div class="flex justify-center gap-4">
              
                    <div class="w-10 h-10 animate-pulse bg-gray-700 rounded-full" ></div>
                
                    <div  class="w-10 h-10 animate-pulse bg-gray-700 rounded-full"></div>
                
                    <div class="w-10 h-10 animate-pulse bg-gray-700 rounded-full" ></div>
            </div>
        </div>
    </header>

    <!-- Enhanced Gear Layout -->
    <div class="flex items-start justify-center flex-wrap lg:flex-nowrap gap-8">
        <!-- Left Gear Column -->
        <div class="flex flex-col gap-3 order-2 lg:order-1">
                {#each {length: 8} as _, i}
                    <div class="gear-item" style="{staggerDelay(i)}">
                        <div class="w-12 h-12 animate-pulse bg-gray-500 rounded-md"></div>
                    </div>
                {/each}
        </div>

        <!-- Character Render -->
        <div class="flex flex-col items-center order-1 lg:order-2">
            <div class="relative">
                <div class="floating overflow-hidden">
                    <div class="character-render w-[800px] animate-pulse max-w-sm md:max-w-md lg:max-w-lg xl:max-w-xl rounded-2xl h-[500px] bg-gray-500"></div>
                </div>
            </div>

            <!-- Weapons -->
            <div class="flex gap-6 mt-6">
                    {#each {length: 2}as _, index}
                        <div class="gear-item" style="{staggerDelay(index + 16)}">
                            <div class="gear-item" style="{staggerDelay(index + 8)}">
                        <div class="w-12 h-12 animate-pulse bg-gray-500 rounded-md"></div>
                    </div>
                        </div>
                    {/each}
            </div>
        </div>

        <!-- Right Gear Column -->
        <div class="flex flex-col gap-3 order-3">
                {#each {length: 8} as _, i}
                    <div class="gear-item" style="{staggerDelay(i + 8)}">
                            <div class="w-12 h-12 animate-pulse bg-gray-500 rounded-md"></div>

                    </div>
                {/each}
        </div>
    </div>
</div>
{/if}