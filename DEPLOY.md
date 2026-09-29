# Deploying this project for free

Three pieces, three free services:

| Piece                         | Service          | Why |
|--------------------------------|------------------|-----|
| Frontend (`BG/*.html,js,css`) | Vercel or Deployr | static hosting, free forever |
| Backend (`BG/MyBackendAPI`)   | Render           | free web service, runs the Dockerfile in this repo |
| MySQL database                | Aiven (or Railway) | free MySQL service |

## 1. Database — Aiven

1. [console.aiven.io](https://console.aiven.io) → sign in → **Create service** → **MySQL** → **Free** tier.
2. Once it's running, open the service → **Connection information** → reveal the password.
3. Build a connection string in this format:
   `server=<host>;port=<port>;database=<database>;user=avnadmin;password=<password>;`

(Railway's MySQL plugin works too — its `MYSQL_URL` is in `mysql://user:pass@host:port/db` format, which `Program.cs` also auto-detects and converts.)

## 2. Backend — Render

1. [render.com](https://render.com) → sign in with GitHub → **New** → **Web Service** → pick `DRDO-Project-Sample`.
2. Render should detect `render.yaml` (Docker runtime, `BG/MyBackendAPI/Dockerfile`) — otherwise set:
   - Runtime: **Docker**
   - Dockerfile path: `BG/MyBackendAPI/Dockerfile`
   - Docker context: `.` (repo root)
3. Under **Environment**, add:
   - `ConnectionStrings__DefaultConnection` = the connection string from step 1
   - `Jwt__Key` = a random secret **at least 32 characters long** (e.g. generate with `openssl rand -base64 32`) — required for login to work at all; the checked-in placeholder in `appsettings.json` is deliberately too short to pass validation.
   - `SEED_ADMIN_EMAIL` / `SEED_ADMIN_PASSWORD` (optional) — if both are set, the backend creates this one admin account on first startup (only when the `users` table is still empty). Not committed to the repo on purpose.
4. Deploy. Render gives you a URL like `https://drdo-project-sample.onrender.com`.
   - Free tier sleeps after 15 min idle — first request after a while takes ~30-50s to wake up.

## 3. Frontend — Vercel or Deployr

**Vercel:**
1. Open `BG/config.js` and set the production branch of `API_BASE` to your actual Render URL from step 2.
2. [vercel.com](https://vercel.com) → **Add New Project** → pick `DRDO-Project-Sample`. Picks up `vercel.json` automatically (builds via `package.json`'s `build` script, which just copies `BG/` into `dist/`).

**Deployr:** same repo URL, same build (`package.json` → `node build.js` → `dist/`) — no extra config needed beyond the project name.

## 4. Auth

There's a real JWT-based login now (`POST /api/auth/login`, `POST /api/auth/register`) backed by a `users` table —
no more hardcoded credentials. Passwords are hashed with PBKDF2-SHA256 (100k iterations), never stored in plain text.
`GET /api/dashboard/summary` requires a valid token (`Authorization: Bearer <token>`) to demonstrate the auth wiring
end to end.

## 5. Wire CORS (optional but tidy)

The API currently allows any origin (`AllowAnyOrigin`) in `Program.cs`, so step 3 will work as-is.
If you want to lock it down later, restrict the CORS policy to your frontend's actual domain.

## First run

EF Core migrations (including the new `users` table) run automatically on startup via `db.Database.Migrate()`
in `Program.cs` — no manual `dotnet ef database update` step needed.
