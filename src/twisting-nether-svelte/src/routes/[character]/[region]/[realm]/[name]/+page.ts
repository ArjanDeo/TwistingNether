import { error } from '@sveltejs/kit';
import type { PageLoad } from './$types';
import type { Character } from '$lib/types/character';
import { API_BASE_URL } from '$lib/common';

export const load = (async ({ fetch, params }) => {
    // We wrap the fetch in a function but don't await it yet
    const characterPromise: Promise<Character> = (async () => {
        const res = await fetch(`${API_BASE_URL}/characters?realm=${params.realm}&name=${params.name}&region=${params.region}`);
        if (!res.ok) {
            throw error(404, `Error fetching character. Status: ${res.status} ${res.statusText}`);
        }
        return res.json() as Promise<Character>;
    })();

    return {
        // SvelteKit will treat this as deferred data
        character: characterPromise
    };
}) satisfies PageLoad;
