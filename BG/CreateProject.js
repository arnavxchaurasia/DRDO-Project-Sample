// CreateProject.js

const APIBASE = `${API_BASE}/api/Projects`;

async function createProject(e) {
  e.preventDefault();
  const projectData = {
    name: document.getElementById("projectName").value,
    category: document.getElementById("category").value,
    description: document.getElementById("description").value,
    startDate: document.getElementById("date").value
  };

  try {
    const response = await fetch(APIBASE, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(projectData)
    });

    if (response.ok) {
      alert("Project created successfully!");
      document.getElementById("createForm").reset();
    } else {
      const msg = await response.text();
      alert("Failed to create project: " + msg);
    }
  } catch (error) {
    console.error(error);
    alert("An error occurred while creating the project.");
  }
}

// Attach the event listener when DOM is ready
document.addEventListener("DOMContentLoaded", function () {
  const form = document.getElementById("createForm");
  if (form) {
    form.addEventListener("submit", createProject);
  }
});


