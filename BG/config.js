// Backend API base URL. Points at localhost during local dev,
// and at the deployed Render backend everywhere else.
const API_BASE =
  window.location.hostname === "localhost" || window.location.hostname === "127.0.0.1"
    ? "http://localhost:5270"
    : "https://drdo-project-sample.onrender.com";
