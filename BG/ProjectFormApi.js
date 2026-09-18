const SPAPI = "http://localhost:5270/api/ProjectSanctions";
const CPAPI = "http://localhost:5270/api/ProjectClosures";
const MRAPI = "http://localhost:5270/api/MonitoringReviews";
const PPAPI = "http://localhost:5270/api/PreProjects/upload";

const urlParams = new URLSearchParams(window.location.search);
const projectIdFromURL = urlParams.get("id");
const projectId = projectIdFromURL ? parseInt(projectIdFromURL) : 0;

// PROJECT SANCTION API
// PROJECT SANCTION API (with file uploads)
async function CreateProjectSanction(e) {
  e.preventDefault();

  if (!projectId || isNaN(projectId)) {
    alert("❌ Project ID is missing or invalid in the URL!");
    return;
  }

  const formData = new FormData();
  formData.append("ProjectId", projectId);
  formData.append("Category", document.getElementById("category")?.value || "");
  formData.append("DeliverableDetails", document.getElementById("deliverableDetails")?.value || "");

  const deliverableFile = document.getElementById("deliverableFile")?.files[0];
  if (deliverableFile) formData.append("DeliverableFile", deliverableFile);

  formData.append("ParticipatingLabName", document.getElementById("spparticipatingLabName")?.value || "");
  formData.append("SanctionDescription", document.getElementById("sanctionDescription")?.value || "");
  formData.append("SanctionDate", document.getElementById("sanctionDate")?.value || "");

  formData.append("PdcDetails", document.getElementById("pdcDetails")?.value || "");
  formData.append("PdcDate", document.getElementById("pdcDate")?.value || "");

  formData.append("SanctionApproval", document.getElementById("sanctionApproval")?.value || "");
  const sanctionFile = document.getElementById("sanctionFile")?.files[0];
  if (sanctionFile) formData.append("SanctionLetterFile", sanctionFile);

  formData.append("CorrigendumDescription", document.getElementById("corrigendumDescription")?.value || "");
  const corrigendumFile = document.getElementById("scopeFile")?.files[0];
  if (corrigendumFile) formData.append("CorrigendumFile", corrigendumFile);

  formData.append("EbmBrief", document.getElementById("ebmBrief")?.value || "");
  formData.append("EbmDate", document.getElementById("ebmDate")?.value || "");

  const ebmPpt = document.querySelector('input[name="ebPresentation[]"]')?.files[0];
  const ebmMom = document.querySelector('input[name="ebMOM[]"]')?.files[0];

  if (ebmPpt) formData.append("EbmPptFile", ebmPpt);
  if (ebmMom) formData.append("EbmMomFile", ebmMom);

  try {
    const response = await fetch(SPAPI, {
      method: "POST",
      body: formData
    });

    if (response.ok) {
      alert("✅ Project Sanction created successfully!");
      document.getElementById("spForm").reset();
    } else {
      const msg = await response.text();
      alert("❌ Failed to create Project Sanction: " + msg);
    }
  } catch (error) {
    console.error("❌ API Error:", error);
    alert("❌ An error occurred while creating the Project Sanction.");
  }
}


// PROJECT CLOSURE API
async function CreateProjectClosure(e) {
  e.preventDefault();

  if (!projectId || isNaN(projectId)) {
    alert("❌ Project ID is missing or invalid in the URL!");
    return;
  }

  const formData = new FormData();

  formData.append("ProjectId", projectId);
  formData.append("IdcmApproval", document.getElementById("idcmApproval")?.value || "");
  formData.append("IdcmDate", document.getElementById("idcmDate")?.value || "");
  formData.append("IdcmRecommendation", document.getElementById("idcmRecommendation")?.value || "");
  formData.append("TcrApproval", document.getElementById("tcrApproval")?.value || "");
  formData.append("TcrReport", document.getElementById("tcrReport")?.value || "");
  formData.append("ClDate", document.getElementById("clDate")?.value || "");

  // Append files properly from input elements
  const idcmMomFile = document.getElementById("idcmMomFile")?.files[0];
  if (idcmMomFile) formData.append("IdcmMomFile", idcmMomFile);

  const idcmFile = document.getElementById("idcmFile")?.files[0];
  if (idcmFile) formData.append("IdcmFile", idcmFile);

  const tcrFile = document.getElementById("tcrFile")?.files[0];
  if (tcrFile) formData.append("TcrFile", tcrFile);

  const acFile = document.getElementById("acFile")?.files[0];
  if (acFile) formData.append("AcFile", acFile);

  const clFile = document.getElementById("clFile")?.files[0];
  if (clFile) formData.append("ClFile", clFile);

  try {
    const response = await fetch(CPAPI, {
      method: "POST",
      body: formData, // No JSON.stringify, let browser set headers automatically
    });

    if (response.ok) {
      alert("✅ Project Closure created successfully!");
      document.getElementById("cpForm").reset();
    } else {
      const msg = await response.text();
      alert("❌ Failed to create Project Closure: " + msg);
    }
  } catch (error) {
    console.error("❌ API Error:", error);
    alert("❌ An error occurred while creating the Project Closure.");
  }
}


// MONITORING REVIEW API (with file uploads)
async function CreateMonitoringReview(e) {
  e.preventDefault();

  if (!projectId || isNaN(projectId)) {
    alert("❌ Project ID is missing or invalid in the URL!");
    return;
  }

  const formData = new FormData();

  formData.append("ProjectId", projectId);
  formData.append("KickoffBrief", document.getElementById("kickoffBrief")?.value || "");
  formData.append("KickoffDate", document.getElementById("kickoffDate")?.value || "");
  formData.append("PmrcBrief", document.querySelector('input[name="PmrcBrief"]')?.value || "");
  formData.append("PmrcDate", document.querySelector('input[name="PmrcDate"]')?.value || "");

  const kickoffPpt = document.querySelector('input[name="KickoffPptFileName"]')?.files[0];
  const kickoffMom = document.querySelector('input[name="KickoffMomFile"]')?.files[0];
  const pmrcPpt = document.querySelector('input[name="PmrcPptFileName"]')?.files[0];
  const pmrcMom = document.querySelector('input[name="PmrcMomFile"]')?.files[0];

  if (kickoffPpt) formData.append("KickoffPptFileName", kickoffPpt);
  if (kickoffMom) formData.append("KickoffMomFile", kickoffMom);
  if (pmrcPpt) formData.append("PmrcPptFileName", pmrcPpt);
  if (pmrcMom) formData.append("PmrcMomFile", pmrcMom);

  try {
    const response = await fetch(MRAPI, {
      method: "POST",
      body: formData
    });

    if (response.ok) {
      alert("✅ Monitoring Review created successfully!");
      document.getElementById("mrForm").reset();
    } else {
      const msg = await response.text();
      alert("❌ Failed to create Monitoring Review: " + msg);
    }
  } catch (error) {
    console.error("❌ API Error:", error);
    alert("❌ An error occurred while creating the Monitoring Review.");
  }
}

// PRE-PROJECT API
async function CreatePreProject(e) {
  e.preventDefault();

  if (!projectId || isNaN(projectId)) {
    alert("❌ Project ID is missing or invalid in the URL!");
    return;
  }

  const formData = new FormData();
  formData.append("ProjectId", projectId);

  // Append all your text inputs, e.g.:
  formData.append("Title", document.getElementById("title")?.value || "");
  formData.append("Description", document.getElementById("description")?.value || "");
  formData.append("DraftBrief", document.getElementById("draftBrief")?.value || "");
  formData.append("Date", document.getElementById("date")?.value || "");

  // Append file inputs, e.g.:
  const minutesFile = document.getElementById("minutesFile")?.files[0];
  if (minutesFile) formData.append("MinutesFile", minutesFile);

  const executiveSumFile = document.getElementById("executiveSumFile")?.files[0];
  if (executiveSumFile) formData.append("ExecutiveSumFile", executiveSumFile);

  // Continue appending other text and file inputs similarly...

  try {
    const response = await fetch(PPAPI, {
      method: "POST",
      body: formData  // Note: no Content-Type header! Browser sets multipart/form-data automatically
    });

    if (response.ok) {
      alert("✅ Pre-Project created successfully!");
      document.getElementById("multiStepForm").reset();
    } else {
      const msg = await response.text();
      alert("❌ Failed to create Pre-Project: " + msg);
    }
  } catch (error) {
    console.error("❌ API Error:", error);
    alert("❌ An error occurred while creating the Pre-Project.");
  }
}

