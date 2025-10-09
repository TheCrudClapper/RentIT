import { registerUser } from "../services/userService";

export function renderRegisterPage() {
    const loginPage = document.createElement("div");
    loginPage.classList.add("container-fluid");
    const loginPageContent = `
    <div class="container vh-100">
        <div class = "row">
            <div class = "col-md-3 bg-dark vh-100"></div>
            <div class = "col-md-6 bg-warning vh-100">
                <h1 class = "fw-bold text-center">Register</h1>
                <form id="registerForm">
                    <div class="mb-3">      
                        <label for="firstNameInput" class="fs-4 fw-bold">First Name</label>         
                        <div class = "input-group mb-3">
                            <input id="firstNameInput" type="text" class="form-control" placeholder="John">
                        </div>
                    </div>
                    <div class="mb-3">      
                        <label for="lastNameInput" class="fs-4 fw-bold">Last Name</label>         
                        <div class = "input-group mb-3">
                            <input id="lastNameInput" type="text" class="form-control" placeholder="Doe">
                        </div>
                    </div>
                    <div class="mb-3">      
                        <label for="emailInput" class="fs-4 fw-bold">Email</label>         
                        <div class = "input-group mb-3">
                            <input id="emailInput" type="text" class="form-control" placeholder="johndoe69@domain.com">
                        </div>
                    </div>
                    <div class="mb-3">      
                        <label for="passwordInput" class="fs-4 fw-bold">Password</label>         
                        <div class = "input-group mb-3">
                            <input id="passwordInput" type="password" class="form-control" placeholder="Password123#@">
                        </div>
                    </div>
                    <div class="mb-3">
                        <input class="btn btn-dark" type=submit >
                    </div>
                </form>
            </div>
            <div class = "col-md-3 bg-dark vh-100"></div>
        </div>
    </div>
    `;

    loginPage.innerHTML = loginPageContent;

    const submit = loginPage.querySelector("#registerBtn");
    const form = loginPage.querySelector("#registerForm");
    
    form.addEventListener("submit", async (e) => {
        e.preventDefault();

        const firstName = loginPage.querySelector("#firstNameInput").value;
        const lastName = loginPage.querySelector("#lastNameInput").value;
        const email = loginPage.querySelector("#emailInput").value;
        const password = loginPage.querySelector("#passwordInput").value;

        const formData =  { firstName, lastName, email, password };
        await registerUser(formData);
    });

    return loginPage;
}
