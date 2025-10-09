export function renderLoginPage(){
    const loginPage = document.createElement("div");
    loginPage.classList.add("container-fluid");
    
    
    const loginPageContent = `
    <div class="container vh-100">
        <h1>Theres will be some shit</h1>
    </div>
    `;

    loginPage.innerHTML = loginPageContent;
    return loginPage;
}