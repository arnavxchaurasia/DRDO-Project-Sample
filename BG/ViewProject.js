const VIEWAPI = `${API_BASE}/api/Projects`;

window.fetchProjects = async function () {
  try {
    const res = await fetch(VIEWAPI);
    if (!res.ok) throw new Error("API returned status " + res.status);

    const data = await res.json();
    // /api/Projects now returns a paginated { items, page, pageSize, ... }
    // envelope instead of a bare array, and the API's ReferenceHandler.Preserve
    // setting wraps any array as { $id, $values } — so the real list could be
    // at data.items.$values, data.items, data.$values, or data itself
    // depending on API version. Cover all four rather than guess one.
    const projects = data.items?.$values ?? data.items ?? data.$values ?? data;

    console.log(projects);

    const list = document.getElementById("view-project-list");
    list.innerHTML = "";

    projects.forEach((proj) => {
      const card = document.createElement("div");
      card.className = "project-card";

      card.innerHTML = `
        <h4>${proj.name}</h4>
        <div class="badge">${proj.category}</div>
        <p>${proj.description}</p>
        <small><b>Start Date:</b> ${new Date(proj.startDate).toLocaleDateString()}</small>
        <br/>
        <a href="ViewProjectForm.html?id=${proj.projectId}" class="view-btn">View</a>
      `;

      list.appendChild(card);
    });
  } catch (error) {
    console.error("❌ Could not fetch projects:", error);
  }
};

