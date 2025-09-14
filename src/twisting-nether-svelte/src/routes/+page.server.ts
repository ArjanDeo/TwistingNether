import { setError, superValidate } from 'sveltekit-superforms';
import type { PageServerLoad, Actions } from './$types';
import { characterFormSchema } from './characterFormSchema';
import { zod } from "sveltekit-superforms/adapters";
import { fail, redirect } from "@sveltejs/kit";
import { API_BASE_URL } from '$lib/common';
import { type Token } from '$lib/types'
import { dev } from '$app/environment';
if (dev) {
  process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = '0';
}
export const load: PageServerLoad = (async (event) => {
      const tokenResponse = await event.fetch(`${API_BASE_URL}/general/token-price`);
      let token: Token | null;
      if (!tokenResponse.ok) {
        token = null;
      } else {
        token = await tokenResponse.json();
      }
    return {
        form: await superValidate(zod(characterFormSchema)),
        token
    };
}) satisfies PageServerLoad;

export const actions: Actions = {
  default: async (event) => {
    const form = await superValidate(event, zod(characterFormSchema));
    if (!form.valid) {
      return fail(400, {
        form,
      });
    }
    const pingCharacter = await event.fetch(`${API_BASE_URL}/characters/?name=${form.data.name.toLowerCase()}&realm=${form.data.realm.toLowerCase()}&region=${form.data.region.toLowerCase()}`, {
      method: 'HEAD',
    });
    if (!pingCharacter.ok) {
      return setError(form, 'Character not found.')
    } else {
    throw redirect(303, `/character/${form.data.region.toLowerCase()}/${form.data.realm.toLowerCase()}/${form.data.name.toLowerCase()}`)
    }
  },
};