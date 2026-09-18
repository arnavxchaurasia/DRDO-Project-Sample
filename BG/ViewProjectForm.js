// ========== WRAPPED IN DOMContentLoaded ========== //
document.addEventListener('DOMContentLoaded', () => {
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

  const isViewMode = true; // Change based on context

  newSidebarButtons.forEach(button => {
    button.addEventListener('click', () => {
      newSidebarButtons.forEach(btn => btn.classList.remove('active'));
      button.classList.add('active');

      const selectedPane = button.getAttribute('data-pane');

      ppContainer.style.display = selectedPane === 'pp' ? 'flex' : 'none';
      spContainer.style.display = selectedPane === 'sp' ? 'flex' : 'none';
      cpContainer.style.display = selectedPane === 'cp' ? 'flex' : 'none';
      mrContainer.style.display = selectedPane === 'mr' ? 'flex' : 'none';
    });
  });

  function setupFormNavigation(form, steps, formSteps, stepIndex, updateBtnFn) {
    form?.querySelector('.continue-btn')?.addEventListener('click', e => {
      e.preventDefault();
      if (stepIndex.value < formSteps.length - 1) {
        formSteps[stepIndex.value].classList.remove('active');
        steps[stepIndex.value].classList.remove('active');
        stepIndex.value++;
        formSteps[stepIndex.value].classList.add('active');
        steps[stepIndex.value].classList.add('active');
      }
      updateBtnFn();
    });

    steps.forEach((step, index) => {
      step.addEventListener('click', () => {
        steps.forEach(s => s.classList.remove('active'));
        formSteps.forEach(f => f.classList.remove('active'));
        step.classList.add('active');
        formSteps[index].classList.add('active');
        stepIndex.value = index;
        updateBtnFn();
      });
    });
  }

  setupFormNavigation(ppForm, ppSteps, ppFormSteps, { value: ppCurrentStep }, () => {
    const btn = ppForm.querySelector('.continue-btn');
    if (!btn) return;
    btn.style.display = 'inline-block';
    btn.textContent = ppCurrentStep === ppFormSteps.length - 1 ? 'Submit' : 'Next Step';
  });

  setupFormNavigation(spForm, spSteps, spFormSteps, { value: spCurrentStep }, () => {
    const btn = spForm.querySelector('.continue-btn');
    if (!btn) return;
    if (spCurrentStep === spFormSteps.length - 1) {
      btn.textContent = 'Submit';
      if (isViewMode) btn.style.display = 'none';
      else btn.style.display = 'inline-block';
    } else {
      btn.style.display = 'inline-block';
      btn.textContent = 'Next Step';
    }
  });

  setupFormNavigation(cpForm, cpSteps, cpFormSteps, { value: cpCurrentStep }, () => {
    const btn = cpForm.querySelector('.continue-btn');
    if (!btn) return;
    btn.style.display = 'inline-block';
    btn.textContent = cpCurrentStep === cpFormSteps.length - 1 ? 'Submit' : 'Next Step';
  });

  mrForm?.querySelector('.continue-btn')?.addEventListener('click', (e) => {
    e.preventDefault();
    if (mrCurrentStep < mrFormSteps.length - 1) {
      mrFormSteps[mrCurrentStep].classList.remove('active');
      mrCurrentStep++;
      mrFormSteps[mrCurrentStep].classList.add('active');
      if (mrCurrentStep === mrFormSteps.length - 1) {
        mrForm.querySelector('.continue-btn').textContent = 'Submit';
      }
    } else {
      mrForm.submit();
    }
  });

  // Category Change Logic
  const categorySelect = document.getElementById('category');
  const stContainer = document.getElementById('stDetailsContainer');
  const tdContainer = document.getElementById('tdDetailsContainer');
  categorySelect?.addEventListener('change', function () {
    stContainer.style.display = 'none';
    tdContainer.style.display = 'none';
    if (this.value === 'ST') stContainer.style.display = 'block';
    else if (this.value === 'TD') tdContainer.style.display = 'block';
  });

  // Initializing today's date on load
  const today = new Date().toISOString().split("T")[0];
  const firstPMRC = document.querySelector('input[name="pmrcScheduleDate[]"]');
  const firstEB = document.querySelector('input[name="ebScheduleDate[]"]');
  if (firstPMRC) firstPMRC.value = today;
  if (firstEB) firstEB.value = today;
  const firstMrPMRC = document.querySelector('#mrForm input[name="pmrcScheduleDate[]"]');
  if (firstMrPMRC) firstMrPMRC.value = today;
});

// ========== PMRC/EB LOGIC OUTSIDE DOMContentLoaded ==========
let pmrcCount = 1;
let ebCount = 1;

function getFutureDate(baseDate, monthsToAdd) {
  const date = new Date(baseDate);
  date.setMonth(date.getMonth() + monthsToAdd);
  return date.toISOString().split("T")[0];
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
  div.innerHTML = `...`;
  container.appendChild(div);
}

function addEBMeeting() {
  ebCount++;
  const container = document.getElementById("eb-container");
  const lastDate = getLastScheduleDate(container, "ebScheduleDate");
  const newDate = getFutureDate(lastDate, 3);

  const div = document.createElement("div");
  div.className = "eb-meeting";
  div.innerHTML = `...`;
  container.appendChild(div);
}

function removeMeeting(btn) {
  const block = btn.closest(".pmrc-meeting, .eb-meeting");
  if (block) block.remove();
}

function goToMRStep(stepIndex) {
  const steps = document.querySelectorAll('.mr-step');
  const contents = document.querySelectorAll('.mr-step-content');
  steps.forEach((step, idx) => {
    step.classList.toggle('active', idx === stepIndex);
    contents[idx].classList.toggle('active', idx === stepIndex);
  });
}
