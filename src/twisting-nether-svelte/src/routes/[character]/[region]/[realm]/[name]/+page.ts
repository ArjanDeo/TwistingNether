import { error } from '@sveltejs/kit';
import type { PageLoad } from './$types';
import type { CharacterData } from '$lib/types';
import { API_BASE_URL } from '$lib/common';

export const load = (async ({fetch, params}) => {
    const charDataResponse = await fetch(`${API_BASE_URL}/characters?realm=${params.realm}&name=${params.name}&region=${params.region}`);
    if (!charDataResponse.ok) {
        throw error(404, `Error fetching character. Status: ${charDataResponse.status} ${charDataResponse.statusText}`);
    }
    const character: CharacterData = await charDataResponse.json();
    return { character };
}) satisfies PageLoad;