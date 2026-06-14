# Unit Conversion API

ASP.NET Core REST API for converting values between units. Supports **length**, **weight/mass**, **temperature**, and **volume** (extra). Unit definitions and conversion factors are **hardcoded in memory** for this version; the layered design is intended to scale to hundreds of units later via repositories and category-specific strategies.

Built with **.NET 9** and structured for team maintenance (separate API, Application, Domain, Infrastructure, Common, and test projects).

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (see `global.json` for the pinned SDK version)
- Any REST client (curl, Postman, or the built-in Swagger UI)

Verify your install:

```powershell
dotnet --version
```

---

## Quick start

### Quick check (Azure — no local setup)

The API is deployed to Azure App Service. Open Swagger in your browser:

**[https://unit-conversion-hjemh3fzahfpf4cb.canadacentral-01.azurewebsites.net/](https://unit-conversion-hjemh3fzahfpf4cb.canadacentral-01.azurewebsites.net/)**

1. Call **POST /api/auth/token** with the [demo credentials](#authentication) below.
2. Click **Authorize**, enter `Bearer <accessToken>`, then try **GET /api/units** or **POST /api/convert**.

No clone or SDK required — use this to verify the conversion API quickly.

### Run locally

For development or debugging on your machine, see **[Run and debug locally](#run-and-debug-locally)** below.

---

## Authentication

Convert and list-units endpoints require a JWT. Obtain a token first, then send `Authorization: Bearer {token}` on subsequent requests.

> JWT auth is included as a production-style extra. It is **not** part of the original conversion assignment spec, but it is required to call the protected endpoints in this build.

**Demo credentials** (in-memory seed — local and Azure demo deployment)

| Field | Value |
|-------|-------|
| Username | `demo` |
| Password | `Demo@123` |

### Get a token

`POST /api/auth/token`

```json
{
  "grantType": "Password",
  "username": "demo",
  "password": "Demo@123"
}
```

**Response (200)**

```json
{
  "accessToken": "<jwt>",
  "tokenType": "Bearer",
  "expiresInMinutes": 60
}
```

### Using Swagger

1. Call **POST /api/auth/token** and copy `accessToken` from the response.
2. Click **Authorize** (lock icon).
3. Enter: `Bearer <paste-token-here>` (include the word `Bearer` and a space).
4. Call **GET /api/units** or **POST /api/convert**.

---

## Run and debug locally

Requires the [.NET 9 SDK](#prerequisites) and a clone of this repository.

Local Swagger URL: **https://localhost:7204/swagger** (HTTPS only). The root URL (`/`) redirects to Swagger in Development.

### Option A — Command Prompt or PowerShell

PowerShell uses the same commands (forward slashes also work):

```powershell
dotnet restore
dotnet run --project src/API/UnitConversion.Api.csproj --launch-profile https
```

When the app is listening, open **https://localhost:7204/swagger** in your browser.

> If the browser warns about the development certificate, run `dotnet dev-certs https --trust` once, then retry.

### Option B — Visual Studio 2022

1. Open **`UnitConversion.sln`** in Visual Studio.
2. In **Solution Explorer**, right-click **`UnitConversion.Api`** → **Set as Startup Project**.
3. In the toolbar, open the launch-profile dropdown and select **`https`** (not `http`).
4. Press **F5** (Debug) or **Ctrl+F5** (Run without debugging).
5. Visual Studio opens **https://localhost:7204/swagger** (or follow the URL shown in the output window).

To debug: set breakpoints in controllers, handlers, or strategies under `src/`, then press **F5** and invoke an endpoint from Swagger.

---

## API endpoints

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `POST` | `/api/auth/token` | No | Issue a JWT (password grant) |
| `GET` | `/api/units` | Yes | List supported units; optional `?category=Length` |
| `POST` | `/api/convert` | Yes | Convert a value between two units |

### Convert (core requirement)

`POST /api/convert`

**Request**

```json
{
  "value": 100,
  "fromUnit": "meter",
  "toUnit": "kilometer"
}
```

**Response (200)**

```json
{
  "inputValue": 100,
  "fromUnit": "meter",
  "toUnit": "kilometer",
  "resultValue": 0.1,
  "category": "Length"
}
```

**Rules**

- `fromUnit` and `toUnit` must be valid unit codes (see below or `GET /api/units`).
- Both units must belong to the **same category** (e.g. meter → kilometer, not meter → celsius).
- Source and target must differ.

**Errors (400)** — JSON body with a `message` field, for example:

- Unknown unit code
- Cross-category conversion
- Same source and target unit

### List units

`GET /api/units`
Optional query: `category` — `Length`, `Weight`, `Temperature`, or `Volume`.


## Example: curl

Replace `{token}` with the JWT from `/api/auth/token`.

**Azure (deployed)**

```powershell
curl -s -X POST "https://unit-conversion-hjemh3fzahfpf4cb.canadacentral-01.azurewebsites.net/api/auth/token" `
  -H "Content-Type: application/json" `
  -d "{\"grantType\":\"Password\",\"username\":\"demo\",\"password\":\"Demo@123\"}"

curl -s -X POST "https://unit-conversion-hjemh3fzahfpf4cb.canadacentral-01.azurewebsites.net/api/convert" `
  -H "Authorization: Bearer {token}" `
  -H "Content-Type: application/json" `
  -d "{\"value\":100,\"fromUnit\":\"meter\",\"toUnit\":\"kilometer\"}"
```

**Local (HTTPS — `-k` skips dev certificate validation in curl)**

```powershell
curl -s -k -X POST "https://localhost:7204/api/auth/token" `
  -H "Content-Type: application/json" `
  -d "{\"grantType\":\"Password\",\"username\":\"demo\",\"password\":\"Demo@123\"}"

curl -s -k "https://localhost:7204/api/units" `
  -H "Authorization: Bearer {token}"

curl -s -k -X POST "https://localhost:7204/api/convert" `
  -H "Authorization: Bearer {token}" `
  -H "Content-Type: application/json" `
  -d "{\"value\":100,\"fromUnit\":\"meter\",\"toUnit\":\"kilometer\"}"
```

---

## Supported units (v1)

Units are seeded in `src/Infrastructure/Persistence/UnitCatalog.cs`. Use `GET /api/units` for the live list.

| Category | Unit codes |
|----------|------------|
| **Length** | `meter`, `kilometer`, `foot`, `inch`, `yard`, `mile`, `millimeter`, `centimeter` |
| **Weight** | `kilogram`, `pound`, `tonne`, `gram`, `ounce` |
| **Temperature** | `celsius`, `fahrenheit` |
| **Volume** | `liter`, `gallon` |

Temperature uses formula-based conversion; other categories use linear factors relative to a base unit.

---

## Running tests

```powershell
dotnet test
```

Tests cover conversion strategies (length, weight, temperature) and request validation. Fourteen tests at the time of writing.

---

## Project structure

```
unit-conversion-api/
├── UnitConversion.sln
├── global.json                 # .NET SDK pin
├── Directory.Build.props       # Shared net9.0, nullable, implicit usings
├── .editorconfig
├── src/
│   ├── API/                    # HTTP entry, controllers, Swagger, middleware
│   ├── Core/
│   │   ├── Application/        # Commands, queries, handlers, validators
│   │   ├── Domain/             # Models, enums, repository/strategy contracts
│   │   └── Common/             # Shared enums and constants
│   ├── Infrastructure/         # In-memory catalog, conversion strategies, JWT
│   └── Tests/                  # xUnit tests
```

**Dependency flow:** `API` → `Application` + `Infrastructure` → `Domain` (+ `Common` where shared types are needed).

---

## Design decisions & trade-offs

This section highlights choices that support the assignment’s **future scale** note (“hundreds of units and conversion types”) and **team-maintained** structure.

### Primary — extension architecture (future scale)

These are the main reasons the convert flow can grow without rewriting controllers or handlers.

| Decision | Rationale | Trade-off |
|----------|-----------|-----------|
| **Category strategy pattern** (`IConversionStrategy` + `ConversionStrategyFactory`) | Each category owns its conversion rules (linear factors vs temperature formulas). **Adding a category** = implement a new strategy and register it in DI — the convert pipeline (`ConversionsController` → `ConvertUnitCommandHandler` → factory) stays unchanged. Existing categories are unaffected. | A new category still needs one strategy class and a DI registration line; not zero work, but **additive and isolated**. |
| **`IUnitRepository` + string unit codes** | Unit identity is a string code (`meter`, `celsius`), not a compile-time enum — new units are **data**, not code changes. v1 uses `UnitCatalog` + `InMemoryUnitRepository`; a **DB-backed repository** plugs in under Infrastructure only — Application and API unchanged. Supports **100+ units** without enum explosion. | v1 is in-memory (restart resets catalog); unknown codes fail at runtime with 400. v2: consolidate metadata and factors in one DB/catalog source. |
| **Stable convert handler (Open–Closed)** | `ConvertUnitCommandHandler` only resolves units, validates same category, and delegates to the factory. Business rules live in strategies and repository — not in the controller. | Slightly more indirection than a single `Convert()` method in the API project. |

**How scaling works in practice**

| Change | What you touch | Convert flow impact |
|--------|----------------|---------------------|
| Add units in an existing category | Strategy factor map + `UnitCatalog` (or DB rows) | None on controller/handler |
| Add a new category (e.g. pressure) | New `IConversionStrategy` + DI registration + catalog entries | None on existing strategies |
| Move catalog to SQL | New `IUnitRepository` implementation in Infrastructure | None on Application/API |

### Supporting — team maintainability & production habits

| Decision | Rationale | Trade-off |
|----------|-----------|-----------|
| **Layered solution (6 projects)** | Separates HTTP, application rules, domain contracts, and infrastructure — credible for a multi-developer codebase per assignment. | More projects and wiring than a single Web API file. |
| **Command/query handlers + decorator pipeline** | Controllers stay thin; validation (`FluentValidation`) and logging wrap handlers consistently. **New operations** add a handler + validator without bloating controllers. | More types than inline controller logic for v1’s few endpoints. |
| **Domain contracts, Infrastructure implementations** | `IUnitRepository`, `IConversionStrategy`, auth abstractions live in Domain; in-memory store, JWT, and strategies live in Infrastructure — testable core, swappable persistence. | Requires discipline to keep Domain free of EF/HTTP dependencies. |
| **Exception middleware** | Maps domain/validation failures to uniform `{ "message": "..." }` responses and structured logs in one place. | Must extend mapping as new exception types appear. |
| **Grant provider pattern** (`IGrantAuthService`) | Same additive model as strategies: new grant type = new service + DI registration. Only **Password** is implemented; others return 501. | Auth surface beyond assignment spec. |
| **JWT on protected endpoints** | Production-style security demo; Swagger documents Bearer flow. | Must obtain a token first — see [Authentication](#authentication). |
| **`Common` for shared constants/enums** | One source for validation limits, user-facing messages, and Enums — reused across validators, exceptions, and API contracts. | Extra project; keep business logic out of Common. |
| **Structured logging (Serilog + `ILogger`)** | Request logging, handler decorators, and exception middleware give operational visibility. Sinks are config-driven — see [Logging & observability](#logging--observability). | Extra packages and config; tune log levels under high traffic. |
| **In-memory v1 (no database)** | Matches spec: units and factors may be hardcoded in this version. | No persistence; catalog and users reset on restart. |

### Future scale summary

| Concern | v1 | Future direction |
|---------|----|------------------|
| Unit catalog | `UnitCatalog` + `InMemoryUnitRepository` | DB or config-driven `IUnitRepository` |
| Conversion logic | One strategy per category | New category = new strategy; convert handler unchanged |
| Auth | In-memory user + JWT (password grant) | Identity provider; refresh/client grants via grant provider |
| Logging | Console + rolling JSON files | Application Insights, Azure Monitor, or centralized log store |

Controllers stay thin; business rules live in Application handlers, domain contracts, and Infrastructure services.

---

## Logging & observability

Logging is included as a **production-style** concern — useful for debugging and demos, but not part of the core conversion requirement.

### What is logged

| Layer | Mechanism | What gets recorded |
|-------|-----------|-------------------|
| **HTTP** | Serilog request logging (`Program.cs`) | Method, path, status code, duration; enriched with host, scheme, user agent |
| **Application** | `LoggingCommandHandlerDecorator` / `LoggingQueryHandlerDecorator` | Command/query name, payload (`{@Command}`), elapsed ms, failures |
| **API** | `ExceptionHandlingMiddleware` | Validation failures, unknown units, cross-category attempts, unhandled errors |
| **Host** | Serilog bootstrap | Startup and fatal shutdown events |

### Where logs go

Configured under the `Serilog` section in `appsettings.json` (overrides in `appsettings.Development.json` / `appsettings.Production.json`):

| Sink | Location | Format |
|------|----------|--------|
| **Console** | Terminal running `dotnet run` | Human-readable template |
| **File** | `src/API/logs/unit-conversion-YYYYMMDD.log` | Compact JSON (one event per line) |

Development uses **Debug** minimum level; Production keeps **Information** (see `appsettings.Production.json`).

### Viewing logs locally

After a convert call you should see request logging plus handler entries such as `Handling command ConvertUnitCommand` and elapsed milliseconds.

### Why Serilog (trade-off)

- **Pros:** Sinks driven by config (add Application Insights without code changes); structured properties for search; industry-standard for .NET teams.
---
