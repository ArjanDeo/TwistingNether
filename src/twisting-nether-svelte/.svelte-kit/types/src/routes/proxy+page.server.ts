// @ts-nocheck
import { setError, superValidate } from 'sveltekit-superforms';
import type { PageServerLoad, Actions } from './$types';
import { characterFormSchema } from './characterFormSchema';
import { zod } from "sveltekit-superforms/adapters";
import { fail, redirect } from "@sveltejs/kit";
import { API_BASE_URL } from '$lib/common';

export const load = (async () => {
    return {
        form: await superValidate(zod(characterFormSchema)),
    };
}) satisfies PageServerLoad;

export const actions = {
  default: async (event: import('./$types').RequestEvent) => {
    const form = await superValidate(event, zod(characterFormSchema));
    if (!form.valid) {
      return fail(400, {
        form,
      });
    }
    const pingCharacter = await event.fetch(`${API_BASE_URL}/character/pingcharacter?name=${form.data.name}&realm=${form.data.realm}&region=${form.data.region}`);
    if (!pingCharacter.ok) {
      return setError(form, 'Character not found.')
    } else {
    throw redirect(303, `/character/${form.data.region}/${form.data.realm}/${form.data.name}`)
    }
  },
};;null as any as PageServerLoad;;null as any as Actions;