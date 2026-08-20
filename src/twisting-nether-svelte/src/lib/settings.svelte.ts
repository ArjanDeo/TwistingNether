import type { TwistingNetherSettings } from "$lib/types";

const STORAGE_KEY = "twistingnether-settings";

const defaultSettings: TwistingNetherSettings = {
	useWowheadTooltips: false
};

let settings = $state<TwistingNetherSettings>({
	...defaultSettings
});

let loaded = $state(false);

function load() {
	if (typeof localStorage === "undefined") return;

	const stored = localStorage.getItem(STORAGE_KEY);

	if (!stored) {
		settings = { ...defaultSettings };
		save();
	} else {
		try {
			const parsed = JSON.parse(stored) as Partial<TwistingNetherSettings>;

			settings = {
				...defaultSettings,
				...parsed
			};
		} catch {
			settings = { ...defaultSettings };
			save();
		}
	}

	loaded = true;
}

function save() {
	if (typeof localStorage === "undefined") return;

	localStorage.setItem(STORAGE_KEY, JSON.stringify(settings));
}

function commit(newSettings: TwistingNetherSettings) {
	settings = {
		...newSettings
	};

	save();
}

function reset() {
	settings = {
		...defaultSettings
	};

	save();
}

function get<K extends keyof TwistingNetherSettings>(
	key: K
): TwistingNetherSettings[K] {
	return settings[key];
}

export const appSettings = {
	get settings() {
		return settings;
	},

	get loaded() {
		return loaded;
	},

	load,
	save,
	commit,
	reset,
	get,

	defaults: defaultSettings
};