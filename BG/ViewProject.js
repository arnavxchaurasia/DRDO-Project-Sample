const VIEWAPI = `${API_BASE}/api/Projects`;

window.fetchProjects = async function () {
  try {
    const res = await fetch(VIEWAPI);
    if (!res.ok) throw new Error("API returned status " + res.status);

    const data = await res.json();
    const projects = data.$values ?? data;

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

