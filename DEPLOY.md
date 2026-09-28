# Deploying this project for free

Three pieces, three free services:

| Piece                         | Service | Why |
|--------------------------------|---------|-----|
| Frontend (`BG/*.html,js,css`) | Vercel  | static hosting, free forever |
| Backend (`BG/MyBackendAPI`)   | Render  | free web service, runs the Dockerfile in this repo |
| MySQL database                | Railway | free MySQL plugin |

## 1. Database — Railway

1. [railway.app](https://railway.app) → sign in with GitHub → **New Project** → **Provision MySQL**.
2. Open the MySQL service → **Variables** tab → copy `MYSQL_URL` (or the individual host/port/user/password/database fields).
3. Build a connection string in this format:
   `server=<host>;port=<port>;database=<database>;user=<user>;password=<password>;`

## 2. Backend — Render

1. [render.com](https://render.com) → sign in with GitHub → **New** → **Web Service** → pick `DRDO-Project-Sample`.
2. Render should detect `render.yaml` (Docker runtime, `BG/MyBackendAPI/Dockerfile`) — otherwise set:
   - Runtime: **Docker**
   - Dockerfile path: `BG/MyBackendAPI/Dockerfile`
   - Docker context: `BG/MyBackendAPI`
3. Under **Environment**, add:
   - `ConnectionStrings__DefaultConnection` = the connection string from step 1
4. Deploy. Render gives you a URL like `https://drdo-project-sample.onrender.com`.
   - Free tier sleeps after 15 min idle — first request after a while takes ~30-50s to wake up.

## 3. Frontend — Vercel

1. Open `BG/config.js` in this repo and set the production branch of `API_BASE` to your actual Render URL from step 2 (it's currently a placeholder: `https://drdo-project-sample.onrender.com`).
2. [vercel.com](https://vercel.com) → sign in with GitHub → **Add New Project** → pick `DRDO-Project-Sample`.
3. Vercel should pick up `vercel.json` (serves the `BG` folder as-is, no build step needed).
4. Deploy. You get a URL like `https://drdo-project-sample.vercel.app`.

## 4. Wire CORS (optional but tidy)

The API currently allows any origin (`AllowAnyOrigin`) in `Program.cs`, so step 3 will work as-is.
If you want to lock it down later, restrict the CORS policy to your Vercel domain only.

## First run

EF Core migrations need to run once against the Railway database. Easiest path: run
`dotnet ef database update` locally against the Railway connection string before/after first deploy,
or add a one-time startup migration call in `Program.cs` (`db.Database.Migrate()`) if you want it automatic.
