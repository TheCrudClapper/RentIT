import { getAllEquipments, getEquipment } from "../services/equipmentService";
export function renderHome(){
    const container = document.createElement("div");
    container.classList.add("container-fluid");

    const heroImg = document.createElement("img");
    heroImg.src = "../../public/hero-image.jpg";
    heroImg.classList.add("img-fluid");
    heroImg.classList.add("hero");
    container.appendChild(heroImg);

    const button = document.createElement("button");
    button.classList.add("btn", "btn-warning");
    button.innerText = "Fetch All Equipments!";
    container.appendChild(button);

    const list = document.createElement("ul");
    container.appendChild(list);

    button.addEventListener("click", async () => {
        const equipments = await getAllEquipments();
        list.innerHTML = '';
        equipments.forEach(equipment => {
            const li = document.createElement("li");
            li.textContent = `${equipment.id} + ${equipment.name} - ${equipment.status} - ${equipment.rentalPricePerDay}$`;
            list.appendChild(li);
        });
    });

    return container;
}
