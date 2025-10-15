import { loginUser } from "../services/userService";

export function renderLoginPage() {
  const loginPage = document.createElement("div");
  loginPage.classList.add(
    "container-fluid",
    "bg-warning",
    "vh-100",
    "d-flex",
    "justify-content-center",
    "align-items-center"
  );

  const loginPageContent = `
    <div class="card shadow-lg border-0 rounded-2 bg-dark text-white" style="max-width: 480px; width: 100%;">
      <div class="card-body p-5">
        <h1 class="fw-bold text-center mb-4">Login</h1>

        <form id="loginForm">
          <div class="mb-4">
            <label for="emailInput" class="form-label fw-semibold fs-5">Email</label>
            <input 
              id="emailInput" 
              type="email" 
              class="form-control form-control-lg bg-secondary text-white border-0 rounded-3" 
              placeholder="you@example.com" 
              required
            >
          </div>

          <div class="mb-5">
            <label for="passwordInput" class="form-label fw-semibold fs-5">Password</label>
            <input 
              id="passwordInput" 
              type="password" 
              class="form-control form-control-lg bg-secondary text-white border-0 rounded-3" 
              placeholder="Your password" 
              required
            >
          </div>

          <div class="d-grid">
            <button 
              id="loginBtn" 
              type="submit" 
              class="btn btn-warning btn-lg rounded-3 fw-bold text-dark shadow-sm"
            >
              Sign In
            </button>
          </div>
        </form>
      </div>
    </div>
  `;

  loginPage.innerHTML = loginPageContent;

  const form = loginPage.querySelector("#loginForm");
  form.addEventListener("submit", async (e) => {
    console.log("clicked");
    e.preventDefault();

    const email = loginPage.querySelector("#emailInput").value.trim();
    const password = loginPage.querySelector("#passwordInput").value.trim();

    const formData = { email, password };
    await loginUser(formData);
  });

  return loginPage;
}
