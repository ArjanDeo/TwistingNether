import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({
	plugins: [sveltekit()],
	server: {
		proxy: {
			'/api': {
				target: 'https://twistingnetherapi.furyshiftz.com',
				changeOrigin: true,
				secure: false
			}
		}
	}
});
