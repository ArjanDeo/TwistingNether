import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";
import { ItemQuality } from "./types";

export function cn(...inputs: ClassValue[]) {
	return twMerge(clsx(inputs));
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export type WithoutChild<T> = T extends { child?: any } ? Omit<T, "child"> : T;
// eslint-disable-next-line @typescript-eslint/no-explicit-any
export type WithoutChildren<T> = T extends { children?: any } ? Omit<T, "children"> : T;
export type WithoutChildrenOrChild<T> = WithoutChildren<WithoutChild<T>>;
export type WithElementRef<T, U extends HTMLElement = HTMLElement> = T & { ref?: U | null };

export function itemQualityColor(quality: number): string {
    switch (quality) {
        case ItemQuality.Common: return '#ffffff';
        case ItemQuality.Uncommon: return '#1eff00';
        case ItemQuality.Rare: return '#0070dd';
        case ItemQuality.Epic: return '#a335ee';
        case ItemQuality.Legendary: return '#ff8000';
        case ItemQuality.Artifact: return '#e6cc80';
        case ItemQuality.Heirloom: return '#00ccff';
        default: return ''; // fallback to classColor
    }
}