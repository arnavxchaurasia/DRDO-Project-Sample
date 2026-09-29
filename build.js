// Deployr (and other generic static-site hosts) default to `npm run build`
// producing a `dist/` folder. This repo's frontend is plain static
// HTML/CSS/JS living in BG/ with no real build step, so this script just
// copies it into dist/ to satisfy that convention — excluding
// MyBackendAPI/, which is server-side C# source, not part of the site.
const fs = require("fs");
const path = require("path");

const src = path.join(__dirname, "BG");
const dest = path.join(__dirname, "dist");

fs.rmSync(dest, { recursive: true, force: true });
fs.cpSync(src, dest, {
  recursive: true,
  filter: (source) => path.basename(source) !== "MyBackendAPI",
});

console.log(`Copied ${src} -> ${dest} (excluding MyBackendAPI/)`);
