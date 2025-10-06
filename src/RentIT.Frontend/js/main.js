import { renderNavbar } from "../components/navbar.js";
import { renderHome } from "../pages/homePage.js";

const app = document.getElementById('app');

function router(){
    const path = window.location.hash.replace("#", '') || '/';

    app.innerHTML = '';
    app.innerHTML = renderNavbar();

    switch(path){
        case "/":
            app.innerHTML += renderHome();
        case "/login":

    }
}

window.addEventListener('hashchange', router);
window.addEventListener('load', router)