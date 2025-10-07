const API_URL = "http://localhost:5050/gateway/equipments";

export async function getAllEquipments() {
    let data = null;
    try {
        const response = await fetch(API_URL);
        if (!response.ok)
            throw new Error("Http Error" + response.status);
        data = await response.json();
    } catch (err) {
        console.log(err);
        return [];
    }

    return data;
}


export async function getEquipment(equipmentId) {
    try {
        const response = await fetch(`${API_URL}/${equipmentId}`);
        if (!response.ok) throw new Error("HTTP Error: " + response.status);
        return await response.json();
    } catch (err) {
        console.error("Błąd pobierania pojedynczego sprzętu:", err);
        return null;
    }
}
