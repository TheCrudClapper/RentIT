import { registerUser } from "../services/userService";

export function renderRegisterPage() {
  const registerPage = document.createElement("div");
  registerPage.classList.add(
    "container-fluid",
    "bg-warning",         
    "vh-100",
    "d-flex",
    "justify-content-center",
    "align-items-center"
  );

  const registerPageContent = `
    <div class="card shadow-lg border-0 rounded-2 bg-dark text-white" style="max-width: 480px; width: 100%;">
      <div class="card-body p-5">
        <h1 class="fw-bold text-center mb-4">Register</h1>

        <form id="registerForm">
          <div class="mb-3">
            <label for="firstNameInput" class="form-label fw-semibold fs-5">First Name</label>
            <input id="firstNameInput" type="text" class="form-control form-control-lg bg-secondary text-white border-0 rounded-3" placeholder="John" required>
          </div>

          <div class="mb-3">
            <label for="lastNameInput" class="form-label fw-semibold fs-5">Last Name</label>
            <input id="lastNameInput" type="text" class="form-control form-control-lg bg-secondary text-white border-0 rounded-3" placeholder="Doe" required>
          </div>

          <div class="mb-3">
            <label for="emailInput" class="form-label fw-semibold fs-5">Email</label>
            <input id="emailInput" type="email" class="form-control form-control-lg bg-secondary text-white border-0 rounded-3" placeholder="johndoe@example.com" required>
          </div>

          <div class="mb-4">
            <label for="passwordInput" class="form-label fw-semibold fs-5">Password</label>
            <input id="passwordInput" type="password" class="form-control form-control-lg bg-secondary text-white border-0 rounded-3" placeholder="Password123#@" required>
          </div>

          <div class="d-grid">
            <button id="registerBtn" type="submit" class="btn btn-warning btn-lg rounded-3 fw-bold text-dark shadow-sm">
              Create Account
            </button>
          </div>
        </form>
      </div>
    </div>
  `;

  registerPage.innerHTML = registerPageContent;

  // Obsługa formularza
  const form = registerPage.querySelector("#registerForm");
  form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const firstName = registerPage.querySelector("#firstNameInput").value.trim();
    const lastName = registerPage.querySelector("#lastNameInput").value.trim();
    const email = registerPage.querySelector("#emailInput").value.trim();
    const password = registerPage.querySelector("#passwordInput").value.trim();

    const formData = { firstName, lastName, email, password };
    await registerUser(formData);
  });

  return registerPage;
}
