import React, { useState } from "react";
import { createEvent } from "../api";

export default function EventForm() {
  const [name, setName] = useState("");
  const [date, setDate] = useState("");
  const [location, setLocation] = useState("");
  const [zones, setZones] = useState<any[]>([]);

  const addZone = () => {
    setZones([...zones, { name: "", price: 0, capacity: 0 }]);
  };

  const updateZone = (index: number, field: string, value: any) => {
    const newZones = [...zones];
    newZones[index][field] = value;
    setZones(newZones);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await createEvent({ name, date, location, zones });
    alert("Evento creado con éxito 🚀");
    setName(""); setDate(""); setLocation(""); setZones([]);
  };

  return (
    <form onSubmit={handleSubmit} className="p-4 space-y-4">
      <h2 className="text-xl font-bold">Registrar Evento</h2>
      <input type="text" placeholder="Nombre" value={name} onChange={e => setName(e.target.value)} required />
      <input type="date" value={date} onChange={e => setDate(e.target.value)} required />
      <input type="text" placeholder="Lugar" value={location} onChange={e => setLocation(e.target.value)} required />

      {zones.map((z, i) => (
        <div key={i}>
          <input type="text" placeholder="Zona" value={z.name} onChange={e => updateZone(i, "name", e.target.value)} />
          <input type="number" placeholder="Precio" value={z.price} onChange={e => updateZone(i, "price", Number(e.target.value))} />
          <input type="number" placeholder="Capacidad" value={z.capacity} onChange={e => updateZone(i, "capacity", Number(e.target.value))} />
        </div>
      ))}

      <button type="button" onClick={addZone}>+ Agregar Zona</button>
      <button type="submit">Guardar</button>
    </form>
  );
}
