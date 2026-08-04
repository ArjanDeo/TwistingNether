# The Twisting Nether

A full-stack web application for looking up World of Warcraft character profiles and live in-game economy data, built on a SvelteKit front end and a .NET Web API back end.

**Live site:** [twistingnether.furyshiftz.com](https://twistingnether.furyshiftz.com)

![Status](https://img.shields.io/badge/status-in%20development-yellow)
![Frontend](https://img.shields.io/badge/frontend-SvelteKit-FF3E00?logo=svelte&logoColor=white)
![Backend](https://img.shields.io/badge/backend-.NET-512BD4?logo=dotnet&logoColor=white)
![CI](https://img.shields.io/badge/CI-GitHub%20Actions-2088FF?logo=githubactions&logoColor=white)

<!-- Optional: replace with an actual screenshot once you have one, e.g. -->
<!-- ![App screenshot](docs/screenshot.png) -->

## About

Twisting Nether lets players search for a World of Warcraft character by realm and region and view their profile at a glance, alongside a live WoW Token price feed pulled from Blizzard's own API.

## Main Features

-  **Character lookup** — search any character by name, realm, and region and retrieve all the important information you'd need immediately.
-  **Live WoW Token price** — real-time gold-to-token exchange rate

## Architecture

```
Browser (SvelteKit frontend)
        │
        ▼
TwistingNether.API   (ASP.NET Core Web API — endpoints, controllers)
        │
        ▼
TwistingNether.Core        (domain models, business logic, service interfaces)
        │
        ▼
TwistingNether.DataAccess  (data access / external API integration)
        │
        ▼
 Blizzard Battle.net Game Data API
```

The backend follows a layered architecture rather than a single monolithic API project:

- **`TwistingNether.API`** — the ASP.NET Core Web API surface; handles HTTP requests from the frontend and maps them to Core services.
- **`TwistingNether.Core`** — domain models and business logic, independent of how data is fetched or how requests come in.
- **`TwistingNether.DataAccess`** — talks to Blizzard's Battle.net API (and any caching/persistence), isolated behind interfaces defined in Core.
- **`twisting-nether-svelte`** — the SvelteKit frontend that consumes the API.

Separating these keeps the domain logic testable and decoupled from both the web layer and the external Blizzard API.

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | SvelteKit, TypeScript |
| Backend | C#, ASP.NET Core Web API, layered (API / Core / DataAccess) |
| External data | Blizzard Battle.net API |
| CI/CD | GitHub Actions |
| Hosting/DNS | Cloudflare |

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (8.0+)
- [Node.js](https://nodejs.org/) (18+) and npm
- A [Blizzard developer application](https://develop.battle.net/) (client ID/secret) for Battle.net API access

### Backend
Create an appsettings.json in the API project folder root, with sections dedicated to each KVP accessed in Program.cs through app.Configuration.GetSection[].
List of current credentials needed to be added in appsettings:
- WarcraftLogs: ClientID + ClientSecret
- Battle.Net API Client: ClientID + ClientSecret
- (If using in conjunction with [old-bnet-tauri](https://github.com/ArjanDeo/old-bnet-tauri)) old-bnet-tauri Battle.Net API Client: ClientID + ClientSecret
```bash
cd src
dotnet restore src.sln
dotnet run --project TwistingNether.API
```

### Frontend

```bash
cd src/twisting-nether-svelte
npm install
npm run dev
```

The frontend expects the API to be running locally and reachable via the configured base URL (see `.env` / SvelteKit config).

## Project Structure

```
TwistingNether/
├── .github/workflows/          # CI/CD pipelines
├── src/
│   ├── TwistingNether.API/         # ASP.NET Core Web API layer
│   ├── TwistingNether.Core/        # Domain models & business logic
│   ├── TwistingNether.DataAccess/  # Blizzard API integration & data access
│   ├── twisting-nether-svelte/     # SvelteKit frontend
│   └── src.sln
├── TwistingNether.sln
└── README.md
```

## Roadmap

- [ ] Character talents breakdown
- [ ] Guild lookup
- [ ] Historical token price chart
- [ ] Swapping from In-Memory Caching layer to Redis for Blizzard API responses

## Author

**Arjan Deo** — [github.com/arjandeo](https://github.com/ArjanDeo)
