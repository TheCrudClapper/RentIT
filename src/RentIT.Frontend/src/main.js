import { renderNavbar } from "./components/navbar.js";
import { renderHome } from "./pages/homePage.js";
import { renderLoginPage } from "./pages/loginPage.js";
import { renderFooter} from './components/footer.js';
import { renderRegisterPage } from "./pages/registerPage.js";

const app = document.getElementById('app');

function router(){
    const path = window.location.hash.replace("#", '') || '/';

    app.innerHTML = '';
    app.appendChild(renderNavbar());

    switch(path){
        case "/":
            app.appendChild(renderHome())
            break;
        case "/login":
            app.appendChild(renderLoginPage());
            break;
        case "/register":
            app.appendChild(renderRegisterPage());
            break;
         case "/about-us":
            app.innerHTML += `<div class="container py-5"><h2>About Us</h2><p>Informacje o firmie...</p></div>`;
            break;
        case "/services":
            app.innerHTML += `<div class="container py-5"><h2>Services</h2><p>Nasze usługi...</p></div>`;
            break;
       
        default:
            app.innerHTML += `<div class="container py-5"><h2>404</h2><p>Page doest exist.</p></div>`;

    }

    app.appendChild(renderFooter());
}

window.addEventListener('hashchange', router);
window.addEventListener('load', router)