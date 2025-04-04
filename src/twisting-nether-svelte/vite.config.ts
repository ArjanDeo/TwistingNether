import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({
	plugins: [sveltekit()],
	server: {
		proxy: {
			'/api': {
				target: 'https://twistingnether-atcpfye3hbhjd3az.westus-01.azurewebsites.net',
				changeOrigin: true,
				secure: false
			}
		}
	}
});
