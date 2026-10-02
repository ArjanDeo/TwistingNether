import { error } from '@sveltejs/kit';
import type { PageLoad } from './$types';
import type { Character } from '$lib/types/character';
import { API_BASE_URL } from '$lib/common';

// The API reports a character Blizzard couldn't find either as a 404, or as a 400 wrapping Blizzard's 404 response.
async function isCharacterNotFound(res: Response): Promise<boolean> {
    if (res.status === 404) return true;
    if (res.status !== 400) return false;
    const body = await res.text().catch(() => '');
    return /\\?"code\\?":\s*404/.test(body);
}

export const load = (async ({ fetch, params }) => {
    // We wrap the fetch in a function but don't await it yet
    const characterPromise: Promise<Character> = (async () => {
        let res: Response;
        try {
            res = await fetch(`${API_BASE_URL}/characters?realm=${params.realm}&name=${params.name}&region=${params.region}`);
        } catch {
            error(503, 'Could not reach the server.');
        }
        if (!res.ok) {
            if (await isCharacterNotFound(res)) {
                error(404, 'Character not found.');
            }
            error(res.status, `Error fetching character. Status: ${res.status} ${res.statusText}`);
        }
        return await res.json() as Character;
    })();
    // The page renders the rejection via {:catch}; mark it handled so the server doesn't crash on it.
    characterPromise.catch(() => {});
    return {
        // SvelteKit will treat this as deferred data
        character: characterPromise
    };
}) satisfies PageLoad;
