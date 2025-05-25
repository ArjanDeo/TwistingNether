import { realms } from '$lib/realms';
import { z } from 'zod';

const realmSet = new Set(realms.map(r => r.realmName.toLowerCase()));


export const characterFormSchema = z.object({
    name: z.string().min(3, { message: "Name must be longer than 3 characters"}).max(12, { message: "Name cannot be longer than 12 characters"}),
    realm: z.string()
    .transform(val => val.trim())
    .refine(val => realmSet.has(val.toLowerCase()), {
    message: "Invalid realm"}),
    region: z.enum(["US", "EU", "KR", "CN", "TW"])
});

export type FormSchema = typeof characterFormSchema;