const EDITAPI = "http://localhost:5270/api/Projects";

// ✅ Make it global so `showSection('edit')` can call it
window.fetchProjectsForEdit = async function () {
  try {
    const res = await fetch(EDITAPI);
    if (!res.ok) throw new Error("API returned " + res.status);

    const data = await res.json();
    const projects = data.$values ?? data; // ✅ Handle both .NET and plain array

    const list = document.getElementById("edit-project-list");
    list.innerHTML = "";

    projects.forEach(proj => {
      const card = document.createElement("div");
      card.className = "project-card";
      card.innerHTML = `
        <h4>${proj.name}</h4>
        <div class="badge">${proj.category}</div>
        <p>${proj.description}</p>
        <small><b>Start Date:</b> ${new Date(proj.startDate).toLocaleDateString()}</small>
        <br><br>
        <a href="ProjectForm.html?id=${proj.projectId}" class="edit-btn">Edit</a>
      `;
      list.appendChild(card);
    });
  } catch (error) {
    console.error("❌ Could not fetch projects:", error);
  }
};
