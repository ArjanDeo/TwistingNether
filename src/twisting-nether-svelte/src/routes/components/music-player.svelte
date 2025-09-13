<script lang="ts">
  import { Fa } from 'svelte-fa';
  import { faPlay, faPause, faBackward, faForward, faVolumeUp } from '@fortawesome/free-solid-svg-icons';

  let isPlaying = false;

  function togglePlay() {
    isPlaying = !isPlaying;
  }
</script>

  <div class="bg-black bg-opacity-50 backdrop-blur-md text-white p-6 rounded-2xl w-full max-w-md shadow-xl border border-purple-700">
    <!-- Header -->
    <div class="flex items-center space-x-4 mb-4">
      <img src="/kingdomswillburn.jpg" alt="Music Cover" class="w-16 h-16 rounded-md" />
      <div>
        <h3 class="text-sm text-gray-300">World of Warcraft</h3>
        <h2 class="text-xl font-semibold text-white">Twisting Nether</h2>
      </div>
    </div>

    <!-- Progress Bar -->
    <div class="mb-4">
      <div class="relative w-full h-2 bg-gray-700 rounded-full">
        <div class="absolute top-0 left-0 h-2 bg-purple-400 rounded-full" style="width: 30%;"></div>
      </div>
      <div class="flex justify-between text-xs text-gray-400 mt-1">
        <span>0:45</span>
        <span>2:34</span>
      </div>
    </div>

    <!-- Controls -->
    <div class="flex items-center justify-center space-x-6 mt-4">
      <button class="text-white hover:text-purple-400">
        <Fa icon={faBackward} class="w-6 h-6" />
      </button>
      <button
        on:click={togglePlay}
        class="bg-purple-600 hover:bg-purple-700 text-white w-12 h-12 rounded-full flex items-center justify-center shadow-lg"
      >
        <Fa icon={isPlaying ? faPause : faPlay} class="w-6 h-6" />
      </button>
      <button class="text-white hover:text-purple-400">
        <Fa icon={faForward} class="w-6 h-6" />
      </button>
    </div>

    <!-- Volume -->
    <div class="mt-6 flex items-center justify-end text-gray-400">
      <Fa icon={faVolumeUp} class="w-5 h-5 mr-2" />
      <input type="range" min="0" max="100" value="60" class="w-32 accent-purple-500 bg-gray-700 rounded-full h-1" />
    </div>
  </div>