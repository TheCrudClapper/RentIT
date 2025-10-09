export function renderHome() {
    const container = document.createElement("div");
    container.classList.add("container-fluid", "hero-container", "vh-100");

    const heroSection = document.createElement("div");
    heroSection.classList.add("hero-section");

    const heroImg = document.createElement("img");
    heroImg.src = "../../public/hero.jpg";
    heroImg.alt = "Hero background";
    heroImg.classList.add("hero");

    const heroOverlay = document.createElement("div");
    heroOverlay.classList.add("hero-overlay");

    const slogan = document.createElement("h1");
    slogan.classList.add("hero-slogan");
    slogan.innerText = "Rent. Run. Return. Repeat.";

    const description = document.createElement("h2");
    description.classList.add("hero-description");
    description.innerText = "Rent IT Stuff for heavy play";

    const btnContainer = document.createElement("div");
    btnContainer.classList.add("hero-buttons");

    const btnOffer = document.createElement("button");
    btnOffer.classList.add("hero-btn");
    btnOffer.innerText = "Rent Now";

    const btnContact = document.createElement("button");
    btnContact.classList.add("hero-btn", "hero-btn-outline");
    btnContact.innerText = "Contact";

    btnContainer.append(btnOffer, btnContact);
    heroOverlay.append(slogan, btnContainer);
    heroOverlay.append(slogan, description, btnContainer);

    heroSection.append(heroImg, heroOverlay);
    container.appendChild(heroSection);

    return container;
}
