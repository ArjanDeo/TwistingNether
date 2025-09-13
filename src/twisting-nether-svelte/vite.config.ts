/* eslint-disable @typescript-eslint/no-unused-vars */
import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite'
import mkcert from 'vite-plugin-mkcert'
import path from 'path';
import tailwindcss from '@tailwindcss/vite'

export default defineConfig({
	plugins: [tailwindcss(), sveltekit(), mkcert()],
	server: {
		proxy: {
			'/api': {
				target: 'https://localhost:7176',
				changeOrigin: true,
				secure: false,
				 configure: (proxy, _options) => {
            proxy.on('error', (err, _req, _res) => {
              console.log('proxy error', err);
            });
            proxy.on('proxyReq', (proxyReq, req, _res) => {
              console.log('Sending Request to the Target:', req.method, req.url);
            });
            proxy.on('proxyRes', (proxyRes, req, _res) => {
              console.log('Received Response from the Target:', proxyRes.statusCode, req.url, '\n');
            });
          },
			},
			'/devapi': {
				target: 'https://localhost:7176',
				changeOrigin: true,
				secure: false
			}
		}
	},
	resolve: {
		alias: {
		$lib: path.resolve("./src/lib"),
		},
	},
});
