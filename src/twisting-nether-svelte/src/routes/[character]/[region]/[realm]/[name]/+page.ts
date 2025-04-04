import { dev } from '$app/environment';
import { error } from '@sveltejs/kit';
import type { PageLoad } from './$types';
import type { CharacterData } from '$lib/types';

export const load = (async ({fetch, params}) => {
    let charDataEndpoint = "https://twistingnetherapi.azurewebsites.net/api/v1/general/character"
    if (dev) {
        console.log('Dev Environment Active')
        charDataEndpoint = "/api/character"
    }
    const charDataResponse = await fetch(`${charDataEndpoint}?realm=${params.realm}&name=${params.name}&region=${params.region}`);
    
    if (!charDataResponse.ok) {
        throw error(400, 'Error fetching character.')
    }
    const character: CharacterData = await charDataResponse.json();
    return { character };
}) satisfies PageLoad;