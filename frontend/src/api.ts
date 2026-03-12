export async function createEvent(eventData: any) {
  const response = await fetch("http://localhost:5000/api/events", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer demo-jwt-token"
    },
    body: JSON.stringify(eventData),
  });

  if (!response.ok) {
    throw new Error("Error al crear el evento");
  }

  return response.json();
}

export async function getEvents() {
  const response = await fetch("http://localhost:5000/api/events", {
    headers: {
      "Authorization": "Bearer demo-jwt-token"
    }
  });

  if (!response.ok) {
    throw new Error("Error al obtener los eventos");
  }

  return response.json();
}
