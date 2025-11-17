import { dev } from "$app/environment";

export const API_BASE_URL = dev ? "/api" : import.meta.env.VITE_PROD_API_URL;

export function getParseColor(score: number) {
  if (score >= 0 && score <= 24) return "#666666";
  if (score >= 25 && score <= 49) return "#1eff00";
  if (score >= 50 && score <= 74) return "#0070ff";
  if (score >= 75 && score <= 94) return "#a335ee";
  if (score >= 95 && score <= 98) return "#ff8000";
  if (score === 99) return "#e268a8";
  if (score === 100) return "#e5cc80";
  return "#000000";
}

export function getRaidDifficultyString(difficultyId: number) {
  switch (difficultyId) {
    case 1:
      return "LFR"
    case 2:
      return "Flex"
    case 3:
      return "Normal"
    case 4:
      return "Heroic"
    case 5:
      return "Mythic"
    case 10:
      return "M+"
    default:
      return "Unknown"
  }
}
export function getRaidDifficultyId(difficultyName: string) {
  switch (difficultyName) {
    case "LFR":
      return 1
    case "Flex":
      return 2
    case "Normal":
      return 3
    case "Heroic":
      return 4
    case "Mythic":
      return 5
    case "M+":
      return 10
    default:
      return 0
  }
}
