export function renderFooter() {
    const footer = document.createElement("footer");
    footer.classList.add("footer");
    const innerFooterHtml = `
    <div class="container-fluid">
    <div class="container">
            <div class="row mt-3">
                <div class="col-lg-3 col-6">
                    <span class="fs-6 underline mb-4 fw-bold">Working hours</span>
                    <ul class="list-unstyled footer-list">
                        <li>
                            <span>Monday to Friday 8:00 - 20:00</span>
                        </li>
                        <li>
                            <span>Saturday 10:00 - 15:00</span>
                        </li>
                        <li>
                            <span>Saturday - Closed</span>
                        </li>
                    </ul>
                </div>
                <div class="col-lg-3 col-6">
                    <span class="fs-6 underline mb-4 fw-bold">Company</span>
                    <ul class="list-unstyled footer-list">
                        <li>
                            <span>About Us</span>
                        </li>
                        <li>
                            <span><a asp-controller="Login" asp-action="Login">Login</a></span>
                        </li>
                        <li>
                            <span>Privacy Policy</span>
                        </li>
                    </ul>
                </div>
                <div class="col-lg-3 col-6">
                    <span class="fs-6 underline mb-4 fw-bold">Rentals</span>
                    <ul class="list-unstyled footer-list">
                        <li>
                            <span>Rent Equipment</span>
                        </li>
                        <li>
                            <span><a asp-controller="Cart" asp-action="Index">Index</a></span>
                        </li>
                        <li>
                            <span><a asp-controller="Contact" asp-action="Index">Index</a></span>
                        </li>
                    </ul>
                </div>
                <div class="col-lg-3 col-6 fw-bold">
                    <span class="fs-6 underline mb-4">Follow Us</span>
                    <ul class="list-unstyled footer-list">
                        <li>
                            <a href="https://www.youtube.com/c/VortexOoN"><i class="fa-brands fa-youtube icon"></i></a>
                            <a href="https://github.com/TheCrudClapper"><i class="fa-brands fa-github icon"></i></a>
                            <a href="https://www.linkedin.com/in/wojciech-mucha-b127b0252/"><i class="fa-brands fa-linkedin icon"></i></a>
                            <a href="https://discord.gg/NKyGBQtAU7"><i class="fa-brands fa-discord icon"></i></a>
                        </li>
                    </ul>

                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 text-center">
                    <span>&copy Wojciech Mucha - Presentation Purposes Only</span>
                </div>
            </div>
        </div>
        </div>`
    footer.innerHTML = innerFooterHtml
    return footer;
}