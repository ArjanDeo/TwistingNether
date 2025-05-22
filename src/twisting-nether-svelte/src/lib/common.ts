import { dev } from "$app/environment";

export const API_BASE_URL = dev ? "/api" : import.meta.env.VITE_PROD_API_URL;