const urlParams = new URLSearchParams(window.location.search);
const projectId = urlParams.get("id");
fetchProjectSanctionDetails(projectId);
fetchProjectClosureDetails(projectId);
fetchMonitoringReviewDetails(projectId);
fetchPreProjectDetails(projectId);

// PROJECT SANCTION
async function fetchProjectSanctionDetails(projectId) {
  try {
    const response = await fetch(`${API_BASE}/api/ProjectSanctions/project/${projectId}`);
    if (!response.ok) throw new Error("Failed to fetch Project Sanction");

    const data = await response.json();
    console.log(data);

    document.getElementById("categoryDisplay").value = data.category || "";
    document.getElementById("deliverableDetails").value = data.deliverableDetails || "";
    document.getElementById("deliverableFileLink").href = `${API_BASE}/api/ProjectSanctions/DownloadFile/${projectId}/deliverable`;
    document.getElementById("deliverableFileLink").textContent = "View Deliverable File";

    document.getElementById("participatingLabName").value = data.participatingLabName || "";
    document.querySelector('input[placeholder="Additional comments"]').value = data.sanctionDescription || "";
    document.getElementById("sanctionDate").value = data.sanctionDate?.split("T")[0] || "";

    document.getElementById("pdcDetails").value = data.pdcDetails || "";
    document.getElementById("pdcDate").value = data.pdcDate?.split("T")[0] || "";

    document.getElementById("sanctionApproval").value = data.sanctionApproval || "";
    document.getElementById("SanctionLetterFileLink").href = `${API_BASE}/api/ProjectSanctions/DownloadFile/${projectId}/sanctionletter`;
    document.getElementById("SanctionLetterFileLink").textContent = "View Sanction Letter File";

    document.getElementById("corrigendumDescription").value = data.corrigendumDescription || "";
    document.getElementById("corrigendumFileName").href = `${API_BASE}/api/ProjectSanctions/DownloadFile/${projectId}/corrigendum`;
    document.getElementById("corrigendumFileName").textContent = "View Corrigendum File";

    const container = document.getElementById("eb-container");
    container.innerHTML = `
      <hr />
      <table>
        <tr><td><strong>Schedule Date:</strong></td><td>${formatDate(data.ebmDate)}</td></tr>
        <tr><td><strong>Brief:</strong></td><td>${data.ebmBrief || "N/A"}</td></tr>
        <tr><td><strong>Presentation:</strong></td><td><a href="${API_BASE}/api/ProjectSanctions/DownloadFile/${projectId}/ebmppt" target="_blank">Download</a></td></tr>
        <tr><td><strong>MOM:</strong></td><td><a href="${API_BASE}/api/ProjectSanctions/DownloadFile/${projectId}/ebmmom" target="_blank">Download</a></td></tr>
      </table>`;
  } catch (err) {
    console.error("❌ ProjectSanction fetch failed:", err);
  }
}

// PROJECT CLOSURE
async function fetchProjectClosureDetails(projectId) {
  try {
    const response = await fetch(`${API_BASE}/api/ProjectClosures/project/${projectId}`);
    if (!response.ok) throw new Error("Failed to fetch Project Closures");

    const data = await response.json();

    document.getElementById("idcmApproval").value = data.idcmApproval || "";
    document.getElementById("idcmDate").value = data.idcmDate?.split("T")[0] || "";
    document.getElementById("IDCMFileLink").href = `${API_BASE}/api/ProjectClosures/DownloadFile/${projectId}/idcmfile`;
    document.getElementById("MOMFileLink").href = `${API_BASE}/api/ProjectClosures/DownloadFile/${projectId}/idcmmom`;
    document.getElementById("idcmRecommendation").value = data.idcmRecommendation || "";

    document.getElementById("tcrApproval").value = data.tcrApproval || "";
    document.getElementById("tcrReport").value = data.tcrReport || "";
    document.getElementById("tcrFileName").href = `${API_BASE}/api/ProjectClosures/DownloadFile/${projectId}/tcrfile`;

    document.getElementById("acFileName").href = `${API_BASE}/api/ProjectClosures/DownloadFile/${projectId}/acfile`;
    document.getElementById("clFileName").href = `${API_BASE}/api/ProjectClosures/DownloadFile/${projectId}/clfile`;
    document.getElementById("clDate").value = data.clDate?.split("T")[0] || "";
  } catch (err) {
    console.error("❌ ProjectClosure fetch failed:", err);
  }
}

// MONITORING REVIEW
async function fetchMonitoringReviewDetails(projectId) {
  try {
    const response = await fetch(`${API_BASE}/api/MonitoringReviews/project/${projectId}`);
    if (!response.ok) throw new Error("Failed to fetch Monitoring Review");

    const data = await response.json();

    document.getElementById("kickoffBrief").value = data.kickoffBrief || "";
    document.getElementById("kickoffDate").value = data.kickoffDate?.split("T")[0] || "";
    document.getElementById("kickoffPptFileName").href = `${API_BASE}/api/MonitoringReviews/DownloadFile/${projectId}/kickoffppt`;
    document.getElementById("kickoffMomFile").href = `${API_BASE}/api/MonitoringReviews/DownloadFile/${projectId}/kickoffmom`;

    const container = document.getElementById("pmrc-container");
    container.innerHTML = `
      <hr />
      <table>
        <tr><td><strong>Schedule Date:</strong></td><td>${formatDate(data.pmrcDate)}</td></tr>
        <tr><td><strong>Brief:</strong></td><td>${data.pmrcBrief || "N/A"}</td></tr>
        <tr><td><strong>Presentation:</strong></td><td><a href="${API_BASE}/api/MonitoringReviews/DownloadFile/${projectId}/pmrcppt" target="_blank">Download</a></td></tr>
        <tr><td><strong>MOM:</strong></td><td><a href="${API_BASE}/api/MonitoringReviews/DownloadFile/${projectId}/pmrcmom" target="_blank">Download</a></td></tr>
      </table>`;
  } catch (err) {
    console.error("❌ MonitoringReview fetch failed:", err);
  }
}

// PRE PROJECT
async function fetchPreProjectDetails(projectId) {
  try {
    const response = await fetch(`${API_BASE}/api/PreProjects/project/${projectId}`);
    if (!response.ok) throw new Error("Failed to fetch Preliminay Information");

    const data = await response.json();

    document.getElementById("title").value = data.title || "";
    document.getElementById("description").value = data.description || "";
    document.getElementById("draftBrief").value = data.draftBrief || "";
    document.getElementById("minutesFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/minutesfile`;
    document.getElementById("date").value = data.date?.split("T")[0] || "";

    document.getElementById("executiveSummary").value = data.executiveSummary || "";
    document.getElementById("executiveSumFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/executivesumfile`;

    document.getElementById("objectiveBrief").value = data.objectiveBrief || "";
    document.getElementById("objectiveFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/objectivefile`;

    document.getElementById("scopeBrief").value = data.scopeBrief || "";
    document.getElementById("scopeFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/scopefile`;

    document.getElementById("participatingLabs").value = data.participatingLabs || "";
    document.getElementById("user").value = data.user || "";
    document.getElementById("userFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/userfile`;

    document.getElementById("costBrief").value = data.costBrief || "";
    document.getElementById("costFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/costfile`;
    document.getElementById("revenue").value = data.revenue || "";
    document.getElementById("capital").value = data.capital || "";
    document.getElementById("feDetails").value = data.feDetails || "";
    document.getElementById("icDetails").value = data.icDetails || "";
    document.getElementById("reDetails").value = data.reDetails || "";

    document.getElementById("durationBrief").value = data.durationBrief || "";
    document.getElementById("durationEstFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/durationestfile`;

    document.getElementById("managementCounBrief").value = data.managementCounBrief || "";
    document.getElementById("managementApproval").value = data.managementApproval || "";
    document.getElementById("managementMinutesFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/managementminutesfile`;
    document.getElementById("councilFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/councilfile`;

    document.getElementById("ccmApproval").value = data.ccmApproval || "";
    document.getElementById("ccmMinutesFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/ccmminutesfile`;
    document.getElementById("ccmDate").value = data.ccmDate?.split("T")[0] || "";

    document.getElementById("prcApproval").value = data.prcApproval || "";
    document.getElementById("prcMinutesFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/prcminutesfile`;
    document.getElementById("prcDate").value = data.prcDate?.split("T")[0] || "";

    document.getElementById("pdrBrief").value = data.pdrBrief || "";
    document.getElementById("pdrMinutesFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/pdrminutesfile`;
    document.getElementById("pdrFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/pdrfile`;
    document.getElementById("pdrDate").value = data.pdrDate?.split("T")[0] || "";

    document.getElementById("tiecBrief").value = data.tiecBrief || "";
    document.getElementById("tiecDate").value = data.tiecDate?.split("T")[0] || "";
    document.getElementById("tiecMinutesFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/tiecmom`;

    document.getElementById("cecBrief").value = data.cecBrief || "";
    document.getElementById("cecDate").value = data.cecDate?.split("T")[0] || "";
    document.getElementById("cecMinutesFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/cecmom`;

    document.getElementById("dmcBrief").value = data.dmcBrief || "";
    document.getElementById("dmcDate").value = data.dmcDate?.split("T")[0] || "";
    document.getElementById("dmcMinutesFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/dmcmom`;

    document.getElementById("sosBrief").value = data.sosBrief || "";
    document.getElementById("sosDate").value = data.sosDate?.split("T")[0] || "";
    document.getElementById("sosMinutesFile").href = `${API_BASE}/api/PreProjects/DownloadFile/${projectId}/sosmom`;

  } catch (err) {
    console.error("❌ PreProject fetch failed:", err);
  }
}

// Helper
function formatDate(dateStr) {
  return dateStr ? new Date(dateStr).toLocaleDateString('en-GB') : 'N/A';
}

// 👇 Add a global function for inline file preview (Monitoring Review)
function showFile(fileType) {
  const viewer = document.getElementById("fileViewer");
  viewer.innerHTML = "Loading file...";

  fetch(`${API_BASE}/api/MonitoringReviews/FileBase64/${projectId}/${fileType}`)
    .then(response => {
      if (!response.ok) throw new Error("File fetch failed");
      return response.json();
    })
    .then(({ base64, contentType }) => {
      let content;

      if (contentType === "application/pdf") {
        content = `<iframe src="data:${contentType};base64,${base64}" width="100%" height="600px" style="border:none;"></iframe>`;
      } else if (contentType.startsWith("image/")) {
        content = `<img src="data:${contentType};base64,${base64}" style="max-width:100%; height:auto;" />`;
      } else {
        content = `<a href="data:${contentType};base64,${base64}" download="file">Download File</a>`;
      }

      viewer.innerHTML = content;
    })
    .catch(err => {
      viewer.innerHTML = `<p style="color:red;">Failed to load file.</p>`;
      console.error("❌ File preview error:", err);
    });
}
