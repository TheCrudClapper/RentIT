export async function registerUser(data) {
    try {
        const response = await fetch("http://localhost:5050/gateway/auth/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });

        if (!response.ok) {
            const errorData = await response.json();
            console.error("API validation errors:", errorData);
            alert("Something went wrong with the request");
            return null;
        }

        console.log("Registration successful");
        return await response.json();
    } catch (e) {
        console.error("Fetch error:", e);
        alert(e);
    }
}

export async function loginUser(params) {
    try{
        const response = await fetch("http://localhost:5050/gateway/auth/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });
    }catch(e){

    }
}