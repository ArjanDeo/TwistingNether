import forms from '@tailwindcss/forms';
import type { Config } from 'tailwindcss';
import daisyui from "daisyui"
export default {
	content: ['./src/**/*.{html,js,svelte,ts}'],

	theme: {
		extend: {}
	},

	plugins: [
		daisyui,
		forms
	]
} satisfies Config;
