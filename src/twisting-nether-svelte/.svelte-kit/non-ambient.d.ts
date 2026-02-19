
// this file is generated — do not edit it


declare module "svelte/elements" {
	export interface HTMLAttributes<T> {
		'data-sveltekit-keepfocus'?: true | '' | 'off' | undefined | null;
		'data-sveltekit-noscroll'?: true | '' | 'off' | undefined | null;
		'data-sveltekit-preload-code'?:
			| true
			| ''
			| 'eager'
			| 'viewport'
			| 'hover'
			| 'tap'
			| 'off'
			| undefined
			| null;
		'data-sveltekit-preload-data'?: true | '' | 'hover' | 'tap' | 'off' | undefined | null;
		'data-sveltekit-reload'?: true | '' | 'off' | undefined | null;
		'data-sveltekit-replacestate'?: true | '' | 'off' | undefined | null;
	}
}

export {};


declare module "$app/types" {
	export interface AppTypes {
		RouteId(): "/" | "/about" | "/components" | "/io-calculator" | "/[character]" | "/[character]/[region]" | "/[character]/[region]/[realm]" | "/[character]/[region]/[realm]/[name]" | "/[character]/[region]/[realm]/[name]/components" | "/[character]/[region]/[realm]/[name]/components/characterInfoSection" | "/[character]/[region]/[realm]/[name]/components/characterInfo" | "/[character]/[region]/[realm]/[name]/components/characterPane" | "/[character]/[region]/[realm]/[name]/components/gearIcon";
		RouteParams(): {
			"/[character]": { character: string };
			"/[character]/[region]": { character: string; region: string };
			"/[character]/[region]/[realm]": { character: string; region: string; realm: string };
			"/[character]/[region]/[realm]/[name]": { character: string; region: string; realm: string; name: string };
			"/[character]/[region]/[realm]/[name]/components": { character: string; region: string; realm: string; name: string };
			"/[character]/[region]/[realm]/[name]/components/characterInfoSection": { character: string; region: string; realm: string; name: string };
			"/[character]/[region]/[realm]/[name]/components/characterInfo": { character: string; region: string; realm: string; name: string };
			"/[character]/[region]/[realm]/[name]/components/characterPane": { character: string; region: string; realm: string; name: string };
			"/[character]/[region]/[realm]/[name]/components/gearIcon": { character: string; region: string; realm: string; name: string }
		};
		LayoutParams(): {
			"/": { character?: string; region?: string; realm?: string; name?: string };
			"/about": Record<string, never>;
			"/components": Record<string, never>;
			"/io-calculator": Record<string, never>;
			"/[character]": { character: string; region?: string; realm?: string; name?: string };
			"/[character]/[region]": { character: string; region: string; realm?: string; name?: string };
			"/[character]/[region]/[realm]": { character: string; region: string; realm: string; name?: string };
			"/[character]/[region]/[realm]/[name]": { character: string; region: string; realm: string; name: string };
			"/[character]/[region]/[realm]/[name]/components": { character: string; region: string; realm: string; name: string };
			"/[character]/[region]/[realm]/[name]/components/characterInfoSection": { character: string; region: string; realm: string; name: string };
			"/[character]/[region]/[realm]/[name]/components/characterInfo": { character: string; region: string; realm: string; name: string };
			"/[character]/[region]/[realm]/[name]/components/characterPane": { character: string; region: string; realm: string; name: string };
			"/[character]/[region]/[realm]/[name]/components/gearIcon": { character: string; region: string; realm: string; name: string }
		};
		Pathname(): "/" | "/about" | "/about/" | "/components" | "/components/" | "/io-calculator" | "/io-calculator/" | `/${string}` & {} | `/${string}/` & {} | `/${string}/${string}` & {} | `/${string}/${string}/` & {} | `/${string}/${string}/${string}` & {} | `/${string}/${string}/${string}/` & {} | `/${string}/${string}/${string}/${string}` & {} | `/${string}/${string}/${string}/${string}/` & {} | `/${string}/${string}/${string}/${string}/components` & {} | `/${string}/${string}/${string}/${string}/components/` & {} | `/${string}/${string}/${string}/${string}/components/characterInfoSection` & {} | `/${string}/${string}/${string}/${string}/components/characterInfoSection/` & {} | `/${string}/${string}/${string}/${string}/components/characterInfo` & {} | `/${string}/${string}/${string}/${string}/components/characterInfo/` & {} | `/${string}/${string}/${string}/${string}/components/characterPane` & {} | `/${string}/${string}/${string}/${string}/components/characterPane/` & {} | `/${string}/${string}/${string}/${string}/components/gearIcon` & {} | `/${string}/${string}/${string}/${string}/components/gearIcon/` & {};
		ResolvedPathname(): `${"" | `/${string}`}${ReturnType<AppTypes['Pathname']>}`;
		Asset(): "/faavicon.png" | "/faavicon.webp" | "/favicon.png" | "/favicon.webp" | "/kingdomswillburn.jpg" | "/money-gold.webp" | "/raiderioicon.webp" | "/raids-manaforge-omega.webp" | "/script.bat" | "/warcraftlogsicon.webp" | "/wowicon.webp" | "/WoW_7.3_Shadows_of_Argus_05.webp" | string & {};
	}
}