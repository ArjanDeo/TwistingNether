/* eslint-disable @typescript-eslint/no-unused-vars */
import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite'
import { enhancedImages } from '@sveltejs/enhanced-img';
import mkcert from 'vite-plugin-mkcert'
import path from 'path';
import tailwindcss from '@tailwindcss/vite'
import fs from 'fs';
import os from 'os';
import tls from 'tls';

// SvelteKit's server-side fetches go back through the HTTPS dev server, whose mkcert
// certificate Node doesn't trust by default. Add the mkcert root CA to Node's trust store.
// tls.setDefaultCACertificates needs Node 22.15+ / 23.5+, older versions skip this.
const mkcertRootCA = path.join(os.homedir(), '.vite-plugin-mkcert', 'rootCA.pem');
const tlsWithDefaults = tls as typeof tls & { setDefaultCACertificates?: (certs: string[]) => void };
if (tlsWithDefaults.setDefaultCACertificates && fs.existsSync(mkcertRootCA)) {
	tlsWithDefaults.setDefaultCACertificates([...tls.getCACertificates('default'), fs.readFileSync(mkcertRootCA, 'utf8')]);
}

export default defineConfig({
	plugins: [enhancedImages(), tailwindcss(), sveltekit(), mkcert()],
	server: {
		proxy: {
			'/api': {
				target: 'https://localhost:7176',
				changeOrigin: true,
				secure: false,
				 configure: (proxy, _options) => {
            proxy.on('error', (err, _req, res) => {
              console.log('proxy error', err);
              // Report an unreachable API as 503 (like production) rather than a generic 500.
              if ('writeHead' in res && !res.headersSent) {
                res.writeHead(503).end();
              }
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
