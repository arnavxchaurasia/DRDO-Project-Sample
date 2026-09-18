// Selecting all important elements
const ppContainer = document.getElementById('pp');
const spContainer = document.getElementById('sp');
const cpContainer = document.getElementById('cp');
const mrContainer = document.getElementById('mr');
const mrForm = document.getElementById('mrForm');
const mrFormSteps = document.querySelectorAll('#mrForm .form-step');
let mrCurrentStep = 0;

const ppForm = document.getElementById('multiStepForm');
const spForm = document.getElementById('spForm');
const cpForm = document.getElementById('cpForm');

const ppSteps = document.querySelectorAll('#sidebarList .step');
const ppFormSteps = document.querySelectorAll('#multiStepForm .form-step');

const spSteps = document.querySelectorAll('#spSidebar .sp-step');
const spFormSteps = document.querySelectorAll('#spForm .sp-step-content');

const cpSteps = document.querySelectorAll('#cpSidebar .cp-step');
const cpFormSteps = document.querySelectorAll('#cpForm .cp-step-content');

const newSidebarButtons = document.querySelectorAll('.new-sidebar button');

let ppCurrentStep = 0;
let spCurrentStep = 0;
let cpCurrentStep = 0;

// Section switching
newSidebarButtons.forEach(button => {
  button.addEventListener('click', () => {
    newSidebarButtons.forEach(btn => btn.classList.remove('active'));
    button.classList.add('active');
    const pane = button.getAttribute('data-pane');
    ppContainer.style.display = pane === 'pp' ? 'flex' : 'none';
    spContainer.style.display = pane === 'sp' ? 'flex' : 'none';
    cpContainer.style.display = pane === 'cp' ? 'flex' : 'none';
    mrContainer.style.display = pane === 'mr' ? 'flex' : 'none';
  });
});

// PP Form Stepper
ppForm.querySelector('.continue-btn').addEventListener('click', async (e) => {
  e.preventDefault();
  if (ppCurrentStep < ppFormSteps.length - 1) {
    ppFormSteps[ppCurrentStep].classList.remove('active');
    ppSteps[ppCurrentStep].classList.remove('active');
    ppCurrentStep++;
    ppFormSteps[ppCurrentStep].classList.add('active');
    ppSteps[ppCurrentStep].classList.add('active');
    ppForm.querySelector('.continue-btn').textContent =
      ppCurrentStep === ppFormSteps.length - 1 ? 'Submit' : 'Next Step';
  } else {
    await CreatePreProject(e);
  }
});

// SP Form Stepper
spForm.querySelector('.continue-btn').addEventListener('click', async (e) => {
  e.preventDefault();
  if (spCurrentStep < spFormSteps.length - 1) {
    spFormSteps[spCurrentStep].classList.remove('active');
    spSteps[spCurrentStep].classList.remove('active');
    spCurrentStep++;
    spFormSteps[spCurrentStep].classList.add('active');
    spSteps[spCurrentStep].classList.add('active');
    spForm.querySelector('.continue-btn').textContent =
      spCurrentStep === spFormSteps.length - 1 ? 'Submit' : 'Next Step';
  } else {
    await CreateProjectSanction(e);
  }
});

// CP Form Stepper
cpForm.querySelector('.continue-btn').addEventListener('click', async (e) => {
  e.preventDefault();
  if (cpCurrentStep < cpFormSteps.length - 1) {
    cpFormSteps[cpCurrentStep].classList.remove('active');
    cpSteps[cpCurrentStep].classList.remove('active');
    cpCurrentStep++;
    cpFormSteps[cpCurrentStep].classList.add('active');
    cpSteps[cpCurrentStep].classList.add('active');
    cpForm.querySelector('.continue-btn').textContent =
      cpCurrentStep === cpFormSteps.length - 1 ? 'Submit' : 'Next Step';
  } else {
    await CreateProjectClosure(e);
  }
});

// MR Form Stepper
mrForm.querySelector('.continue-btn').addEventListener('click', async (e) => {
  e.preventDefault();
  if (mrCurrentStep < mrFormSteps.length - 1) {
    mrFormSteps[mrCurrentStep].classList.remove('active');
    mrCurrentStep++;
    mrFormSteps[mrCurrentStep].classList.add('active');
    mrForm.querySelector('.continue-btn').textContent =
      mrCurrentStep === mrFormSteps.length - 1 ? 'Submit' : 'Next Step';
  } else {
    await CreateMonitoringReview(e);
  }
});

// MR Sidebar clicking logic
function goToMRStep(index) {
  const steps = document.querySelectorAll('.mr-step');
  const contents = document.querySelectorAll('.mr-step-content');
  steps.forEach((s, i) => {
    s.classList.toggle('active', i === index);
    contents[i].classList.toggle('active', i === index);
  });
  mrCurrentStep = index;
  mrForm.querySelector('.continue-btn').textContent =
    mrCurrentStep === mrFormSteps.length - 1 ? 'Submit' : 'Next Step';
}

// Sidebar clicks (PP, SP, CP)
[ppSteps, spSteps, cpSteps].forEach((steps, formIndex) => {
  steps.forEach((step, index) => {
    step.addEventListener('click', () => {
      const forms = [ppFormSteps, spFormSteps, cpFormSteps];
      const buttons = [ppForm, spForm, cpForm];
      steps.forEach(s => s.classList.remove('active'));
      forms[formIndex].forEach(f => f.classList.remove('active'));
      step.classList.add('active');
      forms[formIndex][index].classList.add('active');
      [ppCurrentStep, spCurrentStep, cpCurrentStep][formIndex] = index;
      const btn = buttons[formIndex].querySelector('.continue-btn');
      btn.textContent =
        index === forms[formIndex].length - 1 ? 'Submit' : 'Next Step';
    });
  });
});

// Category select logic
document.addEventListener('DOMContentLoaded', function () {
  const categorySelect = document.getElementById('category');
  const stContainer = document.getElementById('stDetailsContainer');
  const tdContainer = document.getElementById('tdDetailsContainer');

  if (categorySelect) {
    categorySelect.addEventListener('change', function () {
      const selected = categorySelect.value;
      stContainer.style.display = selected === 'ST' ? 'block' : 'none';
      tdContainer.style.display = selected === 'TD' ? 'block' : 'none';
    });
  }
});

// Reusable utility for PMRC/EB meeting dates
let pmrcCount = 1;
let ebCount = 1;

function getFutureDate(baseDate, monthsToAdd) {
  const date = new Date(baseDate);
  date.setMonth(date.getMonth() + monthsToAdd);
  return date.toISOString().split('T')[0];
}

function getLastScheduleDate(container, inputName) {
  const inputs = container.querySelectorAll(`input[name="${inputName}[]"]`);
  if (inputs.length === 0) return new Date();
  const last = inputs[inputs.length - 1].value;
  return new Date(last || new Date());
}

function addPMRCMeeting() {
  pmrcCount++;
  const container = document.getElementById("pmrc-container");
  const lastDate = getLastScheduleDate(container, "pmrcScheduleDate");
  const newDate = getFutureDate(lastDate, 3);

  const div = document.createElement("div");
  div.className = "pmrc-meeting";
  div.innerHTML = `
    <hr />
    <h4>PMRC Meeting ${pmrcCount}</h4>
    <table>
      <tr><td><label>Schedule Date</label></td><td><input type="date" name="pmrcScheduleDate[]" value="${newDate}" readonly required /></td></tr>
      <tr><td><label>Held Date</label></td><td><input type="date" name="pmrcHeldDate[]" required /></td></tr>
      <tr><td><label>Brief</label></td><td><input type="text" name="pmrcBrief[]" required /></td></tr>
      <tr><td><label>Presentation</label></td><td><input type="file" name="pmrcPresentation[]" accept=".pdf,.ppt,.pptx" required /></td></tr>
      <tr><td><label>MOM</label></td><td><input type="file" name="pmrcMOM[]" accept=".pdf,.doc,.docx" required /></td></tr>
      <tr><td><label>Members</label></td><td><input type="text" name="pmrcCommitteeMembers[]" required /></td></tr>
    </table>
    <button type="button" onclick="removeMeeting(this)">Remove This Meeting</button>
  `;
  container.appendChild(div);
}

function removeMeeting(btn) {
  const block = btn.closest(".pmrc-meeting, .eb-meeting");
  if (block) block.remove();
}

window.addEventListener("DOMContentLoaded", () => {
  const today = new Date().toISOString().split("T")[0];
  const firstPMRC = document.querySelector('input[name="pmrcScheduleDate[]"]');
  if (firstPMRC) firstPMRC.value = today;
  const firstMrPMRC = document.querySelector('#mrForm input[name="pmrcScheduleDate[]"]');
  if (firstMrPMRC) firstMrPMRC.value = today;
});
